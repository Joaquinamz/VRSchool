using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script SIMPLE para cargar/descargar escenas desde botones.
/// Basado en los principios del antiguo SceneLoaderExtintor.cs
/// pero mejorado para manejar Lobby ↔ Cursos sin problemas.
/// 
/// USO:
/// 1. Asigna este script a CADA botón (en Lobby y en Cursos)
/// 2. En Inspector, elige:
///    - Mode: "LoadCourse" (para botones en Lobby)
///    - Mode: "ReturnToLobby" (para botón "Volver" en Cursos)
/// 3. Si es LoadCourse, escribe el nombre de la escena
/// 4. En Button.OnClick → +
///    → Arrastra GameObject del botón
///    → Dropdown: SimpleLobbyLoader > OnButtonClick()
/// </summary>
public class SimpleLobbyLoader : MonoBehaviour
{
    public enum LoadMode
    {
        LoadCourse,      // Carga un curso (descarga Lobby)
        ReturnToLobby    // Regresa a Lobby (descarga curso)
    }

    [SerializeField] private LoadMode mode = LoadMode.LoadCourse;
    [SerializeField] private string targetSceneName = "";
    [SerializeField] private string lobbySceneName = "Lobby";

    // Se llama desde el evento OnClick del botón
    public void OnButtonClick()
    {
        if (mode == LoadMode.LoadCourse)
        {
            LoadCourse(targetSceneName);
        }
        else if (mode == LoadMode.ReturnToLobby)
        {
            ReturnToLobby();
        }
    }

    /// <summary>
    /// Carga una escena de curso (descarga todo lo demás)
    /// Usado en botones del Lobby
    /// </summary>
    public void LoadCourse(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError($"[SimpleLobbyLoader] ❌ {gameObject.name}: targetSceneName está vacío!");
            return;
        }

        Debug.Log($"[SimpleLobbyLoader] 📂 Cargando curso: {sceneName}");
        
        // Cargar la escena (Replace = reemplaza todo, descarga Lobby automáticamente)
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Regresa a Lobby desde un curso
    /// Usado en botones "Volver" dentro de los cursos
    /// </summary>
    public void ReturnToLobby()
    {
        Debug.Log($"[SimpleLobbyLoader] 🏠 Regresando a Lobby");
        
        // Cargar Lobby (descarga el curso automáticamente)
        SceneManager.LoadScene(lobbySceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Método estático para llamar desde código si lo necesitas
    /// Ejemplo: SimpleLobbyLoader.LoadCourseStatic("FireExtinguisherLesson1");
    /// </summary>
    public static void LoadCourseStatic(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("[SimpleLobbyLoader] ❌ sceneName está vacío!");
            return;
        }

        Debug.Log($"[SimpleLobbyLoader] 📂 [STATIC] Cargando curso: {sceneName}");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Método estático para regresar a Lobby desde código
    /// Ejemplo: SimpleLobbyLoader.ReturnToLobbyStatic();
    /// </summary>
    public static void ReturnToLobbyStatic()
    {
        Debug.Log("[SimpleLobbyLoader] 🏠 [STATIC] Regresando a Lobby");
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
