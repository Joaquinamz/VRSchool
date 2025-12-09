# ✅ CHECKLIST COMPLETO: DE CÓDIGO A JUEGO FUNCIONAL

Este documento te guía EXACTAMENTE cómo convertir el código en un juego funcional.

---

## 📋 FASE 1: PREPARACIÓN (10 minutos)

- [ ] Abieres Unity
- [ ] No hay errores de compilación (Ctrl+R)
- [ ] Leíste FLUJO_EVENTOS_COMPLETO.md
- [ ] Leíste VR_CANVAS_GUIDE.md
- [ ] Leíste EXTINTOR_SETUP_NUEVO.md

---

## 📋 FASE 2: SETUP LOBBY (15 minutos)

**Archivo**: LobbyVR.unity

### 2.1 CourseManager
- [ ] Hierarchy → Create Empty
- [ ] Nombre: **CourseManager**
- [ ] Add Component → **CourseManager.cs**
- [ ] Resultado: Singleton que persiste entre escenas

### 2.2 Canvas Lobby
- [ ] Hierarchy → UI → Canvas
- [ ] Nombre: **LobbyCanvas**
- [ ] Canvas Scaler:
  - [ ] UI Scale Mode: **Constant Physical Size**
  - [ ] Physical Unit: **Centimeters**
- [ ] Rect Transform:
  - [ ] Scale: (0.01, 0.01, 1)
  - [ ] Position: (0, 1.5, 2)

### 2.3 Panel de Módulos
- [ ] Click derecho en LobbyCanvas → UI → Panel
- [ ] Nombre: **ModulesPanel**
- [ ] Crear 2 botones:
  - [ ] Botón 1: **FireExtinguisherButton** ("Extintor")
  - [ ] Botón 2: **EarthquakeButton** ("Sismo")

### 2.4 Panel de Dificultad
- [ ] Click derecho en LobbyCanvas → UI → Panel
- [ ] Nombre: **DifficultyPanel**
- [ ] Crear 4 botones:
  - [ ] **DifficultyAButton** ("Fácil")
  - [ ] **DifficultyBButton** ("Normal")
  - [ ] **DifficultyCButton** ("Difícil")
  - [ ] **DifficultyRandomButton** ("Aleatorio")
- [ ] DifficultyPanel: Inicialmente **desactivado** (SetActive false)

### 2.5 LobbyManager
- [ ] Hierarchy → Create Empty
- [ ] Nombre: **LobbyUI**
- [ ] Add Component → **LobbyManager.cs**
- [ ] Inspector → Asignar referencias:
  - [ ] Fire Extinguisher Button: arrastra FireExtinguisherButton
  - [ ] Earthquake Button: arrastra EarthquakeButton
  - [ ] Difficulty A Button: arrastra DifficultyAButton
  - [ ] (... y los demás)
  - [ ] Difficulty Selection Canvas: arrastra DifficultyPanel

---

## 📋 FASE 3: SETUP EXTINTOR (20 minutos)

**Archivo**: FireExtinguisherLesson.unity

### 3.1 GameObject Extintor
- [ ] Hierarchy → 3D Object → Cube
- [ ] Nombre: **ExtintorObject**
- [ ] Scale: (0.1, 0.3, 0.1)
- [ ] Position: (0, 1, 0)
- [ ] Material: Rojo

### 3.2 Componentes del Extintor
- [ ] Add Component → **XRGrabInteractable**
  - [ ] Grab Type: Single Hand
  - [ ] Drop on Deselect: ON
- [ ] Add Component → **WorkingExtinguisher.cs**
- [ ] Add Component → **ParticleSystem** (para espuma)
  - [ ] Start Lifetime: 2
  - [ ] Start Size: 0.2
  - [ ] Emission Rate: 50

### 3.3 Configurar WorkingExtinguisher
- [ ] Inspector → WorkingExtinguisher
  - [ ] Foam Particle: arrastra el ParticleSystem
  - [ ] Damage Per Second: 30
  - [ ] Damage Range: 5

---

## 📋 FASE 4: SETUP FUEGOS (20 minutos)

**Archivo**: FireExtinguisherLesson.unity

### 4.1 Crear Fire_1
- [ ] Hierarchy → Effects → Particle System
- [ ] Nombre: **Fire_1**
- [ ] Position: (2, 0.5, 0)
- [ ] Add Component → **FireBehavior.cs**
- [ ] Configurar FireBehavior:
  - [ ] Max Intensity: 100
  - [ ] Initial Intensity: 100
  - [ ] Particle System: arrastra el ParticleSystem

### 4.2 Duplicar fuegos
- [ ] Selecciona Fire_1 → Ctrl+D
- [ ] Renombra: **Fire_2**
- [ ] Position: (-2, 0.5, 0)
- [ ] Repite para Fire_3, Fire_4, Fire_5:
  - [ ] Fire_3: (0, 0.5, 2)
  - [ ] Fire_4: (0, 0.5, -2)
  - [ ] Fire_5: (2, 0.5, 2)

### 4.3 Crear contenedor
- [ ] Hierarchy → Create Empty
- [ ] Nombre: **Fires**
- [ ] Arrastra Fire_1 a Fire_5 dentro de Fires

---

## 📋 FASE 5: SETUP GAMEMANAGER EXTINTOR (20 minutos)

**Archivo**: FireExtinguisherLesson.unity

### 5.1 GameObject GameManager
- [ ] Hierarchy → Create Empty
- [ ] Nombre: **FireGameManager**
- [ ] Add Component → **FireGameManager.cs**

### 5.2 Configurar referencias
- [ ] Inspector → FireGameManager
  - [ ] Fire Prefab: arrastra Fire_1
  - [ ] Timer Text: [crea TextMeshPro]
  - [ ] Score Text: [crea TextMeshPro]
  - [ ] Fire Count Text: [crea TextMeshPro]

### 5.3 Crear Canvas UI del Juego
- [ ] Hierarchy → UI → Canvas
- [ ] Nombre: **GameCanvas**
- [ ] Canvas Scaler: Constant Physical Size
- [ ] Scale: (0.01, 0.01, 1)
- [ ] Crear Textos dentro:
  - [ ] **TimerText** ("Tiempo: 150s")
  - [ ] **ScoreText** ("Puntos: 0")
  - [ ] **FireCountText** ("Fuegos: 3")
- [ ] Arrastra a FireGameManager

---

## 📋 FASE 6: SETUP PROFESOR Y DIÁLOGOS (15 minutos)

**Archivo**: FireExtinguisherLesson.unity

### 6.1 GameObject Profesor
- [ ] Hierarchy → Create Empty
- [ ] Nombre: **Profesor**
- [ ] Position: (0, 1.5, 2)
- [ ] Add Component → **InstructorController.cs**

### 6.2 Canvas Diálogos
- [ ] Hierarchy → UI → Canvas
- [ ] Nombre: **DialogueCanvas**
- [ ] Canvas Scaler: Constant Physical Size
- [ ] Scale: (0.01, 0.01, 1)
- [ ] Position: (0, 1.5, 2)

### 6.3 Panel de Diálogo
- [ ] Click derecho en DialogueCanvas → UI → Panel
- [ ] Nombre: **DialoguePanel**
- [ ] Crear TextMeshPro dentro:
  - [ ] **DialogueText** (Font Size: 40)
- [ ] Crear Botón dentro:
  - [ ] **NextButton** ("Siguiente")

### 6.4 Asignar referencias a InstructorController
- [ ] Inspector → InstructorController
  - [ ] Dialogue Text: arrastra DialogueText
  - [ ] Dialogue Canvas: arrastra DialogueCanvas
  - [ ] Next Button: arrastra NextButton
  - [ ] Fire Dialogues: [8 diálogos predefinidos]
  - [ ] Earthquake Dialogues: [8 diálogos predefinidos]

---

## 📋 FASE 7: SETUP PANTALLA RESULTADOS (15 minutos)

**Archivo**: FireExtinguisherLesson.unity

### 7.1 Canvas Resultados
- [ ] Hierarchy → UI → Canvas
- [ ] Nombre: **ResultsCanvas**
- [ ] Canvas Scaler: Constant Physical Size
- [ ] Scale: (0.01, 0.01, 1)
- [ ] Position: (0, 1.5, 2)
- [ ] Inicialmente: **desactivado** (SetActive false)

### 7.2 Panel de Resultados
- [ ] Click derecho en ResultsCanvas → UI → Panel
- [ ] Nombre: **ResultsPanel**
- [ ] Crear elementos dentro:
  - [ ] **TitleText** ("¡ÉXITO!")
  - [ ] **ScoreText** ("Puntuación: 450")
  - [ ] **TimeText** ("Tiempo: 120s")
  - [ ] **StatsText** ("Éxitos: 5")

### 7.3 Botones de Resultados
- [ ] Crear Button → **RetryButton** ("Reintentar")
- [ ] Crear Button → **LobbyButton** ("Volver a Lobby")

### 7.4 Asignar ResultsScreen
- [ ] Crear Empty → **ResultsScreenManager**
- [ ] Add Component → **ResultsScreen.cs**
- [ ] Inspector → Asignar todas las referencias

---

## 📋 FASE 8: ESCENA SISMO (Similar a Extintor)

**Archivo**: EarthquakeLesson.unity

Repite Fase 3-7 pero:
- [ ] En lugar de Extintor: crea Mesas (Cubes)
- [ ] En lugar de Fuegos: crea Escombros (Cubes + Rigidbody)
- [ ] Add Component → **EarthquakeGameManager.cs**
- [ ] Crear Estudiantes (StudentAI + NavMeshAgent)
- [ ] Add Component → **EarthquakeSimulator.cs**

---

## 📋 FASE 9: BUILD SETTINGS (5 minutos)

- [ ] **File → Build Settings**
- [ ] Haz clic en **Add Open Scenes** 3 veces:
  - [ ] LobbyVR (Index 0)
  - [ ] FireExtinguisherLesson (Index 1)
  - [ ] EarthquakeLesson (Index 2)

---

## 📋 FASE 10: TESTING (30 minutos)

### TEST 1: Lobby
- [ ] Abre LobbyVR.unity
- [ ] Presiona **Play**
- [ ] ¿Ves 2 botones?
- [ ] Haz clic en "Extintor"
- [ ] ¿Aparece panel de dificultad?
- [ ] Selecciona "Fácil"
- [ ] ¿Se carga FireExtinguisherLesson?

### TEST 2: Diálogos
- [ ] ¿Ves al Profesor?
- [ ] ¿Canvas muestra primer diálogo?
- [ ] Presiona "Siguiente"
- [ ] ¿Cambia a diálogo 2?
- [ ] Presiona "Siguiente" 7 veces más
- [ ] ¿Diálogo 8 es el último?

### TEST 3: Minijuego Extintor
- [ ] Aún en Play, después del último diálogo
- [ ] ¿Aparecen 3 fuegos?
- [ ] ¿Canvas muestra: Tiempo 150s, Puntos 0?
- [ ] Agarra extintor
- [ ] Presiona trigger
- [ ] ¿Sale espuma?
- [ ] Apunta a fuego
- [ ] ¿El fuego se apaga?

### TEST 4: Resultados
- [ ] Después de apagar todos
- [ ] ¿Canvas de resultados aparece?
- [ ] ¿Muestra "¡ÉXITO!"?
- [ ] ¿Muestra puntuación?
- [ ] Presiona "Volver a Lobby"
- [ ] ¿Vuelves al Lobby?

### TEST 5: Dificultades
- [ ] En Lobby, selecciona "Sismo" → "Difícil"
- [ ] ¿Se carga EarthquakeLesson?
- [ ] ¿Hay más escombros que antes?
- [ ] Vuelve a Lobby
- [ ] Selecciona "Extintor" → "Normal"
- [ ] ¿Hay 5 fuegos (no 3)?

---

## ✅ RESULTADO FINAL

Si pasaste TODOS los tests:

```
✅ Lobby funcional
✅ Selección de módulo + dificultad
✅ Diálogos del profesor
✅ Minijuego extintor con fuegos reales
✅ Minijuego sismo con escombros reales
✅ Pantalla de resultados
✅ Transiciones entre escenas
✅ Sistema de dificultad A/B/C
✅ Reintentar y volver a Lobby

🎉 ¡PROYECTO COMPLETO!
```

---

## 🚀 SIGUIENTE

Cuando todo funcione:

1. **Agregar sonidos** (opcional)
2. **Mejorar materiales y texturas** (opcional)
3. **Crear prefabs reutilizables** (opcional)
4. **Tesar en VR real** (importante)
5. **Balancear dificultades** (importante)

---

*Checklist Completo - De Código a Juego*
*29 de Noviembre, 2025*
