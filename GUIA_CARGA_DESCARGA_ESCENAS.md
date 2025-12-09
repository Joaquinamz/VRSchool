# Guía de Carga/Descarga de Escenas - SceneManagerVR

## Descripción General

Sistema moderno y flexible para manejar la carga y descarga de escenas en tu proyecto VR.

**Dos scripts:**
1. **SceneManagerVR.cs** - Gestor central (Singleton)
2. **SceneLoaderButton.cs** - Script para asignar a botones

---

## Instalación

### Paso 1: Crear el SceneManagerVR (Una sola vez)

1. Crea un GameObject vacío en la escena **Lobby**
   - Click derecho en Hierarchy → `3D Object > Empty`
   - Nombre: `SceneManager`

2. Agrega el componente `SceneManagerVR`
   - Click en Add Component → Busca "SceneManagerVR"
   - Check: `Don't Destroy On Load` está marcado automáticamente

3. Configura (opcional):
   - **Lobby Scene Name**: "Lobby" (por defecto)
   - **Transition Delay**: 0.5s (tiempo entre descargar y cargar)
   - **Debug Mode**: true (muestra logs en console)

---

## Configuración de Botones

### Opción A: Botón de Carga (Lobby → Curso)

Usado en los 6 botones del Lobby.

1. **Selecciona el botón** en la escena
2. **Add Component → SceneLoaderButton**
3. Inspector > SceneLoaderButton:
   - **Load Mode**: `Replace` ← Reemplaza Lobby
   - **Target Scene Name**: `FireExtinguisherLesson1` (o el nombre de tu escena)
4. En el botón (Button component):
   - **On Click (Runtime Only)** → `+` (agregar evento)
   - Arrastra el GameObject con SceneLoaderButton
   - Dropdown: `SceneLoaderButton > OnButtonPressed()`

**Resultado**: Al presionar, descarga Lobby y carga el curso.

### Opción B: Botón de Retorno (Curso → Lobby)

Usado en los botones "Volver" dentro de los cursos.

1. **Selecciona el botón** en la escena del curso
2. **Add Component → SceneLoaderButton**
3. Inspector > SceneLoaderButton:
   - **Load Mode**: `ReturnLobby` ← Retorna a Lobby
   - **Target Scene Name**: (dejalo vacío, no se usa)
4. En el botón (Button component):
   - **On Click (Runtime Only)** → `+` (agregar evento)
   - Arrastra el GameObject con SceneLoaderButton
   - Dropdown: `SceneLoaderButton > OnButtonPressed()`

**Resultado**: Al presionar, descarga el curso y regresa a Lobby.

---

## Métodos Disponibles

### Desde SceneLoaderButton (para botones)
```csharp
OnButtonPressed()  // Llama automáticamente al presionar el botón
```

### Desde código C# (si necesitas controlar manualmente)
```csharp
// Reemplazar escena actual (recomendado para Lobby)
SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1");

// Cargar aditivamente (mantiene Lobby visible)
SceneManagerVR.LoadSceneAdditive_Static("FireExtinguisherLesson1");

// Regresar a Lobby
SceneManagerVR.ReturnToLobby_Static();
```

---

## Flujo de Ejemplo

### Escenario 1: Lobby → Curso de Fuego
```
1. Usuario presiona botón "Extintor Lección 1" en Lobby
2. SceneLoaderButton detecta clic
3. LoadMode = Replace
4. SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1")
5. Lobby se descarga
6. FireExtinguisherLesson1 se carga
7. Usuario juega el curso
```

### Escenario 2: Curso → Lobby (Botón "Volver")
```
1. Usuario presiona botón "Volver a Lobby" en el curso
2. SceneLoaderButton detecta clic
3. LoadMode = ReturnLobby
4. SceneManagerVR.ReturnToLobby_Static()
5. FireExtinguisherLesson1 se descarga (con delay de 0.5s)
6. Lobby se mantiene (ya estaba precargada)
7. Usuario vuelve a ver los 6 botones de lecciones
```

---

## Configuración de Build Settings

Asegúrate que TODAS las escenas estén en `File > Build Settings`:

```
Scenes In Build:
0. Lobby
1. FireExtinguisherLesson1
2. FireExtinguisherLesson2
3. FireExtinguisherLesson3
4. EarthQuakeLesson1
5. EarthQuakeLesson2
6. EarthQuakeLesson3
```

**Importante**: Los nombres deben coincidir exactamente (case-sensitive).

---

## Debugging

### Activar Logs
Inspector > SceneManagerVR > Debug Mode: `true`

### Ejemplo de Logs
```
[SceneManagerVR] Cargando escena (reemplazar): FireExtinguisherLesson1
[SceneManagerVR] ✓ Escena descargada: Lobby
[SceneManagerVR] Regresando a Lobby, descargando: FireExtinguisherLesson1
[SceneManagerVR] ✓ Transición completada: Lobby
```

### Troubleshooting

**"No está cargando la escena"**
- Verifica que el nombre de escena en SceneLoaderButton coincida exactamente
- Revisa Build Settings → Scenes In Build (¿Está la escena ahí?)

**"Se ve lag al cambiar"**
- Ajusta `Transition Delay` en SceneManagerVR (por defecto 0.5s)

**"El SceneManager desaparece después de cargar"**
- Está dentro de DontDestroyOnLoad, es normal
- Aparecerá en la siguiente escena cuando regreses

**"Los botones no responden"**
- Verifica que el botón tenga el evento OnClick configurado
- Asegúrate que SceneLoaderButton sea un componente del GameObject principal del botón

---

## Configuración Avanzada

### Crear SceneManagerVR en Código (automático)
Si prefieres no crear el GameObject manualmente:

```csharp
// Al inicio, verifica si existe
if (FindFirstObjectByType<SceneManagerVR>() == null)
{
    GameObject manager = new GameObject("SceneManager");
    manager.AddComponent<SceneManagerVR>();
    DontDestroyOnLoad(manager);
}
```

### Cargar Múltiples Escenas Aditivamente
El sistema detecta automáticamente si hay un curso cargado y lo descarga primero. Pero si quieres cargar dos cursos simultáneamente:

```csharp
// Llamar directamente al método interno
SceneManager.LoadSceneAsync("FireExtinguisherLesson1", LoadSceneMode.Additive);
SceneManager.LoadSceneAsync("EarthQuakeLesson1", LoadSceneMode.Additive);
```

---

## Comparación: Antes vs Después

### Antes (SceneLoaderExtintor.cs)
```csharp
// Muy básico, solo reemplaza
public void LoadByName(string cursoExtintor1)
{
    SceneManager.LoadScene(cursoExtintor1);
}
```

### Después (SceneManagerVR.cs)
```csharp
// Flexible, soporta múltiples modos
public void LoadSceneReplace(string sceneName)
public void LoadSceneAdditive(string sceneName)
public void UnloadScene(string sceneName)
public void ReturnToLobby()
```

---

## Checklist de Configuración

### Escena Lobby
- [ ] Crea GameObject "SceneManager" con SceneManagerVR
- [ ] Configura los 6 botones de lecciones:
  - [ ] Botón Extintor 1 → SceneLoaderButton (Replace, "FireExtinguisherLesson1")
  - [ ] Botón Extintor 2 → SceneLoaderButton (Replace, "FireExtinguisherLesson2")
  - [ ] Botón Extintor 3 → SceneLoaderButton (Replace, "FireExtinguisherLesson3")
  - [ ] Botón Sismo 1 → SceneLoaderButton (Replace, "EarthQuakeLesson1")
  - [ ] Botón Sismo 2 → SceneLoaderButton (Replace, "EarthQuakeLesson2")
  - [ ] Botón Sismo 3 → SceneLoaderButton (Replace, "EarthQuakeLesson3")

### Cada Escena de Curso
- [ ] Crea o identifica el botón "Volver a Lobby"
- [ ] Agrega SceneLoaderButton (ReturnLobby mode)
- [ ] Configura el evento On Click

### Build Settings
- [ ] Todas las escenas están en Build Settings
- [ ] Nombres coinciden exactamente

---

**Versión**: 1.0 - Sistema Moderno de Carga de Escenas  
**Última actualización**: Diciembre 2025  
**Compatibilidad**: Unity 2022+

