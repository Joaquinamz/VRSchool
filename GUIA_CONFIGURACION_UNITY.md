# 🔧 GUÍA PASO A PASO - CONFIGURACIÓN EN UNITY

## PARTE 1: PREPARAR ESCENAS

### 1.1 Crear Escena de Extintor

```
File > New Scene > 2D > Save as "FireExtinguisherLesson"
```

**Jerarquía de GameObjects:**

```
FireExtinguisherLesson
├── XR Origin (ya debería existir del template)
│   ├── Camera Offset
│   │   └── Main Camera
│   ├── LeftController
│   └── RightController
│
├── Canvas_Dialogue (3D Canvas)
│   ├── Panel
│   ├── DialogueText (TextMeshPro)
│   │   └── Text: "Bienvenido a la lección de extintor"
│   ├── NextButton (Button)
│   │   └── TextMesh: "Siguiente"
│   └── Image (background)
│
├── Canvas_Game (3D Canvas)
│   ├── TimerText: "Tiempo: 2:00"
│   ├── ScoreText: "Puntuación: 0"
│   └── FireCountText: "Fuegos: 0/5"
│
├── Canvas_Results (3D Canvas) - Oculto inicialmente
│   ├── Panel
│   ├── TitleText: "¡ÉXITO!"
│   ├── ScoreText: "Puntuación: 500"
│   ├── StatsText: "Éxitos: 5 Errores: 0"
│   ├── ContinueButton
│   ├── RetryButton
│   └── LobbyButton
│
├── Profesor (Humanoid)
│   ├── [Modelo 3D o Capsule]
│   ├── Animator (opcional)
│   └── InstructorController (Script)
│
├── Extintor (Interactable)
│   ├── Cilindro rojo (modelo)
│   ├── XRGrabInteractable
│   ├── Rigidbody
│   ├── Collider
│   ├── WorkingExtinguisher (Script)
│   ├── ParticleSystem (espuma)
│   └── Nozzle (Boquilla)
│       ├── Cono pequeño
│       └── XRSimpleInteractable
│
├── Fire (Prefab de prueba)
│   ├── Cubo rojo (llamas visuales)
│   ├── ParticleSystem
│   ├── Light (fuego)
│   ├── BoxCollider
│   ├── Rigidbody (Kinematic)
│   └── FireBehavior (Script)
│
└── GameManager
    ├── FireGameManager (Script)
    ├── CourseManager (Script - Singleton)
    └── ResultsScreen (Script)
```

---

## PARTE 2: CONFIGURAR REFERENCES (INSPECTOR)

### 2.1 InstructorController

Arrastra a Inspector:
```
Profesor > InstructorController

Campos a llenar:
- dialogueCanvas: Arrastra Canvas_Dialogue
- dialogueText: Arrastra DialogueText component
- nextButton: Arrastra NextButton component
- nextButtonText: Arrastra el TextMeshPro del botón
- Diálogos de fuego: (auto-cargan por defecto)
```

### 2.2 WorkingExtinguisher

```
Extintor > WorkingExtinguisher

Campos a llenar:
- nozzle: Arrastra el objeto "Nozzle" hijo
- foamParticle: Arrastra ParticleSystem del extintor
- damagePerSecond: 0.3
- damageRange: 5
```

### 2.3 FireGameManager

Crear GameObject vacío llamado "GameManager":
```
GameManager > FireGameManager

Campos a llenar:
- numberOfFires: 5
- gameTimeLimit: 120
- spawnRadius: 8
- timerText: Arrastra TimerText
- scoreText: Arrastra ScoreText
- fireCountText: Arrastra FireCountText
- gameCanvas: Arrastra Canvas_Game
- pointsPerFireExtinguished: 100
- timeBonus: 1
```

### 2.4 ResultsScreen

```
GameManager > ResultsScreen

Campos a llenar:
- resultsCanvas: Arrastra Canvas_Results
- titleText: Arrastra TitleText
- scoreText: Arrastra ScoreText
- timeText: Arrastra TimeText
- statsText: Arrastra StatsText
- continueButton: Arrastra ContinueButton
- retryButton: Arrastra RetryButton
- lobbyButton: Arrastra LobbyButton
```

### 2.5 CourseManager

```
GameManager > CourseManager

Campos a llenar:
- instructor: Arrastra Profesor > InstructorController
- fireGameManagerPrefab: Arrastra prefab de FireGameManager (crear)
- earthquakeGameManagerPrefab: Arrastra prefab de EarthquakeGameManager
- resultsScreen: Arrastra ResultsScreen
```

---

## PARTE 3: CREAR PREFABS

### 3.1 Fire Prefab

1. Crear GameObject en la escena:
   ```
   Right-click en Hierarchy > 3D Object > Cube
   Rename: "Fire"
   ```

2. Configurar:
   ```
   Transform:
     - Position: (0, 1, 0)
     - Scale: (0.5, 1, 1)
   
   Material: Red
   
   Agregar:
     - ParticleSystem (red/yellow)
     - Light (color rojo, intensity 2, range 5)
     - BoxCollider (isTrigger: false)
     - Rigidbody (Kinematic)
     - FireBehavior (Script)
   ```

3. Convertir a prefab:
   ```
   Drag "Fire" a Assets/Prefabs/ > Fire.prefab
   ```

### 3.2 Debris Prefab

```
Create > 3D Object > Cube
Rename: "Debris"

Transform:
  - Scale: (0.3, 0.5, 0.3)

Material: Brown/Gray

Agregar:
  - BoxCollider
  - Rigidbody (Gravity: true, Mass: 1)
  - Tag: "Debris"

Drag a Assets/Prefabs/ > Debris.prefab
```

### 3.3 Student Prefab

```
Create > 3D Object > Capsule
Rename: "Student"

Transform:
  - Scale: (0.6, 1.5, 0.6)

Agregar:
  - NavMeshAgent (Speed: 3.5, Angular Speed: 180)
  - CapsuleCollider
  - StudentAI (Script)
  - Tag: "Student"

Drag a Assets/Prefabs/ > Student.prefab
```

---

## PARTE 4: CREAR ESCENA DE SISMO

```
File > New Scene > 2D > Save as "EarthquakeLesson"
```

**Jerarquía:**

```
EarthquakeLesson
├── XR Origin (del template)
├── Canvas_Dialogue
├── Canvas_Game
│   ├── PhaseText: "¡TERREMOTO!"
│   ├── InstructionText: "¡AGÁCHATE!"
│   ├── TimerText: "Tiempo: 8s"
│   └── ScoreText: "Puntuación: 0"
├── Canvas_Results
│
├── Profesor
│   └── InstructorController (Script)
│
├── Escenario
│   ├── Mesas (5)
│   │   ├── Cube, scale (2, 0.3, 1)
│   │   ├── Tag: "Table"
│   │   ├── Layer: "Tables"
│   │   └── BoxCollider
│   │
│   ├── Paredes (4)
│   ├── Techo
│   └── Puertas (2)
│       └── ExitPoint (empty GameObject)
│           └── Position: (-5, 0, -8)
│
├── Estudiantes (3-5)
│   ├── Student_1 (con Student.prefab)
│   ├── Student_2
│   └── Student_3
│
├── GameManager
│   ├── EarthquakeSimulator (Script)
│   ├── EarthquakeGameManager (Script)
│   ├── PlayerEarthquakeBehavior (en XR Origin)
│   └── ResultsScreen (Script)
```

---

## PARTE 5: CONFIGURAR PLAYER

### 5.1 Agregar PlayerEarthquakeBehavior

```
Selecciona XR Origin

Agregar script: PlayerEarthquakeBehavior

Inspector:
- crouchHeight: 0.5
- normalHeight: 1.8
- crouchInput: Crea Input Action para "Crouch"
  (o cópialo del template)
```

### 5.2 Configurar Input de Crouch

```
Project Settings > Input Manager
Agregar nueva acción:
  - Name: "Crouch"
  - Positive Button: "space" (o joystick click)
  - Gravity: 1
  - Dead: 0
```

---

## PARTE 6: BAKE NAVMESH (Importante para Sismo)

1. Selecciona GameObjects que son walkable:
   ```
   Piso, Mesas, etc.
   ```

2. Window > AI > Navigation
   
3. Pestaña "Bake":
   ```
   Agent Radius: 0.5
   Max Slope: 45
   Step Height: 0.3
   Max Drop: 0.3
   ```

4. Click "Bake"

---

## PARTE 7: CONFIGURAR ESCENAS EN BUILD SETTINGS

```
File > Build Settings
Add Open Scenes:
  0: LobbyVR
  1: FireExtinguisherLesson
  2: EarthquakeLesson
```

---

## PARTE 8: CONFIGURAR COURSEMANAGER

```
Create > Empty > "Bootstrapper"

Agregar scripts:
- CourseManager.cs

Configurar en Inspector:
- currentModule: FireExtinguisher (por defecto)
- instructor: Arrastra InstructorController
- fireGameManagerPrefab: Arrastra prefab
- earthquakeGameManagerPrefab: Arrastra prefab
- resultsScreen: Arrastra ResultsScreen

Agregar a todas las escenas O marcar como
"DontDestroyOnLoad" (ya está en script)
```

---

## PARTE 9: TESTING BÁSICO

### Checklist:

```
EXTINTOR:
[ ] Presionar "Siguiente" 8 veces avanza diálogos
[ ] Agarrar extintor
[ ] Presionar boquilla = espuma sale
[ ] Fuego se apaga al golpearlo
[ ] Contador aumenta
[ ] Timer funciona
[ ] Al apagar todos = GameComplete
[ ] ResultsScreen muestra puntuación
[ ] "Continuar" lleva a Sismo

SISMO:
[ ] 8 diálogos funcionan
[ ] Cámara tiembla
[ ] Escombros caen
[ ] Input crouch funciona
[ ] Bajo mesa = protección visual
[ ] Estudiantes se mueven
[ ] Timer evacuation
[ ] Resultados correctos
```

---

## PARTE 10: DEBUGGING

### Si los fuegos no se apagan:
```
1. Verifica WorkingExtinguisher.damageRange
2. Verifica que foamParticle está asignado
3. Abre consola: Player.GetActiveFiresCount() debe > 0
```

### Si los diálogos no avanzan:
```
1. Verifica que NextButton tiene Select evento
2. Chequea que dialogueText está asignado
3. Revisa log: "Diálogo X/Y"
```

### Si el sismo no comienza:
```
1. Verifica NavMesh está baked
2. Verifica StudentAI tiene NavMeshAgent
3. Chequea que ExitPoint existe
```

---

## ARCHIVOS FINALES

Una vez completado:

```
Assets/
├── Scenes/
│   ├── LobbyVR.unity
│   ├── FireExtinguisherLesson.unity
│   └── EarthquakeLesson.unity
├── Scripts/
│   ├── CourseManager.cs
│   ├── InstructorController.cs
│   ├── WorkingExtinguisher.cs
│   ├── FireBehavior.cs
│   ├── FireGameManager.cs
│   ├── ResultsScreen.cs
│   ├── EarthquakeSimulator.cs
│   ├── PlayerEarthquakeBehavior.cs
│   ├── StudentAI.cs
│   └── EarthquakeGameManager.cs
├── Prefabs/
│   ├── Fire.prefab
│   ├── Debris.prefab
│   ├── Student.prefab
│   ├── FireGameManager.prefab
│   └── EarthquakeGameManager.prefab
└── Materials/
    ├── Fire.mat
    ├── Debris.mat
    └── Student.mat
```

---

## ⚠️ COSAS CRÍTICAS

1. **No olvides asignar referencias en Inspector**
   - Los scripts usan `GetComponent<>()` como fallback
   - Pero es más seguro asignarlo manualmente

2. **Prefabs deben estar en Assets/Prefabs/**
   - No en Scenes/
   - Úsalos desde Inspector, no hardcoded

3. **NavMesh es ESENCIAL**
   - Sin él, StudentAI no funciona
   - Bake después de crear el escenario

4. **Layers y Tags**
   ```
   Layer "Tables" para mesas
   Tag "Debris" para escombros
   Tag "Student" para estudiantes
   ```

5. **Canvas debe ser WorldSpace**
   - No ScreenSpace
   - Para que sea visible en VR

---

## PRÓXIMO PASO

Cuando termines la configuración:
1. Play mode
2. Testea desde Lobby
3. Verifica flujo completo
4. Si hay errores, revisa console

¡Listo! 🎉
