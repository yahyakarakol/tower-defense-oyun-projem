using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f; // Düþmanýn yürüme hýzý
    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Path.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Path.points.Length - 1)
        {
            Enemy enemyStats = GetComponent<Enemy>();
            float damage = enemyStats != null ? enemyStats.baseDamage : 10f;

            if (BaseTower.instance != null)
            {
                BaseTower.instance.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Path.points[waypointIndex];
    }
}