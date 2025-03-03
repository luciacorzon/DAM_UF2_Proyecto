using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform player; 
    public float smoothSpeed = 5f; 
    public float fixedY = 0f; 
    public float offsetX = 0f; 

    void LateUpdate()
    {
        if (player != null)
        {
            // Mover solo en X
            Vector3 targetPosition = new Vector3(player.position.x + offsetX, fixedY, transform.position.z);

            // Suavizar movimiento con Lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
