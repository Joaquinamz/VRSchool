using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Script para arreglar problemas comunes con botones UI TextMeshPro
/// Asegura que todo esté configurado correctamente para la interacción
/// </summary>
public class UIButtonFixer : MonoBehaviour
{
    void Awake()
    {
        // Arreglar el Canvas y su EventSystem
        FixCanvasAndEventSystem();
        
        // Arreglar todos los botones en la escena
        FixAllButtons();
    }
    
    void FixCanvasAndEventSystem()
    {
        // Buscar el Canvas
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogWarning("⚠️ No hay Canvas en la escena");
            return;
        }
        
        // ARREGLAR 1: Agregar GraphicRaycaster si no existe
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            raycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
            Debug.Log("✅ GraphicRaycaster agregado al Canvas");
        }
        
        // ARREGLAR 2: Asegurar que el Canvas tiene CanvasGroup
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = true;
            Debug.Log("✅ CanvasGroup agregado al Canvas");
        }
        else
        {
            canvasGroup.blocksRaycasts = true;
        }
        
        // ARREGLAR 3: Verificar que el Canvas esté activado
        if (!canvas.gameObject.activeSelf)
        {
            canvas.gameObject.SetActive(true);
            Debug.Log("✅ Canvas activado");
        }
        
        // ARREGLAR 4: Crear EventSystem si no existe
        EventSystem eventSystem = FindFirstObjectByType<EventSystem>();
        if (eventSystem == null)
        {
            GameObject eventSystemGO = new GameObject("EventSystem");
            eventSystem = eventSystemGO.AddComponent<EventSystem>();
            eventSystemGO.AddComponent<StandaloneInputModule>();
            Debug.Log("✅ EventSystem creado en la escena");
        }
        
        // ARREGLAR 5: Asegurar que el Canvas está en Screen Space
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Debug.Log("✅ Canvas cambiado a ScreenSpaceOverlay");
        }
    }
    
    void FixAllButtons()
    {
        // Encontrar todos los botones en la escena
        Button[] allButtons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        
        if (allButtons.Length == 0)
        {
            Debug.LogWarning("⚠️ No se encontraron botones en la escena");
            return;
        }
        
        foreach (Button button in allButtons)
        {
            FixSingleButton(button);
        }
        
        Debug.Log($"✅ {allButtons.Length} botones arreglados");
    }
    
    void FixSingleButton(Button button)
    {
        // ARREGLAR 1: Asegurar que el botón está activado
        if (!button.gameObject.activeSelf)
        {
            button.gameObject.SetActive(true);
        }
        
        // ARREGLAR 2: Asegurar que es interactable
        button.interactable = true;
        
        // ARREGLAR 3: Agregar CanvasGroup si no existe
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        
        // ARREGLAR 4: Verificar que tiene Image component
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage == null)
        {
            buttonImage = button.gameObject.AddComponent<Image>();
            buttonImage.color = new Color(1, 1, 1, 0.5f); // Semi-transparent white
            Debug.LogWarning($"⚠️ Botón '{button.name}' no tenía Image, agregado");
        }
        
        // ARREGLAR 5: Configurar Navigation
        Navigation nav = button.navigation;
        nav.mode = Navigation.Mode.Automatic;
        button.navigation = nav;
        
        // ARREGLAR 6: Asegurar que el RectTransform está bien
        RectTransform rect = button.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
        
        // ARREGLAR 7: Verificar TextMeshProUGUI hijo
        TextMeshProUGUI tmpText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            // Asegurar que el texto puede recibir clicks pasando por el botón
            GraphicRaycaster parentRaycaster = button.GetComponentInParent<GraphicRaycaster>();
            if (parentRaycaster == null)
            {
                Canvas parentCanvas = button.GetComponentInParent<Canvas>();
                if (parentCanvas != null && parentCanvas.GetComponent<GraphicRaycaster>() == null)
                {
                    parentCanvas.gameObject.AddComponent<GraphicRaycaster>();
                }
            }
        }
        
        // ARREGLAR 8: Asegurar que tiene un trigger de selección visual
        ColorBlock colors = button.colors;
        if (colors.highlightedColor == Color.white)
        {
            colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1);
            colors.pressedColor = new Color(0.78f, 0.78f, 0.78f, 1);
            colors.selectedColor = new Color(0.9f, 0.9f, 0.9f, 1);
            button.colors = colors;
        }
    }
}
