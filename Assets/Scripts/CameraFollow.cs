using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // Referencia al jugador
    public float minX, maxX; // L�mites para el movimiento horizontal de la c�mara
    public float minY, maxY; // L�mites para el movimiento vertical de la c�mara

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Calcula la nueva posici�n x e y de la c�mara
            float x = Mathf.Clamp(player.position.x, minX, maxX);
            float y = Mathf.Clamp(player.position.y, minY, maxY);

            // Establece la posici�n de la c�mara
            // Asume que la c�mara solo se mueve en el plano XY
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}


