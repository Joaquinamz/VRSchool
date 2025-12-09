using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Gestor moderno de escenas para VR con carga/descarga flexible.
/// Soporta:
/// - Carga de escena única (reemplaza actual)
/// - Carga aditiva (mantiene lobby)
/// - Descarga de escena específica
/// - Transiciones suave con delay
/// </summary>
public class SceneManagerVR : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string lobbySceneName = "Lobby";
    [SerializeField] private float transitionDelay = 0.5f;  // Delay antes de descargar
    [SerializeField] private bool debugMode = true;
    
    private static SceneManagerVR instance;
    private string currentCourseScene = "";  // Escena de curso actual cargada
    
    void Awake()
    {
        // Singleton para acceso global
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Carga una escena reemplazando la actual (usado en Lobby → Curso)
    /// </summary>
    public void LoadSceneReplace(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            LogError("SceneName is null or empty");
            return;
        }

        LogInfo($"Cargando escena (reemplazar): {sceneName}");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Carga una escena de forma aditiva (Lobby abierto + Curso)
    /// Útil para mantener la Lobby visible mientras carga el curso
    /// </summary>
    public void LoadSceneAdditive(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            LogError("SceneName is null or empty");
            return;
        }

        // Si ya hay un curso cargado, descargarlo primero
        if (!string.IsNullOrEmpty(currentCourseScene))
        {
            StartCoroutine(UnloadAndLoadRoutine(currentCourseScene, sceneName));
        }
        else
        {
            LogInfo($"Cargando escena aditiva: {sceneName}");
            StartCoroutine(LoadAdditiveRoutine(sceneName));
        }
    }

    /// <summary>
    /// Descarga una escena específica (usado en "Volver a Lobby")
    /// </summary>
    public void UnloadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            LogError("SceneName is null or empty");
            return;
        }

        LogInfo($"Descargando escena: {sceneName}");
        StartCoroutine(UnloadSceneRoutine(sceneName));
    }

    /// <summary>
    /// Regresa a Lobby descargando la escena de curso actual
    /// </summary>
    public void ReturnToLobby()
    {
        if (string.IsNullOrEmpty(currentCourseScene))
        {
            LogWarning("No hay escena de curso cargada, ya estamos en Lobby");
            return;
        }

        LogInfo($"Regresando a Lobby, descargando: {currentCourseScene}");
        StartCoroutine(UnloadSceneRoutine(currentCourseScene));
    }

    // ============ CORRUTINAS ============

    private IEnumerator LoadAdditiveRoutine(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            yield return null;
        }

        currentCourseScene = sceneName;
        LogInfo($"✓ Escena aditiva cargada: {sceneName}");
    }

    private IEnumerator UnloadAndLoadRoutine(string unloadScene, string loadScene)
    {
        LogInfo($"Descargando {unloadScene} y cargando {loadScene}...");
        
        // Descargar anterior
        var unloadOp = SceneManager.UnloadSceneAsync(unloadScene);
        while (!unloadOp.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(transitionDelay);

        // Cargar nuevo
        var loadOp = SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
        while (!loadOp.isDone)
        {
            yield return null;
        }

        currentCourseScene = loadScene;
        LogInfo($"✓ Transición completada: {loadScene}");
    }

    private IEnumerator UnloadSceneRoutine(string sceneName)
    {
        yield return new WaitForSeconds(transitionDelay);

        var operation = SceneManager.UnloadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }

        currentCourseScene = "";
        LogInfo($"✓ Escena descargada: {sceneName}");
    }

    // ============ HELPERS ============

    private void LogInfo(string message)
    {
        if (debugMode)
            Debug.Log($"[SceneManagerVR] {message}");
    }

    private void LogWarning(string message)
    {
        Debug.LogWarning($"[SceneManagerVR] {message}");
    }

    private void LogError(string message)
    {
        Debug.LogError($"[SceneManagerVR] {message}");
    }

    // Acceso estático para usar desde botones sin Script
    public static void LoadScene_Static(string sceneName) => instance?.LoadSceneReplace(sceneName);
    public static void LoadSceneAdditive_Static(string sceneName) => instance?.LoadSceneAdditive(sceneName);
    public static void ReturnToLobby_Static() => instance?.ReturnToLobby();
}
