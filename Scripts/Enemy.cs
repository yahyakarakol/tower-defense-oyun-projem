using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float baseDamage = 10f; // Yolun sonuna ulaþýnca ana kuleye vereceði hasar
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthBar;

    [Header("Can Barý Görünürlük Ayarý")]
    public float hideDelay = 2.5f; // Son hasardan kaç saniye sonra gizlensin?
    private float hideTimer = 0f;

    [Header("Vuruþ Efekti Ayarlarý")]
    public Color flashColor = new Color(1f, 0.25f, 0.25f, 1f); // Belirgin parlak kýrmýzý
    public float flashDuration = 0.1f;
    public float hitPunchScale = 0.85f; // Vurulunca hafifçe irkilme oraný (1 = kapalý)

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Vector3 originalScale;
    private Coroutine flashCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;

        Canvas enemyCanvas = GetComponentInChildren<Canvas>();
        if (enemyCanvas != null)
        {
            enemyCanvas.worldCamera = Camera.main;
        }

        // Hem ana objede hem alt objelerde SpriteRenderer ara
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            healthBar.gameObject.SetActive(false); // Baþlangýçta gizli
        }
    }

    void Update()
    {
                    
        if (healthBar != null && healthBar.gameObject.activeSelf)
        {
            healthBar.value = Mathf.Lerp(healthBar.value, currentHealth, Time.deltaTime * 10f);

            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f)
            {
                healthBar.gameObject.SetActive(false); 
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

       
        if (healthBar != null)
        {
            if (!healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
            }
            hideTimer = hideDelay; 
        }

       
        if (spriteRenderer != null)
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            flashCoroutine = StartCoroutine(FlashRoutine());
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;
        transform.localScale = originalScale * hitPunchScale;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
        transform.localScale = originalScale;
        flashCoroutine = null;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}