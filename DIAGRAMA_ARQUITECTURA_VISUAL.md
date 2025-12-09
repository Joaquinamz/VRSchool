# 🏗️ DIAGRAMA VISUAL: ARQUITECTURA DEL PROYECTO

## FLUJO COMPLETO: LOBBY → LECCIÓN → RESULTADOS → VOLVER

```
┌─────────────────────────────────────────────────────────────────┐
│                          LOBBY SCENE                            │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Canvas/                                                        │
│  ├─ Button_Extintor1 → SimpleLobbyLoader(LoadCourse)           │
│  ├─ Button_Extintor2 → SimpleLobbyLoader(LoadCourse)           │
│  ├─ Button_Extintor3 → SimpleLobbyLoader(LoadCourse)           │
│  ├─ Button_Sismo1    → SimpleLobbyLoader(LoadCourse)           │
│  ├─ Button_Sismo2    → SimpleLobbyLoader(LoadCourse)           │
│  └─ Button_Sismo3    → SimpleLobbyLoader(LoadCourse)           │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                                 │
                                 │ Usuario presiona botón
                                 ↓
┌─────────────────────────────────────────────────────────────────┐
│                 FIRE EXTINGUISHER LESSON SCENE                  │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  NPCProfessor                                                   │
│  ├─ ShowIntroduction()  → Diálogos iniciales                  │
│  └─ ShowPostFirstFireDialogue() → Post-fuego                   │
│                                                                 │
│  FireGameManager (7 Fases)                                      │
│  ├─ NotStarted           ← Inicio                              │
│  ├─ Introduction         → Mostrando diálogos                  │
│  ├─ WaitingForFireSpawn  → Preparando spawn                    │
│  ├─ FirstFire            → Fuego activo, usuario lo apaga      │
│  ├─ WaitingForPostFireDialog → Esperando siguiente botón      │
│  ├─ Minigame             → Múltiples fuegos                    │
│  └─ Complete             → Fin                                 │
│                                                                 │
│  ExtintorController                                             │
│  └─ isFiring + spray damage → Daña fuego                       │
│                                                                 │
│  FireBehavior                                                   │
│  └─ currentIntensity-- → Se reduce cuando lo apagan            │
│                                                                 │
│  Canvas/                                                        │
│  ├─ TimerText    → Tiempo transcurrido                         │
│  ├─ StatusText   → "Apaga el fuego..."                         │
│  └─ Button_Return → SimpleLobbyLoader(ReturnToLobby)           │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                                 │
                                 │ Usuario apaga fuego
                                 │ + presiona Continuar × N veces
                                 │
                                 ↓
                         Lección Completada
                                 │
                                 ↓
            Usuario presiona "Volver a Lobby"
                                 │
                                 ↓
┌─────────────────────────────────────────────────────────────────┐
│              EARTHQUAKE LESSON SCENE (NUEVO)                    │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  EarthquakeProfessor                                            │
│  ├─ ShowIntroduction() → DROP, COVER, HOLD ON                 │
│  └─ ShowResults()      → Feedback final                        │
│                                                                 │
│  EarthquakeGameManager (7 Fases)                               │
│  ├─ NotStarted         ← Inicio                               │
│  ├─ Introduction       → Diálogos                              │
│  ├─ Earthquake_Starting → Iniciando shake                      │
│  ├─ Earthquake_Active  → Shake + Escombros cayendo            │
│  │  └─ T=0-3s: Solo shake                                      │
│  │  └─ T=3-30s: Escombros caen constantemente                 │
│  │     └─ Si jugador está BAJO MESA: Impactos NO cuentan ✓    │
│  │     └─ Si jugador está AFUERA: Impactos cuentan ✗          │
│  ├─ Earthquake_Ending → Finalizando                           │
│  └─ Complete           → Resultados                            │
│                                                                 │
│  DebrisSpawner                                                  │
│  ├─ StartSpawning()  → Comienza a spawnear escombros          │
│  ├─ SpawnDebris()    → Crea escombro cada 0.5s                │
│  └─ StopSpawning()   → Detiene al terminar                    │
│                                                                 │
│  DebrisHitDetector (en cada escombro)                           │
│  └─ OnTriggerEnter() → Registra impacto                        │
│                                                                 │
│  SafeZones (Mesas)                                              │
│  ├─ SafeZone_Table1  → Pos: (-3, 1, 0)                         │
│  └─ SafeZone_Table2  → Pos: (3, 1, 0)                          │
│                                                                 │
│  Canvas/                                                        │
│  ├─ TimerText       → "Tiempo: 23.5s"                          │
│  ├─ StatusText      → "¡¡TERREMOTO!!"                          │
│  ├─ HitCountText    → "Impactos: 5" (aumenta si no está seguro)│
│  ├─ ResultsCanvas   → Panel con resultados                    │
│  │  └─ ResultsFeedback → "Impactos: 5, Puntaje: 50/100"       │
│  └─ Button_Return   → SimpleLobbyLoader(ReturnToLobby)         │
│                                                                 │
│  Puntaje Sistema:                                              │
│  └─ Final = 100 - (Impactos × 10)                              │
│     └─ 0 impactos → 100 (EXCELENTE)                            │
│     └─ 3 impactos → 70 (BIEN)                                  │
│     └─ 5 impactos → 50 (ACEPTABLE)                             │
│     └─ 10+ impactos → 0 (NECESITA PRACTICAR)                   │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                                 │
                                 │ Usuario presiona "Volver"
                                 ↓
                         Regresa a LOBBY
```

---

## COMPONENTES POR ESCENA

### LOBBY
```
GameObject Structure:
├─ Canvas
│  ├─ Button_ExtintorA (SimpleLobbyLoader → FireExtinguisherLesson1)
│  ├─ Button_ExtintorB (SimpleLobbyLoader → FireExtinguisherLesson2)
│  ├─ Button_ExtintorC (SimpleLobbyLoader → FireExtinguisherLesson3)
│  ├─ Button_SismoA    (SimpleLobbyLoader → EarthquakeLesson1)
│  ├─ Button_SismoB    (SimpleLobbyLoader → EarthquakeLesson2)
│  └─ Button_SismoC    (SimpleLobbyLoader → EarthquakeLesson3)
├─ LobbyManager (referencia opcional)
└─ Lights, XR stuff, etc.
```

### FIRE EXTINGUISHER LESSON
```
GameObject Structure:
├─ Canvas
│  ├─ TimerText
│  ├─ StatusText
│  ├─ DialogueText
│  ├─ NextButton
│  └─ Button_Return (SimpleLobbyLoader)
│
├─ FireGameManager
│  ├─ Validations: firePrefab, professorController
│  ├─ Phases: 7 estados
│  └─ Invoke: SpawnPracticeFire() after 0.5s
│
├─ NPCProfessor
│  ├─ Diálogos (array)
│  └─ OnNextClicked() → CompleteIntroduction() o CompletePracticeFire()
│
├─ FireMinigameManager
│  └─ Múltiples fuegos
│
├─ ExtintorController
│  └─ isFiring, spray damage
│
└─ Lighting, Camera, etc.
```

### EARTHQUAKE LESSON
```
GameObject Structure:
├─ Canvas
│  ├─ TimerText
│  ├─ StatusText
│  ├─ HitCountText
│  ├─ DialogueText
│  ├─ NextButton
│  ├─ ResultsCanvas (Panel)
│  │  └─ ResultsFeedbackText
│  └─ Button_Return (SimpleLobbyLoader)
│
├─ EarthquakeGameManager
│  ├─ Validations: professorController, debrisSpawner
│  ├─ Phases: 7 estados
│  ├─ MainCamera referencia (para shake)
│  └─ Safe Zones array
│
├─ EarthquakeProfessor
│  ├─ Diálogos (array)
│  └─ ShowResults()
│
├─ DebrisSpawner
│  ├─ debrisPrefab asignado
│  ├─ Zona de spawn: (-10,-10,-10) a (10,10,10)
│  └─ Spawn rate: 2 escombros/seg
│
├─ SafeZone_Table1 (Collider + IsTrigger)
├─ SafeZone_Table2 (Collider + IsTrigger)
│
└─ Lighting, Camera, etc.
```

---

## LLAMADAS ENTRE SCRIPTS

### FIRE LESSON
```
Button (On Click)
  └─ SimpleLobbyLoader.OnButtonClick()
     └─ SceneManager.LoadScene(sceneName)

FireExtinguisherLesson1 carga:
  └─ FireGameManager.Start()
     └─ AutoFind: NPCProfessor, ValidatePrefabs
  
Usuario presiona Continuar:
  └─ NPCProfessor.OnNextClicked()
     └─ (última línea) gameController.CompleteIntroduction()
        └─ FireGameManager.CompleteIntroduction()
           └─ Invoke(0.5s) SpawnPracticeFire()
              └─ Instantiate(firePrefab)
                 └─ Fuego aparece (FirstFire phase)

Usuario apaga fuego:
  └─ ExtintorController.Update() → isFiring = true
     └─ FireBehavior.TakeDamage()
        └─ currentIntensity--
           └─ FireGameManager.CheckPracticeFireComplete()
              └─ currentIntensity <= 0
                 └─ CompletePracticeFire()
                    └─ NPCProfessor.ShowPostFirstFireDialogue()

Usuario presiona Continuar (post-fuego):
  └─ NPCProfessor.OnNextClicked() (última línea)
     └─ gameController.StartMinigame()
```

### EARTHQUAKE LESSON
```
Button (On Click)
  └─ SimpleLobbyLoader.OnButtonClick()
     └─ SceneManager.LoadScene("EarthquakeLesson1")

EarthquakeLesson1 carga:
  └─ EarthquakeGameManager.Start()
     └─ AutoFind: EarthquakeProfessor, DebrisSpawner
  
SceneStarter (o event):
  └─ EarthquakeGameManager.StartIntroduction()
     └─ EarthquakeProfessor.ShowIntroduction()

Usuario presiona Continuar:
  └─ EarthquakeProfessor.OnNextClicked()
     └─ (última línea) gameController.CompleteIntroduction()
        └─ EarthquakeGameManager.CompleteIntroduction()
           └─ Invoke(0.5s) StartEarthquakePhase()
              └─ currentPhase = Earthquake_Active
                 └─ Update() → earthquakeTimer += dt
                    └─ T >= 3s → DebrisSpawner.StartSpawning()
                       └─ Escombros caen
                    └─ T >= 30s → CompleteEarthquake()
                       └─ DebrisSpawner.StopSpawning()
                       └─ ShowResults()
                          └─ EarthquakeProfessor.ShowResults(hits, score)

Escombro toca jugador:
  └─ DebrisHitDetector.OnTriggerEnter()
     └─ EarthquakeGameManager.RegisterDebrisHit()
        └─ IsPlayerInSafeZone() ?
           ├─ YES → No aumenta totalHits ✓
           └─ NO → totalHits++ ✗
```

---

## SCRIPTS Y MÉTODOS CLAVE

### SimpleLobbyLoader
```csharp
public enum LoadMode { LoadCourse, ReturnToLobby }

// Llamado desde Button.OnClick
public void OnButtonClick()

// Internos
public void LoadCourse(string sceneName)
public void ReturnToLobby()

// Estáticos (opcional)
public static void LoadCourseStatic(string sceneName)
public static void ReturnToLobbyStatic()
```

### FireGameManager
```csharp
public enum GamePhase { 
    NotStarted, Introduction, WaitingForFireSpawn, 
    FirstFire, WaitingForPostFireDialog, Minigame, Complete 
}

void Start()                    // Validación de prefabs
void Update()                   // Manejo de fases
public void StartIntroduction() // Fase 1
public void CompleteIntroduction() // Usuario presiona Continuar
void SpawnPracticeFire()        // Spawn con validaciones
void CompletePracticeFire()     // Fuego apagado
public void StartMinigame()     // Múltiples fuegos
```

### EarthquakeGameManager
```csharp
public enum GamePhase { 
    NotStarted, Introduction, Earthquake_Starting, 
    Earthquake_Active, Earthquake_Ending, Complete 
}

void Start()                    // Validación
void Update()                   // Shake + Debris timer
public void StartIntroduction() // Fase 1
public void CompleteIntroduction() // Usuario presiona Continuar
void StartEarthquakePhase()     // Transición a activo
void CompleteEarthquake()       // 30 segundos terminados
void ShowResults()              // Mostrar puntaje
public void RegisterDebrisHit() // Escape registra impacto
bool IsPlayerInSafeZone()       // ¿Está bajo mesa?
```

### DebrisSpawner
```csharp
void Start()                // Validación prefab
void Update()               // Timer para spawn
void SpawnDebris()          // Crea escombro con Rigidbody
public void StartSpawning() // Comienza
public void StopSpawning()  // Detiene
public bool IsSpawning()    // ¿Está activo?
```

---

## DEBUGGING VISUAL

```
┌─ ESPERADO ─────────────────────────────────────────┐
│ [FireGameManager] ✓ Inicializado                  │
│ [FireGameManager] ✓ firePrefab está asignado      │
│ [FireGameManager] ✓ CompleteIntroduction()        │
│ [FireGameManager] 🔥 Spawneando fuego             │
│ [FireGameManager] ✓ Fuego instanciado            │
│ [FireGameManager] ✓✓✓ FUEGO LISTO               │
└────────────────────────────────────────────────────┘

┌─ SI FALLA ─────────────────────────────────────────┐
│ [FireGameManager] ❌ firePrefab NO ESTÁ ASIGNADO  │
│   → Solución: Asigna en Inspector                 │
│                                                   │
│ [FireGameManager] ❌ Fuego NO tiene FireBehavior  │
│   → Solución: Añade component al prefab           │
│                                                   │
│ [FireGameManager] ❌ TIMEOUT: Fuego no apareció   │
│   → Solución: Verifica Rigidbody y Collider       │
└────────────────────────────────────────────────────┘
```

---

¡Así es cómo funciona todo! 🎯

