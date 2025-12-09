using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Gestor de Juego para Lecciones de Extintor.
/// SIMPLE Y FUNCIONAL: El fuego spawnea cuando se llama CompleteIntroduction()
/// </summary>
public class FireGameManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private NPCProfessor professorController;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Transform fireSpawnPoint;
    [SerializeField] private TextMeshProUGUI uiTimer;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Canvas resultsCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    // Estado del juego
    public enum GamePhase { Introduction, FirstFire, Dialog_AfterFirstFire, MultipleFires, Results, Complete }
    private GamePhase currentPhase = GamePhase.Introduction;
    
    private float gameTimer = 0f;
    private bool gameActive = false;
    private List<GameObject> activeFireList = new List<GameObject>();
    private int totalFireCount = 0;
    
    void Start()
    {
        Debug.Log("[FireGameManager] Inicializado");
        
        if (professorController == null)
        {
            professorController = FindFirstObjectByType<NPCProfessor>();
            Debug.Log("[FireGameManager] NPCProfessor encontrado automáticamente");
        }
        
        if (resultsCanvas != null)
            resultsCanvas.gameObject.SetActive(false);
            
        // VALIDACIÓN CRÍTICA
        if (firePrefab == null)
        {
            Debug.LogError("[FireGameManager] ❌ CRÍTICO: firePrefab NO ESTÁ ASIGNADO en Inspector");
            if (statusText != null)
                statusText.text = "ERROR: Fire Prefab no asignado";
        }
        else
        {
            Debug.Log("[FireGameManager] ✓ firePrefab está asignado");
        }
    }
    
    void Update()
    {
        if (!gameActive) return;
        
        gameTimer += Time.deltaTime;
        UpdateUI();
        CheckPhaseCompletion();
    }
    
    void UpdateUI()
    {
        if (uiTimer != null)
            uiTimer.text = $"Tiempo: {gameTimer:F1}s";
    }
    
    void CheckPhaseCompletion()
    {
        if (currentPhase == GamePhase.FirstFire)
        {
            int remainingFires = CountActiveFires();
            if (remainingFires == 0)
            {
                Debug.Log("[FireGameManager] ✓ Fuego de práctica apagado");
                gameActive = false;
                CompleteFirstFirePhase();
            }
        }
        // MultipleFires es controlado por FireMinigameManager, no por este manager
    }
    
    int CountActiveFires()
    {
        // Eliminar fuegos nulos de la lista
        activeFireList.RemoveAll(f => f == null);
        
        // Contar fuegos que aún están activos (no apagados)
        int count = 0;
        foreach (GameObject fire in activeFireList)
        {
            if (fire != null)
            {
                FireBehavior fb = fire.GetComponent<FireBehavior>();
                if (fb != null && !fb.IsExtinguished())
                    count++;
            }
        }
        return count;
    }
    
    // Llamado si alguien lo necesita, pero no es requerido
    // El Update() verifica continuamente el estado de los fuegos
    
    public void StartIntroduction()
    {
        Debug.Log("[FireGameManager] 📖 Iniciando introducción");
        currentPhase = GamePhase.Introduction;
        gameActive = false;
        
        if (professorController != null)
            professorController.ShowIntroduction();
    }
    
    /// <summary>
    /// LLAMADO DESDE NPCProfessor cuando el usuario presiona "Continuar" en la introducción
    /// AQUÍ SPAWNEA EL FUEGO
    /// </summary>
    public void CompleteIntroduction()
    {
        Debug.Log("[FireGameManager] ✓ CompleteIntroduction() LLAMADO");
        
        // Validar prefab
        if (firePrefab == null)
        {
            Debug.LogError("[FireGameManager] ❌ firePrefab es NULL - NO SE PUEDE SPAWNEAR FUEGO");
            if (statusText != null)
                statusText.text = "ERROR: Fire Prefab NULL";
            return;
        }
        
        Debug.Log("[FireGameManager] 🔥 Spawneando fuego de práctica");
        
        currentPhase = GamePhase.FirstFire;
        gameActive = true;
        gameTimer = 0f;
        totalFireCount = 1;
        
        // SPAWNEAR FUEGO
        Vector3 spawnPos = fireSpawnPoint != null ? fireSpawnPoint.position : new Vector3(0, 1, 5);
        GameObject fire = Instantiate(firePrefab, spawnPos, Quaternion.identity);
        
        if (fire == null)
        {
            Debug.LogError("[FireGameManager] ❌ Instantiate retornó NULL");
            return;
        }
        
        Debug.Log("[FireGameManager] ✓ Fuego instanciado en posición: " + spawnPos);
        
        // Asegurar Rigidbody
        Rigidbody rb = fire.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            Debug.Log("[FireGameManager] ✓ Rigidbody configurado");
        }
        
        activeFireList.Clear();
        activeFireList.Add(fire);
        
        if (statusText != null)
            statusText.text = "Apaga el fuego con el extintor";
        
        Debug.Log("[FireGameManager] ✓✓✓ FUEGO LISTO - ESPERANDO QUE LO APAGUEN");
    }
    
    void CompleteFirstFirePhase()
    {
        Debug.Log("[FireGameManager] ✓ Fuego de práctica completado - mostrando siguiente diálogo");
        currentPhase = GamePhase.Dialog_AfterFirstFire;
        
        if (professorController != null)
        {
            Debug.Log("[FireGameManager] ✓ Llamando a ShowPostFirstFireDialogue()");
            professorController.ShowPostFirstFireDialogue();
        }
        else
        {
            Debug.LogError("[FireGameManager] ❌ professorController es NULL");
        }
    }
    
    public void CompletePostFireDialogue()
    {
        Debug.Log("[FireGameManager] ✓✓✓ CompletePostFireDialogue LLAMADO - Iniciando minijuego");
        
        // Buscar FireMinigameManager en la escena
        FireMinigameManager minigameManager = FindFirstObjectByType<FireMinigameManager>();
        
        if (minigameManager != null)
        {
            Debug.Log("[FireGameManager] ✓ FireMinigameManager encontrado - delegando al minijuego");
            minigameManager.StartMultipleFires();
        }
        else
        {
            Debug.LogError("[FireGameManager] ❌ FireMinigameManager NO ENCONTRADO en escena");
        }
    }

    
    void ShowResults()
    {
        Debug.Log("[FireGameManager] Mostrando resultados - Tiempo total: " + gameTimer);
        
        if (resultsCanvas != null)
        {
            resultsCanvas.gameObject.SetActive(true);
            
            if (scoreText != null)
                scoreText.text = $"Tiempo: {gameTimer:F1}s";
        }
        
        currentPhase = GamePhase.Complete;
    }
}
