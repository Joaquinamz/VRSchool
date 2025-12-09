using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Gestor de Lobby - Maneja los botones de lecciones.
/// Usa SceneManagerVR para carga/descarga de escenas.
/// Este script es OPCIONAL si usas SceneLoaderButton en cada botón.
/// </summary>
public class LobbyManager : MonoBehaviour
{
    public Button btnExtintorA;
    public Button btnExtintorB;
    public Button btnExtintorC;
    
    public Button btnSismoA;
    public Button btnSismoB;
    public Button btnSismoC;
    
    void Start()
    {
        // Extintor
        if (btnExtintorA != null) btnExtintorA.onClick.AddListener(() => LoadCourse("FireExtinguisherLesson1"));
        if (btnExtintorB != null) btnExtintorB.onClick.AddListener(() => LoadCourse("FireExtinguisherLesson2"));
        if (btnExtintorC != null) btnExtintorC.onClick.AddListener(() => LoadCourse("FireExtinguisherLesson3"));
        
        // Sismo (EarthQuake)
        if (btnSismoA != null) btnSismoA.onClick.AddListener(() => LoadCourse("EarthQuakeLesson1"));
        if (btnSismoB != null) btnSismoB.onClick.AddListener(() => LoadCourse("EarthQuakeLesson2"));
        if (btnSismoC != null) btnSismoC.onClick.AddListener(() => LoadCourse("EarthQuakeLesson3"));
    }
    
    void LoadCourse(string sceneName)
    {
        Debug.Log($"[LobbyManager] Cargando curso: {sceneName}");
        SceneManagerVR.LoadScene_Static(sceneName);
    }
}