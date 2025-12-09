# 📊 FLOWCHART - Navegación de Escenas

## Flujo Completo del Usuario

```
┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
┃       JUEGO INICIA            ┃
┃   (Build Scene #0 = Lobby)    ┃
└━━━━━━┬━━━━━━━━━━━━━━━━━━━━━━┘
       │
       ▼
┌─────────────────────────────────────────┐
│          LOBBY SCENE CARGADA             │
│  ┌───────────────────────────────────┐   │
│  │  SceneManager (Singleton)         │   │
│  │  - DontDestroyOnLoad = true       │   │
│  │  - Persiste siempre               │   │
│  └───────────────────────────────────┘   │
│                                           │
│  ┌─────────────┐  ┌─────────────────┐   │
│  │  Extintor 1 │  │   Sismo 1       │   │
│  │  + Click    │  │   + Click       │   │
│  └──────┬──────┘  └────────┬────────┘   │
│         │                  │             │
│  ┌─────────────┐  ┌─────────────────┐   │
│  │  Extintor 2 │  │   Sismo 2       │   │
│  │  + Click    │  │   + Click       │   │
│  └──────┬──────┘  └────────┬────────┘   │
│         │                  │             │
│  ┌─────────────┐  ┌─────────────────┐   │
│  │  Extintor 3 │  │   Sismo 3       │   │
│  │  + Click    │  │   + Click       │   │
│  └──────┬──────┘  └────────┬────────┘   │
└─────────┼──────────────────┼─────────────┘
          │ (Usuario elige)  │
          └────┬─────────────┘
               │
               ▼ LoadSceneReplace()
    ┌──────────────────────────────────────┐
    │  TRANSICION (0.5s aprox)             │
    │  1. Descarga Lobby                   │
    │  2. Carga curso (FireLesson1, etc)   │
    └─────────┬────────────────────────────┘
              │
              ▼
    ┌──────────────────────────────────────┐
    │   CURSO CARGADO (Solo esta escena)   │
    │   ┌────────────────────────────────┐ │
    │   │ Contenido del Curso             │ │
    │   │ - NPCProfessor                  │ │
    │   │ - FireMinigameManager           │ │
    │   │ - Extintor                      │ │
    │   │ - Canvas UI                     │ │
    │   └────────────────────────────────┘ │
    │                                       │
    │   ┌────────────────────────────────┐ │
    │   │ Botón "VOLVER A LOBBY"         │ │
    │   │ + Click                        │ │
    │   └─────────┬──────────────────────┘ │
    │             │ ReturnToLobby()        │
    └─────────────┼───────────────────────┘
                  │
                  ▼
    ┌──────────────────────────────────────┐
    │  TRANSICION (0.5s aprox)             │
    │  1. Descarga Curso                   │
    │  2. SceneManager sigue en memoria    │
    │  3. Lobby se activa                  │
    └─────────┬────────────────────────────┘
              │
              ▼
    ┌──────────────────────────────────────┐
    │  VUELVES A LOBBY                     │
    │  (SceneManager persiste)             │
    │  - Puedes elegir otro curso          │
    │  - O el mismo nuevamente             │
    │  - Sin perder SceneManager           │
    └──────────────────────────────────────┘
              │
              └─ LOOP (vuelve al usuario elige)
```

---

## Flujo Técnico (Developer View)

```
INICIO
  ↓
[Build Scene #0]
  ↓ LoadScene("Lobby", Single)
  ↓
LOBBY SCENE START
  ├─ LobbyManager.Start()
  ├─ SceneManager.Start() ← Singleton creado
  └─ Botones esperando clicks
  ↓
USUARIO PRESIONA BOTÓN
  ├─ Button.onClick.Invoke()
  ├─ SceneLoaderButton.OnButtonPressed()
  ├─ LoadMode = Replace
  └─ SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1")
  ↓
TRANSICION
  ├─ SceneManager.LoadScene(sceneName, LoadSceneMode.Single)
  ├─ [Descarga] → Lobby destroyed
  ├─ [Carga] → FireExtinguisherLesson1 loaded
  └─ [Espera 0.5s]
  ↓
CURSO ACTIVO
  ├─ FireGameManager.Start()
  ├─ FireMinigameManager.Start()
  ├─ NPCProfessor.Start()
  └─ Botón "Volver" esperando
  ↓
USUARIO PRESIONA "VOLVER"
  ├─ Button.onClick.Invoke()
  ├─ SceneLoaderButton.OnButtonPressed()
  ├─ LoadMode = ReturnLobby
  └─ SceneManagerVR.ReturnToLobby_Static()
  ↓
TRANSICION
  ├─ [Delay 0.5s]
  ├─ SceneManager.UnloadSceneAsync("FireExtinguisherLesson1")
  ├─ FireExtinguisherLesson1 destroyed
  └─ Lobby vuelve a ser visible
  ↓
LOBBY LISTO
  ├─ Canvas visible
  ├─ Botones clickeables
  ├─ SceneManager aún en memoria (DontDestroyOnLoad)
  └─ Usuario puede elegir otro curso
  ↓
VUELVE AL INICIO (LOOP)
```

---

## Métodos Calls Secuencia

### Escenario: Presionas Botón "Extintor 1"

```
1. User clicks Button
   └─ Button.onClick.Invoke()

2. SceneLoaderButton.OnButtonPressed()
   └─ switch(loadMode) → case Replace

3. SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1")
   └─ instance.LoadSceneReplace("FireExtinguisherLesson1")

4. SceneManager.LoadScene("FireExtinguisherLesson1", Single)
   ├─ Descarga escena actual (Lobby)
   └─ Carga escena nueva (FireExtinguisherLesson1)

5. Espera ~1s (transición)
   └─ Debug.Log("[SceneManagerVR] ✓ Escena cargada...")

6. FireExtinguisherLesson1.Start()
   ├─ FireGameManager.Start()
   ├─ NPCProfessor.Start()
   └─ SceneManager persiste (DontDestroyOnLoad)
```

### Escenario: Presionas "Volver a Lobby"

```
1. User clicks Button
   └─ Button.onClick.Invoke()

2. SceneLoaderButton.OnButtonPressed()
   └─ switch(loadMode) → case ReturnLobby

3. SceneManagerVR.ReturnToLobby_Static()
   └─ instance.ReturnToLobby()

4. StartCoroutine(UnloadSceneRoutine("FireExtinguisherLesson1"))
   └─ WaitForSeconds(0.5f) [transitionDelay]

5. SceneManager.UnloadSceneAsync("FireExtinguisherLesson1")
   ├─ Descarga FireExtinguisherLesson1
   └─ Lobby permanece visible

6. Debug.Log("[SceneManagerVR] ✓ Escena descargada...")

7. Vuelves a ver Lobby con todos los botones

8. SceneManager sigue en memoria (LISTO PARA SIGUIENTE CARGA)
```

---

## Estados de Escena

```
ESTADO 1: Lobby Inicial
├─ Active: Lobby
├─ Loaded: Lobby
└─ SceneManager: En memoria (persiste)

ESTADO 2: Transición (0.5s)
├─ Active: None (descargando Lobby)
├─ Loaded: FireExtinguisherLesson1 (cargando)
└─ SceneManager: En memoria (persiste)

ESTADO 3: Curso Activo
├─ Active: FireExtinguisherLesson1
├─ Loaded: FireExtinguisherLesson1
└─ SceneManager: En memoria (persiste)

ESTADO 4: Transición 2 (0.5s)
├─ Active: None
├─ Loaded: Lobby (activándose)
└─ SceneManager: En memoria (persiste)

ESTADO 5: Lobby Nuevamente
├─ Active: Lobby
├─ Loaded: Lobby
└─ SceneManager: En memoria (persiste)
```

---

## Componentes por Escena

```
LOBBY SCENE
├─ SceneManager [SINGLETON]
│  └─ SceneManagerVR.cs
├─ LobbyManager [LOCAL]
│  └─ LobbyManager.cs
├─ Canvas [UI]
│  ├─ Button Extintor 1
│  │  └─ SceneLoaderButton (Replace → FireExtinguisherLesson1)
│  ├─ Button Extintor 2
│  │  └─ SceneLoaderButton (Replace → FireExtinguisherLesson2)
│  └─ ... (más botones)
└─ ... (otros objetos de Lobby)

FIRE LESSON SCENE
├─ SceneManager [PERMANECE - NO DUPLICADO]
├─ FireGameManager
├─ FireMinigameManager
├─ NPCProfessor
├─ Extintor
├─ Canvas [UI]
│  └─ Button "Volver"
│     └─ SceneLoaderButton (ReturnLobby)
└─ ... (contenido del curso)
```

---

## Tiempo de Transición

```
Timeline (Usuario presiona botón)

t=0.0s
├─ Button.onClick
└─ SceneLoaderButton.OnButtonPressed()

t=0.0s
├─ SceneManagerVR.LoadScene_Static()
└─ SceneManager.LoadScene()

t=0.0-0.2s
├─ Lobby descargando
├─ Recursos liberados
└─ FireLesson1 cargando

t=0.2-0.8s
├─ FireLesson1 instanciando GameObjects
├─ Scripts ejecutando Start()
└─ Scene activándose

t=0.8-1.0s
├─ Scene.isLoaded = true
├─ Scene.isActive = true
└─ Usuario puede interactuar

t=1.0s+
└─ Escena completamente cargada
```

---

## Alternativa: Carga Aditiva (Opcional)

```
Si quisieras mantener Lobby visible mientras cargas curso:

Lobby Scene (SIGUE VISIBLE)
├─ Canvas visible
├─ Botones clickeables
└─ Otros elementos

+ Curso Scene (Superpuesto)
├─ Nuevo contenido
├─ Canvas de curso
└─ Nuevos botones

Resultado: DOS escenas activas simultáneamente

Uso: SceneManagerVR.LoadSceneAdditive_Static("FireExtinguisherLesson1")
```

---

## Resumen de Flujo

```
┌─ START (Lobby)
│
├─ User selects course
│
├─ LoadSceneReplace() 
│  ├─ Unload Lobby
│  └─ Load Course
│
├─ COURSE ACTIVE
│  └─ User plays
│
├─ User presses "Return"
│
├─ ReturnToLobby()
│  ├─ Delay 0.5s
│  └─ Unload Course
│
└─ END (Vuelve a Lobby)
   └─ Loop (puede elegir otro curso)
```

---

**Diagrama generado**: Diciembre 2025  
**Para entender visualmente**: Ver DIAGRAMA_ESCENAS_VISUAL.md  
**Para código**: Ver EJEMPLOS_CODIGO_ESCENAS.md  
