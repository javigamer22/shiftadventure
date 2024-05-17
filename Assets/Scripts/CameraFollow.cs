using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // Referencia al jugador
    public float minX, maxX; // Límites para el movimiento horizontal de la cámara
    public float minY, maxY; // Límites para el movimiento vertical de la cámara
    private float smoothTime = 0.05f; // Tiempo de suavizado para la interpolación de la cámara
    private float verticalVelocity = 0.0f; // Velocidad inicial para el suavizado vertical

    public float verticalBuffer = 0.1f; // Buffer vertical para evitar el seguimiento de pequeñas variaciones
    private float targetY; // Posición Y objetivo para la cámara

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetY = transform.position.y;
    }

    void Update()
    {
        if (player != null)
        {
            float x = Mathf.Clamp(player.position.x, minX, maxX);
            float playerY = Mathf.Clamp(player.position.y, minY, maxY);

            // Actualiza la posición Y objetivo de la cámara solo si el jugador se mueve más allá del buffer
            if (Mathf.Abs(transform.position.y - playerY) > verticalBuffer)
            {
                targetY = playerY;
            }

            // Suaviza la transición hacia la posición Y objetivo
            float smoothY = Mathf.SmoothDamp(transform.position.y, targetY, ref verticalVelocity, smoothTime);

            // Asegura que smoothY esté siempre dentro de los límites
            smoothY = Mathf.Clamp(smoothY, minY, maxY);

            transform.position = new Vector3(x, smoothY, transform.position.z);
        }
    }
}



