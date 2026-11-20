using UnityEngine;

public class MermiAtesTopu : MonoBehaviour
{
    public float hiz = 10f;
    public float hasar = 25f;
    public GameObject patlamaEfekti; // Buraya PatlamaPrefab'ý baðlayacaðýz

    private Transform hedef;

    public void HedefBelirle(Transform _hedef)
    {
        hedef = _hedef;
    }

    void Update()
    {
        if (hedef == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 yon = hedef.position - transform.position;
        float buKareMesafe = hiz * Time.deltaTime;

        // Ateþ topunun ucunu düþmana çevir
        float aci = Mathf.Atan2(yon.y, yon.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, aci);

        // Hedefe ulaþtýysa patlat!
        if (yon.magnitude <= buKareMesafe)
        {
            HedefiVur();
            return;
        }

        transform.Translate(yon.normalized * buKareMesafe, Space.World);
    }

    void HedefiVur()
    {
        // 1. Tam çarpma noktasýnda patlamayý oluþtur
        if (patlamaEfekti != null)
        {
            Instantiate(patlamaEfekti, transform.position, Quaternion.identity);
        }

        // 2. Düþmana hasar ver
        Enemy dusman = hedef.GetComponent<Enemy>();
        if (dusman != null)
        {
            dusman.TakeDamage(hasar);
        }

        // 3. Ateþ topunu yok et
        Destroy(gameObject);
    }
}