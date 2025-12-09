# ⚡ CANVAS - REFERENCIA RÁPIDA (TL;DR)

## Los 3 Canvas que necesitas

### 1️⃣ Canvas_Dialogue (DIÁLOGOS + BOTÓN)
```
- Qué: Texto del profesor + botón "Siguiente"
- Dónde: Raíz Hierarchy
- Estado: ACTIVO ✓
- Dentro: Panel_Dialogue
  - Text_Dialogue (el diálogo)
  - Button_Next (el botón)
```

### 2️⃣ Canvas_Gameplay (INFO DE JUEGO)
```
- Qué: Timer + Contador de fuegos
- Dónde: Raíz Hierarchy
- Estado: ACTIVO ✓
- Dentro: 
  - Text_Timer (muestra segundos)
  - Text_Fires (muestra "3/3")
```

### 3️⃣ Canvas_Results (RESULTADOS)
```
- Qué: Score + Feedback + Botones
- Dónde: Raíz Hierarchy
- Estado: INACTIVO ❌ ← ¡IMPORTANTE!
- Dentro: Panel_Results
  - Text_Score (muestra puntuación)
  - Text_Feedback (muestra "¡Excelente!")
  - Button_Retry (reintentar)
  - Button_Menu (volver)
```

---

## Qué va a pasar

```
PLAY
  ↓
Canvas_Dialogue ACTIVO → Profesor habla
  ↓
Click "Siguiente" 6x
  ↓
Canvas_Dialogue INACTIVO
Canvas_Gameplay ACTIVO → Primer fuego + timer
  ↓
Apagaste fuego
  ↓
Canvas_Gameplay INACTIVO
Canvas_Dialogue ACTIVO → Profesor habla de nuevo
  ↓
Click "Siguiente" 6x
  ↓
Canvas_Dialogue INACTIVO
Canvas_Gameplay ACTIVO → Múltiples fuegos + timer
  ↓
Apagaste todos
  ↓
Canvas_Gameplay INACTIVO
Canvas_Results ACTIVO (se activa automáticamente) → Score + botones
```

---

## Asignaciones en Inspector

### NPCProfessor.cs (en objeto Professor)
- `dialogueText` ← Canvas_Dialogue > Panel > Text
- `nextButton` ← Canvas_Dialogue > Panel > Button
- `gameController` ← FireGameManager (objeto)

### FireGameManager.cs (en objeto FireGameManager)
- `uiTimer` ← Canvas_Gameplay > Text_Timer
- `uiFiresRemaining` ← Canvas_Gameplay > Text_Fires
- `resultsCanvas` ← Canvas_Results (EL CANVAS, no un objeto dentro)
- `scoreText` ← Canvas_Results > Panel > Text_Score
- `feedbackText` ← Canvas_Results > Panel > Text_Feedback

---

## Ojo: Canvas_Results DEBE estar INACTIVO

En Inspector de Canvas_Results:
```
[ ] Canvas_Results ← Checkbox OFF (desmarcado)
```

Si está marcado ✓, el juego mostrará los resultados desde el inicio (MAL).

---

**Para DETALLES**: Lee `CANVAS_GUIA_DEFINITIVA.md`
