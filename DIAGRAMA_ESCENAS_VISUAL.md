# Diagrama Visual - Sistema de Carga de Escenas

## Flujo de Navegación

```
┌─────────────────────────────────────────────────────────────────┐
│                          LOBBY SCENE                             │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │                    SceneManager (Singleton)              │   │
│  │  - DontDestroyOnLoad = true (persiste entre escenas)    │   │
│  │  - Maneja toda carga/descarga                           │   │
│  └──────────────────────────────────────────────────────────┘   │
│                                                                   │
│  ┌────────────────┐  ┌────────────────┐  ┌────────────────┐     │
│  │   Extintor 1   │  │   Extintor 2   │  │   Extintor 3   │     │
│  │ (Botón + Click)│  │ (Botón + Click)│  │ (Botón + Click)│     │
│  └────────┬───────┘  └────────┬───────┘  └────────┬───────┘     │
│           │                   │                   │              │
│  ┌────────────────┐  ┌────────────────┐  ┌────────────────┐     │
│  │   Sismo 1      │  │   Sismo 2      │  │   Sismo 3      │     │
│  │ (Botón + Click)│  │ (Botón + Click)│  │ (Botón + Click)│     │
│  └────────┬───────┘  └────────┬───────┘  └────────┬───────┘     │
└───────────┼──────────────────┼──────────────────┼──────────────┘
            │                  │                  │
            └──────────────────┼──────────────────┘
                               │ LoadSceneReplace()
                               ▼
            ┌──────────────────────────────────────┐
            │   COURSE SCENE (FireLesson1, etc)    │
            │   ┌──────────────────────────────┐   │
            │   │   Botón "Volver a Lobby"     │   │
            │   │   (Click → ReturnToLobby())   │   │
            │   └──────────────────────────────┘   │
            └──────────────────────────────────────┘
                               │
                    ReturnToLobby()
                               │
                               ▼
            ┌──────────────────────────────────────┐
            │   Vuelve a LOBBY (SceneManager OK)   │
            └──────────────────────────────────────┘
```

---

## Componentes por Escena

### LOBBY SCENE
```
Hierarchy:
├─ Canvas (UI)
│  ├─ Button_Extintor1  → SceneLoaderButton (Replace → FireExtinguisherLesson1)
│  ├─ Button_Extintor2  → SceneLoaderButton (Replace → FireExtinguisherLesson2)
│  ├─ Button_Extintor3  → SceneLoaderButton (Replace → FireExtinguisherLesson3)
│  ├─ Button_Sismo1     → SceneLoaderButton (Replace → EarthQuakeLesson1)
│  ├─ Button_Sismo2     → SceneLoaderButton (Replace → EarthQuakeLesson2)
│  └─ Button_Sismo3     → SceneLoaderButton (Replace → EarthQuakeLesson3)
├─ LobbyManager         → Script (opcional, si usas métodos en código)
└─ SceneManager         → SceneManagerVR (SINGLETON - NO DESTRUIR)
```

### COURSE SCENES (FireExtinguisherLesson1, etc)
```
Hierarchy:
├─ Canvas (UI)
│  └─ Button_Return     → SceneLoaderButton (ReturnLobby)
├─ NPCProfessor
├─ FireMinigameManager
├─ Extintor
└─ ... (resto del curso)

Nota: SceneManager se mantiene desde Lobby
```

---

## Métodos de Carga

```
SceneManagerVR
│
├─ LoadSceneReplace(sceneName)
│  └─ Descarga Lobby, carga Curso
│     Usado: Botones en Lobby
│     Resultado: Solo una escena activa
│
├─ LoadSceneAdditive(sceneName)
│  └─ Mantiene Lobby, carga Curso encima
│     Usado: Si quieres Lobby visible mientras juegas
│     Resultado: Dos escenas activas
│
├─ UnloadScene(sceneName)
│  └─ Descarga una escena específica
│     Usado: Descarga manual de escenas
│
└─ ReturnToLobby()
   └─ Descarga curso actual, mantiene Lobby
      Usado: Botón "Volver" en cursos
      Resultado: SceneManager persiste, Lobby activo
```

---

## Versiones de Implementación

### OPCIÓN 1: LobbyManager (Tradicional)
```csharp
// Todos los botones se configuran en LobbyManager.cs
LobbyManager.cs
│
├─ btnExtintorA → LoadCourse("FireExtinguisherLesson1")
├─ btnExtintorB → LoadCourse("FireExtinguisherLesson2")
├─ btnExtintorC → LoadCourse("FireExtinguisherLesson3")
├─ btnSismoA → LoadCourse("EarthQuakeLesson1")
├─ btnSismoB → LoadCourse("EarthQuakeLesson2")
└─ btnSismoC → LoadCourse("EarthQuakeLesson3")

Ventaja: Un solo script, fácil de mantener
Desventaja: Menos flexible
```

### OPCIÓN 2: SceneLoaderButton (Modular)
```
Cada botón → SceneLoaderButton.cs
│
├─ Botón 1 → SceneLoaderButton (Extintor 1)
├─ Botón 2 → SceneLoaderButton (Extintor 2)
├─ Botón 3 → SceneLoaderButton (Extintor 3)
└─ ... (resto)

Ventaja: Máxima flexibilidad, fácil de replicar
Desventaja: Más componentes que mantener
```

---

## Transición de Escenas (Tiempo)

```
Timeline:
Time 0.0s  → Usuario presiona botón
Time 0.0s  → SceneLoaderButton.OnButtonPressed()
Time 0.0s  → SceneManagerVR.LoadScene_Static(sceneName)
Time 0.0s  → SceneManager.LoadScene(sceneName, Single)
Time 0.0s  → Lobby inicia descarga
Time ~0.2s → Lobby descargada
Time ~0.4s → Curso cargando
Time ~0.8s → Curso activo
Time 1.0s  → SceneManager.isLoading = false

Transición Total: ~1 segundo
(Depende del tamaño del curso)
```

---

## Archivos Creados

```
Assets/
├─ SceneManagerVR.cs          ← Gestor principal (Singleton)
├─ SceneLoaderButton.cs        ← Script para botones
├─ LobbyManager.cs             ← Actualizado (ahora usa SceneManagerVR)
└─ SceneLoaderExtintor.cs      ← OBSOLETO (reemplazado)

Documentation/
├─ GUIA_CARGA_DESCARGA_ESCENAS.md      ← Guía completa
├─ IMPLEMENTACION_RAPIDA_ESCENAS.md    ← Quickstart (5 min)
└─ DIAGRAMA_ESCENAS.md                  ← Este archivo
```

---

## Configuración Build Settings

**Requerido**: Todas las escenas en Build Settings

```
File > Build Settings > Scenes In Build

Index 0: Lobby
Index 1: FireExtinguisherLesson1
Index 2: FireExtinguisherLesson2
Index 3: FireExtinguisherLesson3
Index 4: EarthQuakeLesson1
Index 5: EarthQuakeLesson2
Index 6: EarthQuakeLesson3
```

**Nombres deben coincidir exactamente** (case-sensitive)

---

## Troubleshooting Visual

```
Problem: "No carga la escena"
Solution: 
  ✓ ¿Escena en Build Settings? 
  ✓ ¿Nombre exacto?
  ✓ ¿SceneLoaderButton referencia correcta?

Problem: "Se ve lag entre escenas"
Solution:
  ✓ Aumenta Transition Delay en SceneManagerVR
  ✓ O reduce el tamaño del curso (menos objetos)

Problem: "Botón no responde"
Solution:
  ✓ ¿Botón tiene On Click configurado?
  ✓ ¿Está llamando OnButtonPressed()?
  ✓ ¿SceneManagerVR en la escena?

Problem: "SceneManager desaparece"
Solution:
  ✓ Es NORMAL - está en DontDestroyOnLoad
  ✓ Aparecerá cuando cargues siguiente escena
```

---

## Ejemplo Completo (Copy-Paste)

### Setup Mínimo
```
1. En Lobby → Crea GameObject "SceneManager"
2. Add Component → SceneManagerVR (listo)
3. En cada botón → Add Component → SceneLoaderButton
4. Configura Load Mode + Scene Name
5. En Button On Click → Arrastra, selecciona OnButtonPressed()
```

### Test
```
▶ Play
✓ Presiona botón → Carga curso
✓ En curso, presiona Volver → Vuelve a Lobby
✓ Repite → Funciona 10 veces seguidas sin problemas
```

---

**Diagrama generado**: Diciembre 2025
