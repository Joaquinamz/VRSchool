using UnityEngine;

/// <summary>
/// Detecta cuando un escombro impacta al jugador usando colisiones normales.
/// Se agrega automáticamente a cada escombro spawneado.
/// </summary>
public class DebrisHitDetector : MonoBehaviour
{
    private EarthquakeGameManager gameManager;
    private bool hasHitPlayer = false;
    private Collider debrisCollider;
    
    void Start()
    {
        // Auto-encontrar GameManager
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<EarthquakeGameManager>();
        }
        
        // Asegurar que este GameObject tiene collider
        debrisCollider = GetComponent<Collider>();
        if (debrisCollider == null)
        {
            debrisCollider = gameObject.AddComponent<BoxCollider>();
            Debug.Log("[DebrisHitDetector] ✓ BoxCollider añadido");
        }
        
        // IMPORTANTE: NO es trigger - usa colisión normal
        debrisCollider.isTrigger = false;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        CheckHit(collision.collider);
    }
    
    void CheckHit(Collider collision)
    {
        if (hasHitPlayer) return;  // Solo contar una vez
        
        // Detectar si es el jugador
        bool isPlayer = collision.CompareTag("Player") || 
                       collision.gameObject.name.Contains("Player") || 
                       collision.gameObject.name.Contains("player") ||
                       collision.gameObject.name.Contains("MainCamera") ||
                       collision.gameObject.name.Contains("Camera") ||
                       collision.gameObject.name.Contains("Head");
        
        // También detectar si tiene Rigidbody (probablemente es el jugador)
        Rigidbody otherRb = collision.GetComponent<Rigidbody>();
        if (otherRb != null && !otherRb.isKinematic)
        {
            // Si es un Rigidbody no kinematic que no está cayendo, probablemente es el jugador
            if (collision.gameObject.name.Contains("Character") || 
                collision.gameObject.name.Contains("Player") ||
                collision.gameObject.name.Contains("Camera"))
            {
                isPlayer = true;
            }
        }
        
        if (isPlayer)
        {
            OnPlayerHit(collision);
        }
    }
    
    void OnPlayerHit(Collider playerCollider)
    {
        if (hasHitPlayer) return;
        hasHitPlayer = true;
        
        if (gameManager != null)
        {
            Debug.Log("[DebrisHitDetector] 💥 ¡IMPACTO EN JUGADOR: " + playerCollider.gameObject.name + "!");
            gameManager.RegisterDebrisHit(debrisCollider);
        }
        else
        {
            Debug.LogError("[DebrisHitDetector] ❌ GameManager no encontrado");
        }
        
        // Destruir el escombro
        Destroy(gameObject);
    }
}


