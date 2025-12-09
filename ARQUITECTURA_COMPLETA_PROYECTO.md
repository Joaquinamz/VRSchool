# 🎓 ARQUITECTURA COMPLETA: Sistema de Cursos VR (Extintor + Sismo)

**Proyecto:** VRDemo - Cursos Educativos VR
**Autor:** Tu equipo
**Fecha:** 30 Noviembre 2025
**Estado:** Guía de implementación detallada

---

## 📋 ÍNDICE EJECUTIVO

```
1. Arquitectura General (Sistema)
2. Flujo Principal (Usuario)
3. Escenas y sus Responsabilidades
4. Canvas y Controles
5. Cursos: Extintor
6. Cursos: Sismo
7. Resultados y Menú
8. Detalles Técnicos
9. Scripts Necesarios
10. Checklist de Implementación
```

---

# PARTE 1: ARQUITECTURA GENERAL

## 🎯 VISIÓN GENERAL DEL SISTEMA

Tu proyecto tiene esta estructura:

```
LOBBY (Escena Principal)
│
├─ Camera VR (Main Camera)
├─ Player (XR Rig)
├─ Canvas de Selección de Curso
│  ├─ Panel Extintor (Botones A/B/C/Aleatorio)
│  └─ Panel Sismo (Botones A/B/C/Aleatorio)
│
├─ Canvas de NPC (Diálogos)
│  └─ Inicialmente inactivo
│
└─ Entrada Escuela Kansai
   ├─ Modelo 3D
   ├─ Colisiones
   └─ (Prefab o instancia)

CUANDO SELECCIONA CURSO:
│
├─ Escena Lobby DESAPARECE
├─ Escena Sala de Clase APARECE
│
SALA DE CLASE (Escena Secundaria)
│
├─ Camera VR (heredada)
├─ Player (heredado)
├─ NPC Profesor
│  ├─ Modelo 3D
│  ├─ Canvas de Diálogos
│  └─ Sistema de estados
│
├─ Canvas de Diálogos (UI)
│  ├─ Texto de profesor
│  ├─ Botón "Siguiente"
│  └─ Indicador de progreso
│
├─ Sistema de Eventos
│  ├─ FireGame (si es Extintor)
│  ├─ EarthquakeGame (si es Sismo)
│  └─ ResultsUI (para ambos)
│
└─ Elementos según curso
   ├─ SI EXTINTOR:
   │  ├─ Fuego de entrenamiento
   │  ├─ Extintor (preposicionado)
   │  └─ Sistema de daño
   │
   └─ SI SISMO:
      ├─ Mesa (para ocultarse)
      ├─ Sistema de temblor
      ├─ Escombros (cayendo)
      └─ Puertas de salida
```

---

## 🔄 FLUJO GENERAL DEL USUARIO

```
PASO 1: Usuario entra en VR
        └─ Aparece en LOBBY

PASO 2: Ve Canvas de Selección
        ├─ Panel Extintor con botones (A/B/C/Aleatorio)
        └─ Panel Sismo con botones (A/B/C/Aleatorio)

PASO 3: Usuario presiona un botón
        ├─ Se guarda: Tipo de curso (Extintor/Sismo)
        ├─ Se guarda: Dificultad (A/B/C)
        └─ Canvas desaparece, Sala aparece

PASO 4: Usuario entra a Sala de Clase
        ├─ Ve NPC Profesor
        ├─ Canvas de diálogos aparece
        ├─ Profesor da introducción (3-5 líneas)
        └─ Usuario presiona "Siguiente"

PASO 5: Sistema entra en JUEGO
        │
        ├─ SI EXTINTOR:
        │  ├─ Aparece UN fuego
        │  ├─ Usuario lo apaga
        │  ├─ Profesor dialoga sobre resultado
        │  ├─ Usuario presiona "Siguiente"
        │  ├─ Aparecen MÚLTIPLES fuegos (minijuego)
        │  ├─ Usuario apaga todos
        │  └─ Ir a RESULTADOS
        │
        └─ SI SISMO:
           ├─ Comienza temblor
           ├─ Caen escombros
           ├─ Usuario se ocupa bajo mesa
           ├─ Temblor para (después ~30 seg)
           ├─ Profesor da instrucciones de salida
           ├─ Usuario presiona "Siguiente"
           ├─ Usuario se retira de sala (evita alumnos)
           └─ Ir a RESULTADOS

PASO 6: RESULTADOS
        ├─ Se muestra puntuación/tiempo
        ├─ Canvas ofrece:
        │  ├─ Botón "Reintentar"
        │  └─ Botón "Volver a Lobby"
        │
        ├─ SI "Reintentar": Resetea todo y vuelve a PASO 4
        └─ SI "Volver a Lobby": Carga Lobby nuevamente
```

---

# PARTE 2: GESTIÓN DE DATOS ENTRE ESCENAS

## 💾 ¿CÓMO PASAMOS DATOS ENTRE ESCENAS?

Necesitas una clase GLOBAL que sobreviva entre escenas:

```csharp
// GameManager.cs (SINGLETON - Solo una copia)

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public string selectedCourse;  // "Extintor" o "Sismo"
    public string difficulty;      // "A", "B", "C", "Random"
    public int score;
    public float time;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ← CLAVE: No destruye entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

// USO:
// GameManager.instance.selectedCourse = "Extintor";
// GameManager.instance.difficulty = "B";
```

---

# PARTE 3: ESCENAS NECESARIAS

## 📍 ESCENA 1: LOBBY

### Jerarquía en Editor

```
LOBBY (Escena)
│
├─ XROrigin (o XRRig)
│  ├─ Camera (Main Camera)
│  ├─ LeftController
│  └─ RightController
│
├─ WorldSceneCanvases
│  └─ Entrada Escuela (Modelo 3D Kansai)
│
├─ GameManager
│  └─ Script: GameManager.cs (Singleton)
│
└─ UI Canvas (ScreenSpace Overlay)
   ├─ Panel SelectionUI
   │  ├─ Text: "Selecciona Curso"
   │  ├─ Panel ExtintorOptions
   │  │  ├─ Button A
   │  │  ├─ Button B
   │  │  ├─ Button C
   │  │  └─ Button Aleatorio
   │  │
   │  └─ Panel SismoOptions
   │     ├─ Button A
   │     ├─ Button B
   │     ├─ Button C
   │     └─ Button Aleatorio
   │
   └─ NPCDialogCanvas
      ├─ Panel DialogBox (inicialmente OFF)
      ├─ Text DialogText
      ├─ Button NextButton
      └─ Image NPCPortrait
```

### Scripts en LOBBY

```
1. LobbyManager.cs
   ├─ Detecta clicks en botones de curso
   ├─ Guarda datos en GameManager
   └─ Carga escena "SalaDeClase"

2. SelectionUIController.cs
   ├─ Maneja visibilidad de paneles
   ├─ Responde a botones
   └─ Controla efectos visuales
```

---

## 📍 ESCENA 2: SALA DE CLASE

### Jerarquía en Editor

```
SALA DE CLASE (Escena)
│
├─ XROrigin (o XRRig)
│  ├─ Camera (Main Camera)
│  ├─ LeftController
│  └─ RightController
│
├─ NPCProfesor
│  ├─ Model (Mesh + Animator)
│  ├─ Collider (para detectar proximidad)
│  └─ Script: NPCProfessor.cs
│
├─ ClassroomEnvironment
│  ├─ Pizarra (Mesh)
│  ├─ Escritorio (Mesh)
│  ├─ Puertas (Mesh + Colliders)
│  │
│  ├─ SI EXTINTOR:
│  │  ├─ Extintor (Prefab: ExtintorPrincipal)
│  │  │  ├─ CuerpoExtintor
│  │  │  └─ BoquillaExtintor
│  │  └─ Areas de fuegos
│  │
│  └─ SI SISMO:
│     ├─ Mesa (Mesh + Collider)
│     ├─ Escombros (Prefabs con Rigidbody)
│     └─ Markers de salida
│
├─ GameSystems
│  ├─ SI EXTINTOR:
│  │  ├─ FireGameManager.cs
│  │  ├─ Fire (Prefab)
│  │  └─ ParticleEffects
│  │
│  └─ SI SISMO:
│     ├─ EarthquakeManager.cs
│     ├─ DebrisSpawner.cs
│     └─ Temblor (Script con animación de cámara)
│
├─ UICanvases
│  ├─ DialogUICanvas
│  │  ├─ Panel DialogBox
│  │  ├─ Text DialogText
│  │  ├─ Button NextButton
│  │  └─ Image NPCPortrait
│  │
│  ├─ GameplayUICanvas
│  │  ├─ SI EXTINTOR:
│  │  │  ├─ Text Fires Remaining
│  │  │  ├─ Timer
│  │  │  └─ Progress Bar
│  │  │
│  │  └─ SI SISMO:
│  │     ├─ Timer (Temblor restante)
│  │     └─ Safety Indicator
│  │
│  └─ ResultsCanvas (inicialmente OFF)
│     ├─ Panel ResultsPanel
│     ├─ Text Score
│     ├─ Text Time
│     ├─ Text Feedback
│     ├─ Button Retry
│     └─ Button BackToLobby
│
└─ GameManager (referencia del Singleton)
```

---

# PARTE 4: FLUJO DETALLADO - LOBBY

## FASE 1: INICIALIZACIÓN LOBBY

### Qué ocurre cuando entras a LOBBY

```
1. Escena LOBBY carga
2. GameManager se crea (si no existe)
3. LobbyManager busca UI Canvas
4. SelectionUIController muestra paneles de selección
5. Player aparece frente a Canvas
6. Se aguarda input del usuario
```

### Script: LobbyManager.cs

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Button[] extintorButtons;  // A, B, C, Random
    public Button[] sismoButtons;     // A, B, C, Random
    
    void Start()
    {
        // Asignar listeners a botones Extintor
        extintorButtons[0].onClick.AddListener(() => SelectCourse("Extintor", "A"));
        extintorButtons[1].onClick.AddListener(() => SelectCourse("Extintor", "B"));
        extintorButtons[2].onClick.AddListener(() => SelectCourse("Extintor", "C"));
        extintorButtons[3].onClick.AddListener(() => SelectCourse("Extintor", "Random"));
        
        // Asignar listeners a botones Sismo
        sismoButtons[0].onClick.AddListener(() => SelectCourse("Sismo", "A"));
        sismoButtons[1].onClick.AddListener(() => SelectCourse("Sismo", "B"));
        sismoButtons[2].onClick.AddListener(() => SelectCourse("Sismo", "C"));
        sismoButtons[3].onClick.AddListener(() => SelectCourse("Sismo", "Random"));
    }
    
    void SelectCourse(string courseName, string difficulty)
    {
        // Guardar en GameManager
        GameManager.instance.selectedCourse = courseName;
        GameManager.instance.difficulty = (difficulty == "Random") 
            ? GetRandomDifficulty() 
            : difficulty;
        
        Debug.Log($"Curso seleccionado: {courseName} - Dificultad: {GameManager.instance.difficulty}");
        
        // Cargar escena
        SceneManager.LoadScene("SalaDeClase");
    }
    
    string GetRandomDifficulty()
    {
        int random = Random.Range(0, 3);
        return random == 0 ? "A" : (random == 1 ? "B" : "C");
    }
}
```

---

# PARTE 5: FLUJO DETALLADO - ENTRADA A SALA DE CLASE

## FASE 2: INICIALIZACIÓN SALA DE CLASE

### Qué ocurre cuando carga SalaDeClase

```
1. Escena carga
2. GameManager.instance se recupera
3. Se lee: selectedCourse y difficulty
4. Sistema CONDICIONAL se activa:

   ├─ SI selectedCourse == "Extintor"
   │  └─ Se instancia FireGameManager
   │  └─ Se preparan fuegos (pero NO aparecen aún)
   │  └─ Se prepara Extintor
   │
   └─ SI selectedCourse == "Sismo"
      └─ Se instancia EarthquakeManager
      └─ Se prepara mesa
      └─ Se preparan escombros (pero NO caen aún)

5. NPCProfessor se inicializa
6. Canvas de diálogos se muestra
7. Profesor dice monólogo inicial
8. Se aguarda click en "Siguiente"
```

### Script: SalaDeClaseManager.cs

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalaDeClaseManager : MonoBehaviour
{
    public GameObject fireGameManagerPrefab;
    public GameObject earthquakeManagerPrefab;
    public NPCProfessor professor;
    
    void Start()
    {
        string course = GameManager.instance.selectedCourse;
        string difficulty = GameManager.instance.difficulty;
        
        Debug.Log($"Iniciando: {course} - {difficulty}");
        
        if (course == "Extintor")
        {
            InitializeExtintorCourse(difficulty);
        }
        else if (course == "Sismo")
        {
            InitializeEarthquakeCourse(difficulty);
        }
        
        // Iniciar diálogos
        professor.StartIntroduction();
    }
    
    void InitializeExtintorCourse(string difficulty)
    {
        var fireGame = Instantiate(fireGameManagerPrefab);
        var fireManager = fireGame.GetComponent<FireGameManager>();
        fireManager.SetDifficulty(difficulty);
        // NO iniciar juego aún
    }
    
    void InitializeEarthquakeCourse(string difficulty)
    {
        var earthquakeGame = Instantiate(earthquakeManagerPrefab);
        var earthquakeManager = earthquakeGame.GetComponent<EarthquakeManager>();
        earthquakeManager.SetDifficulty(difficulty);
        // NO iniciar temblor aún
    }
}
```

---

# PARTE 6: SISTEMA DE DIÁLOGOS

## FASE 3: DIÁLOGOS CON NPC

### Estructura de Diálogos

```csharp
[System.Serializable]
public class DialogueSequence
{
    public string[] lines;        // Líneas a mostrar
    public string type;           // "Intro", "PreGame", "PostGame", "Evacuation"
    public int expectedClicks;    // Cuántos "Siguiente" espera
}

// En NPCProfessor.cs
private DialogueSequence[] dialogues = new DialogueSequence[]
{
    // EXTINTOR - INTRO
    new DialogueSequence 
    {
        type = "Intro_Extintor",
        lines = new string[]
        {
            "Hola estudiantes, hoy aprenderemos a usar un extintor",
            "Es muy importante saber cómo actuar en caso de incendio",
            "Vamos a practicar: Aquí hay un fuego pequeño",
            "Intenta apagarlo usando el extintor",
            "Presiona siguiente cuando estés listo"
        }
    },
    
    // EXTINTOR - AFTER FIRST FIRE
    new DialogueSequence 
    {
        type = "PostGame_Extintor",
        lines = new string[]
        {
            "¡Excelente! Apagaste el fuego",
            "Ahora vamos a complicarlo un poco",
            "Habrá múltiples fuegos esta vez",
            "¿Estás listo? Presiona siguiente"
        }
    },
    
    // SISMO - INTRO
    new DialogueSequence 
    {
        type = "Intro_Sismo",
        lines = new string[]
        {
            "Alumnos, hoy aprenderemos qué hacer durante un terremoto",
            "El procedimiento es: Drop, Cover, Hold On",
            "Primero, nos colocamos bajo una mesa",
            "Ve a la mesa y cúbrete",
            "Presiona siguiente cuando estés listo"
        }
    },
    
    // SISMO - AFTER EARTHQUAKE
    new DialogueSequence 
    {
        type = "PostGame_Sismo",
        lines = new string[]
        {
            "Bien hecho! El terremoto ha cesado",
            "Ahora debemos evacuar ordenadamente",
            "Camina hacia la puerta sin empujar a otros",
            "Vamos, presiona siguiente"
        }
    }
};
```

### Script: NPCProfessor.cs

```csharp
using UnityEngine;
using UnityEngine.UI;

public class NPCProfessor : MonoBehaviour
{
    public Text dialogueText;
    public Button nextButton;
    public Image npcPortrait;
    
    private DialogueSequence currentSequence;
    private int currentLineIndex = 0;
    private int clickCount = 0;
    
    void Start()
    {
        nextButton.onClick.AddListener(OnNextClicked);
    }
    
    public void StartIntroduction()
    {
        string course = GameManager.instance.selectedCourse;
        currentSequence = GetDialogueSequence($"Intro_{course}");
        currentLineIndex = 0;
        clickCount = 0;
        
        ShowNextLine();
    }
    
    void OnNextClicked()
    {
        clickCount++;
        
        if (currentLineIndex < currentSequence.lines.Length - 1)
        {
            // Más líneas
            currentLineIndex++;
            ShowNextLine();
        }
        else
        {
            // Fin del diálogo, iniciar juego
            EndDialogueSequence();
        }
    }
    
    void ShowNextLine()
    {
        dialogueText.text = currentSequence.lines[currentLineIndex];
    }
    
    void EndDialogueSequence()
    {
        string course = GameManager.instance.selectedCourse;
        
        if (currentSequence.type.Contains("Intro"))
        {
            // Iniciar juego
            if (course == "Extintor")
                StartCoroutine(StartExtintorGame());
            else
                StartCoroutine(StartEarthquakeGame());
        }
        else if (currentSequence.type.Contains("PostGame"))
        {
            // Continuar a siguiente fase
            if (course == "Extintor")
                StartCoroutine(ContinueExtintorGame());
            else
                StartCoroutine(ContinueEarthquakeGame());
        }
    }
    
    DialogueSequence GetDialogueSequence(string type)
    {
        // Buscar en array según type
        foreach (var seq in dialogues)
        {
            if (seq.type == type)
                return seq;
        }
        return null;
    }
    
    IEnumerator StartExtintorGame() { /* ... */ }
    IEnumerator StartEarthquakeGame() { /* ... */ }
    IEnumerator ContinueExtintorGame() { /* ... */ }
    IEnumerator ContinueEarthquakeGame() { /* ... */ }
}
```

---

# PARTE 7: CURSO DE EXTINTOR - DETALLADO

## FASE 4A: PRIMER FUEGO (ENTRENAMIENTO)

### Flujo Extintor - Primera Parte

```
1. Usuario ve Canvas de diálogos
2. Profesor dice introducción (5 líneas)
3. Usuario presiona "Siguiente"
4. Canvas desaparece
5. Aparece UN fuego en posición fija
6. Aparece Extintor cerca del usuario
7. Usuario agarraExtintor con mano IZQ
8. Usuario presiona boquilla con mano DER
9. Espuma sale y colisiona con fuego
10. Fuego se va apagando (partículas disminuyen)
11. Cuando intensidad = 0:
    ├─ Fuego desaparece
    ├─ Canvas reaparece
    ├─ Profesor habla (elogio)
    ├─ Usuario presiona "Siguiente"
    └─ Ir a FASE 4B

Tiempo máximo: 2 minutos (después auto-completa)
```

### Script: FireGameManager.cs (PARTE 1)

```csharp
using UnityEngine;
using System.Collections;

public class FireGameManager : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform[] fireSpawns;  // Posiciones de fuegos
    public GameObject extintorPrefab;
    public Text uiTimerText;
    public Text uiFiresRemainingText;
    
    private string difficulty;
    private int firesRemaining;
    private float gameTimer;
    private GameObject currentFire;
    private bool gameActive = false;
    
    void Start()
    {
        // NO empezar aún
        gameActive = false;
    }
    
    public void SetDifficulty(string diff)
    {
        difficulty = diff;
        // Configurar según dificultad
        // A = 1 fuego, B = 2 fuegos, C = 3 fuegos
    }
    
    public void StartFirstFire()
    {
        gameActive = true;
        StartCoroutine(FirstFirePhase());
    }
    
    IEnumerator FirstFirePhase()
    {
        // Instanciar extintor
        var extintor = Instantiate(extintorPrefab, 
            new Vector3(0, 1, 1), Quaternion.identity);
        
        // Esperar pequeño delay
        yield return new WaitForSeconds(0.5f);
        
        // Aparecer fuego único
        currentFire = Instantiate(firePrefab, fireSpawns[0].position, Quaternion.identity);
        var fireScript = currentFire.GetComponent<FireBehavior>();
        
        // Esperar a que se apague
        while (fireScript.currentIntensity > 0)
        {
            gameTimer += Time.deltaTime;
            uiTimerText.text = $"Tiempo: {gameTimer:F1}s";
            yield return null;
        }
        
        // Fuego apagado
        gameActive = false;
        yield return new WaitForSeconds(0.5f);
        
        // Llamar a profesor
        var professor = FindObjectOfType<NPCProfessor>();
        professor.ShowPostFirstFireDialogue();
    }
}
```

---

## FASE 4B: MÚLTIPLES FUEGOS (MINIJUEGO)

### Flujo Extintor - Segunda Parte

```
1. Usuario ve Canvas de diálogos (post primer fuego)
2. Profesor dice: "Ahora haremos más difícil"
3. Usuario presiona "Siguiente"
4. Canvas desaparece
5. Aparecen MÚLTIPLES fuegos
   ├─ Dificultad A: 2 fuegos
   ├─ Dificultad B: 3 fuegos
   └─ Dificultad C: 4 fuegos
6. UI muestra: "Fuegos: 3/3" (restantes/total)
7. Usuario apaga fuegos uno por uno
8. UI actualiza: "Fuegos: 2/3" → "Fuegos: 1/3" → "Fuegos: 0/3"
9. Cuando todos = 0:
   ├─ Canvas reaparece
   ├─ Se calcula puntuación
   ├─ Se muestra RESULTS Canvas
   └─ Usuario ve botones: Reintentar / Volver a Lobby

Tiempo máximo: 5 minutos
Puntuación: 100 * (fuegos apagados) - (segundos * 0.5)
```

### Script: FireGameManager.cs (PARTE 2)

```csharp
public void StartMultipleFires()
{
    gameActive = true;
    StartCoroutine(MultipleFiresPhase());
}

IEnumerator MultipleFiresPhase()
{
    // Determinar cantidad de fuegos según dificultad
    int fireCount = difficulty == "A" ? 2 : (difficulty == "B" ? 3 : 4);
    firesRemaining = fireCount;
    
    // Instanciar fuegos en diferentes posiciones
    GameObject[] fires = new GameObject[fireCount];
    for (int i = 0; i < fireCount; i++)
    {
        fires[i] = Instantiate(firePrefab, fireSpawns[i].position, Quaternion.identity);
    }
    
    // Monitorear fuegos
    gameTimer = 0;
    while (firesRemaining > 0)
    {
        gameTimer += Time.deltaTime;
        
        // Contar fuegos activos
        firesRemaining = 0;
        foreach (var fire in fires)
        {
            if (fire != null && fire.GetComponent<FireBehavior>().currentIntensity > 0)
                firesRemaining++;
        }
        
        // Actualizar UI
        uiFiresRemainingText.text = $"Fuegos: {firesRemaining}/{fireCount}";
        uiTimerText.text = $"Tiempo: {gameTimer:F1}s";
        
        // Check timeout
        if (gameTimer > 300) // 5 minutos
            break;
        
        yield return null;
    }
    
    // Juego terminado
    gameActive = false;
    CalculateAndShowResults();
}

void CalculateAndShowResults()
{
    int score = 100 * (difficulty == "A" ? 2 : (difficulty == "B" ? 3 : 4));
    score -= (int)(gameTimer * 0.5f);
    
    GameManager.instance.score = score;
    GameManager.instance.time = gameTimer;
    
    ShowResultsCanvas();
}
```

---

# PARTE 8: CURSO DE SISMO - DETALLADO

## FASE 5A: TEMBLOR Y REFUGIO

### Flujo Sismo - Primera Parte

```
1. Usuario ve Canvas de diálogos
2. Profesor dice introducción (5 líneas sobre terremoto)
3. Usuario presiona "Siguiente"
4. Canvas desaparece
5. COMIENZA TEMBLOR:
   ├─ Cámara tiembla (posición + rotación)
   ├─ Sonido de temblor
   ├─ Caen escombros (debris)
   ├─ Duración: 20-30 segundos según dificultad
6. Usuario debe:
   ├─ Dirigirse a la MESA
   ├─ Colocarse DEBAJO de la mesa
   ├─ Esperar a que termine el temblor
7. Sistema detecta si usuario está BAJO mesa:
   ├─ SI está bajo mesa: +50 puntos de seguridad
   ├─ SI está fuera: -10 puntos por cada impacto de debris
8. Cuando termina temblor:
   ├─ Cámara se estabiliza
   ├─ Escombros paran
   ├─ Canvas reaparece
   ├─ Profesor habla (instrucciones de evacuación)
   ├─ Usuario presiona "Siguiente"
   └─ Ir a FASE 5B

Tiempo máximo: Dificultad A=20s, B=25s, C=30s
```

### Script: EarthquakeManager.cs

```csharp
using UnityEngine;
using System.Collections;

public class EarthquakeManager : MonoBehaviour
{
    public Transform playerCameraTransform;
    public DebrisSpawner debrisSpawner;
    public AudioSource earthquakeAudio;
    public GameObject tablePrefab;
    
    private string difficulty;
    private float earthquakeDuration;
    private int safetyScore = 0;
    private bool isUnderTable = false;
    
    void Start()
    {
        // NO empezar aún
    }
    
    public void SetDifficulty(string diff)
    {
        difficulty = diff;
        earthquakeDuration = diff == "A" ? 20f : (diff == "B" ? 25f : 30f);
    }
    
    public void StartEarthquake()
    {
        StartCoroutine(EarthquakeSequence());
    }
    
    IEnumerator EarthquakeSequence()
    {
        // Instanciar mesa
        var table = Instantiate(tablePrefab, new Vector3(0, 0, 3), Quaternion.identity);
        var tableCollider = table.GetComponent<TableSafetyZone>();
        tableCollider.onPlayerUnderTable += OnPlayerUnderTable;
        tableCollider.onPlayerOutsideTable += OnPlayerOutsideTable;
        tableCollider.onDebrisHit += OnDebrisHit;
        
        // Empezar temblor
        earthquakeAudio.Play();
        
        float elapsedTime = 0;
        while (elapsedTime < earthquakeDuration)
        {
            // Agitar cámara
            playerCameraTransform.localPosition += 
                new Vector3(Random.Range(-0.05f, 0.05f), 
                           Random.Range(-0.05f, 0.05f), 
                           0);
            
            playerCameraTransform.localRotation *= 
                Quaternion.Euler(Random.Range(-1f, 1f), 
                                Random.Range(-1f, 1f), 
                                Random.Range(-0.5f, 0.5f));
            
            // Spawnear debris
            debrisSpawner.SpawnDebris(new Vector3(Random.Range(-5, 5), 5, Random.Range(-5, 5)));
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Temblor termina
        earthquakeAudio.Stop();
        playerCameraTransform.localPosition = Vector3.zero;
        playerCameraTransform.localRotation = Quaternion.identity;
        
        yield return new WaitForSeconds(1f);
        
        // Mostrar diálogos de evacuación
        var professor = FindObjectOfType<NPCProfessor>();
        professor.ShowEvacuationDialogue();
    }
    
    void OnPlayerUnderTable()
    {
        isUnderTable = true;
        safetyScore += 50;
        Debug.Log($"¡Seguro bajo la mesa! Puntos: {safetyScore}");
    }
    
    void OnPlayerOutsideTable()
    {
        isUnderTable = false;
    }
    
    void OnDebrisHit()
    {
        if (!isUnderTable)
        {
            safetyScore -= 10;
            Debug.Log($"¡Impactado por escombros! Puntos: {safetyScore}");
        }
    }
}

// Script para detectar si jugador está bajo mesa
public class TableSafetyZone : MonoBehaviour
{
    public delegate void PlayerEvent();
    public event PlayerEvent onPlayerUnderTable;
    public event PlayerEvent onPlayerOutsideTable;
    public event PlayerEvent onDebrisHit;
    
    private bool playerInside = false;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            onPlayerUnderTable?.Invoke();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            onPlayerOutsideTable?.Invoke();
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Debris"))
        {
            onDebrisHit?.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
```

---

## FASE 5B: EVACUACIÓN

### Flujo Sismo - Segunda Parte

```
1. Temblor termina, profesor habla
2. Usuario presiona "Siguiente"
3. Canvas desaparece
4. Aparecen ALUMNOS NPC caminando hacia salida
5. Sistema genera RUTA A SALIDA:
   ├─ Puertas se abren
   ├─ Markers de ruta visible para usuario
6. Usuario debe:
   ├─ Seguir la ruta
   ├─ Llegar a la puerta SIN colisionar con alumnos
   ├─ Colisionar = -5 puntos por colisión
7. Cuando cruza puerta:
   ├─ Evacuación completada
   ├─ Canvas reaparece
   ├─ Se calcula puntuación final
   ├─ Se muestra RESULTS Canvas
   └─ Usuario ve botones: Reintentar / Volver a Lobby

Tiempo máximo: 3 minutos
Puntuación Final: safetyScore + evacuationBonus - collisionPenalties
```

### Script: EvacuationManager.cs

```csharp
using UnityEngine;
using System.Collections;

public class EvacuationManager : MonoBehaviour
{
    public Transform playerTransform;
    public Transform[] doorExits;
    public GameObject studentNPCPrefab;
    public int studentCount = 5;
    
    private int collisionCount = 0;
    private float evacuationTimer;
    private bool evacuationComplete = false;
    
    public void StartEvacuation()
    {
        StartCoroutine(EvacuationSequence());
    }
    
    IEnumerator EvacuationSequence()
    {
        // Instanciar estudiantes caminando
        for (int i = 0; i < studentCount; i++)
        {
            var studentPos = new Vector3(Random.Range(-2, 2), 0, Random.Range(0, 3));
            var student = Instantiate(studentNPCPrefab, studentPos, Quaternion.identity);
            var studentAI = student.GetComponent<StudentNPCAI>();
            studentAI.targetDoor = doorExits[Random.Range(0, doorExits.Length)];
            studentAI.onCollisionWithPlayer += OnStudentCollision;
        }
        
        // Esperar a que usuario llegue a la puerta
        evacuationTimer = 0;
        while (!evacuationComplete && evacuationTimer < 180) // 3 minutos max
        {
            // Detectar si usuario está cerca de puerta
            foreach (var door in doorExits)
            {
                if (Vector3.Distance(playerTransform.position, door.position) < 1f)
                {
                    evacuationComplete = true;
                    break;
                }
            }
            
            evacuationTimer += Time.deltaTime;
            yield return null;
        }
        
        // Evacuación completada
        yield return new WaitForSeconds(1f);
        ShowResults();
    }
    
    void OnStudentCollision()
    {
        collisionCount++;
        Debug.Log($"Colisión con estudiante! Total: {collisionCount}");
    }
    
    void ShowResults()
    {
        int safetyScore = FindObjectOfType<EarthquakeManager>().safetyScore;
        int evacuationBonus = evacuationComplete ? 50 : 0;
        int finalScore = safetyScore + evacuationBonus - (collisionCount * 5);
        
        GameManager.instance.score = finalScore;
        GameManager.instance.time = evacuationTimer;
        
        FindObjectOfType<NPCProfessor>().ShowResultsCanvas();
    }
}
```

---

# PARTE 9: SISTEMA DE RESULTADOS

## FASE 6: MOSTRAR RESULTADOS

### Canvas de Resultados

```
┌─────────────────────────────────┐
│      RESULTADOS FINALES         │
│─────────────────────────────────│
│                                 │
│  Puntuación: 385                │
│  Tiempo: 45.3 segundos          │
│                                 │
│  Calificación: EXCELENTE        │
│  (Basada en puntuación)         │
│                                 │
│  Feedback:                      │
│  "¡Apagaste todos los fuegos!"  │
│                                 │
│  ┌─────────────┬──────────────┐ │
│  │  Reintentar │ Volver Lobby │ │
│  └─────────────┴──────────────┘ │
└─────────────────────────────────┘
```

### Script: ResultsUIController.cs

```csharp
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsUIController : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public Text ratingText;
    public Text feedbackText;
    public Button retryButton;
    public Button lobbyButton;
    
    public void ShowResults()
    {
        int score = GameManager.instance.score;
        float time = GameManager.instance.time;
        string course = GameManager.instance.selectedCourse;
        
        // Actualizar UI
        scoreText.text = $"Puntuación: {score}";
        timeText.text = $"Tiempo: {time:F1}s";
        
        // Calificación
        string rating;
        string feedback;
        
        if (course == "Extintor")
        {
            if (score >= 300)
            {
                rating = "EXCELENTE";
                feedback = "¡Apagaste todos los fuegos perfctamente!";
            }
            else if (score >= 200)
            {
                rating = "BUENO";
                feedback = "Buen trabajo, pero puedes mejorar tu tiempo.";
            }
            else
            {
                rating = "NECESITA MEJORAR";
                feedback = "Sigue practicando.";
            }
        }
        else // Sismo
        {
            if (score >= 250)
            {
                rating = "EXCELENTE";
                feedback = "¡Evacuaste con seguridad!";
            }
            else if (score >= 150)
            {
                rating = "BUENO";
                feedback = "Bien, pero protégete mejor del debris.";
            }
            else
            {
                rating = "NECESITA MEJORAR";
                feedback = "Debes mejorar tu técnica de seguridad.";
            }
        }
        
        ratingText.text = rating;
        feedbackText.text = feedback;
        
        // Listeners
        retryButton.onClick.AddListener(RetryCurrentCourse);
        lobbyButton.onClick.AddListener(BackToLobby);
    }
    
    void RetryCurrentCourse()
    {
        SceneManager.LoadScene("SalaDeClase");
    }
    
    void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
```

---

# PARTE 10: CHECKLIST DE IMPLEMENTACIÓN

## 📋 ORDEN DE DESARROLLO RECOMENDADO

### SEMANA 1: Setup Base

```
☐ 1. Crear GameManager.cs (Singleton)
☐ 2. Crear escena LOBBY
   ├─ Importar modelo Kansai University
   ├─ Crear Canvas de selección
   ├─ Crear LobbyManager.cs
   └─ Testear carga de escena

☐ 3. Crear escena SALA DE CLASE
   ├─ Setup básico (luz, suelo)
   ├─ Crear prefab NPC Profesor
   ├─ Crear Canvas de diálogos
   └─ Testear diálogos básicos
```

### SEMANA 2: Curso Extintor

```
☐ 1. Implementar sistema de fuegos
   ├─ Crear Prefab Fire
   ├─ FireBehavior.cs ya existe ✓
   ├─ Crear partículas
   └─ Testear apagado

☐ 2. FireGameManager.cs
   ├─ Primer fuego (entrenamiento)
   ├─ Múltiples fuegos (minijuego)
   ├─ UI de contadores
   └─ Testear ambas fases

☐ 3. NPCProfessor - Diálogos Extintor
   ├─ Intro
   ├─ Post primer fuego
   └─ Testear flujo completo
```

### SEMANA 3: Curso Sismo

```
☐ 1. Implementar temblor
   ├─ EarthquakeManager.cs
   ├─ Agitar cámara
   ├─ Sonido
   └─ Testear

☐ 2. Implementar debris y mesa
   ├─ Crear Prefab Debris
   ├─ DebrisSpawner.cs
   ├─ TableSafetyZone.cs
   └─ Testear detección

☐ 3. Implementar evacuación
   ├─ EvacuationManager.cs
   ├─ Crear Prefab StudentNPC
   ├─ StudentNPCAI.cs
   ├─ Crear puertas
   └─ Testear flujo completo

☐ 4. NPCProfessor - Diálogos Sismo
   ├─ Intro
   ├─ Post temblor
   ├─ Instrucciones evacuación
   └─ Testear flujo completo
```

### SEMANA 4: Polish y Testing

```
☐ 1. ResultsUIController.cs
   ├─ Mostrar resultados
   ├─ Calificaciones
   ├─ Botones reintentar/lobby

☐ 2. Testing completo
   ├─ Flujo Extintor A/B/C
   ├─ Flujo Sismo A/B/C
   ├─ Transiciones entre escenas
   ├─ Errores de datos

☐ 3. UI Polish
   ├─ Mejorar textos
   ├─ Animaciones
   ├─ Efectos visuales

☐ 4. Performance
   ├─ Optimizar shaders
   ├─ Reducir Drawcalls
   ├─ Testear en VR
```

---

# PARTE 11: SCRIPTS NECESARIOS (RESUMEN)

```
CORE MANAGEMENT:
✓ GameManager.cs (SINGLETON)
✓ SalaDeClaseManager.cs
✓ LobbyManager.cs
✓ ResultsUIController.cs

NPC Y DIÁLOGOS:
✓ NPCProfessor.cs
✓ DialogueSequence.cs (datos)

EXTINTOR:
✓ FireGameManager.cs
✓ FireBehavior.cs (ya existe ✓)

SISMO:
✓ EarthquakeManager.cs
✓ DebrisSpawner.cs
✓ TableSafetyZone.cs
✓ StudentNPCAI.cs
✓ EvacuationManager.cs

UI:
✓ SelectionUIController.cs
✓ DialogUIController.cs
✓ GameplayUIController.cs
```

---

# PARTE 12: ESTRUCTURA DE PREFABS

```
PREFABS NECESARIOS:

1. Fire
   ├─ Mesh (esfera)
   ├─ Rigidbody
   ├─ Collider
   ├─ ParticleSystem
   ├─ Light
   └─ FireBehavior.cs

2. Debris
   ├─ Mesh (rock)
   ├─ Rigidbody (Dynamic)
   ├─ Collider
   └─ Tag: "Debris"

3. StudentNPC
   ├─ Mesh (humanoid model)
   ├─ Animator
   ├─ Collider
   ├─ NavMeshAgent
   └─ StudentNPCAI.cs

4. Table
   ├─ Mesh (mesa)
   ├─ Collider TriggerBOX (debajo)
   ├─ Collider Convex (arriba, para debris)
   └─ TableSafetyZone.cs

5. NPCProfessor
   ├─ Mesh (modelo profesor)
   ├─ Animator
   ├─ Collider
   └─ NPCProfessor.cs

6. ExtintorPrincipal
   ├─ CuerpoExtintor (ya existe ✓)
   ├─ BoquillaExtintor (ya existe ✓)
   ├─ ExtintorController.cs (ya existe ✓)
   └─ BoquillaController.cs (ya existe ✓)
```

---

# CONCLUSIÓN

Este documento te proporciona:

✅ Arquitectura completa del proyecto
✅ Flujo paso a paso de cada curso
✅ Scripts necesarios (pseudocódigo funcional)
✅ Checklist de implementación
✅ Estructura de prefabs

**Próximos pasos:**
1. Leer esta guía completamente
2. Crear GameManager.cs como base
3. Implementar escena LOBBY
4. Testear carga de escenas
5. Seguir checklist semana por semana

**¡Éxito con tu proyecto! 🎓🔥🌍**

