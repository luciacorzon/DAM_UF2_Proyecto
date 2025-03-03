using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; 
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer; 
    private AudioSource audioSource; 
    public AudioClip shootSound;
    public AudioClip hitSound;  
    public AudioClip gemSound;  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Invertir o sprite según hacia donde vai o xogador
        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x > 0; 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // Mover personaxe
        rb.linearVelocity = movement * speed;
    }

    private void Shoot()
    {
        // Reproducir sonido de disparo
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem"))
        {
            GameManager.instance.GainHealth(2);
            PlayGemSound();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Portal"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (collision.CompareTag("EnemyBubble"))
        {
            GameManager.instance.TakeDamage(1); 
            StartCoroutine(FlashRed());
            PlayHitSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.TakeDamage(1);
            StartCoroutine(FlashRed());
            PlayHitSound(); 
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void PlayGemSound()
    {
        if (gemSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gemSound);
        }
    }
}
