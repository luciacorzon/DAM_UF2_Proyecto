using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Final Boss do último nivel, o xogo acábase cando o xogador o mata
public class FinalBoss : MonoBehaviour
{
    public int maxHealth = 10;
    public GameObject enemyPrefab;
    public GameObject bubblePrefab; 
    public float enemySpeed = 3f;
    public float bubbleSpeed = 3f;
    public float shootInterval = 3f;
    public float bubbleShootInterval = 8f;
    public string nextSceneName = "GameOver";

    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // O FinalBoss dispara enemigos e ademais dispara burbujas especiales que atrapan ao xogador
        InvokeRepeating(nameof(ShootEnemy), shootInterval, shootInterval);
        InvokeRepeating(nameof(ShootBubble), bubbleShootInterval, bubbleShootInterval);
    }

    // O Final Boss siempre está fixo
    void Update()
    {
        transform.position = new Vector2(118, 0);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }
void Die()
{
    // Faise desaparecer o sprite do final boss e cambiáse á escena de final de xogo
    gameObject.SetActive(false);
    Invoke(nameof(ChangeScene), 1f);
}

void ChangeScene()
{
    SceneManager.LoadScene(nextSceneName);
}


    void ShootEnemy()
    {
        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                  // Os enemigos do final boss sempre van hacia á esquerda
                enemyRb.linearVelocity = Vector2.left * enemySpeed;
            }
        }
    }

    void ShootBubble()
    {
        if (bubblePrefab != null)
        {
            GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            Rigidbody2D bubbleRb = bubble.GetComponent<Rigidbody2D>();

            if (bubbleRb != null)
            {
                // As burbujas do final boss sempre van hacia á esquerda
                bubbleRb.linearVelocity = Vector2.left * bubbleSpeed;
            }
        }
    }

    // Cando recibe daño píntase de rojo
    IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
        }
    }
}
