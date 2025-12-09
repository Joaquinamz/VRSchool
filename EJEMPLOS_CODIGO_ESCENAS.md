# Ejemplos de Código - Sistema de Carga de Escenas

## 1. SceneManagerVR - Métodos Principales

### Carga Directa (Reemplaza Escena)
```csharp
// Desde un botón o script
SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1");

// Internamente hace:
SceneManager.LoadScene("FireExtinguisherLesson1", LoadSceneMode.Single);
// Descarga Lobby, carga FireExtinguisherLesson1
```

### Carga Aditiva (Mantiene Escena Actual)
```csharp
SceneManagerVR.LoadSceneAdditive_Static("FireExtinguisherLesson1");

// Internamente hace:
SceneManager.LoadSceneAsync("FireExtinguisherLesson1", LoadSceneMode.Additive);
// Mantiene Lobby cargada, agrega FireExtinguisherLesson1
```

### Retorno a Lobby
```csharp
SceneManagerVR.ReturnToLobby_Static();

// Internamente:
// 1. Descarga escena de curso actual
// 2. Espera delay de transición
// 3. Mantiene Lobby visible
```

### Descarga Específica
```csharp
SceneManagerVR.UnloadScene("FireExtinguisherLesson1");

// Internamente:
SceneManager.UnloadSceneAsync("FireExtinguisherLesson1");
```

---

## 2. SceneLoaderButton - Modos de Carga

### Modo 1: Replace (Reemplaza)
```csharp
[SerializeField] private LoadMode loadMode = LoadMode.Replace;
[SerializeField] private string targetSceneName = "FireExtinguisherLesson1";

// Cuando presionas el botón:
// → SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1")
// → Descarga Lobby, carga curso
```

### Modo 2: Additive (Aditivo)
```csharp
[SerializeField] private LoadMode loadMode = LoadMode.Additive;
[SerializeField] private string targetSceneName = "FireExtinguisherLesson1";

// Cuando presionas el botón:
// → SceneManagerVR.LoadSceneAdditive_Static("FireExtinguisherLesson1")
// → Mantiene Lobby, carga curso encima
```

### Modo 3: ReturnLobby
```csharp
[SerializeField] private LoadMode loadMode = LoadMode.ReturnLobby;
[SerializeField] private string targetSceneName = "";  // No usado

// Cuando presionas el botón:
// → SceneManagerVR.ReturnToLobby_Static()
// → Descarga curso, regresa a Lobby
```

---

## 3. Integración con LobbyManager

### Versión Antigua
```csharp
void LoadScene(string sceneName)
{
    SceneManager.LoadScene(sceneName);  // Básico
}
```

### Versión Nueva
```csharp
void LoadCourse(string sceneName)
{
    Debug.Log($"[LobbyManager] Cargando curso: {sceneName}");
    SceneManagerVR.LoadScene_Static(sceneName);  // Moderno
}
```

---

## 4. Configuración de Botones (Código)

### Opción A: En Editor (Recomendado)
```
1. Selecciona botón
2. Add Component → SceneLoaderButton
3. Load Mode = Replace
4. Target Scene Name = FireExtinguisherLesson1
5. Button On Click → + → Arrastra botón → OnButtonPressed()
```

### Opción B: Por Código
```csharp
// En algún script de inicialización
Button btn = GetComponent<Button>();
SceneLoaderButton loader = btn.gameObject.AddComponent<SceneLoaderButton>();
btn.onClick.AddListener(loader.OnButtonPressed);
```

---

## 5. Transiciones Personalizadas

### Aumentar Delay
```csharp
// En SceneManagerVR
[SerializeField] private float transitionDelay = 1.5f;  // Más tiempo
```

### Agregar Fade Out (Avanzado)
```csharp
// Extender SceneManagerVR para agregar:
private IEnumerator UnloadSceneRoutine(string sceneName)
{
    // Agregar animación aquí
    FadeOut();  // Tu función de fade
    yield return new WaitForSeconds(transitionDelay);
    
    var operation = SceneManager.UnloadSceneAsync(sceneName);
    while (!operation.isDone)
    {
        yield return null;
    }
    
    FadeIn();  // Tu función de fade in
    currentCourseScene = "";
}
```

---

## 6. Debugging

### Ver Logs
```csharp
// En SceneManagerVR, Inspector:
Debug Mode = true

// Console muestra:
[SceneManagerVR] Cargando escena (reemplazar): FireExtinguisherLesson1
[SceneManagerVR] ✓ Escena descargada: Lobby
[SceneManagerVR] Regresando a Lobby...
```

### Logs Personalizados
```csharp
// En tu script
void Start()
{
    if (FindFirstObjectByType<SceneManagerVR>() == null)
    {
        Debug.LogError("SceneManagerVR no encontrado!");
    }
}
```

---

## 7. Casos de Uso Avanzados

### Cargar Múltiples Cursos
```csharp
// Cargar dos cursos simultáneamente
SceneManagerVR.LoadSceneAdditive_Static("FireExtinguisherLesson1");
SceneManagerVR.LoadSceneAdditive_Static("EarthQuakeLesson1");
// Ambos activos simultáneamente
```

### Cambiar Curso Sin Volver a Lobby
```csharp
// Si hay curso cargado, lo descarga y carga otro
SceneManagerVR.LoadSceneAdditive_Static("FireExtinguisherLesson2");
// Internamente detecta y descarga FireExtinguisherLesson1 primero
```

### Verificar Escena Actual
```csharp
// Acceder a SceneManagerVR
SceneManagerVR manager = FindFirstObjectByType<SceneManagerVR>();
// Luego verificar estado (si lo expones públicamente)
```

---

## 8. Errores Comunes y Soluciones

### Error: "Scene not found"
```csharp
// ❌ Incorrecto
SceneManagerVR.LoadScene_Static("FireExtinguisherlesson1");  // lowercase 'l'

// ✅ Correcto
SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1");  // Mayúscula exacta
```

### Error: "NullReferenceException"
```csharp
// ❌ SceneManager no existe
SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1");

// ✅ Crear SceneManager primero
// En Lobby: + Empty > SceneManager > Add Component > SceneManagerVR
```

### Error: "Button On Click no funciona"
```csharp
// ❌ Event no configurado
Button btn = GetComponent<Button>();
// (Sin On Click configurado)

// ✅ Configurar en Inspector
// Button > On Click () > + > Arrastra GameObject > OnButtonPressed()
```

---

## 9. Estructura Recomendada

```csharp
public class MyGame : MonoBehaviour
{
    public void StartFireLesson()
    {
        // Opción 1: Direct call
        SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1");
        
        // Opción 2: Through manager
        var manager = FindFirstObjectByType<SceneManagerVR>();
        if (manager != null)
            manager.LoadSceneReplace("FireExtinguisherLesson1");
    }
    
    public void ReturnHome()
    {
        SceneManagerVR.ReturnToLobby_Static();
    }
}
```

---

## 10. Testing Script

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTest : MonoBehaviour
{
    void Update()
    {
        // Test con teclas (solo para debug)
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Testing: Load FireExtinguisherLesson1");
            SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1");
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Testing: Return to Lobby");
            SceneManagerVR.ReturnToLobby_Static();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Testing: Load Additively");
            SceneManagerVR.LoadSceneAdditive_Static("FireExtinguisherLesson1");
        }
    }
}
```

---

## Resumen Rápido

| Necesidad | Código |
|-----------|--------|
| Cargar curso desde Lobby | `SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1")` |
| Volver a Lobby desde curso | `SceneManagerVR.ReturnToLobby_Static()` |
| Cargar sin descargar Lobby | `SceneManagerVR.LoadSceneAdditive_Static("FireExtinguisherLesson1")` |
| Descarga específica | `SceneManagerVR.UnloadScene("FireExtinguisherLesson1")` |

---

**Última actualización**: Diciembre 2025
