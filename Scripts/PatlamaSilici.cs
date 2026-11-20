using UnityEngine;

public class PatlamaSilici : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.6f); // 0.6 saniye sonra kendini yok et
    }
}