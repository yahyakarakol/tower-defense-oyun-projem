using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþi için eklendi

[System.Serializable]
public class EnemySpawnRule
{
    public string enemyName;          // Karýþmamasý için isim (Örn: "Küçük Düþman", "Büyük Düþman")
    public GameObject enemyPrefab;    // Doðacak düþman kalýbý
    public int spawnCount = 5;        // BU TÜRDE KAÇ DÜÞMAN DOÐACAK? (Yeni eklendi)
    public float initialDelay = 1f;   // Oyun baþladýktan kaç saniye sonra ilk kez doðsun?
    public float spawnInterval = 2f;  // Kaç saniyede bir yeni bir tane doðsun?
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Düþman Doðum Kurallarý")]
    public EnemySpawnRule[] enemyRules; // Her düþman türü için baðýmsýz zamanlayýcý listesi

    [Header("Doðum Noktasý")]
    public Transform spawnPoint;

    [Header("Bölüm Geçiþ Ayarlarý")]
    public string nextLevelName = "Level2"; // Tüm düþmanlar bitince geçilecek sahne adý
    public float levelTransitionDelay = 1.5f; // Son düþman ölünce kaç saniye sonra geçsin?

    private int activeRoutines = 0;
    private bool isLevelChanging = false;

    void Start()
    {
        PlayerPrefs.SetString("KayitliBolum", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        foreach (EnemySpawnRule rule in enemyRules)
        {
            if (rule.enemyPrefab != null && rule.spawnCount > 0)
            {
                activeRoutines++;
                StartCoroutine(SpawnRoutine(rule));
            }
        }
    }

    IEnumerator SpawnRoutine(EnemySpawnRule rule)
    {
        // Ýlk doðuma kadar bekleme süresi
        yield return new WaitForSeconds(rule.initialDelay);

        // Sadece belirlenen spawnCount adedi kadar düþman doður
        for (int i = 0; i < rule.spawnCount; i++)
        {
            Instantiate(rule.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // Son düþmandan sonra fazladan bekleme yapma
            if (i < rule.spawnCount - 1)
            {
                yield return new WaitForSeconds(rule.spawnInterval);
            }
        }

        // Bu düþman türünün doðumu tamamlandý
        activeRoutines--;
    }

    void Update()
    {
        if (isLevelChanging) return;

        // Tüm düþman türlerinin doðumu bittiyse (activeRoutines == 0)
        if (activeRoutines == 0)
        {
            // Sahnede "Enemy" etiketli canlý düþman kalýp kalmadýðýný kontrol et
            GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if (remainingEnemies.Length == 0)
            {
                isLevelChanging = true;
                Invoke(nameof(LoadNextLevel), levelTransitionDelay);
            }
        }
    }

    void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}