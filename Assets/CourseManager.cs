using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestor central del curso - Singleton que persiste entre escenas
/// Maneja: transiciones de escenas, dificultad, estado del curso
/// </summary>
public class CourseManager : MonoBehaviour
{
    public enum ModuleType
    {
        FireExtinguisher,  // Extintor
        Earthquake         // Sismo
    }

    public enum CourseState
    {
        AtLobby,           // En el lobby
        Instruction,       // Viendo instrucciones del profesor
        PreGame,           // Antes de empezar minijuego
        InGame,            // Durante el minijuego
        PostGame,          // Después del minijuego
        LoadingScene       // Cargando una escena
    }

    public enum Difficulty
    {
        A,      // Fácil
        B,      // Normal
        C,      // Difícil
        Random  // Aleatorio
    }

    // Singleton instance
    public static CourseManager Instance { get; private set; }

    [Header("Estado Actual")]
    public ModuleType selectedModule;
    public Difficulty selectedDifficulty = Difficulty.B;
    public CourseState currentState = CourseState.AtLobby;

    // Events
    public event Action<ModuleType> OnModuleSelected;
    public event Action<CourseState> OnStateChanged;
    public event Action OnGameStarted;
    public event Action OnGameCompleted;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.Log("CourseManager inicializado");
    }

    /// <summary>
    /// Usuario selecciona un módulo con una dificultad específica
    /// </summary>
    public void SelectModule(ModuleType module, Difficulty difficulty)
    {
        Debug.Log($"Módulo seleccionado: {module}, Dificultad: {difficulty}");
        
        selectedModule = module;
        selectedDifficulty = difficulty;
        
        OnModuleSelected?.Invoke(module);
        
        // Cargar la escena correspondiente
        LoadModuleScene(module);
    }

    /// <summary>
    /// Carga la escena del módulo seleccionado
    /// </summary>
    private void LoadModuleScene(ModuleType module)
    {
        ChangeState(CourseState.LoadingScene);
        
        string sceneName = module == ModuleType.FireExtinguisher 
            ? "FireExtinguisherLesson" 
            : "EarthquakeLesson";
        
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Cambia el estado actual del curso
    /// </summary>
    public void ChangeState(CourseState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        Debug.Log($"Estado cambiado a: {newState}");
        OnStateChanged?.Invoke(newState);
    }

    /// <summary>
    /// Notifica que el minijuego comenzó
    /// </summary>
    public void StartGamePhase()
    {
        ChangeState(CourseState.InGame);
        OnGameStarted?.Invoke();
    }

    /// <summary>
    /// Notifica que el minijuego terminó
    /// </summary>
    public void CompleteGamePhase(CourseResults results)
    {
        ChangeState(CourseState.PostGame);
        OnGameCompleted?.Invoke();
    }

    /// <summary>
    /// Reintentar el módulo actual con la misma dificultad
    /// </summary>
    public void RetryModule()
    {
        Debug.Log($"Reintentando módulo: {selectedModule}");
        SelectModule(selectedModule, selectedDifficulty);
    }

    /// <summary>
    /// Volver al Lobby
    /// </summary>
    public void ReturnToLobby()
    {
        Debug.Log("Volviendo al Lobby");
        ChangeState(CourseState.AtLobby);
        selectedDifficulty = Difficulty.B; // Reset a dificultad por defecto
        SceneManager.LoadScene("LobbyVR");
    }
}
