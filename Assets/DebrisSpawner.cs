using UnityEngine;

/// <summary>
/// Sistema de spawning de escombros para lecciones de terremoto.
/// Spawnea escombros aleatoriamente del techo durante 30 segundos.
/// 
/// CONFIGURACIÓN:
/// - Prefab de escombro (debe tener Rigidbody + Collider + DebrisHitDetector)
/// - Zona de spawn (rectángulo en el techo)
/// - Tasa de spawn (escombros por segundo)
/// - Velocidad de caída
/// </summary>
public class DebrisSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject debrisPrefab;
    
    [Header("Zona de Spawn")]
    [SerializeField] private Vector3 spawnAreaMin = new Vector3(-5, 3, -5);
    [SerializeField] private Vector3 spawnAreaMax = new Vector3(5, 5, 5);
    [SerializeField] private float debrisMinScale = 0.3f;
    [SerializeField] private float debrisMaxScale = 0.7f;
    
    [Header("Configuración")]
    [SerializeField] private float spawnRate = 2f;  // Escombros por segundo
    [SerializeField] private float debrisForce = 20f;
    [SerializeField] private float debrisLifetime = 10f;  // Segundos antes de auto-destruir
    [SerializeField] private int maxDebrisActive = 50;  // Límite máximo simultáneo
    
    private bool isSpawning = false;
    private float spawnTimer = 0f;
    private int currentDebrisCount = 0;
    
    void Start()
    {
        if (debrisPrefab == null)
        {
            Debug.LogError("[DebrisSpawner] ❌ debrisPrefab no asignado en Inspector");
            return;
        }
        
        Debug.Log("[DebrisSpawner] ✓ Inicializado");
    }
    
    void Update()
    {
        if (!isSpawning) return;
        
        spawnTimer += Time.deltaTime;
        
        // Calcular cuántos escombros spawnear este frame
        float debrisPerFrame = spawnRate * Time.deltaTime;
        
        if (spawnTimer >= (1f / spawnRate))
        {
            spawnTimer = 0f;
            
            if (currentDebrisCount < maxDebrisActive)
            {
                SpawnDebris();
            }
        }
    }
    
    void SpawnDebris()
    {
        if (debrisPrefab == null) return;
        
        // Posición aleatoria en la zona de spawn
        Vector3 randomPos = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );
        
        // Instanciar escombro
        GameObject debris = Instantiate(debrisPrefab, randomPos, Quaternion.identity);
        debris.name = "Debris_" + currentDebrisCount;
        
        // Tamaño aleatorio para más realismo
        float randomScale = Random.Range(debrisMinScale, debrisMaxScale);
        debris.transform.localScale = Vector3.one * randomScale;
        
        // Configurar Rigidbody para que caiga
        Rigidbody rb = debris.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.linearVelocity = Vector3.down * debrisForce;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            rb = debris.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.linearVelocity = Vector3.down * debrisForce;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        
        // Collider NORMAL (NO trigger) para colisionar con suelo
        Collider col = debris.GetComponent<Collider>();
        if (col == null)
        {
            col = debris.AddComponent<BoxCollider>();
        }
        col.isTrigger = false;  // CRÍTICO: NO es trigger para que colisione con suelo
        
        // Añadir script de detección de impactos
        DebrisHitDetector detector = debris.GetComponent<DebrisHitDetector>();
        if (detector == null)
        {
            detector = debris.AddComponent<DebrisHitDetector>();
        }
        
        // Auto-destruir después de cierto tiempo
        Destroy(debris, debrisLifetime);
        
        currentDebrisCount++;
        Debug.Log($"[DebrisSpawner] 💨 Escombro {currentDebrisCount} spawnado en {randomPos}");
    }
    
    public void StartSpawning()
    {
        isSpawning = true;
        spawnTimer = 0f;
        currentDebrisCount = 0;
        Debug.Log("[DebrisSpawner] ▶️  Empezando a spawnear escombros");
    }
    
    public void StopSpawning()
    {
        isSpawning = false;
        Debug.Log("[DebrisSpawner] ⏹️  Deteniendo spawn de escombros");
    }
    
    public bool IsSpawning() => isSpawning;
    public int GetActiveDebrisCount() => currentDebrisCount;
}
