using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Script de diagnóstico para verificar problemas con botones UI
/// Agrega esto a un GameObject y ejecuta PLAY para ver el reporte en Console
/// </summary>
public class UIButtonDiagnostic : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("════════════════════════════════════════════════════════════════");
        Debug.Log("🔍 DIAGNÓSTICO DE BOTONES UI - INICIANDO");
        Debug.Log("════════════════════════════════════════════════════════════════");
        
        DiagnoseEventSystem();
        DiagnoseCanvas();
        DiagnoseButtons();
        
        Debug.Log("════════════════════════════════════════════════════════════════");
        Debug.Log("✅ DIAGNÓSTICO COMPLETADO - Revisa los logs arriba");
        Debug.Log("════════════════════════════════════════════════════════════════");
    }
    
    void DiagnoseEventSystem()
    {
        Debug.Log("\n📋 VERIFICANDO EventSystem...");
        
        EventSystem eventSystem = FindFirstObjectByType<EventSystem>();
        
        if (eventSystem == null)
        {
            Debug.LogError("❌ NO HAY EventSystem en la escena (PROBLEMA CRÍTICO)");
            Debug.Log("   SOLUCIÓN: Crea GameObject → Add Component → Event System + Standalone Input Module");
            return;
        }
        
        Debug.Log("✅ EventSystem encontrado");
        
        StandaloneInputModule inputModule = eventSystem.GetComponent<StandaloneInputModule>();
        if (inputModule == null)
        {
            Debug.LogError("❌ EventSystem no tiene StandaloneInputModule (PROBLEMA CRÍTICO)");
            Debug.Log("   SOLUCIÓN: En EventSystem → Add Component → Standalone Input Module");
        }
        else
        {
            Debug.Log("✅ StandaloneInputModule configurado");
        }
    }
    
    void DiagnoseCanvas()
    {
        Debug.Log("\n📋 VERIFICANDO Canvas...");
        
        Canvas canvas = FindFirstObjectByType<Canvas>();
        
        if (canvas == null)
        {
            Debug.LogError("❌ NO HAY Canvas en la escena (PROBLEMA CRÍTICO)");
            return;
        }
        
        Debug.Log("✅ Canvas encontrado: " + canvas.name);
        
        // Verificar RenderMode
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            Debug.LogWarning("⚠️ Canvas en modo: " + canvas.renderMode);
            Debug.Log("   RECOMENDACIÓN: Cambiar a ScreenSpaceOverlay para UI 2D simple");
        }
        else
        {
            Debug.Log("✅ Canvas en ScreenSpaceOverlay");
        }
        
        // Verificar GraphicRaycaster
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            Debug.LogError("❌ Canvas NO tiene GraphicRaycaster (PROBLEMA CRÍTICO)");
            Debug.Log("   SOLUCIÓN: Canvas → Add Component → Graphic Raycaster");
        }
        else
        {
            Debug.Log("✅ GraphicRaycaster presente");
        }
        
        // Verificar CanvasGroup
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("⚠️ Canvas NO tiene CanvasGroup");
            Debug.Log("   RECOMENDACIÓN: Add Component → Canvas Group");
        }
        else
        {
            Debug.Log("✅ CanvasGroup presente");
            Debug.Log("   - Blocks Raycasts: " + canvasGroup.blocksRaycasts);
            Debug.Log("   - Interactable: " + canvasGroup.interactable);
        }
    }
    
    void DiagnoseButtons()
    {
        Debug.Log("\n📋 VERIFICANDO BOTONES...");
        
        Button[] allButtons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        
        if (allButtons.Length == 0)
        {
            Debug.LogWarning("⚠️ No hay botones en la escena");
            return;
        }
        
        Debug.Log($"✅ Encontrados {allButtons.Length} botones\n");
        
        int problemCount = 0;
        
        foreach (Button button in allButtons)
        {
            Debug.Log($"🔹 Botón: {button.name}");
            
            // Verificar si está activado
            if (!button.gameObject.activeSelf)
            {
                Debug.LogWarning("   ❌ Botón desactivado");
                problemCount++;
            }
            else
            {
                Debug.Log("   ✅ Botón activado");
            }
            
            // Verificar Interactable
            if (!button.interactable)
            {
                Debug.LogError("   ❌ Interactable = FALSE (PROBLEMA CRÍTICO)");
                problemCount++;
            }
            else
            {
                Debug.Log("   ✅ Interactable = TRUE");
            }
            
            // Verificar Image component
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage == null)
            {
                Debug.LogError("   ❌ NO tiene Image component (PROBLEMA CRÍTICO)");
                problemCount++;
            }
            else
            {
                Debug.Log("   ✅ Image component presente");
                if (buttonImage.color.a < 0.1f)
                {
                    Debug.LogWarning("   ⚠️ Image es muy transparente (Alpha < 0.1)");
                }
            }
            
            // Verificar CanvasGroup
            CanvasGroup btnCanvasGroup = button.GetComponent<CanvasGroup>();
            if (btnCanvasGroup == null)
            {
                Debug.LogWarning("   ⚠️ NO tiene CanvasGroup");
            }
            else
            {
                Debug.Log("   ✅ CanvasGroup presente");
                if (!btnCanvasGroup.blocksRaycasts)
                {
                    Debug.LogError("   ❌ CanvasGroup.blocksRaycasts = FALSE (PROBLEMA)");
                    problemCount++;
                }
            }
            
            // Verificar listeners
            int listenerCount = button.onClick.GetPersistentEventCount();
            if (listenerCount == 0)
            {
                Debug.LogWarning("   ⚠️ NO tiene listeners en On Click()");
            }
            else
            {
                Debug.Log($"   ✅ {listenerCount} listener(s) en On Click()");
            }
            
            Debug.Log("");
        }
        
        Debug.Log($"\n📊 RESUMEN: {allButtons.Length} botones encontrados, {problemCount} problema(s) detectado(s)");
    }
}
