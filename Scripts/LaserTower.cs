using UnityEngine;

public class LaserTower : MonoBehaviour
{
    [Header("Lazer Kulesi Ayarlarý")]
    [Range(1f, 15f)]
    public float range = 4f;
    public float damageOverTime = 45f; // Saniyede vereceði toplam hasar

    [Header("Baðlantýlar")]
    public string enemyTag = "Enemy";
    public LineRenderer lineRenderer;  // Lazer çizgisini çizecek bileþen
    public Transform firePoint;        // Lazerin çýkacaðý uç nokta (boþ býrakýlýrsa merkezden çýkar)

    private Transform target;
    private Enemy targetEnemy;

    void Start()
    {
        // Hedef taramasýný saniyede 5 kez yap (performans dostu)
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
            targetEnemy = null;
        }
    }

    void Update()
    {
        // Menzilde düþman yoksa lazer ýþýnýný gizle
        if (target == null)
        {
            if (lineRenderer != null && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
            return;
        }

        // Düþmana kilitlen ve lazeri çiz + anlýk hasar ver
        LockOnAndDamage();
    }

    void LockOnAndDamage()
    {
        // Düþmana saniye bazlý kesintisiz hasar ver
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        }

        // Lazer çizgisini aç ve baþlangýç/bitiþ noktalarýný güncelle
        if (lineRenderer != null)
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }

            Vector3 startPos = firePoint != null ? firePoint.position : transform.position;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, target.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}