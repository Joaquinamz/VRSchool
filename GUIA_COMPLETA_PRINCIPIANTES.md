# 🎮 GUÍA COMPLETA PARA PRINCIPIANTES - SETUP EN UNITY

**Para usuarios SIN experiencia en Unity - Léelo TODO paso a paso**

---

## 📌 INDICE DE ESTA GUÍA

1. Entender la nueva arquitectura
2. Preparar las escenas
3. Crear y configurar GameObjects
4. Asignar scripts
5. Rellenar referencias en Inspector
6. Testing

---

# PARTE 1: ENTENDER LA NUEVA ARQUITECTURA

## ¿Cómo funciona ahora?

```
LOBBY (Hub central)
  ├─ Usuario elige: Extintor O Sismo
  ├─ Usuario elige: Dificultad A, B, C o Random
  └─ Carga la escena del módulo elegido

MÓDULO ELEGIDO (Extintor o Sismo)
  ├─ Diálogos del profesor
  ├─ Minijuego (con dificultad aplicada)
  └─ Resultados

RESULTADOS
  ├─ Botón "Reintentar" → Vuelve a cargar el módulo
  └─ Botón "Volver a Lobby" → Vuelve al Lobby
```

**Clave**: El usuario puede hacer CUALQUIER MÓDULO en CUALQUIER ORDEN, tantas veces quiera.

---

# PARTE 2: PREPARAR LAS ESCENAS

## Paso 1: Verificar que existen 3 escenas

En Unity, ve a:
```
Assets > Scenes (carpeta)
```

Deberías ver:
- ✅ LobbyVR.unity (ya existe)
- ⏳ FireExtinguisherLesson.unity (SI NO EXISTE, crear)
- ⏳ EarthquakeLesson.unity (SI NO EXISTE, crear)

### Si NO existen, crearlas ahora:

1. **Para crear FireExtinguisherLesson.unity:**
   - En el proyecto, click derecho en `Assets > Scenes`
   - Click en `Cre ate > Scene`
   - Nombre: `FireExtinguisherLesson`
   - Double-click para abrir esa escena

2. **Para crear EarthquakeLesson.unity:**
   - Repetir el proceso
   - Nombre: `EarthquakeLesson`

---

## Paso 2: Agregar las escenas a Build Settings

**¿QUÉ ES BUILD SETTINGS?** Es donde le dices a Unity qué escenas existen en el juego.

### Cómo hacerlo:

1. Abre Unity
2. Arriba: `File > Build Settings` (o Ctrl+Shift+B)
3. Debajo de "Scenes In Build" verás una lista
4. Verifica que hay 3 escenas:
   - 0: `LobbyVR`
   - 1: `FireExtinguisherLesson`
   - 2: `EarthquakeLesson`

Si falta alguna:
- Arrastra desde el Proyecto la escena FALTANTE hacia la lista de Scenes In Build
- O click el "+" para agregar

**IMPORTANTE**: El orden DEBE ser ese. LobbyVR siempre es la primera (índice 0).

---

# PARTE 3: CONFIGURAR CADA ESCENA

## A. LOBBYVY (Ya existe - Modifica)

Abre `LobbyVR.unity` con doble-click.

### Jerarquía esperada:

```
LobbyVR
├── XR Origin (del template)
│   ├── Camera Offset
│   │   └── Main Camera
│   ├── LeftController
│   └── RightController
│
├── Canvas_Lobby (NEW - CREAR)
│   ├── Panel_Background (NEW - para fondo)
│   ├── Text_Title: "Selecciona un módulo"
│   ├── Button_FireExtinguisher (NEW)
│   │   └── Text: "Extintor de Incendios"
│   └── Button_Earthquake (NEW)
│       └── Text: "Seguridad ante Sismos"
│
├── Canvas_DifficultySelection (NEW - CREAR, oculto)
│   ├── Panel
│   ├── Text_Title: "Elige dificultad:"
│   ├── Button_A
│   │   └── Text: "Fácil (A)"
│   ├── Button_B
│   │   └── Text: "Normal (B)"
│   ├── Button_C
│   │   └── Text: "Difícil (C)"
│   └── Button_Random
│       └── Text: "Aleatorio"
│
├── CourseManager (NEW - GameObject vacío)
│   ├── CourseManager (Script)
│   └── LobbyManager (Script)
```

### Cómo crear esto en Inspector:

1. **Crear Canvas_Lobby:**
   - En Hierarchy, right-click
   - `Create Empty > 3D Object > Text - TextMeshPro`
   - Rename: `Canvas_Lobby`
   - Click derecho > `3D Object > Canvas - TextMeshPro`

   O MEJOR: 
   - Right-click en Hierarchy
   - `UI > Canvas - TextMeshPro`
   - Rename: `Canvas_Lobby`

2. **Configurar Canvas:**
   - Click en `Canvas_Lobby`
   - En Inspector, busca `Canvas`
   - `Render Mode`: Cambiar a `World Space`
   - Position: `(0, 1.5, 2)` - Frente a la cámara
   - Scale: `(0.01, 0.01, 0.01)` - Para que sea de buen tamaño

3. **Crear Botones:**
   - Dentro de `Canvas_Lobby`, click derecho
   - `UI > Button - TextMeshPro`
   - Rename: `Button_FireExtinguisher`
   - Repetir: Crear `Button_Earthquake`

4. **Crear Canvas de Dificultad (oculto):**
   - Repetir proceso
   - Rename: `Canvas_DifficultySelection`
   - En Inspector > Canvas, marcar "Active" como FALSE para ocultarlo

5. **Crear CourseManager:**
   - Right-click en Hierarchy
   - `Create Empty`
   - Rename: `CourseManager`
   - Esto lo usaremos en el siguiente paso

---

## B. FireExtinguisherLesson.unity

Abre esta escena con doble-click.

### Jerarquía esperada:

```
FireExtinguisherLesson
├── XR Origin (copiar del template o crear uno nuevo)
│
├── Canvas_Instruction (para diálogos)
│   ├── Text_Dialogue
│   └── Button_Next
│
├── Canvas_Game (para HUD del minijuego)
│   ├── Text_Timer: "Tiempo: 2:00"
│   ├── Text_Score: "Puntuación: 0"
│   ├── Text_FireCount: "Fuegos: 0/5"
│   └── Text_Difficulty: "Dificultad: NORMAL"
│
├── Canvas_Results (oculto)
│   ├── Text_Title
│   ├── Text_Score
│   ├── Text_Stats
│   ├── Button_Retry
│   └── Button_Lobby
│
├── Profesor (GameObject vacío con modelo 3D)
│   ├── InstructorController (Script)
│
├── Extintor (interactable)
│   ├── WorkingExtinguisher (Script)
│   ├── Rigidbody
│   ├── Collider
│   ├── XRGrabInteractable
│   ├── ParticleSystem (espuma)
│   └── Nozzle (hijo)
│       └── XRSimpleInteractable
│
├── Fire (Prefab - para testing)
│   ├── FireBehavior (Script)
│
└── CourseManager (GameObject vacío)
    ├── CourseManager (Script)
    └── FireGameManager (Script)
```

### Pasos DETALLADOS:

1. **Copiar XR Origin:**
   - Ve a `LobbyVR.unity`
   - Selecciona `XR Origin`
   - Ctrl+C (copiar)
   - Ve a `FireExtinguisherLesson.unity`
   - Ctrl+V (pegar)

2. **Crear Canvas_Instruction:**
   - Right-click en Hierarchy
   - `UI > Canvas - TextMeshPro`
   - Rename: `Canvas_Instruction`
   - Render Mode: `World Space`
   - Position: `(0, 2, 3)`
   - Dentro crea 2 hijos:
     - `UI > Text - TextMeshPro` → rename `Text_Dialogue`
     - `UI > Button - TextMeshPro` → rename `Button_Next`

3. **Crear Canvas_Game:**
   - Right-click > `UI > Canvas - TextMeshPro`
   - Rename: `Canvas_Game`
   - Render Mode: `World Space`
   - Dentro crea 4 hijos TextMeshPro:
     - `Text_Timer`
     - `Text_Score`
     - `Text_FireCount`
     - `Text_Difficulty`

4. **Crear Canvas_Results:**
   - Right-click > `UI > Canvas - TextMeshPro`
   - Rename: `Canvas_Results`
   - **Canvas > Active: marcar FALSE** (oculto)
   - Dentro crea:
     - `Text_Title`
     - `Text_Score`
     - `Text_Stats`
     - `Button_Retry`
     - `Button_Lobby`

5. **Crear Profesor:**
   - Right-click > `Create Empty`
   - Rename: `Profesor`
   - Position: `(0, 0, 3)` - Frente a la cámara

6. **Crear Extintor:**
   - Right-click > `3D Object > Cylinder`
   - Rename: `Extintor`
   - Scale: `(0.3, 0.8, 0.3)`
   - Agregar componentes:
     - Click `Add Component`
     - Busca `XRGrabInteractable` > Add
     - Busca `WorkingExtinguisher` > Add
     - Agregar `Rigidbody`
     - Agregar `Collider` (BoxCollider o CapsuleCollider)
     - Agregar `ParticleSystem`
   - Crear hijo:
     - Right-click en `Extintor` > `3D Object > Cone`
     - Rename: `Nozzle`
     - Scale: `(0.1, 0.2, 0.1)`
     - Position: `(0, 0.5, 0)`
     - Add Component > `XRSimpleInteractable`

7. **Crear CourseManager (GameObject vacío):**
   - Right-click > `Create Empty`
   - Rename: `CourseManager`
   - Position: `(0, 0, 0)`
   - Add Component > `CourseManager`
   - Add Component > `FireGameManager`
   - Add Component > `ResultsScreen`

---

## C. EarthquakeLesson.unity

Pasos similares a FireExtinguisher pero con componentes de sismo:

```
EarthquakeLesson
├── XR Origin (copiar de LobbyVR)
├── Canvas_Instruction
├── Canvas_Game
├── Canvas_Results
├── Profesor
├── Escenario
│   ├── Mesa_1, Mesa_2, etc (Cube con BoxCollider, Tag:"Table")
│   ├── Paredes
│   ├── Techo
│   └── ExitPoint (Empty, position: (-5, 0, -10))
├── EarthquakeSimulator (EarthquakeSimulator Script)
├── StudentAI_1, StudentAI_2, etc
│   ├── Capsule (modelo)
│   ├── NavMeshAgent
│   ├── StudentAI (Script)
└── CourseManager
    ├── CourseManager (Script)
    ├── EarthquakeGameManager (Script)
    └── ResultsScreen (Script)
```

---

# PARTE 4: ASIGNAR SCRIPTS

**PASO CRÍTICO**: Sin esto, nada funciona.

## En LobbyVR.unity:

### Objeto: CourseManager

Click en `CourseManager` en Hierarchy

En Inspector, busca `CourseManager` (Script)

Campos a llenar:
```
Current State: AtLobby
Selected Module: FireExtinguisher
Selected Difficulty: A
Instructor: (Arrastra del Profesor de una escena de módulo)
Fire Game Manager Prefab: (Dejalo vacío por ahora)
Earthquake Game Manager Prefab: (Dejalo vacío por ahora)
Results Screen: (Dejalo vacío por ahora)
```

### Objeto: Profesor (si existe en Lobby)

Busca `InstructorController` (Script)

Si no tiene referencias, puedes dejarlas vacías (usará FindObjectOfType)

---

## En FireExtinguisherLesson.unity:

### Objeto: Profesor

Agregar Script `InstructorController`

```
Dialogue Canvas: Canvas_Instruction
Dialogue Text: Canvas_Instruction > Text_Dialogue
Next Button: Canvas_Instruction > Button_Next
Next Button Text: (el TextMeshPro del botón Next)
```

### Objeto: Extintor

Script `WorkingExtinguisher` debe tener:
```
Nozzle: Extintor > Nozzle (el cone)
Foam Particle: (la ParticleSystem del Extintor)
Damage Per Second: 0.3
Damage Range: 5
```

### Objeto: Canvas_Game

Click en Canvas_Game, luego click en `Add Component` > busca `Canvas` si falta

### Objeto: CourseManager

Script `CourseManager`:
```
Current State: AtLobby
Selected Module: FireExtinguisher
Instructor: Profesor > InstructorController
Fire Game Manager Prefab: (IMPORTANTE: crear prefab, ver más abajo)
Results Screen: Canvas_Results > ResultsScreen (script)
```

Script `FireGameManager`:
```
Fires Easy Mode: 3
Fires Normal Mode: 5
Fires Hard Mode: 7
Time Easy Mode: 150
Time Normal Mode: 120
Time Hard Mode: 90
Timer Text: Canvas_Game > Text_Timer
Score Text: Canvas_Game > Text_Score
Fire Count Text: Canvas_Game > Text_FireCount
Difficulty Text: Canvas_Game > Text_Difficulty
Game Canvas: Canvas_Game
```

Script `ResultsScreen`:
```
Results Canvas: Canvas_Results
Title Text: Canvas_Results > Text_Title
Score Text: Canvas_Results > Text_Score
Time Text: Canvas_Results > Text_Stats (reutilizar)
Stats Text: Canvas_Results > Text_Stats
Retry Button: Canvas_Results > Button_Retry
Lobby Button: Canvas_Results > Button_Lobby
```

---

## En EarthquakeLesson.unity:

Proceso similar a FireExtinguisher.

Script `EarthquakeGameManager`:
```
Phase Text: Canvas_Game > Text_Phase
Instruction Text: Canvas_Game > Text_Instruction
Timer Text: Canvas_Game > Text_Timer
Score Text: Canvas_Game > Text_Score
Exit Point: ExitPoint (transform)
```

---

# PARTE 5: CREAR PREFABS

**¿QUE ES UN PREFAB?** Es una "plantilla" de un objeto que puedes crear muchas veces.

## Crear Fire Prefab:

1. **En una escena cualquiera, crear un fuego:**
   - Right-click > `3D Object > Cube`
   - Scale: `(0.5, 1, 0.5)`
   - Material: Red
   - Add Component > `FireBehavior`
   - Add Component > `BoxCollider`
   - Add Component > `ParticleSystem`
   - Add Component > `Light`

2. **Convertir a Prefab:**
   - Selecciona el Cube
   - Arrastra el Cube desde Hierarchy a `Assets > Prefabs` (crea carpeta si falta)
   - Debería volverse AZUL en Hierarchy
   - Delete el original de la escena

3. **Asignar en FireGameManager:**
   - Abre `FireExtinguisherLesson.unity`
   - Click en `CourseManager`
   - En Inspector, `FireGameManager`
   - `Fire Prefab`: Arrastra el prefab que creaste

---

# PARTE 6: TESTING BASICO

## Paso 1: Verificar sin errores

1. Abre `LobbyVR.unity`
2. Press Play (botón de Play en inspector)
3. Abre Console (Ctrl+Shift+C)
4. Verifica que NO hay errores rojos

Si hay errores:
- Lee el mensaje de error
- Busca en TROUBLESHOOTING.md
- Probablemente falta una referencia en Inspector

## Paso 2: Probar flujo

1. En Play mode, deberías ver el Lobby
2. Click en botón "Extintor"
3. Debería mostrar selector de dificultad
4. Click en una dificultad
5. Debería cambiar a `FireExtinguisherLesson.unity`
6. Deberías ver al Profesor y su diálogo

Si NO pasa:
- Verifica que CourseManager existe en ambas escenas
- Verifica que LobbyManager tiene botones configurados
- Abre Console para ver errores

---

# RESUMEN RÁPIDO

| Paso | Qué hacer | Dónde |
|------|-----------|-------|
| 1 | Crear 2 escenas | Assets > Scenes |
| 2 | Agregar a Build Settings | File > Build Settings |
| 3 | Crear Canvas y botones | Cada escena |
| 4 | Asignar scripts | Component > Add Component |
| 5 | Llenar referencias | Inspector |
| 6 | Crear prefabs | Assets > Prefabs |
| 7 | Testear | Play mode |

---

# PRÓXIMO PASO

Una vez que todo esto funcione:
1. Agregar modelos 3D (profesor, aula, etc)
2. Agregar audio
3. Balancear dificultad

¡ÉXITO! 🚀
