using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseTower : MonoBehaviour
{
    public static BaseTower instance;

    [Header("Ana Kule Can Ayarlarý")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Can Barý Görseli")]
    public Image healthFillImage;

    private bool isDestroyed = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        // Kendi altýndaki "Canvas/BarFill" yolunu otomatik bulup baðla
        Transform fillTransform = transform.Find("Canvas/BarFill");
        if (fillTransform != null)
        {
            healthFillImage = fillTransform.GetComponent<Image>();
        }

       if (healthFillImage != null)
        {
            healthFillImage.type = Image.Type.Filled;
            healthFillImage.fillMethod = Image.FillMethod.Vertical;
            healthFillImage.fillOrigin = (int)Image.OriginVertical.Bottom;
            healthFillImage.fillAmount = 1f;
          
        }
        else
        {
            Debug.LogError("HATA: MainBase -> Canvas -> BarFill yolu bulunamadý!");
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDestroyed) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        Debug.Log("Ana Kule Hasar Aldý! Kalan Can: " + currentHealth);

        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }

        if (currentHealth <= 0f)
        {
            DestroyBase();
        }
    }

    void DestroyBase()
    {
        isDestroyed = true;
        Debug.Log("ANA KULE YIKILDI! Oyun Bitti.");
        Invoke("RestartLevel", 1.5f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}