using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

/// <summary>
/// Controla al profesor: animaciones, diálogos y progresión de instrucciones
/// </summary>
public class InstructorController : MonoBehaviour
{
    [Header("Referencias del Profesor")]
    [SerializeField] private Animator profesorAnimator; // Si tienes animaciones
    [SerializeField] private Transform profesorTransform;

    [Header("UI de Diálogos")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Button nextButton; // Botón "Siguiente"
    [SerializeField] private TextMeshProUGUI nextButtonText;

    [Header("Datos de Diálogos")]
    [SerializeField] private List<string> fireDialogues = new List<string>();
    [SerializeField] private List<string> earthquakeDialogues = new List<string>();

    private List<string> currentDialogues;
    private int currentDialogueIndex = 0;
    private CourseManager.ModuleType currentModule;
    private bool isDialogueActive = false;

    private void Start()
    {
        // Buscar referencias si no están asignadas
        if (nextButton == null)
        {
            nextButton = FindFirstObjectByType<Button>();
        }

        if (dialogueText == null)
        {
            dialogueText = FindFirstObjectByType<TextMeshProUGUI>();
        }

        // Configurar botón
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextButtonPressed);
        }

        // Cargar diálogos predeterminados si no están asignados
        if (fireDialogues.Count == 0)
        {
            LoadDefaultDialogues();
        }
    }

    /// <summary>
    /// Cargar diálogos predeterminados
    /// </summary>
    private void LoadDefaultDialogues()
    {
        fireDialogues = new List<string>
        {
            "¡Hola estudiantes! Hoy aprenderemos a usar un extintor de incendios.",
            "Un extintor es un dispositivo de seguridad para combatir fuegos pequeños.",
            "Los pasos básicos son: 1) JALAR la argolla, 2) APUNTAR a la base del fuego, 3) PRESIONAR la manija.",
            "Es importante dirigir el chorro hacia la base del fuego, no hacia las llamas.",
            "¡Ahora vamos a practicar! Aquí hay un pequeño fuego. ¿Puedes apagarlo?",
            "¡Excelente trabajo! Ahora la prueba final: deberás apagar múltiples fuegos esparcidos por la sala.",
            "Tienes un tiempo límite. Intenta apagar todos los fuegos lo más rápido posible.",
            "¿Listo? ¡Adelante!"
        };

        earthquakeDialogues = new List<string>
        {
            "¡Hola de nuevo! Hoy aprenderemos sobre seguridad ante sismos.",
            "Cuando sientas un terremoto, lo primero es mantener la CALMA.",
            "La estrategia es: AGACHARSE, CUBRIRSE, y SUJETARSE firmemente.",
            "Busca un lugar seguro: bajo una mesa sólida o contra una pared de carga.",
            "Cubre tu cabeza y cuello con los brazos para protegerte de escombros.",
            "¡ATENCIÓN! Está a punto de comenzar un terremoto simulado.",
            "Deberás esconderte bajo las mesas. ¡Cuando termine, sal ordenadamente con tus compañeros!",
            "Recuerda: no corras, no grites, mantén el orden. ¿Listo? ¡Aquí viene!"
        };
    }

    /// <summary>
    /// Iniciar el diálogo del módulo especificado
    /// </summary>
    public void StartDialogue(CourseManager.ModuleType moduleType)
    {
        currentModule = moduleType;
        currentDialogueIndex = 0;
        isDialogueActive = true;

        // Seleccionar lista de diálogos
        if (moduleType == CourseManager.ModuleType.FireExtinguisher)
        {
            currentDialogues = fireDialogues;
            Debug.Log("🔥 Iniciando diálogos de EXTINTOR");
        }
        else
        {
            currentDialogues = earthquakeDialogues;
            Debug.Log("🌍 Iniciando diálogos de SISMO");
        }

        // Mostrar primer diálogo
        ShowCurrentDialogue();
    }

    /// <summary>
    /// Mostrar el diálogo actual
    /// </summary>
    private void ShowCurrentDialogue()
    {
        if (currentDialogueIndex >= currentDialogues.Count)
        {
            CompleteDialogue();
            return;
        }

        // Mostrar texto
        if (dialogueText != null)
        {
            dialogueText.text = currentDialogues[currentDialogueIndex];
        }

        // Actualizar botón
        if (nextButton != null)
        {
            if (currentDialogueIndex < currentDialogues.Count - 1)
            {
                nextButtonText.text = "Siguiente";
            }
            else
            {
                nextButtonText.text = "¡Empezar!";
            }
        }

        // Animar profesor (opcional)
        if (profesorAnimator != null)
        {
            profesorAnimator.SetTrigger("Talk");
        }

        Debug.Log($"📖 Diálogo {currentDialogueIndex + 1}/{currentDialogues.Count}");
    }

    /// <summary>
    /// Presionó el botón "Siguiente"
    /// </summary>
    private void OnNextButtonPressed()
    {
        if (!isDialogueActive) return;

        currentDialogueIndex++;

        // Si llegamos al final, iniciar el minijuego
        if (currentDialogueIndex >= currentDialogues.Count)
        {
            CompleteDialogue();
        }
        else
        {
            ShowCurrentDialogue();
        }
    }

    /// <summary>
    /// Diálogos completados - notificar al CourseManager para iniciar minijuego
    /// </summary>
    private void CompleteDialogue()
    {
        isDialogueActive = false;
        Debug.Log("✅ Diálogos completados. Iniciando minijuego...");

        if (dialogueText != null)
        {
            dialogueText.text = "...";
        }

        // Notificar al CourseManager
        if (CourseManager.Instance != null)
        {
            CourseManager.Instance.StartGamePhase();
        }
    }

    /// <summary>
    /// Agregar diálogos personalizados en runtime (para futuras expansiones)
    /// </summary>
    public void SetCustomDialogues(List<string> dialogues)
    {
        currentDialogues = new List<string>(dialogues);
    }

    public int GetCurrentDialogueIndex() => currentDialogueIndex;
    public int GetTotalDialogues() => currentDialogues.Count;
}
