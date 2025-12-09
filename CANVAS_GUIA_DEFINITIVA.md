# 🎨 CANVAS - GUÍA EXACTA Y CLARA

## 📌 LA REGLA DE ORO

**Tienes que crear 3 Canvas COMPLETAMENTE SEPARADOS:**

| Canvas | Propósito | Cuándo aparece | Cuándo desaparece |
|--------|-----------|---|---|
| **Canvas_Dialogue** | Diálogos del profesor + botón "Siguiente" | Inicio | Cuando termina cada diálogo |
| **Canvas_Gameplay** | Timer + contador de fuegos | Cuando empieza primer fuego | Cuando empieza resultados |
| **Canvas_Results** | Score + Feedback | Cuando termina el juego | Nunca (hasta reintentar) |

---

## 🎯 Canvas 1: DIÁLOGOS (El que presionas)

### Qué es:
- **Contiene**: Texto del diálogo + botón "Siguiente"
- **Cuándo se muestra**: Al inicio y después de cada fuego
- **Cuándo desaparece**: Cuando haces click en "Siguiente" 6 veces

### Dónde va en Hierarchy:
```
Hierarchy
├─ Canvas_Dialogue ← Canvas 1 (PADRE)
│  └─ Panel_Dialogue (Panel - fondo)
│     ├─ Text_Dialogue (TextMeshPro - el diálogo)
│     ├─ Button_Next (Button - botón "Siguiente")
```

### Cómo crearlo EXACTO:

**Paso 1: Crear el Canvas**
1. Right-click en Hierarchy
2. **UI → Canvas - TextMeshPro**
3. Rename: `Canvas_Dialogue`
4. En Inspector, busca **Canvas**
   - Render Mode: **Screen Space - Overlay** ← IMPORTANTE
5. En Inspector, busca **Canvas Scaler**
   - UI Scale Mode: **Scale With Screen Size**

**Paso 2: Crear el Panel (fondo)**
1. Right-click en `Canvas_Dialogue`
2. **UI → Panel - TextMeshPro**
3. Rename: `Panel_Dialogue`
4. En Inspector:
   - Layout: **Stretch, Stretch** (para llenar todo)
   - Color: Negro con Alpha = 0.8 (oscuro pero transparente)

**Paso 3: Crear el Texto (diálogo)**
1. Right-click en `Panel_Dialogue`
2. **UI → Text - TextMeshPro**
3. Rename: `Text_Dialogue`
4. En Inspector:
   - Text: "Hola, aquí va el diálogo"
   - Font Size: 36
   - Color: Blanco
   - Alignment: Center, Middle
   - Wrapping: ON

**Paso 4: Crear el Botón (Siguiente)**
1. Right-click en `Panel_Dialogue`
2. **UI → Button - TextMeshPro**
3. Rename: `Button_Next`
4. En Inspector:
   - Position: (0, -300, 0) ← abajo del texto
   - Size: (300, 100)
   - El botón auto-crea un hijo `Text` con "Button"
5. Edita el texto del botón:
   - Click en el hijo `Text` del botón
   - Cambia a: "Siguiente"
   - Font Size: 32

### Resultado:
```
Cuando presionas PLAY, verás:

┌─────────────────────────────────┐
│                                 │
│                                 │
│   Hola, aquí va el diálogo     │
│                                 │
│          [ Siguiente ]          │
│                                 │
└─────────────────────────────────┘
```

---

## 🎯 Canvas 2: GAMEPLAY (INFO en tiempo real)

### Qué es:
- **Contiene**: Timer (tiempo) + Contador de fuegos
- **Cuándo se muestra**: Cuando empieza el primer fuego
- **Cuándo desaparece**: Cuando aparecen los resultados

### Dónde va en Hierarchy:
```
Hierarchy
├─ Canvas_Gameplay ← Canvas 2 (PADRE)
│  ├─ Text_Timer (TextMeshPro - "Tiempo: 0s")
│  ├─ Text_Fires (TextMeshPro - "Fuegos: 1/1")
│  └─ Panel_Status (Panel - opcional)
│     └─ Text_Status (TextMeshPro - "Apaga el fuego")
```

### Cómo crearlo EXACTO:

**Paso 1: Crear el Canvas**
1. Right-click en Hierarchy
2. **UI → Canvas - TextMeshPro**
3. Rename: `Canvas_Gameplay`
4. En Inspector:
   - Render Mode: **Screen Space - Overlay**
   - Canvas Scaler → UI Scale Mode: **Scale With Screen Size**

**Paso 2: Crear Timer Text**
1. Right-click en `Canvas_Gameplay`
2. **UI → Text - TextMeshPro**
3. Rename: `Text_Timer`
4. En Inspector:
   - Position: (100, -50, 0) ← arriba a la izquierda
   - Size: (200, 50)
   - Text: "Tiempo: 0s"
   - Font Size: 24
   - Color: Blanco

**Paso 3: Crear Fires Counter**
1. Right-click en `Canvas_Gameplay`
2. **UI → Text - TextMeshPro**
3. Rename: `Text_Fires`
4. En Inspector:
   - Position: (100, -100, 0) ← debajo del timer
   - Size: (200, 50)
   - Text: "Fuegos: 1/1"
   - Font Size: 24
   - Color: Rojo (para que llame atención)

**Paso 4 (Opcional): Crear Panel de Estado**
1. Right-click en `Canvas_Gameplay`
2. **UI → Panel - TextMeshPro**
3. Rename: `Panel_Status`
4. En Inspector:
   - Position: (0, 0, 0)
   - Size: (600, 100)
   - Color: Azul semi-transparente
5. Dentro, crear texto:
   - Right-click en `Panel_Status` → **UI → Text - TextMeshPro**
   - Rename: `Text_Status`
   - Text: "Apaga todos los fuegos con el extintor"
   - Font Size: 28

### Resultado:
```
Cuando estés jugando:

Tiempo: 45s
Fuegos: 2/3

┌──────────────────────────────┐
│ Apaga todos los fuegos       │
└──────────────────────────────┘

[El juego ocurre aquí - fuegos y extintor]
```

---

## 🎯 Canvas 3: RESULTADOS (Automático)

### Qué es:
- **Contiene**: Score + Feedback + Botones Reintentar/Menú
- **Cuándo se muestra**: Cuando apagaste todos los fuegos
- **Cuándo desaparece**: Cuando presionas Reintentar o Menú

### Dónde va en Hierarchy:
```
Hierarchy
├─ Canvas_Results ← Canvas 3 (PADRE) - INICIALMENTE INACTIVO
│  └─ Panel_Results (Panel)
│     ├─ Text_Score (TextMeshPro - "Puntuación: 416")
│     ├─ Text_Feedback (TextMeshPro - "¡Excelente!")
│     ├─ Button_Retry (Button)
│     └─ Button_Menu (Button)
```

### Cómo crearlo EXACTO:

**Paso 1: Crear el Canvas**
1. Right-click en Hierarchy
2. **UI → Canvas - TextMeshPro**
3. Rename: `Canvas_Results`
4. En Inspector:
   - Render Mode: **Screen Space - Overlay**
   - **IMPORTANTE: Desactiva el checkbox ON (panel gris) para que sea INACTIVO**
5. Canvas Scaler → UI Scale Mode: **Scale With Screen Size**

**Paso 2: Crear el Panel**
1. Right-click en `Canvas_Results`
2. **UI → Panel - TextMeshPro**
3. Rename: `Panel_Results`
4. En Inspector:
   - Layout: **Stretch, Stretch**
   - Color: Negro con Alpha = 0.9 (bien oscuro)

**Paso 3: Crear Score Text**
1. Right-click en `Panel_Results`
2. **UI → Text - TextMeshPro**
3. Rename: `Text_Score`
4. En Inspector:
   - Position: (0, 100, 0) ← arriba
   - Size: (400, 80)
   - Text: "Puntuación: 416"
   - Font Size: 48
   - Color: Oro/Amarillo (para destacar)
   - Alignment: Center, Middle

**Paso 4: Crear Feedback Text**
1. Right-click en `Panel_Results`
2. **UI → Text - TextMeshPro**
3. Rename: `Text_Feedback`
4. En Inspector:
   - Position: (0, 0, 0) ← centro
   - Size: (500, 100)
   - Text: "¡Excelente! Completaste perfectamente"
   - Font Size: 36
   - Color: Verde claro
   - Wrapping: ON

**Paso 5: Crear Botón Reintentar**
1. Right-click en `Panel_Results`
2. **UI → Button - TextMeshPro**
3. Rename: `Button_Retry`
4. En Inspector:
   - Position: (-200, -150, 0) ← abajo izquierda
   - Size: (300, 80)
5. Edita el texto:
   - Click en hijo `Text`
   - Cambia a: "Reintentar"
   - Font Size: 32

**Paso 6: Crear Botón Menú**
1. Right-click en `Panel_Results`
2. **UI → Button - TextMeshPro**
3. Rename: `Button_Menu`
4. En Inspector:
   - Position: (200, -150, 0) ← abajo derecha
   - Size: (300, 80)
5. Edita el texto:
   - Click en hijo `Text`
   - Cambia a: "Volver al Menú"
   - Font Size: 32

### Resultado:
```
Cuando termina el juego:

┌─────────────────────────────────┐
│                                 │
│      Puntuación: 416            │
│                                 │
│  ¡Excelente! Completaste        │
│       perfectamente             │
│                                 │
│  [ Reintentar ]  [ Volver ]    │
│                                 │
└─────────────────────────────────┘
```

---

## 🔗 CONEXIÓN CON SCRIPTS

### Canvas_Dialogue → NPCProfessor.cs

En `NPCProfessor.cs`:
```csharp
public TextMeshProUGUI dialogueText;  // ← Arrastra: Canvas_Dialogue > Panel_Dialogue > Text_Dialogue
public Button nextButton;              // ← Arrastra: Canvas_Dialogue > Panel_Dialogue > Button_Next
public FireGameManager gameController; // ← Arrastra: objeto FireGameManager
```

### Canvas_Gameplay → FireGameManager.cs

En `FireGameManager.cs`:
```csharp
public TextMeshProUGUI uiTimer;           // ← Arrastra: Canvas_Gameplay > Text_Timer
public TextMeshProUGUI uiFiresRemaining; // ← Arrastra: Canvas_Gameplay > Text_Fires
public Canvas resultsCanvas;              // ← Arrastra: Canvas_Results (el canvas completo)
```

### Canvas_Results → FireGameManager.cs

En `FireGameManager.cs`:
```csharp
public Canvas resultsCanvas;              // ← Arrastra: Canvas_Results
public TextMeshProUGUI scoreText;         // ← Arrastra: Canvas_Results > Panel_Results > Text_Score
public TextMeshProUGUI feedbackText;      // ← Arrastra: Canvas_Results > Panel_Results > Text_Feedback
```

---

## ✅ CHECKLIST FINAL

- [ ] **Canvas_Dialogue** creado (ACTIVO)
  - [ ] Text_Dialogue dentro
  - [ ] Button_Next dentro
  
- [ ] **Canvas_Gameplay** creado (ACTIVO)
  - [ ] Text_Timer dentro
  - [ ] Text_Fires dentro
  
- [ ] **Canvas_Results** creado (INACTIVO ← IMPORTANTE)
  - [ ] Text_Score dentro
  - [ ] Text_Feedback dentro
  - [ ] Button_Retry dentro
  - [ ] Button_Menu dentro

- [ ] NPCProfessor.cs tiene referencias asignadas:
  - [ ] dialogueText → Canvas_Dialogue > Panel > Text
  - [ ] nextButton → Canvas_Dialogue > Panel > Button
  - [ ] gameController → FireGameManager

- [ ] FireGameManager.cs tiene referencias asignadas:
  - [ ] uiTimer → Canvas_Gameplay > Text_Timer
  - [ ] uiFiresRemaining → Canvas_Gameplay > Text_Fires
  - [ ] resultsCanvas → Canvas_Results
  - [ ] scoreText → Canvas_Results > Panel > Text_Score
  - [ ] feedbackText → Canvas_Results > Panel > Text_Feedback

---

## 🎬 FLUJO AUTOMÁTICO

```
1. Presionas PLAY
   ↓
2. Canvas_Dialogue aparece (Introduction)
   - Profesor: "Hola, aprenderemos a usar un extintor"
   - Usuario: clickea "Siguiente" 6 veces
   ↓
3. Canvas_Dialogue desaparece automáticamente
   Canvas_Gameplay aparece (First Fire)
   - 1 fuego spawns
   - Timer empieza
   - Contador: "Fuegos: 1/1"
   ↓
4. Usuario apaga fuego con extintor
   ↓
5. Canvas_Gameplay desaparece
   Canvas_Dialogue aparece (Post First Fire)
   - Profesor: "¡Excelente! Ahora múltiples fuegos"
   - Usuario: clickea "Siguiente" 6 veces
   ↓
6. Canvas_Dialogue desaparece automáticamente
   Canvas_Gameplay aparece (Multiple Fires)
   - 3 fuegos spawns
   - Contador: "Fuegos: 3/3"
   ↓
7. Usuario apaga todos los fuegos
   ↓
8. Canvas_Gameplay desaparece
   Canvas_Results aparece AUTOMÁTICAMENTE
   - Score: 416
   - Feedback: "¡Excelente!"
   - Botones para Reintentar o Volver
```

---

## 🎨 VISUALIZACIÓN

### Inicio
```
┌─ Screen ──────────────┐
│ Canvas_Dialogue       │ ← Visible (diálogos)
│ Canvas_Gameplay       │ ← INVISIBLE
│ Canvas_Results        │ ← INVISIBLE
└───────────────────────┘
```

### Durante Juego (Primer Fuego)
```
┌─ Screen ──────────────┐
│ Canvas_Dialogue       │ ← INVISIBLE
│ Canvas_Gameplay       │ ← Visible (timer + counter)
│ Canvas_Results        │ ← INVISIBLE
└───────────────────────┘
```

### Final
```
┌─ Screen ──────────────┐
│ Canvas_Dialogue       │ ← INVISIBLE
│ Canvas_Gameplay       │ ← INVISIBLE
│ Canvas_Results        │ ← Visible (score + feedback)
└───────────────────────┘
```

---

## 🆘 SI ALGO NO FUNCIONA

**P: "Canvas_Results no aparece al terminar"**
- R: Verifica que `Canvas_Results` esté INACTIVO al inicio (checkbox OFF en inspector)
- R: Verifica que `FireGameManager` tiene `resultsCanvas` asignado

**P: "El botón 'Siguiente' no funciona"**
- R: Verifica que `Button_Next` está asignado en `NPCProfessor.cs`
- R: Verifica que tiene `Button` component (no solo TextMeshPro)
- R: Verifica que el Canvas tiene un `EventSystem` en la escena

**P: "No veo el contador de fuegos"**
- R: Verifica que `Text_Fires` está asignado en `FireGameManager.cs`
- R: El Canvas_Gameplay debe estar ACTIVO cuando empieza el juego

---

**Conclusión**: 
- ✅ 3 Canvas SEPARADOS (Dialogue, Gameplay, Results)
- ✅ Cada uno se activa/desactiva automáticamente por los scripts
- ✅ Tú solo CREAS la estructura, los scripts hacen el rest
