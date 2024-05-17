using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // Referencia al jugador
    public float minX, maxX; // L�mites para el movimiento horizontal de la c�mara
    public float minY, maxY; // L�mites para el movimiento vertical de la c�mara
    private float smoothTime = 0.05f; // Tiempo de suavizado para la interpolaci�n de la c�mara
    private float verticalVelocity = 0.0f; // Velocidad inicial para el suavizado vertical

    public float verticalBuffer = 0.1f; // Buffer vertical para evitar el seguimiento de peque�as variaciones
    private float targetY; // Posici�n Y objetivo para la c�mara

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

            // Actualiza la posici�n Y objetivo de la c�mara solo si el jugador se mueve m�s all� del buffer
            if (Mathf.Abs(transform.position.y - playerY) > verticalBuffer)
            {
                targetY = playerY;
            }

            // Suaviza la transici�n hacia la posici�n Y objetivo
            float smoothY = Mathf.SmoothDamp(transform.position.y, targetY, ref verticalVelocity, smoothTime);

            // Asegura que smoothY est� siempre dentro de los l�mites
            smoothY = Mathf.Clamp(smoothY, minY, maxY);

            transform.position = new Vector3(x, smoothY, transform.position.z);
        }
    }
}



