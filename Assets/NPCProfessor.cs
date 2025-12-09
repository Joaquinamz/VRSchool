using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NPCProfessor : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public FireGameManager gameController;
    
    private string[] currentDialogues;
    private int currentLineIndex = 0;
    
    private enum DialogueType { Introduction, PostFirstFire, PostMultipleFires }
    private DialogueType currentDialogueType = DialogueType.Introduction;
    
    void Start()
    {
        // Buscar FireGameManager en la escena si no está asignado
        if (gameController == null)
            gameController = FindFirstObjectByType<FireGameManager>();
        
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextClicked);
        
        // Iniciar diálogos de extintor automáticamente
        StartCoroutine(ShowIntroductionAfterDelay());
    }
    
    IEnumerator ShowIntroductionAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        ShowIntroduction();
    }
    
    public void ShowIntroduction()
    {
        currentDialogueType = DialogueType.Introduction;
        
        Debug.Log("[NPCProfessor] Mostrando diálogo de introducción - EXTINTOR");
        
        // Diálogos específicos para extintor (sin detectar curso)
        currentDialogues = new string[]
        {
            "Hola estudiantes, hoy aprenderemos a usar un extintor",
            "Es muy importante saber cómo actuar en caso de incendio",
            "Vamos a practicar: Aquí hay un fuego pequeño",
            "Intenta apagarlo usando el extintor",
            "¡Presiona siguiente cuando estés listo!"
        };
        
        currentLineIndex = 0;
        ShowNextLine();
    }
    
    void OnNextClicked()
    {
        if (currentLineIndex < currentDialogues.Length - 1)
        {
            currentLineIndex++;
            ShowNextLine();
        }
        else
        {
            // Fin del diálogo actual
            if (currentDialogueType == DialogueType.Introduction)
            {
                EndIntroduction();
            }
            else if (currentDialogueType == DialogueType.PostFirstFire)
            {
                OnPostFirstFireDialogueComplete();
            }
            else if (currentDialogueType == DialogueType.PostMultipleFires)
            {
                OnPostMultipleFiresDialogueComplete();
            }
        }
    }
    
    void ShowNextLine()
    {
        if (dialogueText != null)
            dialogueText.text = currentDialogues[currentLineIndex];
    }
    
    void EndIntroduction()
    {
        Debug.Log("[NPCProfessor] Diálogo de introducción completado - iniciando primer fuego");
        
        dialogueText.text = "¡El juego comienza!";
        
        if (gameController != null)
        {
            gameController.CompleteIntroduction();
        }
    }
    
    public void ShowPostFirstFireDialogue()
    {
        currentDialogueType = DialogueType.PostFirstFire;
        
        Debug.Log("[NPCProfessor] Mostrando diálogo post-primer-fuego");
        
        currentDialogues = new string[]
        {
            "¡Excelente! Apagaste el fuego correctamente",
            "Ese era un fuego pequeño de entrenamiento",
            "Ahora vamos a practicar con múltiples fuegos",
            "Este será el desafío final del curso",
            "¡Presiona siguiente cuando estés listo!",
            "¡Tú puedes!"
        };
        
        currentLineIndex = 0;
        ShowNextLine();
    }
    
    public void OnPostFirstFireDialogueComplete()
    {
        Debug.Log("[NPCProfessor] Diálogo post-primer-fuego completado");
        
        if (gameController != null)
        {
            gameController.CompletePostFireDialogue();
        }
    }
    
    public void ShowPostMultipleFiresDialogue()
    {
        currentDialogueType = DialogueType.PostMultipleFires;
        
        Debug.Log("[NPCProfessor] Mostrando diálogo post-múltiples-fuegos");
        
        currentDialogues = new string[]
        {
            "¡Felicidades! Completaste todos los fuegos",
            "Demostraste gran dominio del extintor",
            "Aprendiste cuándo y cómo actuar",
            "Estás listo para emergencias reales",
            "¡Excelente trabajo!"
        };
        
        currentLineIndex = 0;
        ShowNextLine();
    }
    
    public void OnPostMultipleFiresDialogueComplete()
    {
        Debug.Log("[NPCProfessor] Diálogo post-múltiples-fuegos completado");
        
        // Encontrar FireMinigameManager y mostrar resultados
        System.Type minigameType = System.Type.GetType("FireMinigameManager");
        if (minigameType != null)
        {
            var minigameObject = FindAnyObjectByType(minigameType);
            if (minigameObject != null)
            {
                var showResultsMethod = minigameType.GetMethod("ShowResults");
                if (showResultsMethod != null)
                {
                    showResultsMethod.Invoke(minigameObject, null);
                }
            }
        }
    }
}

