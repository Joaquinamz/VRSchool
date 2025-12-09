using UnityEngine;

/// <summary>
/// Script de DEBUG para verificar que el sistema está funcionando.
/// Adjúntalo al Canvas para testear con teclas.
/// </summary>
public class SceneLoaderDebug : MonoBehaviour
{
    void Update()
    {
        // Presiona E para cargar FireExtinguisherLesson1
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("[DEBUG] Presionaste E - Cargando FireExtinguisherLesson1");
            SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1");
        }

        // Presiona A para cargar EarthQuakeLesson1
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("[DEBUG] Presionaste A - Cargando EarthQuakeLesson1");
            SceneManagerVR.LoadScene_Static("EarthQuakeLesson1");
        }

        // Presiona R para volver a Lobby
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("[DEBUG] Presionaste R - Volviendo a Lobby");
            SceneManagerVR.ReturnToLobby_Static();
        }

        // Presiona L para ver qué SceneManagerVR existe
        if (Input.GetKeyDown(KeyCode.L))
        {
            var manager = FindFirstObjectByType<SceneManagerVR>();
            if (manager != null)
                Debug.Log("[DEBUG] ✓ SceneManagerVR encontrado en la escena");
            else
                Debug.LogError("[DEBUG] ❌ SceneManagerVR NO encontrado en la escena");
        }

        // Presiona B para listar todos los GameObjects de root
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("[DEBUG] GameObjects en escena:");
            var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in roots)
            {
                Debug.Log($"  • {root.name}");
            }
        }
    }
}
