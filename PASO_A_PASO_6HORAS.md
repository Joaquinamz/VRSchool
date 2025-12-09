# 🎮 GUÍA PASO A PASO: 6 HORAS PARA PROYECTO FUNCIONAL

**Objetivo Final:** Proyecto completamente funcional con cursos Extintor + Sismo

**Tiempo Total:** 6 horas (realista si sigues exactamente)

**Requisitos:** Unity 2022+ con XR Interaction Toolkit instalado

---

## ⚠️ PROBLEMA INICIAL: Errores al Abrir

**Síntoma:** Al abrir Unity, ves error "Missing Prefab with guid: 17b03574..."

**Solución Inmediata (5 minutos):**

1. En la carpeta Assets, encuentra la escena `1.unity`
2. Haz click derecho → Delete
3. Repite para `1FireExtinguisherLesson.unity` (si tiene error similar)
4. Cierra y reabre Unity
5. El error desaparecerá

**Por qué:** Esos prefabs tienen referencias rotas. No los necesitamos para el proyecto.

---

# FASE 1: SETUP INICIAL (30 MINUTOS)

## Paso 1.1: Crear GameManager (Singleton Global)

**QUÉ ES:** Un objeto que vive siempre y guarda datos entre escenas.

**EN EDITOR:**

1. Click derecho en Hierarchy (área vacía)
   - Selecciona `Create Empty` → Nombre: `GameManager`

2. Con `GameManager` seleccionado:
   - Inspector (derecha) → Click `Add Component`
   - Escribe: `GameManager`
   - Click en la sugerencia que aparece o crea script nuevo

3. En la carpeta Assets, crea archivo `GameManager.cs`:

```csharp
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public string selectedCourse = "";      // "Extintor" o "Sismo"
    public string selectedDifficulty = "";  // "A", "B", "C"
    public int score = 0;
    public float time = 0f;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ← IMPORTANTE: No destruye entre escenas
        }
        else
        {
            Destroy(gameObject);  // Si ya existe, destruir copia
        }
    }
}
```

4. Guarda el script (Ctrl+S)
5. Arrastra `GameManager.cs` al objeto `GameManager` en Hierarchy
   - Debe aparecer debajo del nombre

6. Con `GameManager` seleccionado, arrastra al botón "Play" de abajo
   - Esto lo hace un Prefab Global
   - Clic derecho en GameManager → Prefab → Create Prefab

---

## Paso 1.2: Crear Escena LOBBY

**QUÉ ES:** Primera escena donde el usuario elige curso.

**EN EDITOR:**

1. Arriba: File → New Scene (Basic)
2. Renombra: `Lobby` (en la pestaña de escena, click derecho → Rename)
3. Guarda: File → Save Scene as... → Carpeta `Scenes`, Nombre `Lobby.unity`

**Jerarquía de Lobby (lo que debe haber):**

```
Lobby (escena)
├─ Main Camera (rename de "Camera")
├─ GameManager (drag del prefab)
└─ Canvas
   ├─ Panel_Selection
   │  ├─ Button_Extintor_A
   │  ├─ Button_Extintor_B
   │  ├─ Button_Extintor_C
   │  ├─ Button_Extintor_Random
   │  ├─ Button_Sismo_A
   │  ├─ Button_Sismo_B
   │  ├─ Button_Sismo_C
   │  └─ Button_Sismo_Random
   └─ Image_Background
```

**CREAR ESTA JERARQUÍA:**

1. Click derecho en Hierarchy (vacío)
   - Create → UI → Canvas

2. En `Canvas`, click derecho
   - Create → UI → Panel (rename a `Panel_Selection`)

3. En `Panel_Selection`, clic derecho 8 veces
   - Cada vez: Create → UI → Button - TextMeshPro
   - Renombra: Button_Extintor_A, Button_Extintor_B, etc.

4. Resultado final: 8 botones bajo Panel_Selection

---

## Paso 1.3: Crear Script LobbyManager.cs

**EN EDITOR - Crear archivo:**

1. Carpeta Assets, click derecho
   - Create → C# Script → Nombre: `LobbyManager.cs`

2. Contenido del script:

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Button[] extintorButtons = new Button[4];  // A, B, C, Random
    public Button[] sismoButtons = new Button[4];     // A, B, C, Random
    
    void Start()
    {
        // Asignar listeners Extintor
        extintorButtons[0].onClick.AddListener(() => SelectCourse("Extintor", "A"));
        extintorButtons[1].onClick.AddListener(() => SelectCourse("Extintor", "B"));
        extintorButtons[2].onClick.AddListener(() => SelectCourse("Extintor", "C"));
        extintorButtons[3].onClick.AddListener(() => SelectCourse("Extintor", "Random"));
        
        // Asignar listeners Sismo
        sismoButtons[0].onClick.AddListener(() => SelectCourse("Sismo", "A"));
        sismoButtons[1].onClick.AddListener(() => SelectCourse("Sismo", "B"));
        sismoButtons[2].onClick.AddListener(() => SelectCourse("Sismo", "C"));
        sismoButtons[3].onClick.AddListener(() => SelectCourse("Sismo", "Random"));
    }
    
    void SelectCourse(string course, string difficulty)
    {
        // Guardar en GameManager
        GameManager.instance.selectedCourse = course;
        GameManager.instance.selectedDifficulty = 
            (difficulty == "Random") ? GetRandomDifficulty() : difficulty;
        
        Debug.Log($"Curso: {course}, Dificultad: {GameManager.instance.selectedDifficulty}");
        
        // Cargar siguiente escena
        SceneManager.LoadScene("ClassroomScene");
    }
    
    string GetRandomDifficulty()
    {
        int rand = Random.Range(0, 3);
        return rand == 0 ? "A" : (rand == 1 ? "B" : "C");
    }
}
```

3. EN EDITOR - Asignar Script:
   - Crea vacío (Create Empty)
   - Rename: `LobbyManager`
   - Drag `LobbyManager.cs` → Inspector
   - En Inspector, aparecerá:
     - Extintor Buttons (array tamaño 4)
     - Sismo Buttons (array tamaño 4)

4. Drag los 8 botones:
   - Element 0 de Extintor → Button_Extintor_A
   - Element 1 de Extintor → Button_Extintor_B
   - (etc., 8 total)

---

# FASE 2: CREAR ESCENA CLASSROOM (1 HORA 30 MIN)

## Paso 2.1: Crear Escena Vacía

1. File → New Scene (Basic)
2. Rename: `ClassroomScene`
3. File → Save as... → `Scenes/ClassroomScene.unity`

---

## Paso 2.2: Setup Básico Classroom

**EN EDITOR:**

1. Click derecho en Hierarchy (vacío)
   - 3D Object → Plane (rename: `Floor`)
   - Scale: X=10, Y=1, Z=10
   - Position: Y = -1

2. Click derecho
   - 3D Object → Cube (rename: `Walls`)
   - Position: Z = 5, Y = 2
   - Scale: X=10, Y=3, Z=0.5

3. Click derecho
   - Lighting → Create New Light (rename: `Directional Light`)
   - Rotation: X=50, Y=50, Z=0

4. Resultado:

```
ClassroomScene
├─ Main Camera (rename de Camera)
├─ GameManager (drag prefab)
├─ Floor (Plane)
├─ Walls (Cube)
└─ DirectionalLight
```

---

## Paso 2.3: Crear NPC Profesor

**EN EDITOR:**

1. Click derecho en Hierarchy
   - 3D Object → Capsule (rename: `Professor`)
   - Position: X=-3, Y=1, Z=3
   - Scale: X=0.5, Y=1.5, Z=0.5

2. Click derecho en `Professor`
   - Create → UI → Canvas (parent debe ser `Professor`)
   - Rename: `DialogueCanvas`

3. En `DialogueCanvas`, click derecho
   - Create → UI → Panel (rename: `DialoguePanel`)

4. En `DialoguePanel`, clic derecho
   - Create → UI → Text - TextMeshPro (rename: `DialogueText`)
   - Expande y centra texto

5. En `DialoguePanel`, click derecho
   - Create → UI → Button - TextMeshPro (rename: `NextButton`)
   - Coloca abajo del texto

**Resultado esperado:**

```
Professor (Capsule)
├─ Collider
└─ DialogueCanvas
   └─ DialoguePanel
      ├─ DialogueText
      └─ NextButton (con texto "Siguiente")
```

---

## Paso 2.4: Crear Script NPCProfessor.cs

**EN EDITOR - Crear archivo:**

```csharp
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NPCProfessor : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    
    private string[] currentDialogues;
    private int currentLineIndex = 0;
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = GameManager.instance;
        nextButton.onClick.AddListener(OnNextClicked);
        
        // Esperar un frame antes de mostrar diálogos
        StartCoroutine(ShowIntroductionAfterDelay());
    }
    
    IEnumerator ShowIntroductionAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        ShowIntroduction();
    }
    
    void ShowIntroduction()
    {
        if (gameManager.selectedCourse == "Extintor")
        {
            currentDialogues = new string[]
            {
                "Hola estudiantes, hoy aprenderemos a usar un extintor",
                "Es muy importante saber cómo actuar en caso de incendio",
                "Vamos a practicar: Aquí hay un fuego pequeño",
                "Intenta apagarlo usando el extintor",
                "Presiona siguiente cuando estés listo"
            };
        }
        else // Sismo
        {
            currentDialogues = new string[]
            {
                "Alumnos, hoy aprenderemos qué hacer durante un terremoto",
                "El procedimiento es: Drop, Cover, Hold On",
                "Primero, nos colocamos bajo una mesa",
                "Ve a la mesa y cúbrete",
                "Presiona siguiente cuando estés listo"
            };
        }
        
        currentLineIndex = 0;
        ShowNextLine();
    }
    
    void OnNextClicked()
    {
        if (currentLineIndex < currentDialogues.Length - 1)
        {
            currentLineIndex++;
            ShowNextLine();
        }
        else
        {
            // Fin del diálogo intro, iniciar juego
            EndIntroduction();
        }
    }
    
    void ShowNextLine()
    {
        dialogueText.text = currentDialogues[currentLineIndex];
    }
    
    void EndIntroduction()
    {
        Debug.Log("Iniciando juego...");
        dialogueText.text = "¡Juego iniciado!";
        
        if (gameManager.selectedCourse == "Extintor")
        {
            StartCoroutine(StartExtintorGame());
        }
        else
        {
            StartCoroutine(StartEarthquakeGame());
        }
    }
    
    IEnumerator StartExtintorGame()
    {
        yield return new WaitForSeconds(1f);
        // Aquí irá lógica de juego de extintor
        Debug.Log("Juego de Extintor iniciado");
    }
    
    IEnumerator StartEarthquakeGame()
    {
        yield return new WaitForSeconds(1f);
        // Aquí irá lógica de juego de terremoto
        Debug.Log("Juego de Terremoto iniciado");
    }
}
```

**EN EDITOR - Asignar Script:**

1. Selecciona `Professor` en Hierarchy
2. Add Component → Busca `NPCProfessor`
3. En Inspector:
   - Drag `DialogueText` → Dialogue Text
   - Drag `NextButton` → Next Button

---

# FASE 3: SISTEMA DE FUEGOS - EXTINTOR (1 HORA 30 MIN)

## Paso 3.1: Crear Prefab Fire

**EN EDITOR:**

1. Click derecho en Hierarchy
   - 3D Object → Sphere (rename: `Fire`)
   - Position: X=0, Y=1, Z=5
   - Scale: X=0.5, Y=0.5, Z=0.5

2. Con `Fire` seleccionado:
   - Add Component → Sphere Collider
   - Add Component → Rigidbody
     - Body Type: Dynamic
     - Gravity: ON

3. Crea material rojo:
   - Carpeta Assets, click derecho
   - Create → Material (rename: `FireMaterial`)
   - En Inspector: Albedo = Rojo
   - Drag al Sphere

4. Arrastra `Fire` de Hierarchy a carpeta Assets/Prefab
   - Automáticamente se convierte en Prefab
   - Borra la versión en Hierarchy

---

## Paso 3.2: Crear Script FireBehavior.cs (ACTUALIZADO)

**EN EDITOR - Crear archivo `FireBehavior.cs`:**

```csharp
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    [SerializeField] public float currentIntensity = 100f;
    [SerializeField] private float maxIntensity = 100f;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Light fireLight;
    
    void Start()
    {
        if (fireParticles == null)
            fireParticles = GetComponentInChildren<ParticleSystem>();
        
        if (fireLight == null)
            fireLight = GetComponentInChildren<Light>();
    }
    
    void Update()
    {
        // Mantener intensidad >= 0
        if (currentIntensity < 0) currentIntensity = 0;
        if (currentIntensity > maxIntensity) currentIntensity = maxIntensity;
        
        // Actualizar visuals
        UpdateVisuals();
    }
    
    public void TakeDamage(float damage)
    {
        currentIntensity -= damage;
        if (currentIntensity <= 0)
        {
            currentIntensity = 0;
            OnFireExtinguished();
        }
    }
    
    void UpdateVisuals()
    {
        // Escala según intensidad
        float scale = (currentIntensity / maxIntensity) * 0.5f;
        transform.localScale = new Vector3(scale, scale, scale);
        
        // Luz
        if (fireLight != null)
            fireLight.intensity = (currentIntensity / maxIntensity) * 2f;
        
        // Partículas
        if (fireParticles != null)
        {
            var emission = fireParticles.emission;
            emission.rateOverTime = (currentIntensity / maxIntensity) * 50f;
        }
        
        // Destruir si no hay intensidad
        if (currentIntensity <= 0)
            Destroy(gameObject, 0.5f);
    }
    
    void OnFireExtinguished()
    {
        Debug.Log("¡Fuego apagado!");
    }
}
```

**EN EDITOR - Asignar a Prefab Fire:**

1. Abre carpeta Assets/Prefab
2. Selecciona prefab `Fire`
3. Add Component → `FireBehavior`
4. Guarda (Ctrl+S)

---

## Paso 3.3: Crear Script FireGameManager.cs

```csharp
using UnityEngine;
using TMPro;
using System.Collections;

public class FireGameManager : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform fireSpawnPoint;
    public TextMeshProUGUI uiFiresRemaining;
    public TextMeshProUGUI uiTimer;
    
    private int firesRemaining = 0;
    private float gameTimer = 0f;
    private bool gameActive = false;
    
    public void StartFirstFire()
    {
        StartCoroutine(FirstFirePhase());
    }
    
    IEnumerator FirstFirePhase()
    {
        gameActive = true;
        gameTimer = 0f;
        
        // Instanciar 1 fuego
        Vector3 spawnPos = fireSpawnPoint != null ? fireSpawnPoint.position : new Vector3(0, 1, 5);
        var fire = Instantiate(firePrefab, spawnPos, Quaternion.identity);
        
        // Esperar a que se apague
        while (gameActive && fire != null)
        {
            gameTimer += Time.deltaTime;
            
            if (uiTimer != null)
                uiTimer.text = $"Tiempo: {gameTimer:F1}s";
            
            yield return null;
        }
        
        gameActive = false;
        
        // Llamar a profesor para siguiente diálogo
        var professor = FindObjectOfType<NPCProfessor>();
        if (professor != null)
            professor.ShowPostFirstFireDialogue();
    }
    
    public void StartMultipleFires(int count)
    {
        StartCoroutine(MultipleFiresPhase(count));
    }
    
    IEnumerator MultipleFiresPhase(int count)
    {
        gameActive = true;
        firesRemaining = count;
        gameTimer = 0f;
        
        Vector3[] spawnPositions = new Vector3[]
        {
            new Vector3(-2, 1, 5),
            new Vector3(0, 1, 5),
            new Vector3(2, 1, 5),
            new Vector3(0, 1, 7)
        };
        
        GameObject[] fires = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            fires[i] = Instantiate(firePrefab, spawnPositions[i], Quaternion.identity);
        }
        
        // Esperar a que todos se apaguen
        while (gameActive && firesRemaining > 0)
        {
            gameTimer += Time.deltaTime;
            
            // Contar fuegos
            firesRemaining = 0;
            foreach (var fire in fires)
            {
                if (fire != null && fire.GetComponent<FireBehavior>().currentIntensity > 0)
                    firesRemaining++;
            }
            
            if (uiFiresRemaining != null)
                uiFiresRemaining.text = $"Fuegos: {firesRemaining}/{count}";
            
            if (uiTimer != null)
                uiTimer.text = $"Tiempo: {gameTimer:F1}s";
            
            yield return null;
        }
        
        gameActive = false;
        
        // Mostrar resultados
        ShowResults(count);
    }
    
    void ShowResults(int totalFires)
    {
        int score = (100 * totalFires) - (int)(gameTimer * 0.5f);
        GameManager.instance.score = score;
        GameManager.instance.time = gameTimer;
        
        Debug.Log($"Juego terminado. Puntuación: {score}");
    }
}
```

**EN EDITOR - Crear objeto y asignar:**

1. Click derecho en Hierarchy (ClassroomScene)
   - Create Empty → Rename: `FireGameManager`

2. Selecciona `FireGameManager`
   - Add Component → `FireGameManager`

3. En Inspector:
   - Fire Prefab → Arrastra desde Assets/Prefab/Fire
   - Fire Spawn Point → (vacío por ahora)
   - Ui Fires Remaining → (crearemos Canvas pronto)
   - Ui Timer → (crearemos Canvas pronto)

---

## Paso 3.4: Crear Canvas de Gameplay

**EN EDITOR:**

1. Click derecho en Hierarchy
   - UI → Canvas (rename: `GameplayUI`)

2. En `GameplayUI`, click derecho
   - UI → Text - TextMeshPro (rename: `TimerText`)

3. En `GameplayUI`, click derecho
   - UI → Text - TextMeshPro (rename: `FiresText`)

4. Selecciona `FireGameManager`
   - En Inspector, Ui Timer → Drag `TimerText`
   - En Inspector, Ui Fires Remaining → Drag `FiresText`

---

# FASE 4: SISTEMA DE TERREMOTOS - SISMO (1 HORA 30 MIN)

## Paso 4.1: Crear Mesa (Prefab)

**EN EDITOR:**

1. Click derecho en Hierarchy
   - 3D Object → Cube (rename: `Table`)
   - Position: X=3, Y=0.5, Z=3
   - Scale: X=2, Y=0.2, Z=2 (mesa plana)

2. Click derecho en `Table`
   - 3D Object → Cube (rename: `TableLeg1`)
   - Position: X=-0.8, Y=-0.5, Z=-0.8
   - Scale: X=0.1, Y=0.5, Z=0.1

3. Duplica TableLeg1 (Ctrl+D) 3 veces para las 4 patas
   - Ajusta posiciones X y Z

**Resultado esperado:**

```
Table (Cube plano)
├─ TableLeg1 (columna)
├─ TableLeg2 (columna)
├─ TableLeg3 (columna)
└─ TableLeg4 (columna)
```

4. Selecciona `Table` (padre)
5. Con botón derecho, clic en Assets/Prefab
   - Drag Table → Crea Prefab
   - Borra de Hierarchy

---

## Paso 4.2: Crear Debris (Prefab)

**EN EDITOR:**

1. Click derecho en Hierarchy
   - 3D Object → Cube (rename: `Debris`)
   - Scale: X=0.3, Y=0.3, Z=0.3

2. Con `Debris` seleccionado:
   - Add Component → Rigidbody
     - Mass: 1
     - Gravity: ON

3. Drag a Assets/Prefab → Crea Prefab
4. Borra de Hierarchy

---

## Paso 4.3: Crear Script EarthquakeManager.cs

```csharp
using UnityEngine;
using TMPro;
using System.Collections;

public class EarthquakeManager : MonoBehaviour
{
    public Transform mainCamera;
    public GameObject debrisPrefab;
    public GameObject tablePrefab;
    public TextMeshProUGUI earthquakeTimer;
    
    private float earthquakeDuration = 25f;
    private int safetyScore = 100;
    private bool earthquakeActive = false;
    
    public void StartEarthquake(string difficulty)
    {
        earthquakeDuration = difficulty == "A" ? 20f : (difficulty == "B" ? 25f : 30f);
        StartCoroutine(EarthquakeSequence());
    }
    
    IEnumerator EarthquakeSequence()
    {
        earthquakeActive = true;
        
        // Instanciar mesa
        var table = Instantiate(tablePrefab, new Vector3(0, 0, 3), Quaternion.identity);
        
        float elapsedTime = 0;
        while (elapsedTime < earthquakeDuration)
        {
            // Agitar cámara
            if (mainCamera != null)
            {
                mainCamera.localPosition += new Vector3(
                    Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f),
                    0
                );
            }
            
            // Spawnear debris
            Vector3 debrisPos = new Vector3(Random.Range(-5, 5), 5, Random.Range(-5, 5));
            Instantiate(debrisPrefab, debrisPos, Quaternion.identity);
            
            elapsedTime += Time.deltaTime;
            
            if (earthquakeTimer != null)
                earthquakeTimer.text = $"Terremoto: {(earthquakeDuration - elapsedTime):F1}s";
            
            yield return null;
        }
        
        // Terremoto termina
        earthquakeActive = false;
        if (mainCamera != null)
            mainCamera.localPosition = Vector3.zero;
        
        Debug.Log("¡Terremoto terminado!");
        
        // Mostrar siguiente diálogo
        var professor = FindObjectOfType<NPCProfessor>();
        if (professor != null)
            professor.ShowEvacuationDialogue();
    }
}
```

**EN EDITOR - Asignar:**

1. Click derecho en Hierarchy
   - Create Empty → Rename: `EarthquakeManager`

2. Selecciona `EarthquakeManager`
   - Add Component → `EarthquakeManager`

3. En Inspector:
   - Main Camera → Drag Main Camera
   - Debris Prefab → Drag prefab de Assets/Prefab/Debris
   - Table Prefab → Drag prefab de Assets/Prefab/Table
   - Earthquake Timer → Drag `TimerText` de Canvas

---

# FASE 5: SISTEMA DE RESULTADOS (45 MIN)

## Paso 5.1: Crear Canvas de Resultados

**EN EDITOR:**

1. Click derecho en Hierarchy
   - UI → Canvas (rename: `ResultsUI`)
   - Inicialmente DESACTIVADO (click en checkbox)

2. En `ResultsUI`, click derecho:
   - UI → Panel (rename: `ResultsPanel`)

3. En `ResultsPanel`:
   - Clic derecho → UI → Text - TextMeshPro (rename: `ScoreText`)
   - Clic derecho → UI → Text - TextMeshPro (rename: `FeedbackText`)
   - Clic derecho → UI → Button - TextMeshPro (rename: `RetryButton`)
   - Clic derecho → UI → Button - TextMeshPro (rename: `LobbyButton`)

---

## Paso 5.2: Crear Script ResultsUIController.cs

```csharp
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsUIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI feedbackText;
    public Button retryButton;
    public Button lobbyButton;
    
    void Start()
    {
        retryButton.onClick.AddListener(RetryLevel);
        lobbyButton.onClick.AddListener(BackToLobby);
    }
    
    public void ShowResults()
    {
        int score = GameManager.instance.score;
        float time = GameManager.instance.time;
        string course = GameManager.instance.selectedCourse;
        
        scoreText.text = $"Puntuación: {score}\nTiempo: {time:F1}s";
        
        string feedback = "";
        if (score >= 300)
            feedback = "¡EXCELENTE! Trabajo fantástico.";
        else if (score >= 200)
            feedback = "BUENO. Puedes mejorar tu técnica.";
        else if (score >= 100)
            feedback = "ACEPTABLE. Necesitas práctica.";
        else
            feedback = "Necesitas mejorar. ¡Reintentar!";
        
        feedbackText.text = feedback;
        gameObject.SetActive(true);
    }
    
    void RetryLevel()
    {
        SceneManager.LoadScene("ClassroomScene");
    }
    
    void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
```

**EN EDITOR - Asignar:**

1. Selecciona `ResultsUI`
   - Add Component → `ResultsUIController`

2. En Inspector:
   - Score Text → Drag `ScoreText`
   - Feedback Text → Drag `FeedbackText`
   - Retry Button → Drag `RetryButton`
   - Lobby Button → Drag `LobbyButton`

---

# FASE 6: INTEGRACIÓN FINAL (1 HORA)

## Paso 6.1: Actualizar NPCProfessor.cs con Integraciones

**REEMPLAZA el contenido anterior con:**

```csharp
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NPCProfessor : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public GameObject dialogueCanvasGO;
    
    private string[] currentDialogues;
    private int currentLineIndex = 0;
    private GameManager gameManager;
    private FireGameManager fireGameManager;
    private EarthquakeManager earthquakeManager;
    private ResultsUIController resultsController;
    
    void Start()
    {
        gameManager = GameManager.instance;
        fireGameManager = FindObjectOfType<FireGameManager>();
        earthquakeManager = FindObjectOfType<EarthquakeManager>();
        resultsController = FindObjectOfType<ResultsUIController>();
        
        nextButton.onClick.AddListener(OnNextClicked);
        
        StartCoroutine(ShowIntroductionAfterDelay());
    }
    
    IEnumerator ShowIntroductionAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        ShowIntroduction();
    }
    
    void ShowIntroduction()
    {
        if (gameManager.selectedCourse == "Extintor")
        {
            currentDialogues = new string[]
            {
                "Hola estudiantes, hoy aprenderemos a usar un extintor",
                "Es muy importante saber cómo actuar en caso de incendio",
                "Vamos a practicar: Aquí hay un fuego pequeño",
                "Intenta apagarlo usando el extintor",
                "Presiona siguiente cuando estés listo"
            };
        }
        else
        {
            currentDialogues = new string[]
            {
                "Alumnos, hoy aprenderemos qué hacer durante un terremoto",
                "El procedimiento es: Drop, Cover, Hold On",
                "Primero, nos colocamos bajo una mesa",
                "Ve a la mesa y cúbrete",
                "Presiona siguiente cuando estés listo"
            };
        }
        
        currentLineIndex = 0;
        ShowNextLine();
    }
    
    void OnNextClicked()
    {
        if (currentLineIndex < currentDialogues.Length - 1)
        {
            currentLineIndex++;
            ShowNextLine();
        }
        else
        {
            EndIntroduction();
        }
    }
    
    void ShowNextLine()
    {
        dialogueText.text = currentDialogues[currentLineIndex];
    }
    
    void EndIntroduction()
    {
        // Ocultar diálogo
        if (dialogueCanvasGO != null)
            dialogueCanvasGO.SetActive(false);
        
        if (gameManager.selectedCourse == "Extintor")
        {
            if (fireGameManager != null)
                fireGameManager.StartFirstFire();
        }
        else
        {
            if (earthquakeManager != null)
                earthquakeManager.StartEarthquake(gameManager.selectedDifficulty);
        }
    }
    
    public void ShowPostFirstFireDialogue()
    {
        if (dialogueCanvasGO != null)
            dialogueCanvasGO.SetActive(true);
        
        currentDialogues = new string[]
        {
            "¡Excelente! Apagaste el fuego",
            "Ahora vamos a complicarlo",
            "Habrá múltiples fuegos",
            "¿Estás listo?",
            "¡Presiona siguiente!"
        };
        
        currentLineIndex = 0;
        ShowNextLine();
    }
    
    public void ShowEvacuationDialogue()
    {
        if (dialogueCanvasGO != null)
            dialogueCanvasGO.SetActive(true);
        
        currentDialogues = new string[]
        {
            "Bien hecho! El temblor ha cesado",
            "Ahora debemos evacuar",
            "Camina hacia la puerta",
            "¡Presiona siguiente para continuar!"
        };
        
        currentLineIndex = 0;
        ShowNextLine();
    }
}
```

---

## Paso 6.2: Conectar Escenas

**EN EDITOR:**

1. File → Build Settings
2. Scene in Build:
   - Click "Add Open Scenes"
   - Orden debe ser:
     - 0: Lobby
     - 1: ClassroomScene

3. Guardar (Ctrl+S)

---

## Paso 6.3: Setup de Dificultades en Classroom

**EN EDITOR - Actualizar FireGameManager.cs:**

Reemplaza el método `StartFirstFire()` con:

```csharp
public void StartFirstFire()
{
    StartCoroutine(FirstFirePhaseAndThen());
}

IEnumerator FirstFirePhaseAndThen()
{
    gameActive = true;
    gameTimer = 0f;
    
    Vector3 spawnPos = fireSpawnPoint != null ? fireSpawnPoint.position : new Vector3(0, 1, 5);
    var fire = Instantiate(firePrefab, spawnPos, Quaternion.identity);
    
    while (gameActive && fire != null && fire.GetComponent<FireBehavior>().currentIntensity > 0)
    {
        gameTimer += Time.deltaTime;
        if (uiTimer != null)
            uiTimer.text = $"Tiempo: {gameTimer:F1}s";
        yield return null;
    }
    
    gameActive = false;
    yield return new WaitForSeconds(1f);
    
    var professor = FindObjectOfType<NPCProfessor>();
    if (professor != null)
        professor.ShowPostFirstFireDialogue();
}
```

Agrega este método a `FireGameManager.cs`:

```csharp
public void ContinueToMultipleFires()
{
    int fireCount = 2;  // Default
    
    if (GameManager.instance.selectedDifficulty == "A")
        fireCount = 2;
    else if (GameManager.instance.selectedDifficulty == "B")
        fireCount = 3;
    else
        fireCount = 4;
    
    StartMultipleFires(fireCount);
}
```

---

## Paso 6.4: Conectar Botón "Siguiente" a Acción

**EN EDITOR - Actualizar NPCProfessor.cs:**

Agrega al final del método `OnNextClicked()`:

```csharp
void OnNextClicked()
{
    if (currentLineIndex < currentDialogues.Length - 1)
    {
        currentLineIndex++;
        ShowNextLine();
    }
    else
    {
        EndIntroduction();
        // ← NUEVA LÓGICA
        if (gameManager.selectedCourse == "Extintor" && 
            dialogueCanvasGO.activeSelf)
        {
            // Está en post primer fuego, ir a múltiples
            if (fireGameManager != null)
                fireGameManager.ContinueToMultipleFires();
            
            dialogueCanvasGO.SetActive(false);
        }
        else if (gameManager.selectedCourse == "Sismo")
        {
            // Está en evacuación, mostrar resultados
            if (resultsController != null)
                resultsController.ShowResults();
        }
    }
}
```

---

# FASE 7: TESTEAR Y DEPURAR (1 HORA)

## Paso 7.1: Play en Lobby

**EN EDITOR:**

1. Abre escena Lobby
2. Click Play (triángulo arriba)
3. Deberías ver:
   - Canvas con 8 botones
   - Puedes hacer click

4. Click en cualquier botón
   - Debería cargar ClassroomScene
   - Ver profesor (cápsula)
   - Ver diálogos

---

## Paso 7.2: Testear Extintor

1. En Lobby, click "Extintor A"
2. En Classroom:
   - Profesor habla (5 líneas)
   - Click "Siguiente" 5 veces
   - Debería aparecer UN fuego
   - Fire debe desaparecer cuando lo "apagues"
3. Profesor habla de nuevo
4. Click "Siguiente"
   - Múltiples fuegos aparecen
5. Cuando se apagan todos:
   - Resultados aparecen

---

## Paso 7.3: Testear Sismo

1. En Lobby, click "Sismo B"
2. En Classroom:
   - Profesor habla
   - Click "Siguiente" 5 veces
   - Cámara tiembla
   - Debris cae
   - Duración: 25 segundos
3. Profesor habla de evacuación
4. Click "Siguiente"
   - Resultados aparecen

---

# FASE 8: CONEXIÓN CON EXTINTOR (Opcional, 30 MIN)

## Paso 8.1: Integrar Extintor Existente

**EN EDITOR:**

1. Abre ClassroomScene
2. En Hierarchy, clic derecho
   - 3D Object → Create Empty (rename: `ExtintorHolder`)

3. En carpeta Assets, busca `ExtintorPrincipal` (prefab del extintor dual-hitbox)
4. Drag a Hierarchy dentro de ExtintorHolder

5. Configura posición:
   - Position: X=-3, Y=0, Z=4 (cerca del profesor)

---

## Paso 8.2: Hacer Que Extintor Apague Fuegos

**EN EDITOR - Crear script `ExtintorInteraction.cs`:**

```csharp
using UnityEngine;

public class ExtintorInteraction : MonoBehaviour
{
    public float damagePerSecond = 10f;
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            var fire = other.GetComponent<FireBehavior>();
            if (fire != null)
                fire.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
```

6. En Hierarchy, selecciona `BoquillaExtintor` dentro del extintor
7. Add Component → `ExtintorInteraction`
8. Agrega Collider tipo Sphere (IsTrigger: ON) si no existe

---

# RESUMEN: CHECKLIST FINAL

```
✅ HECHO            VERIFICAR
─────────────────────────────────
Setup Inicial:
☐ GameManager creado y Singleton
☐ Lobby escena con 8 botones
☐ ClassroomScene creada

Extintor:
☐ Fire Prefab con FireBehavior
☐ FireGameManager implementado
☐ Múltiples fuegos funcionan

Sismo:
☐ Temblor con agitación de cámara
☐ Debris cae
☐ Mesa aparece

Diálogos:
☐ NPC Profesor habla introducción
☐ Botón "Siguiente" funciona
☐ Transiciones entre fases

Resultados:
☐ Canvas de resultados aparece
☐ Puntuación se calcula
☐ Botones "Reintentar" y "Volver a Lobby" funcionan

Integración Completa:
☐ Lobby → Extintor A funciona
☐ Lobby → Sismo C funciona
☐ Resultados → Reintentar funciona
☐ Resultados → Volver a Lobby funciona
```

---

# SOLUCIÓN AL ERROR INICIAL

**El error "Missing Prefab":**

```
Causa: Archivo 1.unity tiene referencias a prefab que fue eliminado

Solución rápida:
1. Assets folder → Delete "1.unity"
2. Delete "1FireExtinguisherLesson.unity"
3. Restart Unity
4. Problema resuelto
```

**Para futuro:**
- Siempre mantener Prefabs en carpeta `Assets/Prefab`
- No mover prefabs sin usar "Refactor" de Unity
- Usar source control (GitHub/Git) para versioning

---

**¡Ahora sí! Sigue estos pasos exactamente y tendrás el proyecto funcional en 6 horas. 🎓🔥**

