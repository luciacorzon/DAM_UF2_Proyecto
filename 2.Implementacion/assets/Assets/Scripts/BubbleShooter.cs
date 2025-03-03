using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform firePoint;
    public float bubbleSpeed = 5f;

    private Vector2 lastDirection = Vector2.right;

    void Update()
    {
        UpdateDirection(); 
        FlipFirePoint(); 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBubble();
        }
    }

    void UpdateDirection()
    {
        // Detectar teclas presionadas
        bool pressingRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool pressingLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool pressingUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool pressingDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

    

        // Prioridad para as teclas horizontales
        if (pressingRight)
        {
            lastDirection = Vector2.right;
        }
        else if (pressingLeft)
        {
            lastDirection = Vector2.left;
        }
        else if (pressingUp)
        {
            lastDirection = Vector2.up;
        }
        else if (pressingDown)
        {
            lastDirection = Vector2.down;
        }
    }


    // Dispárase na última dirección que tomou o xogador
    void FlipFirePoint()
    {
        firePoint.localPosition = new Vector3(0.5f * lastDirection.x, 0.5f * lastDirection.y, 0);
    }

    void ShootBubble()
    {
        GameObject bubble = Instantiate(bubblePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = lastDirection * bubbleSpeed;
        }
    }
}
