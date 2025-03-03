using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Transform player;
    
    public bool facingRight = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        if (rb != null)
        {
            SetRandomDirection();
        }
    }

    // Facer que os enemigos persigan ao xogador
    void FixedUpdate()
    {
        if (player != null)
        {
            moveDirection = (player.position - transform.position).normalized;
            rb.linearVelocity = moveDirection * speed; 

            FlipSprite();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Facer que rebote con algo de impulso ao chocar cos bordes
        if (collision.gameObject.CompareTag("TopBorder") || collision.gameObject.CompareTag("BottomBorder"))
        {
            moveDirection.y *= -1; 
        }
        else if (collision.gameObject.CompareTag("LeftBorder") || collision.gameObject.CompareTag("RightBorder"))
        {
            moveDirection.x *= -1; 
        }
        // Facer que rebote normal contra os enemigos
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 normal = collision.contacts[0].normal;
            moveDirection = Vector2.Reflect(moveDirection, normal);
        }

        moveDirection = moveDirection.normalized;
        rb.linearVelocity = moveDirection * speed;
    }

    void SetRandomDirection()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.linearVelocity = moveDirection * speed;
    }

    void FlipSprite()
    {
        // Cambiar hacia donde mira o sprite dependendo da dirección horizontal que leve o enemigo
        if ((moveDirection.x > 0 && !facingRight) || (moveDirection.x < 0 && facingRight))
        {
            facingRight = !facingRight; 
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
}
