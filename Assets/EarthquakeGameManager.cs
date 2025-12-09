using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Gestor de Juego para Lecciones de Terremoto.
/// 
/// FLUJO:
/// 1. StartIntroduction() → Muestra diálogo del profesor explicando qué hacer
/// 2. Usuario presiona "Continuar" → CompleteIntroduction()
/// 3. El terreno comienza a temblar (shake animation)
/// 4. Después de 3 segundos, empiezan a caer escombros durante 30 segundos
/// 5. El usuario se esconde debajo de mesas para evitar daño
/// 6. Cada impacto de escombro reduce el puntaje final
/// 7. Después de 30 segundos, el terremoto termina → ShowResults()
/// </summary>
public class EarthquakeGameManager : MonoBehaviour
{
    [Header("Referencias Críticas")]
    [SerializeField] private EarthquakeProfessor professorController;
    [SerializeField] private DebrisSpawner debrisSpawner;
    [SerializeField] private Transform[] safeZones;  // Transformaciones donde hay mesas
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI uiTimer;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI hitCountText;
    
    [Header("Configuración")]
    [SerializeField] private float debrisStartDelay = 3f;  // Segundos antes de empezar a caer escombros
    [SerializeField] private float earthquakeDuration = 30f;  // Duración total del terremoto
    [SerializeField] private float shakeIntensity = 0.5f;
    [SerializeField] private float shakeSpeed = 20f;
    
    // Estado del juego
    public enum GamePhase
    {
        NotStarted,
        Introduction,
        Earthquake_Starting,
        Earthquake_Active,
        Earthquake_Ending,
        Complete
    }
    
    [Header("Audio")]
    [SerializeField] private AudioClip earthquakeSound;
    [SerializeField] private float soundVolume = 0.7f;
    
    private AudioSource audioSource;
    private GamePhase currentPhase = GamePhase.NotStarted;
    private float earthquakeTimer = 0f;
    public bool earthquakeActive = false;  // PUBLIC para que otros scripts lo lean
    private int totalHits = 0;
    
    // Variables de shake
    private Vector3 originalCameraPosition;
    private Camera mainCamera;
    
    void Start()
    {
        Debug.Log("[EarthquakeGameManager] ✓ Inicializado");
        
        // Obtener o crear AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("[EarthquakeGameManager] ✓ AudioSource creado automáticamente");
        }
        audioSource.volume = soundVolume;
        
        // Auto-encontrar profesor
        if (professorController == null)
        {
            professorController = FindFirstObjectByType<EarthquakeProfessor>();
            if (professorController != null)
                Debug.Log("[EarthquakeGameManager] ✓ EarthquakeProfessor encontrado automáticamente");
        }
        
        // Auto-encontrar DebrisSpawner
        if (debrisSpawner == null)
        {
            debrisSpawner = FindFirstObjectByType<DebrisSpawner>();
            if (debrisSpawner != null)
                Debug.Log("[EarthquakeGameManager] ✓ DebrisSpawner encontrado automáticamente");
        }
        
        // Obtener cámara para shake
        mainCamera = Camera.main;
        if (mainCamera != null)
            originalCameraPosition = mainCamera.transform.position;
        
        if (statusText != null)
            statusText.text = "Listos para empezar...";
    }
    
    void Update()
    {
        if (currentPhase == GamePhase.NotStarted)
            return;
        
        UpdateUI();
        
        if (currentPhase == GamePhase.Earthquake_Active)
        {
            earthquakeTimer += Time.deltaTime;
            UpdateShake();
            
            // Cuando el timer llega a debrisStartDelay, empezar escombros
            if (earthquakeTimer >= debrisStartDelay && debrisSpawner != null && !debrisSpawner.IsSpawning())
            {
                Debug.Log("[EarthquakeGameManager] 💨 Empezando a caer escombros en tiempo: " + earthquakeTimer);
                debrisSpawner.StartSpawning();
            }
            else if (earthquakeTimer >= debrisStartDelay && debrisSpawner == null)
            {
                Debug.LogError("[EarthquakeGameManager] ❌ CRÍTICO: debrisSpawner es NULL - no hay escombros");
            }
            
            // Si llegamos al final
            if (earthquakeTimer >= earthquakeDuration)
            {
                CompleteEarthquake();
            }
        }
    }
    
    void UpdateUI()
    {
        if (uiTimer != null)
            uiTimer.text = $"Tiempo: {earthquakeTimer:F1}s";
        
        if (hitCountText != null)
            hitCountText.text = $"Impactos: {totalHits}";
    }
    
    void UpdateShake()
    {
        if (mainCamera == null)
        {
            return;
        }
        
        // Temblor MUY agresivo - cambio cada frame
        float randomX = Random.Range(-shakeIntensity, shakeIntensity);
        float randomY = Random.Range(-shakeIntensity, shakeIntensity);
        float randomZ = Random.Range(-shakeIntensity * 0.3f, shakeIntensity * 0.3f);
        
        Vector3 shakeOffset = new Vector3(randomX, randomY, randomZ);
        mainCamera.transform.position = originalCameraPosition + shakeOffset;
    }
    
    /// <summary>
    /// Inicia la lección - muestra diálogo de introducción
    /// </summary>
    public void StartIntroduction()
    {
        if (professorController == null)
        {
            Debug.LogError("[EarthquakeGameManager] ❌ professorController es NULL");
            statusText.text = "ERROR: Professor no asignado";
            return;
        }
        
        currentPhase = GamePhase.Introduction;
        earthquakeActive = false;
        earthquakeTimer = 0f;
        totalHits = 0;
        
        Debug.Log("[EarthquakeGameManager] 📖 Iniciando introducción");
        statusText.text = "Escucha las instrucciones del profesor...";
        professorController.ShowIntroduction();
    }
    
    /// <summary>
    /// Llamado desde EarthquakeProfessor cuando el usuario presiona "Continuar"
    /// </summary>
    public void CompleteIntroduction()
    {
        if (currentPhase != GamePhase.Introduction)
        {
            Debug.LogWarning($"[EarthquakeGameManager] ⚠️  CompleteIntroduction() en fase {currentPhase}, ignorando");
            return;
        }
        
        Debug.Log("[EarthquakeGameManager] ✓ Introducción completada - TERREMOTO INICIANDO");
        
        currentPhase = GamePhase.Earthquake_Starting;
        earthquakeActive = true;
        earthquakeTimer = 0f;
        statusText.text = "¡¡TERREMOTO!! ¡Cúbrete debajo de las mesas!";
        
        // Sacudida inicial - aplicar directamente
        if (mainCamera != null)
        {
            for (int i = 0; i < 5; i++)
            {
                float randomX = Random.Range(-1f, 1f) * 0.8f;
                float randomY = Random.Range(-0.5f, 0.5f) * 0.8f;
                mainCamera.transform.position = originalCameraPosition + new Vector3(randomX, randomY, 0);
            }
            mainCamera.transform.position = originalCameraPosition;
            Debug.Log("[EarthquakeGameManager] 💥 Sacudida inicial aplicada");
        }
        
        // Transición a fase activa
        Invoke(nameof(StartEarthquakePhase), 0.5f);
    }
    
    void StartEarthquakePhase()
    {
        currentPhase = GamePhase.Earthquake_Active;
        Debug.Log("[EarthquakeGameManager] 🌍 Fase de terremoto activa");
        
        // Reproducir sonido de terremoto
        if (audioSource != null && earthquakeSound != null)
        {
            audioSource.clip = earthquakeSound;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("[EarthquakeGameManager] 🔊 Sonido de terremoto iniciado");
        }
        else
        {
            Debug.LogWarning("[EarthquakeGameManager] ⚠️ earthquakeSound no asignado - sin audio");
        }
    }
    
    void CompleteEarthquake()
    {
        earthquakeActive = false;
        currentPhase = GamePhase.Earthquake_Ending;
        
        // Detener sonido
        if (audioSource != null)
        {
            audioSource.Stop();
            Debug.Log("[EarthquakeGameManager] 🔇 Sonido de terremoto detenido");
        }
        
        // Detener escombros
        if (debrisSpawner != null)
            debrisSpawner.StopSpawning();
        
        // Restaurar cámara
        if (mainCamera != null)
            mainCamera.transform.position = originalCameraPosition;
        
        Debug.Log("[EarthquakeGameManager] ✓ Terremoto completado");
        statusText.text = "Terremoto terminado";
        
        // Mostrar resultados
        Invoke(nameof(ShowResults), 1f);
    }
    
    void ShowResults()
    {
        currentPhase = GamePhase.Complete;
        
        // Calcular puntaje
        float maxPuntaje = 100f;
        float penalizacionPorImpacto = 10f;
        float puntajeFinal = Mathf.Max(0, maxPuntaje - (totalHits * penalizacionPorImpacto));
        
        Debug.Log($"[EarthquakeGameManager] 📊 RESULTADOS:");
        Debug.Log($"   Impactos recibidos: {totalHits}");
        Debug.Log($"   Puntaje final: {puntajeFinal:F0}/100");
        
        if (professorController != null)
        {
            professorController.ShowResults(totalHits, puntajeFinal);
        }
    }
    
    /// <summary>
    /// Llamado desde DebrisHitDetector cuando un escombro impacta al usuario
    /// </summary>
    public void RegisterDebrisHit(Collider debrisCollider)
    {
        // Verificar si el usuario está en una zona segura (debajo de mesa)
        if (IsPlayerInSafeZone())
        {
            Debug.Log("[EarthquakeGameManager] ✓ Impacto bloqueado - Usuario bajo mesa");
            return;
        }
        
        totalHits++;
        Debug.Log($"[EarthquakeGameManager] 💥 Impacto recibido! Total: {totalHits}");
        
        // Actualizar UI inmediatamente
        if (hitCountText != null)
        {
            hitCountText.text = $"Impactos: {totalHits}";
        }
    }
    
    bool IsPlayerInSafeZone()
    {
        // Obtener posición del jugador (cámara principal)
        if (mainCamera == null) return false;
        
        Vector3 playerPos = mainCamera.transform.position;
        
        // Si no hay zonas seguras definidas, no hay protección
        if (safeZones == null || safeZones.Length == 0)
            return false;
        
        // Comprobar si está dentro de cualquier zona segura (radio de 2 metros)
        foreach (Transform safeZone in safeZones)
        {
            if (safeZone == null) continue;
            
            float distance = Vector3.Distance(playerPos, safeZone.position);
            if (distance < 2f)  // Radio de protección de 2 metros
            {
                return true;
            }
        }
        
        return false;
    }
    
    // Getters para debugging
    public GamePhase GetCurrentPhase() => currentPhase;
    public float GetEarthquakeTimer() => earthquakeTimer;
    public int GetTotalHits() => totalHits;
}