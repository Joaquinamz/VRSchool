# 📋 GUÍA DE INTEGRACIÓN - VR EDUCATIVO

## Estado del Desarrollo

### ✅ COMPLETADO (Scripts)
1. **CourseManager.cs** - Sistema central de coordinación
2. **InstructorController.cs** - Profesor y diálogos
3. **WorkingExtinguisher.cs** - Extintor mejorado
4. **FireBehavior.cs** - Comportamiento de fuego
5. **FireGameManager.cs** - Minijuego de extintor
6. **ResultsScreen.cs** - Pantalla de resultados
7. **EarthquakeSimulator.cs** - Simulador de terremoto
8. **PlayerEarthquakeBehavior.cs** - Comportamiento del jugador en sismo
9. **StudentAI.cs** - NPCs estudiantes
10. **EarthquakeGameManager.cs** - Minijuego de sismo

---

## 🔧 SETUP EN UNITY

### PASO 1: Estructura de Escenas
Necesitas 2 escenas:
1. **LobbyVR.unity** (ya existe)
2. **FireExtinguisherLesson.unity** (para extintor)
3. **EarthquakeLesson.unity** (para sismo)

### PASO 2: Escena de Extintor (FireExtinguisherLesson)

#### Gameobjects necesarios:
```
Canvas
├── DialogueUI (Canvas)
│   ├── DialogueText (TextMeshPro)
│   └── NextButton (Button)
├── GameUI (Canvas)
│   ├── TimerText
│   ├── ScoreText
│   └── FireCountText
└── ResultsUI (Canvas)
    ├── TitleText
    ├── ScoreText
    ├── StatsText
    ├── ContinueButton
    ├── RetryButton
    └── LobbyButton

Profesor
├── [Modelo 3D o capsule simple]
├── Animator (opcional)
└── InstructorController.cs

Extintor
├── [Modelo 3D]
├── XRGrabInteractable
├── WorkingExtinguisher.cs
├── ParticleSystem (espuma)
└── Boquilla
    └── XRSimpleInteractable

FireSpawner
└── FireGameManager.cs

FirePrefab
├── [Modelo 3D]
├── ParticleSystem (llamas)
├── Light (fuego)
├── BoxCollider
└── FireBehavior.cs

Jugador (XR Origin)
├── Camera
├── LeftController (XR Controller)
└── RightController (XR Controller)

CourseManager
└── CourseManager.cs (singleton)
```

#### Configuración:
1. Asigna `NextButton` al `InstructorController`
2. Asigna `FireGameManager` prefab al `CourseManager`
3. Crea prefab de fuego con `FireBehavior.cs`
4. Asigna `DialogueText`, `ScoreText`, `TimerText` en UI

### PASO 3: Escena de Sismo (EarthquakeLesson)

#### Gameobjects necesarios:
```
Canvas
├── DialogueUI
├── GameUI
│   ├── PhaseText
│   ├── InstructionText
│   ├── TimerText
│   └── ScoreText
└── ResultsUI

Profesor
└── InstructorController.cs

Escenario
├── Mesas (con BoxCollider)
│   └── Tag: "Table"
│   └── Layer: "Tables"
├── Pupitres
├── Puertas
└── Puntos de salida
    └── ExitPoint (Transform)

EarthquakeSimulator
├── EarthquakeSimulator.cs
└── Prefabs de escombros
    ├── Escombro1 (con Rigidbody)
    │   └── Tag: "Debris"
    ├── Escombro2
    └── Escombro3

Estudiantes (instancias)
├── StudentAI_1
│   ├── [Modelo 3D]
│   ├── NavMeshAgent
│   └── StudentAI.cs
├── StudentAI_2
├── StudentAI_3
└── ... (3-5 estudiantes)

Jugador (XR Origin)
├── CharacterController
├── Camera
├── PlayerEarthquakeBehavior.cs
└── Input Action Reference (crouch)

EarthquakeGameManager
└── EarthquakeGameManager.cs
```

#### Configuración:
1. Bake NavMesh en la escena (Window > AI > Navigation)
2. Tag mesas como "Table" y asigna layer "Tables"
3. Tag escombros como "Debris"
4. Asigna `ExitPoint` transform
5. Configura input de agacharse (crouch) - joystick derecho o tecla específica

---

## 📦 PREFABS NECESARIOS

### FirePrefab
```csharp
GameObject firePrefab
├── Model (Cube o cilindro rojo)
├── ParticleSystem (Particle: Fire)
├── Light (rojo, range: 5)
├── BoxCollider (is Trigger: false)
└── FireBehavior.cs
```

### DebrisPrefab
```csharp
GameObject debrisPrefab
├── Model (escombro visual)
├── BoxCollider (is Trigger: false)
├── Rigidbody (gravity: true, mass: 1)
└── Tag: "Debris"
```

### StudentPrefab
```csharp
GameObject studentPrefab
├── Model (humanoid simple)
├── CapsuleCollider
├── NavMeshAgent
├── StudentAI.cs
└── Tag: "Student"
```

---

## 🎮 FLUJO DE EJECUCIÓN

### Módulo de Extintor:
```
Lobby → Escena Extintor
    ↓
InstructorController muestra diálogos
    ↓
Usuario presiona "Siguiente" x8
    ↓
CourseManager.StartGamePhase()
    ↓
FireGameManager instancia múltiples fuegos
    ↓
Jugador agarra extintor y apaga fuegos
    ↓
Todos los fuegos apagados o timeout
    ↓
ResultsScreen muestra puntuación
    ↓
Usuario presiona "Continuar" → Próximo módulo (Sismo)
```

### Módulo de Sismo:
```
Resultados Extintor → Escena Sismo
    ↓
InstructorController muestra diálogos (8 slides)
    ↓
CourseManager.StartGamePhase()
    ↓
EarthquakeSimulator inicia temblor (8s)
    ↓
Caen escombros (damageZone colisión)
    ↓
Jugador se agacha bajo mesa (input)
    ↓
Terremoto finaliza
    ↓
EarthquakeGameManager inicia evacuación (15s)
    ↓
Estudiantes salen ordenadamente (NavMesh)
    ↓
Jugador sigue sin chocar
    ↓
Timeout o todos evacuados
    ↓
ResultsScreen muestra puntuación
    ↓
Usuario presiona "Continuar" → Celebración final o Lobby
```

---

## ⚙️ CONFIGURACIÓN DE INPUTS

### Necesarios:
1. **Agarre del Extintor** - XR Grab (ya configurado)
2. **Presión de Boquilla** - XR Simple Interact (ya configurado)
3. **Agacharse en Sismo** - Joystick derecho O Botón específico

### Para agregar input de agacharse:
1. Abre Project Settings > Input Manager (o Input System si es nuevo)
2. Crea acción "Crouch" mapeada a:
   - Joystick Right Stick / Button (click)
   - O tecla Ctrl / Espacio

---

## 🎨 ELEMENTOS 3D MÍNIMOS NECESARIOS

### Modelo del Profesor:
- Humanoid simple (Capsule body + cilindro head)
- Posición: frente al aula
- Opcional: Rigging simple para saludar

### Modelo del Extintor:
- Cilindro rojo (base)
- Pequeño cono (boquilla)
- Mango (cilindro fino)
- UV mapping simple o material rojo

### Aula Básica:
- Paredes (4 cuadros)
- Techo (plane)
- Mesas (5-6 cubos bajos)
- Pupitres (cubos pequeños)
- Puertas (marcos con colliders)

### Escombros:
- Bloques variados (cubos, techos rotos)
- Diferentes tamaños (0.5-2m)
- Material marrón/gris

---

## 📊 PUNTUACIÓN Y CRITERIOS

### Extintor:
- Punto por fuego apagado: **100pts**
- Bonus por tiempo: **1pt/segundo restante**
- Total esperado: 500-800pts (5 fuegos en menos de 100s)

### Sismo:
- Punto por agachada correcta: **50pts**
- Punto por estudiante evacuado: **100pts**
- Penalty por golpe de escombro: **-50pts**
- Penalty por choque con estudiante: **-30pts**
- Total esperado: 300-600pts

---

## ⚠️ COSAS IMPORTANTES

1. **Singleton CourseManager**: Persiste entre escenas
2. **NavMesh**: DEBE estar baked en escena de sismo
3. **Layers**: Crea layer "Tables" para detección segura
4. **Prefabs**: Crea prefabs de fuego y escombro ANTES de asignar a GameManager
5. **Canvas**: Cada escena debe tener su propio Canvas con UI completa
6. **XR Setup**: Asegúrate de tener XR Interaction Toolkit instalado

---

## 🧪 TESTING

### Pruebas básicas:
1. Lobby → Extintor (cambio de escena)
2. Diálogos → Presionar siguiente 8 veces
3. Fuegos → Desaparecen al ser golpeados con extintor
4. Contador → Aumenta al apagar fuego
5. Resultados → Muestra puntuación correcta

### Pruebas de sismo:
1. Terremoto → Cámara tiembla
2. Escombros → Caen y rebotan
3. Agacharse → Altura de cámara baja
4. Estudiantes → Se mueven hacia salida
5. Evacuación → Cuenta de estudiantes correcta

---

## 📞 PRÓXIMOS PASOS

- [ ] Agregar audio (TTS para diálogos, efectos de sonido)
- [ ] Mejorar modelos 3D (texturas, animaciones)
- [ ] Agregar feedback haptic (vibración en controllers)
- [ ] Testing en dispositivo VR (Meta Quest, SteamVR, etc.)
- [ ] Optimización de performance
- [ ] Múltiples idiomas
