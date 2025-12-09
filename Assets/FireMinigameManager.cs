using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FireMinigameManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private NPCProfessor professorController;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private TextMeshProUGUI uiFiresRemaining;
    [SerializeField] private TextMeshProUGUI uiTimer;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Canvas minigameCanvas; // NUEVO: Canvas del minijuego
    [SerializeField] private Canvas resultsCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI timeText;  // NUEVO: Para mostrar tiempo en resultados
    
    [Header("Configuración de Fuegos Múltiples")]
    [SerializeField] private Transform[] fireSpawnPoints = new Transform[3]; // Array de posiciones para los fuegos
    
    private float gameTimer = 0f;
    private bool gameActive = false;
    private List<GameObject> activeFireList = new List<GameObject>();
    private int totalFireCount = 0;
    private int firesExtinguished = 0;
    
    void Start()
    {
        Debug.Log("[FireMinigameManager] Inicializado - Minijuego de múltiples fuegos");
        
        if (professorController == null)
            professorController = FindFirstObjectByType<NPCProfessor>();
        
        // NUEVO: Desactivar canvas al inicio
        if (minigameCanvas != null)
            minigameCanvas.gameObject.SetActive(false);
        
        if (resultsCanvas != null)
            resultsCanvas.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (!gameActive) return;
        
        gameTimer += Time.deltaTime;
        UpdateUI();
        CheckAllFiresExtinguished();
    }
    
    void UpdateUI()
    {
        if (uiTimer != null)
            uiTimer.text = $"Tiempo: {gameTimer:F1}s";
        
        int remainingFires = CountActiveFires();
        if (uiFiresRemaining != null)
            uiFiresRemaining.text = $"Fuegos: {remainingFires}/{totalFireCount}";
        
        Debug.Log($"[FireMinigameManager] UI Actualizada - Fuegos activos: {remainingFires}/{totalFireCount}");
    }
    
    void CheckAllFiresExtinguished()
    {
        int activeFires = CountActiveFires();
        
        if (activeFires == 0 && totalFireCount > 0)
        {
            Debug.Log("[FireMinigameManager] ¡TODOS LOS FUEGOS APAGADOS!");
            gameActive = false;
            CompleteMultipleFires();
        }
    }
    
    public void StartMultipleFires()
    {
        Debug.Log("[FireMinigameManager] Iniciando MINIJUEGO de múltiples fuegos");
        
        // NUEVO: Activar canvas del minijuego
        if (minigameCanvas != null)
        {
            minigameCanvas.gameObject.SetActive(true);
            Debug.Log("[FireMinigameManager] ✓ Canvas del minijuego activado");
        }
        else
        {
            Debug.LogWarning("[FireMinigameManager] ⚠️ minigameCanvas no asignado en Inspector");
        }
        
        // Contar cuántos spawn points están asignados
        int fireCount = 0;
        foreach (Transform spawnPoint in fireSpawnPoints)
        {
            if (spawnPoint != null)
                fireCount++;
        }
        
        if (fireCount == 0)
        {
            Debug.LogError("[FireMinigameManager] ❌ No hay spawn points asignados. Asigna al menos uno en el Inspector.");
            return;
        }
        
        gameActive = true;
        gameTimer = 0f;
        totalFireCount = fireCount;
        firesExtinguished = 0;
        activeFireList.Clear();
        
        Debug.Log($"[FireMinigameManager] Spawneando {fireCount} fuegos en posiciones designadas");
        
        // Spawnear fuegos en las posiciones asignadas
        for (int i = 0; i < fireSpawnPoints.Length; i++)
        {
            if (fireSpawnPoints[i] != null)
            {
                Vector3 spawnPos = fireSpawnPoints[i].position;
                GameObject fire = Instantiate(firePrefab, spawnPos, Quaternion.identity);
                fire.name = $"Fire_{i}"; // NUEVO: Nombrar para debug
                
                // Asegurar que el fuego no se cae
                Rigidbody rb = fire.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                
                // NUEVO: Resetear intensidad
                FireBehavior fb = fire.GetComponent<FireBehavior>();
                if (fb != null)
                {
                    fb.currentIntensity = 100f;
                    Debug.Log($"[FireMinigameManager] ✓ Fire_{i} spawneado en {spawnPos} (Intensidad: {fb.currentIntensity})");
                }
                
                activeFireList.Add(fire);
            }
        }
        
        if (statusText != null)
            statusText.text = $"Apaga todos los {totalFireCount} fuegos con el extintor";
    }
    
    void CompleteMultipleFires()
    {
        firesExtinguished = totalFireCount;
        
        Debug.Log("[FireMinigameManager] Todos los fuegos apagados - minijuego completado");
        
        // NUEVO: Desactivar canvas de minijuego
        if (minigameCanvas != null)
            minigameCanvas.gameObject.SetActive(false);
        
        if (professorController != null)
            professorController.ShowPostMultipleFiresDialogue();
    }
    
    public void ShowResults()
    {
        Debug.Log($"[FireMinigameManager] Mostrando resultados - Total tiempo: {gameTimer:F1}s, Fuegos apagados: {firesExtinguished}");
        
        int score = CalculateScore();
        
        // NUEVO: Activar canvas de resultados
        if (resultsCanvas != null)
        {
            resultsCanvas.gameObject.SetActive(true);
            Debug.Log("[FireMinigameManager] ✓ Canvas de resultados activado");
            
            if (scoreText != null)
                scoreText.text = $"Puntuación: {score}";
            
            // NUEVO: Mostrar tiempo en resultados
            if (timeText != null)
                timeText.text = $"Tiempo: {gameTimer:F1}s";
            
            if (feedbackText != null)
            {
                if (score >= 300)
                    feedbackText.text = "¡Excelente! Completaste la lección perfectamente.";
                else if (score >= 200)
                    feedbackText.text = "¡Bueno! Completaste la lección correctamente.";
                else if (score >= 100)
                    feedbackText.text = "Aceptable. Practica más para mejorar tu desempeño.";
                else
                    feedbackText.text = "Necesitas practicar más. ¡Inténtalo de nuevo!";
            }
        }
        else
        {
            Debug.LogError("[FireMinigameManager] ❌ resultsCanvas no asignado en Inspector");
        }
    }
    
    int CountActiveFires()
    {
        int count = 0;
        
        // Limpiar fuegos nulos (destruidos)
        activeFireList.RemoveAll(fire => fire == null);
        
        foreach (GameObject fire in activeFireList)
        {
            if (fire != null)
            {
                FireBehavior fb = fire.GetComponent<FireBehavior>();
                if (fb != null && fb.currentIntensity > 0)
                {
                    count++;
                }
                else if (fb != null && fb.currentIntensity <= 0)
                {
                    Debug.Log($"[FireMinigameManager] Fuego {fire.name} está apagado (Intensidad: {fb.currentIntensity})");
                }
            }
        }
        
        return count;
    }
    
    int CalculateScore()
    {
        // Fórmula: (Fuegos apagados × 100) - (Tiempo × 0.5)
        int baseScore = firesExtinguished * 100;
        int timePenalty = Mathf.Max(0, (int)(gameTimer * 0.5f));
        int finalScore = baseScore - timePenalty;
        
        return Mathf.Max(0, finalScore);
    }
}
