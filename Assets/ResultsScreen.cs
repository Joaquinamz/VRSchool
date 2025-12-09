using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Muestra resultados del minijuego y permite reintentar o volver al lobby
/// </summary>
public class ResultsScreen : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Canvas resultsCanvas;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button lobbyButton;

    private CourseResults currentResults;
    private bool isShowing = false;

    private void Start()
    {
        // Buscar referencias si no están asignadas
        if (resultsCanvas == null)
        {
            resultsCanvas = GetComponentInChildren<Canvas>();
        }

        // Configurar botones
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryPressed);
        }

        if (lobbyButton != null)
        {
            lobbyButton.onClick.AddListener(OnLobbyPressed);
        }

        // Ocultar al inicio
        if (resultsCanvas != null)
        {
            resultsCanvas.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Mostrar resultados del minijuego
    /// </summary>
    public void DisplayResults(CourseResults results)
    {
        currentResults = results;
        isShowing = true;

        if (resultsCanvas != null)
        {
            resultsCanvas.gameObject.SetActive(true);
        }

        // Mostrar información
        if (titleText != null)
        {
            titleText.text = results.isPassed ? "¡ÉXITO!" : "TIEMPO LÍMITE ALCANZADO";
            titleText.color = results.isPassed ? Color.green : Color.red;
        }

        if (scoreText != null)
        {
            scoreText.text = $"Puntuación: <b>{results.score}</b>";
        }

        if (timeText != null)
        {
            timeText.text = $"Tiempo: {results.timeElapsed:F1}s";
        }

        if (statsText != null)
        {
            statsText.text = $"Éxitos: {results.successCount}\nErrores: {results.failureCount}";
        }

        Debug.Log($"📊 Resultados mostrados. Puntuación: {results.score}");
    }

    /// <summary>
    /// Presionó "Reintentar"
    /// </summary>
    private void OnRetryPressed()
    {
        if (!isShowing) return;

        Debug.Log("🔄 Reiniciando módulo");

        if (resultsCanvas != null)
        {
            resultsCanvas.gameObject.SetActive(false);
        }

        if (CourseManager.Instance != null)
        {
            CourseManager.Instance.RetryModule();
        }

        isShowing = false;
    }

    /// <summary>
    /// Presionó "Volver al Lobby"
    /// </summary>
    private void OnLobbyPressed()
    {
        if (!isShowing) return;

        Debug.Log("🏠 Volviendo al Lobby");

        if (resultsCanvas != null)
        {
            resultsCanvas.gameObject.SetActive(false);
        }

        if (CourseManager.Instance != null)
        {
            CourseManager.Instance.ReturnToLobby();
        }

        isShowing = false;
    }

    /// <summary>
    /// Mostrar pantalla de celebración de completación (opcional)
    /// </summary>
    public void ShowCompletionCelebration()
    {
        if (resultsCanvas != null)
        {
            resultsCanvas.gameObject.SetActive(true);
        }

        if (titleText != null)
        {
            titleText.text = "¡EXCELENTE!";
            titleText.color = Color.yellow;
        }

        if (scoreText != null)
        {
            scoreText.text = "¡COMPLETASTE ESTE MÓDULO!";
        }

        if (statsText != null)
        {
            statsText.text = "Puedes volver al Lobby y elegir otro módulo\no reintentar éste para mejorar tu puntuación.";
        }

        Debug.Log("🎉 Pantalla de celebración mostrada");
    }

    private void OnDestroy()
    {
        if (retryButton != null)
            retryButton.onClick.RemoveListener(OnRetryPressed);

        if (lobbyButton != null)
            lobbyButton.onClick.RemoveListener(OnLobbyPressed);
    }
}
