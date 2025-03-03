using UnityEngine;

// Fai que os enemigos disparen burbujas na dirección na que se moven
public class EnemyShooter : MonoBehaviour
{
    public GameObject bubblePrefab; 
    public float bubbleSpeed = 5f;
    public float shootInterval = 2f; 

    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(ShootBubble), shootInterval, shootInterval);
    }

    void Update()
    {
        if (rb != null)
        {
            moveDirection = rb.linearVelocity.normalized;
        }
    }

    void ShootBubble()
    {
        if (bubblePrefab != null && moveDirection != Vector2.zero)
        {
            GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            Rigidbody2D bubbleRb = bubble.GetComponent<Rigidbody2D>();

            if (bubbleRb != null)
            {
                bubbleRb.linearVelocity = moveDirection * bubbleSpeed;
            }
        }
    }
}

