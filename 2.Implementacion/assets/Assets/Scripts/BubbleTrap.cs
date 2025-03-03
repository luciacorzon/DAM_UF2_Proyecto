using UnityEngine;
using System.Collections;

// Burbuja do boss final que deixa atrapado ao xogador durante certo tempo
public class BubbleTrap : MonoBehaviour
{
    public float speed = 3f;
    public float trapDuration = 3f;
    private Transform player;
    private bool hasTrappedPlayer = false;

    void Start()
    {
        // Encontrar ao xogador
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null && !hasTrappedPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Se encontra ao xogador, atrápao
        if (other.CompareTag("Player") && !hasTrappedPlayer)
        {
            hasTrappedPlayer = true;
            StartCoroutine(TrapPlayer(other.gameObject));
        }
    }


    IEnumerator TrapPlayer(GameObject player)
    {
        // Desactivar o movemento do xogador
        PlayerController playerController = player.GetComponent<PlayerController>();
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Colocar o sprite da burbuja sobre o do xogador
        transform.position = player.transform.position;
        GetComponent<SpriteRenderer>().sortingOrder = playerSprite.sortingOrder + 1;

        // Manter a posición da burbuja mentres dure o atrapamento
        float timer = 0f;
        while (timer < trapDuration)
        {
            transform.position = player.transform.position;
            timer += Time.deltaTime;
            yield return null;
        }

        // Liberar ao xogador
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        if (playerRb != null)
        {
            playerRb.bodyType = RigidbodyType2D.Dynamic;
        }

        Destroy(gameObject);
    }

}
