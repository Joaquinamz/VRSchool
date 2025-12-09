# 🔥 FIX: Usar Canvas Existente para Curso de Extintor

## ✅ Lo que acabamos de arreglar

El Canvas existente "UI - Cursos Menu - Panel" ahora funciona correctamente para el curso de Extintor.

### El problema anterior:
- Canvas estaba configurado para mostrar diálogos de Sismo
- Nunca iniciaba `FireGameManager`
- No había secuencia de fuegos

### La solución:
- ✅ `NPCProfessor.cs` ahora detecta correctamente "Extintor"
- ✅ Inicia automáticamente `FireGameManager.CompleteIntroduction()`
- ✅ Maneja la secuencia completa: Diálogo → Fuego → Diálogo → Múltiples Fuegos → Resultados

---

## 🎯 Cómo hacer que el Canvas existente funcione

### Paso 1: En el Lobby (cuando seleccionas el curso)
```
Presionas botón "Extintor A/B/C"
↓
LobbyManager.SelectCourse("Extintor", "A")
↓
GameManager.selectedCourse = "Extintor"
↓
Se carga escena (ClassroomScene o FireExtinguisherLesson)
```

### Paso 2: En la escena del juego
```
Canvas "UI - Cursos Menu - Panel" aparece
↓
NPCProfessor.Start() se ejecuta
  - Lee: GameManager.selectedCourse = "Extintor" ✓
  - Log en Console: "[NPCProfessor] Curso seleccionado: 'Extintor'"
↓
ShowIntroduction() detecta que es Extintor
↓
Muestra diálogos de Extintor (no de Sismo)
  1. "Hola estudiantes, hoy aprenderemos a usar un extintor"
  2. "Es muy importante saber cómo actuar en caso de incendio"
  3. "Vamos a practicar: Aquí hay un fuego pequeño"
  4. "Intenta apagarlo usando el extintor"
  5. "¡Presiona siguiente cuando estés listo!"
↓
Usuario clickea "Siguiente" 5 veces
↓
Se termina diálogo de introducción
↓
EndIntroduction() se ejecuta
  - Log en Console: "[NPCProfessor] Llamando a FireGameManager.CompleteIntroduction()"
↓
FireGameManager.CompleteIntroduction() se ejecuta
  - Inicia: StartFirstFire()
  - 1 fuego spawns en la escena
  - Timer empieza
```

### Paso 3: Usuario apaga el fuego
```
Usa extintor dual-hitbox
↓
Fuego.TakeDamage() se ejecuta
↓
Fuego.currentIntensity llega a 0
↓
Fuego desaparece
↓
FireGameManager.CheckFirstFireCompletion() detecta que no hay fuegos
↓
FireGameManager.CompleteFirstFirePhase() se ejecuta
  - Guarda el tiempo
  - Llama: NPCProfessor.ShowPostFirstFireDialogue()
↓
Canvas muestra nuevo diálogo
  1. "¡Excelente! Apagaste el fuego correctamente"
  2. "Ese era un fuego pequeño de entrenamiento"
  3. "Ahora vamos a practicar con múltiples fuegos"
  4. "Este será el desafío final del curso"
  5. "¡Presiona siguiente cuando estés listo!"
  6. "¡Tú puedes!"
↓
Usuario clickea "Siguiente" 6 veces
↓
Se termina diálogo post-primer-fuego
↓
OnPostFirstFireDialogueComplete() se ejecuta
  - Log en Console: "[NPCProfessor] Llamando a FireGameManager.CompletePostFireDialogue()"
↓
FireGameManager.CompletePostFireDialogue() se ejecuta
  - Inicia: StartMultipleFires(3)
  - 3 fuegos spawns en la escena
  - Timer continúa
```

### Paso 4: Usuario apaga múltiples fuegos
```
Usa extintor para apagar los 3 fuegos
↓
FireGameManager.CheckMultipleFiresCompletion() detecta que no hay fuegos
↓
FireGameManager.CompleteMultipleFiresPhase() se ejecuta
  - Calcula score
  - Llama: ShowResults()
↓
Canvas_Results aparece (automáticamente se activa)
  - Score: [número calculado]
  - Feedback: "¡Excelente!" / "¡Bueno!" / "Aceptable" / "Necesitas practicar"
  - Botones: Reintentar / Volver al Menú
```

---

## 🔌 Asignaciones que NECESITAS hacer en Inspector

Esto es lo MÁS IMPORTANTE. Sin estas asignaciones, nada funcionará.

### En objeto `Professor` (o donde esté NPCProfessor.cs):
```
Componente: NPCProfessor

Campos a rellenar:
- dialogueText
  ↳ Arrastra: Canvas → Panel → Text (el TextMeshPro que muestra diálogos)
  
- nextButton
  ↳ Arrastra: Canvas → Panel → Button (el botón "Siguiente")
  
- gameController
  ↳ Arrastra: Objeto FireGameManager (IMPORTANTE - debe ser la raíz del objeto)
```

### En objeto `FireGameManager`:
```
Componente: FireGameManager

Campos a rellenar:
- professorController
  ↳ Arrastra: Objeto Professor
  
- firePrefab
  ↳ Arrastra: Prefab Fire (Assets/Prefabs/Fire.prefab o donde lo tengas)
  
- uiTimer
  ↳ Arrastra: Canvas → TextMeshPro que muestra "Tiempo: Xs"
  
- uiFiresRemaining
  ↳ Arrastra: Canvas → TextMeshPro que muestra "Fuegos: X/X"
  
- resultsCanvas
  ↳ Arrastra: Canvas_Results (el canvas completo, NO un objeto dentro)
  
- scoreText
  ↳ Arrastra: Canvas_Results → Panel → Text_Score
  
- feedbackText
  ↳ Arrastra: Canvas_Results → Panel → Text_Feedback
```

---

## 🐛 DEBUG: Cómo saber si funciona

### Paso 1: Abre Console (Window → General → Console)

### Paso 2: Presiona PLAY

### Paso 3: Selecciona "Extintor" en el lobby

### Paso 4: Mira los logs en Console:

✅ **Si ves esto, está funcionando:**
```
[NPCProfessor] Curso seleccionado: 'Extintor'
[NPCProfessor.ShowIntroduction] Curso detectado como Extintor: True
[NPCProfessor.EndIntroduction] Iniciando juego...
[NPCProfessor] Llamando a FireGameManager.CompleteIntroduction()
```

❌ **Si ves esto, hay un problema:**
```
[NPCProfessor] Curso seleccionado: '' ← VACÍO
```
→ Significa que `GameManager.selectedCourse` no está siendo asignado por LobbyManager

```
[NPCProfessor] ❌ FireGameManager no asignado en Inspector
```
→ Significa que no arrastraste FireGameManager al campo `gameController`

---

## 🎯 Checklist Final

- [ ] Canvas existente "UI - Cursos Menu - Panel" está en la escena
  - [ ] Tiene TextMeshPro para diálogos
  - [ ] Tiene Button "Siguiente"
  
- [ ] Objeto `Professor` con `NPCProfessor.cs`
  - [ ] `dialogueText` asignado al TextMeshPro del canvas
  - [ ] `nextButton` asignado al Button del canvas
  - [ ] `gameController` asignado a objeto `FireGameManager`
  
- [ ] Objeto `FireGameManager` con `FireGameManager.cs`
  - [ ] `professorController` asignado a `Professor`
  - [ ] `firePrefab` asignado a prefab Fire
  - [ ] `uiTimer` asignado a TextMeshPro
  - [ ] `uiFiresRemaining` asignado a TextMeshPro
  - [ ] `resultsCanvas` asignado a Canvas_Results
  - [ ] `scoreText` asignado a Text en Canvas_Results
  - [ ] `feedbackText` asignado a Text en Canvas_Results
  
- [ ] Prefab `Fire` existe y tiene:
  - [ ] FireBehavior.cs
  - [ ] Sphere Collider (Is Trigger ON)
  - [ ] Rigidbody

- [ ] Canvas_Results existe y está:
  - [ ] INACTIVO (checkbox OFF)
  - [ ] Con Text_Score
  - [ ] Con Text_Feedback
  - [ ] Con botones Reintentar/Menu

---

## 📝 Resumen del Flujo Completo

```
LOBBY
  ├─ Click "Extintor A"
  └─ LobbyManager asigna: GameManager.selectedCourse = "Extintor"

ESCENA DEL JUEGO
  ├─ NPCProfessor.Start()
  │  └─ Detecta: selectedCourse = "Extintor" ✓
  │
  ├─ ShowIntroduction()
  │  └─ Muestra diálogos de Extintor (5 líneas)
  │
  ├─ Usuario clickea "Siguiente" 5x
  │
  ├─ EndIntroduction()
  │  └─ Llama: FireGameManager.CompleteIntroduction()
  │
  ├─ PRIMER FUEGO
  │  ├─ 1 fuego spawns
  │  ├─ Usuario apaga
  │  └─ Timer guardado
  │
  ├─ ShowPostFirstFireDialogue()
  │  └─ Muestra diálogos post-fuego (6 líneas)
  │
  ├─ Usuario clickea "Siguiente" 6x
  │
  ├─ OnPostFirstFireDialogueComplete()
  │  └─ Llama: FireGameManager.CompletePostFireDialogue()
  │
  ├─ MÚLTIPLES FUEGOS (3 o más)
  │  ├─ 3+ fuegos spawns
  │  ├─ Usuario apaga todos
  │  └─ Timer terminado
  │
  └─ RESULTADOS
     ├─ Canvas_Results se activa automáticamente
     ├─ Muestra Score
     ├─ Muestra Feedback
     └─ Botones Reintentar/Menu
```

---

**Si todo está correcto en Inspector, el flujo debería ser automático. ¡No necesitas tocar nada más!**
