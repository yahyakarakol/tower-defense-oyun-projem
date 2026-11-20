using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform target;

    [Range(1f, 15f)]
    public float range = 3f; 
    
    public float fireRate = 1f; 
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    void Update()
    {
        UpdateTarget();         
        if (target == null) return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
    GameObject mermiObjesi = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    MermiAtesTopu mermi = mermiObjesi.GetComponent<MermiAtesTopu>();
    if (mermi != null)
    {
        mermi.HedefBelirle(target);
    }
    }

  
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}