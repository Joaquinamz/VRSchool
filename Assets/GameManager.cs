using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // Estado del juego
    public string currentCourse = "FireExtinguisher";
    public string currentDifficulty = "A"; // A, B, C
    public int totalScore = 0;
    public float totalTime = 0f;
    
    // Control de fase
    public bool introductionComplete = false;
    public bool firstFireComplete = false;
    public bool multipleFiresComplete = false;
    public bool gameComplete = false;
    
    // Puntuación detallada
    public int fireExtinguishedCount = 0;
    public float firstFireTime = 0f;
    public float multipleFiresTime = 0f;
    
    // Compatibilidad con scripts antiguos
    public string selectedCourse = "";
    public string selectedDifficulty = "";
    public int score = 0;
    public float time = 0f;
    
    void Awake()
    {
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
    
    public void ResetForNewGame()
    {
        introductionComplete = false;
        firstFireComplete = false;
        multipleFiresComplete = false;
        gameComplete = false;
        fireExtinguishedCount = 0;
        firstFireTime = 0f;
        multipleFiresTime = 0f;
        totalScore = 0;
        totalTime = 0f;
    }
    
    public void CalculateScore()
    {
        // Fórmula de puntuación
        // Base: 100 puntos por fuego apagado
        // Bonificación por velocidad: -0.5 puntos por segundo
        // Dificultad multiplier: A=1x, B=1.5x, C=2x
        
        int baseScore = fireExtinguishedCount * 100;
        float timeDeduction = totalTime * 0.5f;
        float difficultyMultiplier = currentDifficulty == "A" ? 1f : 
                                     currentDifficulty == "B" ? 1.5f : 2f;
        
        totalScore = (int)((baseScore - timeDeduction) * difficultyMultiplier);
        if (totalScore < 0) totalScore = 0;
        
        // Compatibilidad
        score = totalScore;
        time = totalTime;
        
        Debug.Log($"✅ PUNTUACIÓN CALCULADA: {totalScore} puntos");
    }
}