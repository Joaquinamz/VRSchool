# 🎮 GUÍA CONFIGURACIÓN CANVAS EXTINTOR - FireExtinguisherLesson1

## Objetivo
Configurar 3 Canvas independientes para mostrar:
1. **Canvas de Diálogo** - Introducción y diálogos post-fuegos
2. **Canvas de Minijuego** - Progreso en tiempo real (timer + fuegos restantes)
3. **Canvas de Resultados** - Puntuación y feedback final

---

## PASO 1: Crear Canvas de Diálogo (YA DEBE EXISTIR)

**Nombre:** `UI - Cursos Menu - Panel`

**Estructura esperada:**
```
Canvas (UI - Cursos Menu - Panel)
├── Text_Dialogo (TextMeshPro)
│   └── Texto del profesor
└── Button_Siguiente (Button)
    └── Text (TextMeshPro)
```

**Asignaciones en Inspector:**
- Script `NPCProfessor`
  - `dialogueText` → arrastra `Text_Dialogo`
  - `nextButton` → arrastra `Button_Siguiente`
  - `gameController` → arrastra GameObject con `FireGameManager`

---

## PASO 2: Crear Canvas de Minijuego (NUEVO - IMPORTANTE)

### 2.1 Crear el Canvas
En Hierarchy: `Right Click → UI → Canvas`
- Nombre: `Canvas_Minigame` (o similar)
- Posición: X=0, Y=0, Z=0
- Scale: X=1, Y=1, Z=1

### 2.2 Agregar elementos UI dentro del Canvas

**2.2.1 Panel de fondo (opcional):**
```
Canvas_Minigame
└── Panel_Fondo
    ├── Image (color negro semi-transparente)
    ├── Text_Timer (Título)
    │   └── "Tiempo: 0.0s"
    ├── Text_FiresCount (Información)
    │   └── "Fuegos: 3/3"
```

**Estructura recomendada:**

1. **Text_Timer** (TextMeshPro)
   - Posición: X=0, Y=150, Z=0 (arriba a la izquierda)
   - Tamaño: 200x100
   - Texto: "Tiempo: 0.0s"
   - Color: Blanco
   - Tamaño fuente: 36

2. **Text_FiresCount** (TextMeshPro)
   - Posición: X=0, Y=50, Z=0
   - Tamaño: 200x100
   - Texto: "Fuegos: 3/3"
   - Color: Rojo
   - Tamaño fuente: 36

3. **Text_Status** (TextMeshPro)
   - Posición: X=0, Y=-100, Z=0 (centro)
   - Tamaño: 800x100
   - Texto: "Apaga todos los 3 fuegos con el extintor"
   - Color: Blanco
   - Tamaño fuente: 32

### 2.3 Asignar Canvas_Minigame al FireMinigameManager

En `FireMinigameManager` (en Hierarchy, selecciona el GameObject que tiene este script):

**Inspector:**
```
FireMinigameManager (Script)
├── minigameCanvas → arrastra Canvas_Minigame
├── uiFiresRemaining → arrastra Text_FiresCount
├── uiTimer → arrastra Text_Timer
└── statusText → arrastra Text_Status
```

---

## PASO 3: Crear Canvas de Resultados (NUEVO - MUY IMPORTANTE)

### 3.1 Crear el Canvas
En Hierarchy: `Right Click → UI → Canvas`
- Nombre: `Canvas_Results`
- Posición: X=0, Y=0, Z=0

### 3.2 Agregar elementos de resultados

```
Canvas_Results
├── Panel_Fondo
│   ├── Image (color azul oscuro)
│   ├── Text_Titulo
│   │   └── "¡LECCIÓN COMPLETADA!"
│   ├── Text_Score
│   │   └── "Puntuación: 250"
│   ├── Text_Feedback
│   │   └── "¡Bueno! Completaste la lección correctamente."
│   └── Button_Return (opcional, para volver a lobby)
│       └── Text ("Volver")
```

**Elementos necesarios:**

1. **Text_Titulo** (TextMeshPro)
   - Posición: X=0, Y=200, Z=0 (arriba)
   - Tamaño: 600x100
   - Texto: "¡LECCIÓN COMPLETADA!"
   - Color: Amarillo
   - Tamaño fuente: 48
   - Alineación: Centro

2. **Text_Score** (TextMeshPro)
   - Posición: X=0, Y=50, Z=0
   - Tamaño: 400x100
   - Texto: "Puntuación: 250"
   - Color: Blanco
   - Tamaño fuente: 40
   - Alineación: Centro

3. **Text_Feedback** (TextMeshPro)
   - Posición: X=0, Y=-100, Z=0
   - Tamaño: 600x150
   - Texto: "¡Bueno! Completaste la lección correctamente."
   - Color: Verde claro
   - Tamaño fuente: 32
   - Alineación: Centro

### 3.3 Asignar Canvas_Results al FireMinigameManager

En `FireMinigameManager`:

**Inspector:**
```
FireMinigameManager (Script)
├── resultsCanvas → arrastra Canvas_Results
├── scoreText → arrastra Text_Score
└── feedbackText → arrastra Text_Feedback
```

---

## PASO 4: Configurar FireMinigameManager (Verificar)

**Asignaciones finales (revisar en Inspector):**

```
FireMinigameManager
├── [Profesor Controller] 
│   └── NPCProfessor (el GameObject)
├── [Fire Prefab]
│   └── Prefab del fuego
├── [UI Fires Remaining]
│   └── Text_FiresCount
├── [UI Timer]
│   └── Text_Timer
├── [Status Text]
│   └── Text_Status
├── [Minigame Canvas]
│   └── Canvas_Minigame ✓ NUEVO
├── [Results Canvas]
│   └── Canvas_Results ✓ NUEVO
├── [Score Text]
│   └── Text_Score
├── [Feedback Text]
│   └── Text_Feedback
└── [Fire Spawn Points] (Array)
    ├── [0] → Transform de Fuego 1
    ├── [1] → Transform de Fuego 2
    └── [2] → Transform de Fuego 3
```

---

## PASO 5: Verificar Flujo Completo

**Secuencia esperada:**

1. ✓ PLAY → Canvas_Diálogo visible
2. ✓ Presionar "Siguiente" → Diálogo de introducción
3. ✓ Último "Siguiente" → Spawneá fuego de práctica
4. ✓ Apagar fuego de práctica → Diálogo post-fuego
5. ✓ Siguiente → Canvas_Minigame aparece + 3 fuegos spawneados
6. ✓ Timer y contador de fuegos se actualizan en tiempo real
7. ✓ Apagar todos los fuegos → Desaparece Canvas_Minigame
8. ✓ Diálogo final → Canvas_Results aparece con puntuación

---

## CHECKLIST

- [ ] Canvas_Minigame existe y está desactivado al inicio
- [ ] Canvas_Results existe y está desactivado al inicio
- [ ] minigameCanvas asignado en FireMinigameManager
- [ ] resultsCanvas asignado en FireMinigameManager
- [ ] Todos los TextMeshPro asignados correctamente
- [ ] fireSpawnPoints array tiene 3 Transforms
- [ ] Console muestra logs de actualización de UI

---

## TROUBLESHOOTING

### Problema: Canvas no aparece en minijuego
**Solución:** Verifica que `minigameCanvas` esté asignado en Inspector

### Problema: Timer no se actualiza
**Solución:** Verifica que `uiTimer` esté asignado y que Update() esté siendo ejecutado

### Problema: Fuegos no cuentan correctamente
**Solución:** Mira la Console para logs de `CountActiveFires()` y verifica que FireBehavior tenga `currentIntensity`

### Problema: Resultados no aparecen
**Solución:** 
1. Verifica que `resultsCanvas` esté asignado
2. Verifica que `scoreText` y `feedbackText` estén asignados
3. Mira Console para log "Canvas de resultados activado"

---

## NOTAS IMPORTANTES

⚠️ **No reutilizar Canvas:** Cada Canvas es independiente para evitar conflictos de capas

⚠️ **Orden de actualización:** FireGameManager → FireMinigameManager → NPCProfessor (en cascada)

⚠️ **Destrucción de fuegos:** Cada fuego se destruye individualmente cuando `currentIntensity <= 0`

⚠️ **Logging:** Todos los eventos están logeados en Console para debugging
