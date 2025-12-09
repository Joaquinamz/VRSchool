# 🔄 FLUJO DE EVENTOS - GUÍA COMPLETA

**Tu pregunta**: ¿Cómo funcionan los eventos? ¿Cómo se encadenan?

**Respuesta**: Este documento explica EXACTAMENTE cómo fluye la lógica.

---

## 🎯 ARQUITECTURA DEL FLUJO

```
INICIO
  ↓
Usuario selecciona módulo en Lobby
  ↓
CourseManager.SelectModule() se llama
  ↓
Escena se carga (FireExtinguisherLesson.unity)
  ↓
InstructorController.ShowDialogue(0) se llama
  ↓
Canvas muestra primer diálogo
  ↓
Usuario presiona "Siguiente"
  ↓
InstructorController.OnNextButtonPressed()
  ↓
Diálogo 1 → 2 → 3... → 8
  ↓
Último diálogo (8)
  ↓
CourseManager.StartGamePhase()
  ↓
FireGameManager.StartGame()
  ↓
Aparecen 5 fuegos
  ↓
Usuario apaga fuegos con extintor
  ↓
Todos apagados → FireGameManager.EndGame()
  ↓
ResultsScreen.ShowResults()
  ↓
Usuario presiona "Volver a Lobby"
  ↓
CourseManager.ReturnToLobby()
  ↓
Vuelve a Lobby
  ↓
CICLO COMPLETO
```

---

## 📊 DETALLES DEL FLUJO

### 1️⃣ SELECCIÓN EN LOBBY

**Archivo**: LobbyManager.cs

```csharp
// Usuario hace clic en botón "Extintor"
→ OnModuleSelected(CourseManager.ModuleType.FireExtinguisher)
  {
    - Guarda módulo seleccionado
    - Muestra panel de dificultad
  }

// Usuario selecciona "Fácil"
→ OnDifficultySelected(CourseManager.Difficulty.A)
  {
    - Guarda dificultad seleccionada
  }

// Usuario presiona "Confirmar"
→ OnConfirmModule()
  {
    - Llama: CourseManager.Instance.SelectModule(módulo, dificultad)
  }
```

### 2️⃣ CARGANDO LA ESCENA

**Archivo**: CourseManager.cs

```csharp
→ SelectModule(FireExtinguisher, Fácil)
  {
    - Guarda: selectedModule = FireExtinguisher
    - Guarda: selectedDifficulty = Fácil
    - Dispara: OnModuleSelected.Invoke(FireExtinguisher)
    - Llama: LoadModuleScene(FireExtinguisher)
      {
        - Obtiene nombre escena: "FireExtinguisherLesson"
        - Llama: SceneManager.LoadScene("FireExtinguisherLesson")
      }
  }
```

**Resultado**: Se carga FireExtinguisherLesson.unity

---

### 3️⃣ CUANDO LA ESCENA CARGA

**Automáticamente se ejecuta**:

```csharp
// InstructorController.cs (Start)
→ Start()
  {
    - Busca referencias (Canvas, Botón, etc)
    - Carga diálogos según módulo
    - Llama: ShowDialogue(0)  // Primer diálogo
  }

// FireGameManager.cs (Start)
→ Start()
  {
    - Se prepara pero NO comienza el juego
    - Espera a que termine los diálogos
  }
```

---

### 4️⃣ MOSTRANDO DIÁLOGOS

**Archivo**: InstructorController.cs

```csharp
→ ShowDialogue(0)
  {
    - Pone el texto del diálogo 0 en Canvas
    - Canvas se vuelve visible
    - Botón "Siguiente" se muestra
    
    PANTALLA: "¡Hola! Aprenderemos a usar un extintor..."
    BOTÓN: "Siguiente"
  }

// Usuario presiona botón "Siguiente"
→ OnNextButtonPressed()
  {
    - Incrementa: currentDialogueIndex++
    - Llama: ShowDialogue(1)
  }

→ ShowDialogue(1)
  {
    - Cambia texto al diálogo 1
    
    PANTALLA: "El extintor tiene 3 partes..."
    BOTÓN: "Siguiente"
  }

// Usuario presiona "Siguiente" 7 veces más...
// ... hasta el diálogo 8

→ ShowDialogue(8)  // ÚLTIMO DIÁLOGO
  {
    - Pone texto final
    - Después de este, inicia el minijuego
    
    PANTALLA: "¡Ahora apaga los fuegos! ¡Buena suerte!"
    BOTÓN: "Siguiente" (pero es el último)
  }

// Usuario presiona "Siguiente" (final)
→ OnNextButtonPressed()
  {
    - Detecta: currentDialogueIndex >= 8
    - Llama: CourseManager.Instance.StartGamePhase()
  }
```

---

### 5️⃣ INICIANDO EL MINIJUEGO

**Archivo**: CourseManager.cs

```csharp
→ StartGamePhase()
  {
    - Cambia estado: currentState = InGame
    - Dispara: OnGameStarted.Invoke()
  }

// FireGameManager se suscribió a este evento
→ FireGameManager.OnGameStarted
  {
    - Oye el evento
    - Llama: StartGame()
  }
```

**Archivo**: FireGameManager.cs

```csharp
→ StartGame()
  {
    - Según dificultad (A/B/C):
        Fácil (A): 3 fuegos, 150s, radius 6m
        Normal (B): 5 fuegos, 120s, radius 8m
        Difícil (C): 7 fuegos, 90s, radius 12m
    
    - Crea y posiciona los fuegos
    - Inicia el timer
    - Inicia el contador de puntos
    - Canvas del juego se vuelve visible
    
    PANTALLA: "Timer: 150s | Puntos: 0 | Fuegos: 3"
  }
```

**Usuario ahora puede**:
- Agarrar el extintor
- Presionar trigger para disparar espuma
- Apagar los fuegos

---

### 6️⃣ DURANTE EL MINIJUEGO

**Archivo**: FireGameManager.cs (Update)

```csharp
Cada frame:
  - Reduce timer: timeRemaining -= Time.deltaTime
  - Incrementa puntos: currentScore += points
  - Actualiza Canvas con nuevos valores
  
  Si timeRemaining <= 0:
    → EndGame(false)  // Tiempo agotado, FRACASO
  
  Si allFires apagados:
    → EndGame(true)   // ¡ÉXITO!
```

**Archivo**: FireBehavior.cs (cada fuego)

```csharp
Update:
  - Si se está apagando (ReduceIntensity fue llamado)
  - Reduce intensidad del fuego
  
  Si intensidad <= 0:
    - Destroys al fuego
    - Llama: fireGame.OnFireExtinguished()
    - Se suma puntuación
```

---

### 7️⃣ CUANDO TERMINA EL MINIJUEGO

**Archivo**: FireGameManager.cs

```csharp
→ EndGame(success = true)
  {
    - Detiene timer
    - Calcula puntuación final
    - Crea CourseResults con datos
    - Llama: CourseManager.Instance.CompleteGamePhase(results)
  }

→ CourseManager.CompleteGamePhase(results)
  {
    - Cambia estado: currentState = PostGame
    - Dispara: OnGameCompleted.Invoke()
  }

// ResultsScreen se suscribió a este evento
→ ResultsScreen.OnGameCompleted
  {
    - Oye el evento
    - Llama: ShowResults(results)
  }
```

**Archivo**: ResultsScreen.cs

```csharp
→ ShowResults(results)
  {
    - Canvas de resultados se vuelve visible
    - Pone: "¡ÉXITO!" o "TIEMPO AGOTADO"
    - Pone: "Puntuación: 450"
    - Pone: "Tiempo: 120s"
    - Botones: "Reintentar" y "Volver a Lobby"
    
    PANTALLA: Resultado final
  }
```

---

### 8️⃣ DESPUÉS DE RESULTADOS

**Usuario puede**:

**Opción A: Reintentar**
```csharp
// Usuario presiona "Reintentar"
→ OnRetryPressed()
  {
    - Llama: CourseManager.Instance.RetryModule()
  }

→ RetryModule()
  {
    - Llama: SelectModule(FireExtinguisher, Fácil)
    - Vuelve al paso 2️⃣ (se recarga la escena)
  }
```

**Opción B: Volver a Lobby**
```csharp
// Usuario presiona "Volver a Lobby"
→ OnLobbyPressed()
  {
    - Llama: CourseManager.Instance.ReturnToLobby()
  }

→ ReturnToLobby()
  {
    - Cambia estado: currentState = AtLobby
    - Resetea dificultad: selectedDifficulty = B
    - Carga escena: "LobbyVR"
  }
```

---

## 🔗 EVENTOS CLAVE (El corazón del flujo)

### CourseManager.cs

```csharp
// En Select(): se dispara cuando seleccionas módulo
OnModuleSelected?.Invoke(module);

// En ChangeState(): se dispara cuando cambias estado
OnStateChanged?.Invoke(newState);

// Cuando inicia minijuego
OnGameStarted?.Invoke();

// Cuando termina minijuego
OnGameCompleted?.Invoke();
```

### ¿Quién se suscribe?

```csharp
// InstructorController se suscribe a OnGameCompleted
CourseManager.Instance.OnGameCompleted += HideDialogueCanvas;

// FireGameManager se suscribe a OnGameStarted
CourseManager.Instance.OnGameStarted += StartGame;

// ResultsScreen se suscribe a OnGameCompleted
CourseManager.Instance.OnGameCompleted += ShowResults;
```

---

## 🎓 RESUMEN DEL FLUJO

```
1. Usuario selecciona Módulo + Dificultad en Lobby
2. CourseManager.SelectModule() carga escena
3. InstructorController.ShowDialogue() muestra diálogos
4. Usuario presiona "Siguiente" 8 veces
5. Último diálogo → Llama StartGamePhase()
6. Evento OnGameStarted dispara → FireGameManager.StartGame()
7. Minijuego activo (usuario apaga fuegos)
8. Todos apagados → EndGame(true)
9. Evento OnGameCompleted dispara → ResultsScreen.ShowResults()
10. Usuario ve resultados
11. Presiona "Volver a Lobby" → ReturnToLobby()
12. Vuelve al paso 1
```

---

## 💡 PUNTO CLAVE: Eventos vs Funciones

### ❌ Malo (Acoplado):
```csharp
// InstructorController toca a FireGameManager directamente
fireGameManager.StartGame();  // MALO: dependencia directa
```

### ✅ Bien (Con eventos):
```csharp
// CourseManager dispara evento
OnGameStarted?.Invoke();

// FireGameManager escucha sin saber de InstructorController
CourseManager.Instance.OnGameStarted += StartGame;
```

**Beneficio**: Los sistemas son independientes y pueden reutilizarse.

---

## 🔧 CÓMO AGREGAR NUEVO EVENTO

Si quieres agregar un evento propio (ejemplo: cuando fuego es 50% apagado):

**En FireBehavior.cs**:
```csharp
public event System.Action OnHalfExtinguished;

void Update()
{
    if (currentIntensity <= maxIntensity / 2)
    {
        OnHalfExtinguished?.Invoke();
    }
}
```

**En otro script**:
```csharp
void Start()
{
    fireBehavior.OnHalfExtinguished += PlaySoundEffect;
}
```

---

## ✅ VERIFICAR EL FLUJO

Abre **Console** en Unity (Ctrl+`) y verás los Debug.Log:

```
🔧 Extintor listo
🖐️ Extintor AGARRADO
💨 TRIGGER PRESIONADO
🔥 Fuego apagándose
🔥 Fuego EXTINGUIDO
📊 Resultados mostrados
✅ Volviendo a Lobby
```

Si ves esto → **El flujo funciona perfectamente**

---

*Flujo de Eventos - Guía Completa*
*29 de Noviembre, 2025*
