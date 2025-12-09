using UnityEngine;

public class RespawnByDistance : MonoBehaviour
{
    public Transform player;          // XR Origin o cámara del jugador
    public float maxDistance = 6f;    // Distancia límite
    private Vector3 initialPosition;  // Donde respawnea la pelota

    void Start()
    {
        // Guardamos la posición inicial de la pelota al iniciar la escena
        initialPosition = transform.position;

        // Si no asignaste el jugador, intenta encontrar la cámara XR automáticamente
        if (player == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
                player = mainCam.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculamos distancia entre pelota y el jugador
        float distance = Vector3.Distance(transform.position, player.position);

        // Si se aleja demasiado, respawneamos
        if (distance > maxDistance)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Reinicia posición y resetear físicas
        transform.position = initialPosition;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
