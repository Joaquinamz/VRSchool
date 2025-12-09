# 🎲 VARIABILIDAD A/B/C - GUÍA DE DIFICULTADES

**Explicación de cómo el usuario puede elegir entre 3 variabilidades en cada módulo**

---

## ¿QUE ES LA VARIABILIDAD A/B/C?

Es un sistema donde cada módulo (Extintor o Sismo) tiene 3 variaciones diferentes:

- **A (Fácil)**: Menos elementos, más tiempo, menos puntos en juego
- **B (Normal)**: Estándar, balanceado
- **C (Difícil)**: Más elementos, menos tiempo, más desafiante

Esto permite que:
1. El usuario reintentar el MISMO módulo pero diferente
2. Practicar con dificultades progresivas
3. Obtener diferentes puntuaciones según la dificultad

---

## CÓMO FUNCIONA EN LA INTERFAZ

```
1. Usuario en LOBBY
   ↓
2. Click en "Extintor" o "Sismo"
   ↓
3. Panel de dificultad aparece:
   
   ┌─────────────────────────────┐
   │ Módulo: Extintor            │
   │ Selecciona dificultad:      │
   │                             │
   │ [A] Fácil    [B] Normal     │
   │ [C] Difícil  [Random]       │
   └─────────────────────────────┘
   
   ↓
4. Usuario elige (o Random elige por él)
   ↓
5. Cargar módulo CON ESA DIFICULTAD APLICADA
```

---

## EXTINTOR - VARIACIONES POR DIFICULTAD

### Dificultad A (Fácil)
```
Fuegos a apagar: 3
Tiempo: 150 segundos (2:30)
Radio de spawn: 6 metros
Puntos por fuego: 100 pts
Bonus de tiempo: 1 pt/segundo restante
Puntuación máxima: ~450 pts
```

**Estrategia**: Usuario tiene MUCHO tiempo, pocos fuegos, área pequeña
→ Perfecto para principiantes o practicar técnica

### Dificultad B (Normal)
```
Fuegos a apagar: 5
Tiempo: 120 segundos (2:00)
Radio de spawn: 8 metros
Puntos por fuego: 100 pts
Bonus de tiempo: 1 pt/segundo restante
Puntuación máxima: ~620 pts
```

**Estrategia**: Balanceado, requiere eficiencia
→ Prueba principal

### Dificultad C (Difícil)
```
Fuegos a apagar: 7
Tiempo: 90 segundos (1:30)
Radio de spawn: 12 metros
Puntos por fuego: 100 pts
Bonus de tiempo: 1 pt/segundo restante
Puntuación máxima: ~790 pts
```

**Estrategia**: Usuario debe ser RÁPIDO y eficiente
→ Para jugadores experimentados

### Dificultad Random
```
Sistema elige una de las 3 anteriores ALEATORIAMENTE
Usuario NO sabe cuál hasta que empieza
→ Para mayor desafío y variabilidad
```

---

## SISMO - VARIACIONES POR DIFICULTAD

### Dificultad A (Fácil)
```
Duración temblor: 6 segundos (en lugar de 8)
Caída de escombros: Pocos (cantidad: -20%)
Intensidad: Baja (shake camera menos agresivo)
Estudiantes que evacúan: 2
Tiempo evacuación: 20 segundos (más tiempo)
→ Principiantes: Menos presión, más tiempo para reaccionar
```

### Dificultad B (Normal)
```
Duración temblor: 8 segundos
Caída de escombros: Normal
Intensidad: Media (estándar)
Estudiantes que evacúan: 4
Tiempo evacuación: 15 segundos
→ Prueba balanceada
```

### Dificultad C (Difícil)
```
Duración temblor: 10 segundos
Caída de escombros: Muchos (+30%)
Intensidad: Alta (shake camera muy agresivo)
Estudiantes que evacúan: 5
Tiempo evacuación: 12 segundos (urgencia)
→ Desafiante: Muchos obstáculos, menos tiempo
```

---

## IMPLEMENTACIÓN EN CÓDIGO

### Paso 1: Usuario selecciona dificultad en Lobby

```csharp
// En LobbyManager.cs
// Usuario hace click en botón "Fácil (A)" por ejemplo

private void OnDifficultySelected(CourseManager.Difficulty difficulty)
{
    selectedDifficulty = difficulty; // Guardamos su elección
    CourseManager.Instance.SelectModule(selectedModuleType, selectedDifficulty);
}
```

### Paso 2: CourseManager guarda la elección

```csharp
// En CourseManager.cs
public void SelectModule(ModuleType module, Difficulty difficulty)
{
    selectedModule = module;
    selectedDifficulty = difficulty; // ← Guardamos AQUÍ
    
    // Si es Random, elegir una aleatoria
    if (difficulty == Difficulty.Random)
    {
        int randomChoice = Random.Range(0, 3);
        selectedDifficulty = (Difficulty)randomChoice;
    }
    
    LoadModuleScene(); // Cargar escena
}
```

### Paso 3: El GameManager aplica la dificultad

```csharp
// En FireGameManager.cs
public void StartGame()
{
    // CourseManager ya llamó a SetDifficulty() ANTES de StartGame()
    // Así que currentDifficulty ALREADY tiene el valor correcto
    
    isGameActive = true;
    timeRemaining = gameTimeLimit; // Usa el tiempo correcto por dificultad
    totalFiresToSpawn = totalFiresToSpawn; // USA los fuegos correctos
    
    SpawnFires(); // Genera los fuegos en el área correcta
}
```

---

## FLUJO COMPLETO: EJEMPLO

### Usuario elige Sismo, Dificultad C:

```
1. LobbyManager detecta click en "Sismo"
   → OnModuleSelected(ModuleType.Earthquake)
   → Muestra panel de dificultad

2. Usuario hace click "Difícil (C)"
   → OnDifficultySelected(Difficulty.C)
   → CourseManager.SelectModule(Earthquake, C)

3. CourseManager.SelectModule():
   → selectedModule = Earthquake
   → selectedDifficulty = Difficulty.C
   → LoadModuleScene() → Carga "EarthquakeLesson.unity"

4. En EarthquakeLesson.unity carga:
   → Profesor muestra diálogos
   → Usuario presiona "Siguiente" 8 veces

5. CourseManager.StartGamePhase():
   → Crea EarthquakeGameManager
   → Llama: earthquakeGame.SetDifficulty(Difficulty.C)
   → Llama: earthquakeGame.StartGame()

6. EarthquakeGameManager inicia CON DIFICULTAD C:
   → Temblor: 10 segundos (más largo)
   → Escombros: +30% (más muchos)
   → Intensidad: Alta (shake más fuerte)
   → Tiempo evacuación: 12 segundos (menos)

7. Usuario juega con esa dificultad

8. Resultados muestran puntuación
   → Puntos varían según:
      - Módulo
      - Dificultad elegida
      - Desempeño del usuario

9. Usuario presiona "Volver a Lobby"
   → Vuelve a LobbyVR.unity
   → Puede elegir otro módulo o mismo con dificultad diferente
```

---

## COMO CAMBIAR LOS VALORES

Si quieres ajustar las dificultades:

### Extintor - Editar FireGameManager.cs

En el script, en la sección `[Header("Configuración por Dificultad")]`:

```csharp
[SerializeField] private int firesEasyMode = 3;      // ← Cambiar número
[SerializeField] private int firesNormalMode = 5;    // ← Cambiar número
[SerializeField] private int firesHardMode = 7;      // ← Cambiar número

[SerializeField] private float timeEasyMode = 150f;     // ← Cambiar tiempo
[SerializeField] private float timeNormalMode = 120f;
[SerializeField] private float timeHardMode = 90f;
```

Luego en Inspector:
1. Abre `FireExtinguisherLesson.unity`
2. Click en `CourseManager`
3. En `FireGameManager` ves los campos
4. Cambia los números directamente en Inspector
5. Play mode para testear

### Sismo - Editar EarthquakeGameManager.cs

Agregar variables similares y ajustar lógica:

```csharp
[Header("Configuración por Dificultad")]
[SerializeField] private float shakeTimeEasy = 6f;
[SerializeField] private float shakeTimeNormal = 8f;
[SerializeField] private float shakeTimeHard = 10f;

public void SetDifficulty(CourseManager.Difficulty difficulty)
{
    switch(difficulty)
    {
        case A:
            earthquakeDuration = shakeTimeEasy;
            break;
        case B:
            earthquakeDuration = shakeTimeNormal;
            break;
        case C:
            earthquakeDuration = shakeTimeHard;
            break;
    }
}
```

---

## TESTING DE DIFICULTADES

### Paso 1: Probar en Play mode

1. Abre `LobbyVR.unity`
2. Click en "Extintor"
3. Selecciona "Fácil (A)"
4. Verifica que solo aparecen 3 fuegos
5. Verifica que el timer muestra ~2:30
6. Completa el minijuego
7. Vuelve al Lobby
8. Click en "Extintor" de nuevo
9. Selecciona "Difícil (C)"
10. Verifica que ahora hay 7 fuegos
11. Verifica que el timer muestra ~1:30

Si los números no cambian:
- Verifica que FireGameManager tiene SetDifficulty() llamado
- Verifica que CourseManager pasa la dificultad correcta
- Abre Console para ver errores

### Paso 2: Testing de Random

1. Selecciona "Random"
2. Juega varias veces
3. Cada vez debería haber diferente número de fuegos
4. Abre Console, deberías ver:
   - "🟢 Dificultad FÁCIL (A)"
   - O "🟡 Dificultad NORMAL (B)"
   - O "🔴 Dificultad DIFÍCIL (C)"

---

## PUNTUACIÓN Y BALANCEO

### Considerar:

1. **¿Son justas las puntuaciones por dificultad?**
   - Fácil: Usuario tiene más tiempo, menos presión
   - Difícil: Usuario tiene menos tiempo, MÁS puntos posibles
   - → Es justo: más riesgo = más recompensa

2. **¿Puede user completar cada dificultad?**
   - Fácil: Muy alta probabilidad de éxito
   - Normal: Probabilidad media
   - Difícil: Requiere habilidad

3. **Leaderboard futuro:**
   - Podrías guardar puntuaciones por dificultad
   - Ej: "Top 10 en Difícil (C)"
   - O: "Mejor puntuación total sin importar dificultad"

---

## RESUMEN

```
LOBBY
├─ Usuario selecciona: MÓDULO (Extintor O Sismo)
├─ Usuario selecciona: DIFICULTAD (A, B, C o Random)
└─ CourseManager.SelectModule(módulo, dificultad)
   ├─ Carga escena del módulo
   ├─ Profesor muestra diálogos
   ├─ GameManager.SetDifficulty(dificultad)
   ├─ Parámetros cambian según dificultad:
   │  ├─ Cantidad de fuegos/escombros
   │  ├─ Tiempo disponible
   │  ├─ Intensidad/velocidad
   │  └─ Radio de spawn
   ├─ Minijuego ejecuta con esa dificultad
   └─ Resultados basados en desempeño + dificultad

VOLVER A LOBBY
└─ Usuario puede jugar OTRO módulo O reintentar
   con DIFERENTE dificultad
```

---

## PRÓXIMAS IDEAS

1. **Progresión:** Fácil → Normal → Difícil automáticamente
2. **Medals:** Bronze/Silver/Gold según score y dificultad
3. **Leaderboards:** Top scores por dificultad
4. **Daily Challenge:** Random dificultad cada día
5. **Time Attack:** Ver si puedes batir tu propio record

---

*Guía de Variabilidad A/B/C*
*VR Educativo - Proyecto Multi-módulo*
*Versión 1.0*
