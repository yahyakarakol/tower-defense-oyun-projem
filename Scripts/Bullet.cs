using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f; 
    public float damage = 30f;

    public GameObject hitEffectPrefab; // Kývýlcým efekti yuvasý

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (hitEffectPrefab != null)
        {
            // Merminin hedefteki gidiþ yönünü ve 2D açýsýný hesapla
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion hitRotation = Quaternion.Euler(0f, 0f, angle);

            // Efekti merminin gidiþ yönüne bakacak þekilde oluþtur
            GameObject effect = Instantiate(hitEffectPrefab, transform.position, hitRotation);
            Destroy(effect, 1f);
        }

        Enemy enemyComponent = target.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}