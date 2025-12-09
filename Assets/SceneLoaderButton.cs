using UnityEngine;

/// <summary>
/// Script simplificado para asignar a botones.
/// Usa el SceneManagerVR para cargar/descargar escenas.
/// </summary>
public class SceneLoaderButton : MonoBehaviour
{
    // Enum público para que sea visible en Inspector
    public enum LoadMode
    {
        Replace,    // Reemplaza la escena actual (usado en Lobby)
        Additive,   // Carga aditiva (usado si quieres mantener Lobby)
        ReturnLobby // Regresa a Lobby
    }

    [Header("Tipo de Carga")]
    [SerializeField] public LoadMode loadMode = LoadMode.Replace;
    [SerializeField] public string targetSceneName = "";

    void Start()
    {
        // Verificar que SceneManagerVR existe, si no, crear uno
        if (FindFirstObjectByType<SceneManagerVR>() == null)
        {
            Debug.LogError("[SceneLoaderButton] ❌ SceneManagerVR NO ENCONTRADO en la escena!");
            Debug.LogError("[SceneLoaderButton] Por favor agrega 'SceneManager' GameObject con SceneManagerVR componente");
            return;
        }

        // Validación de configuración
        if (targetSceneName == "" && loadMode != LoadMode.ReturnLobby)
        {
            Debug.LogWarning($"[SceneLoaderButton] ⚠️ {gameObject.name} no tiene escena asignada");
        }
        
        Debug.Log($"[SceneLoaderButton] ✓ {gameObject.name} listo (Modo: {loadMode}, Escena: {targetSceneName})");
    }

    /// <summary>
    /// Llamado por el evento OnClick del botón
    /// </summary>
    public void OnButtonPressed()
    {
        Debug.Log($"[SceneLoaderButton] 🔘 Botón presionado: {gameObject.name}");
        
        // Validar que SceneManagerVR existe
        if (FindFirstObjectByType<SceneManagerVR>() == null)
        {
            Debug.LogError("[SceneLoaderButton] ❌ SceneManagerVR NO existe! Crea un GameObject 'SceneManager' con SceneManagerVR");
            return;
        }

        switch (loadMode)
        {
            case LoadMode.Replace:
                if (string.IsNullOrEmpty(targetSceneName))
                {
                    Debug.LogError($"[SceneLoaderButton] ❌ {gameObject.name}: Target Scene Name está vacío!");
                    return;
                }
                Debug.Log($"[SceneLoaderButton] 📂 Cargando (Replace): {targetSceneName}");
                SceneManagerVR.LoadScene_Static(targetSceneName);
                break;

            case LoadMode.Additive:
                if (string.IsNullOrEmpty(targetSceneName))
                {
                    Debug.LogError($"[SceneLoaderButton] ❌ {gameObject.name}: Target Scene Name está vacío!");
                    return;
                }
                Debug.Log($"[SceneLoaderButton] 📂 Cargando (Aditivo): {targetSceneName}");
                SceneManagerVR.LoadSceneAdditive_Static(targetSceneName);
                break;

            case LoadMode.ReturnLobby:
                Debug.Log($"[SceneLoaderButton] 🏠 Regresando a Lobby");
                SceneManagerVR.ReturnToLobby_Static();
                break;

            default:
                Debug.LogError($"[SceneLoaderButton] ❌ LoadMode desconocido: {loadMode}");
                break;
        }
    }
}
