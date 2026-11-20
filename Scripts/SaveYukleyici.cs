using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveYukleyici : MonoBehaviour
{
    void Awake()
    {
        // Daha önce kaydedilmiþ bir bölüm var mý kontrol et
        if (PlayerPrefs.HasKey("KayitliBolum"))
        {
            string kayitliBolum = PlayerPrefs.GetString("KayitliBolum");
            string suAnkiBolum = SceneManager.GetActiveScene().name;

            // Eðer kayýtlý bölüm þu an açýk olan bölümden farklýysa (örn. Level2 ise) oraya geç
            if (!string.IsNullOrEmpty(kayitliBolum) && kayitliBolum != suAnkiBolum)
            {
                SceneManager.LoadScene(kayitliBolum);
            }
        }
    }
}