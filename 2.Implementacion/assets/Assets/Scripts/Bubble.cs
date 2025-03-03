using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float lifetime = 3f; 
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Para as burbujas do xogador
        if (gameObject.CompareTag("Bubble"))
        {
            // Se golpea a un enemigo, este debe destruirse
            if (other.CompareTag("Enemy") || other.CompareTag("EnemyBubble")) 
            {
                Destroy(gameObject);
                Destroy(other.gameObject); 
            }

            // Se golpea ao final boss solo lle quita puntos de vida
            if (other.CompareTag("FinalBoss")) 
            {
                FinalBoss boss = other.GetComponent<FinalBoss>(); 
                if (boss != null)
                {
                    boss.TakeDamage(damage); 
                }
                Destroy(gameObject); 
            }
        }

        // Para as burbujas do enemigo que lle restan vida ao xogador
        if (gameObject.CompareTag("EnemyBubble"))
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject); 
            }
        }
    }
}
