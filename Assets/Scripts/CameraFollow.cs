using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float minX, maxX; // Límites para el movimiento horizontal de la cámara
    public float minY, maxY; // Límites para el movimiento vertical de la cámara

    void Update()
    {
        if (player != null)
        {
            // Calcula la nueva posición x e y de la cámara
            float x = Mathf.Clamp(player.position.x, minX, maxX);
            float y = Mathf.Clamp(player.position.y, minY, maxY);

            // Establece la posición de la cámara
            // Asume que la cámara solo se mueve en el plano XY
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}


