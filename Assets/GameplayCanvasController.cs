using UnityEngine;

/// <summary>
/// Controla la visibilidad del Canvas de Gameplay durante el terremoto.
/// Se activa solo cuando comienza el sismo.
/// 
/// INSTRUCCIONES:
/// 1. Crea GameObject vacío llamado "CanvasController" en la escena
/// 2. Adjunta ESTE SCRIPT al CanvasController
/// 3. Arrastra Canvas_Gameplay al campo "gameplayCanvas" en Inspector
/// 4. Arrastra EarthquakeGameManager al campo "earthquakeManager" en Inspector
/// </summary>
public class GameplayCanvasController : MonoBehaviour
{
    [SerializeField] private Canvas gameplayCanvas;
    [SerializeField] private EarthquakeGameManager earthquakeManager;
    
    private bool isInitialized = false;
    
    void Start()
    {
        // Buscar Canvas_Gameplay por nombre si no está asignado
        if (gameplayCanvas == null)
        {
            GameObject canvasGO = GameObject.Find("Canvas_Gameplay");
            if (canvasGO != null)
            {
                gameplayCanvas = canvasGO.GetComponent<Canvas>();
                Debug.Log("[GameplayCanvasController] ✓ Canvas_Gameplay encontrado por nombre");
            }
            else
            {
                Debug.LogError("[GameplayCanvasController] ❌ Canvas_Gameplay NO encontrado en escena");
                Debug.Log("[GameplayCanvasController] SOLUCIÓN: Crea un Canvas llamado 'Canvas_Gameplay' en Hierarchy");
                return;
            }
        }
        
        // Buscar EarthquakeGameManager si no está asignado
        if (earthquakeManager == null)
        {
            earthquakeManager = FindFirstObjectByType<EarthquakeGameManager>();
            if (earthquakeManager == null)
            {
                Debug.LogError("[GameplayCanvasController] ❌ EarthquakeGameManager NO encontrado");
                return;
            }
            Debug.Log("[GameplayCanvasController] ✓ EarthquakeGameManager encontrado automáticamente");
        }
        
        // IMPORTANTE: Desactivar canvas al inicio
        if (gameplayCanvas != null)
        {
            gameplayCanvas.gameObject.SetActive(false);
            Debug.Log("[GameplayCanvasController] ✓ Canvas_Gameplay desactivado al inicio");
            isInitialized = true;
        }
    }
    
    void Update()
    {
        if (!isInitialized || earthquakeManager == null || gameplayCanvas == null)
            return;
        
        // Activar canvas SOLO cuando el terremoto comienza
        if (earthquakeManager.earthquakeActive && !gameplayCanvas.gameObject.activeSelf)
        {
            gameplayCanvas.gameObject.SetActive(true);
            Debug.Log("[GameplayCanvasController] ✓ Canvas_Gameplay ACTIVADO - ¡TERREMOTO COMENZÓ!");
        }
        // Desactivar canvas cuando el terremoto termina
        else if (!earthquakeManager.earthquakeActive && gameplayCanvas.gameObject.activeSelf)
        {
            gameplayCanvas.gameObject.SetActive(false);
            Debug.Log("[GameplayCanvasController] ✓ Canvas_Gameplay DESACTIVADO - Terremoto terminó");
        }
    }
}

