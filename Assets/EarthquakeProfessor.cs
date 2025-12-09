using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Diálogos y control del profesor para lecciones de Terremoto.
/// Similar a NPCProfessor pero para escenarios de terremoto.
/// </summary>
public class EarthquakeProfessor : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Canvas resultsCanvas;
    [SerializeField] private TextMeshProUGUI resultsFeedback;
    
    [Header("Referencias")]
    [SerializeField] private EarthquakeGameManager gameController;
    
    private enum DialoguePhase
    {
        Introduction,
        PostEarthquake,
        Results
    }
    
    private DialoguePhase currentDialoguePhase = DialoguePhase.Introduction;
    private string[] introductionDialogues = new string[]
    {
        "¡Hola! Ahora practicaremos qué hacer durante un terremoto.",
        "Lo MÁS IMPORTANTE es PROTEGERSE debajo de una mesa o escritorio.",
        "Cuando empiece el terremoto, busca la mesa más cercana y cúbrete.",
        "Los escombros caerán desde el techo. Evita los impactos.",
        "El terremoto durará 30 segundos. ¡Aguanta allá abajo!",
        "Presiona 'Continuar' cuando estés listo."
    };
    
    private string[] postEarthquakeDialogues = new string[]
    {
        "¡Bien hecho! Sobreviviste al terremoto.",
        "Ahora vamos a ver los resultados."
    };
    
    private int currentLineIndex = 0;
    
    void Start()
    {
        // Auto-encontrar GameController si no está asignado
        if (gameController == null)
        {
            gameController = FindFirstObjectByType<EarthquakeGameManager>();
            if (gameController != null)
                Debug.Log("[EarthquakeProfessor] ✓ GameController encontrado automáticamente");
        }
        
        // Configurar botón
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextClicked);
        }
        
        // Ocultar canvas de resultados
        if (resultsCanvas != null)
            resultsCanvas.gameObject.SetActive(false);
    }
    
    public void ShowIntroduction()
    {
        currentDialoguePhase = DialoguePhase.Introduction;
        currentLineIndex = 0;
        
        if (dialogueText == null)
            return;
        
        Debug.Log("[EarthquakeProfessor] 📖 Mostrando introducción");
        ShowNextLine();
    }
    
    void OnNextClicked()
    {
        if (currentDialoguePhase == DialoguePhase.Introduction)
        {
            currentLineIndex++;
            
            // ¿Última línea de introducción?
            if (currentLineIndex >= introductionDialogues.Length)
            {
                Debug.Log("[EarthquakeProfessor] ✓ Introducción completada");
                
                if (gameController != null)
                    gameController.CompleteIntroduction();
                
                return;
            }
            
            ShowNextLine();
        }
        else if (currentDialoguePhase == DialoguePhase.PostEarthquake)
        {
            currentLineIndex++;
            
            if (currentLineIndex >= postEarthquakeDialogues.Length)
            {
                Debug.Log("[EarthquakeProfessor] ✓ Diálogo post-terremoto completado");
                // Aquí podrías llamar al siguiente nivel o permitir reintentar
                return;
            }
            
            ShowNextLine();
        }
    }
    
    void ShowNextLine()
    {
        string[] currentDialogues = (currentDialoguePhase == DialoguePhase.Introduction) 
            ? introductionDialogues 
            : postEarthquakeDialogues;
        
        if (currentLineIndex < currentDialogues.Length && dialogueText != null)
        {
            dialogueText.text = currentDialogues[currentLineIndex];
        }
    }
    
    public void ShowResults(int totalHits, float finalScore)
    {
        currentDialoguePhase = DialoguePhase.Results;
        currentLineIndex = 0;
        
        // Mostrar canvas de resultados
        if (resultsCanvas != null)
        {
            resultsCanvas.gameObject.SetActive(true);
        }
        
        // Generar feedback basado en desempeño
        string feedback = GenerateFeedback(totalHits, finalScore);
        
        if (resultsFeedback != null)
        {
            resultsFeedback.text = $"Impactos: {totalHits}\nPuntaje: {finalScore:F0}/100\n\n{feedback}";
        }
        
        Debug.Log("[EarthquakeProfessor] 📊 Mostrando resultados");
    }
    
    string GenerateFeedback(int hits, float score)
    {
        if (hits == 0)
            return "¡PERFECTO! No recibiste ningún impacto. ¡Excelente protección!";
        else if (hits <= 2)
            return "¡Muy bien! Solo recibiste pocos impactos. Buen trabajo.";
        else if (hits <= 5)
            return "Bien, pero podrías mejorar. Busca cobertura más rápido.";
        else
            return "Necesitas practicar más. Protégete mejor de los escombros.";
    }
}
