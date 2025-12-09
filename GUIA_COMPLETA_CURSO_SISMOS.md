# GUÍA COMPLETA: CURSO DE SISMOS (EarthquakeLesson)

## 📋 RESUMEN EJECUTIVO

Tienes el sistema completamente reformulado y listo. Solo necesitas:
1. Copiar el curso de extintor → adaptarlo a sismo
2. Usar los scripts ya creados
3. Configurar algunos valores en Inspector

**Tiempo estimado**: 30 minutos para una escena completa de sismo.

---

## 🎯 DIFERENCIAS PRINCIPALES: EXTINTOR vs SISMO

### Extintor (FireGameManager)
- ✅ Fase 1: Introducción
- ✅ Fase 2: Fuego de práctica (usuario lo apaga)
- ✅ Fase 3: Diálogo post-fuego
- ✅ Fase 4: Minijuego (múltiples fuegos)

### Sismo (EarthquakeGameManager) - MÁS SIMPLE
- ✅ Fase 1: Introducción (el profesor explica DROP, COVER, HOLD ON)
- ✅ Fase 2: Terremoto (3 seg shake + 30 seg escombros cayendo)
- ✅ Fase 3: Resultados (basado en cuántos impactos recibió)

**Ventaja**: NO hay minijuego, es una sola fase de terremoto.

---

## 🏗️ PASOS PARA CREAR EarthquakeLesson1

### PASO 1: PREPARAR LA ESCENA EN UNITY (5 min)

1. **Duplica** FireExtinguisherLesson1 y renómbrala a `EarthquakeLesson1`
2. **Reemplaza estos GameObjects** (elimina/oculta):
   - ❌ ExtintorController
   - ❌ FireGameManager (lo reemplazarás)
   - ❌ FireMinigameManager
   - ❌ Extintor (objeto 3D)
   - ✅ Mantén: NPCProfessor, Canvas, Luces, etc.

3. **Crea nuevos GameObjects**:
   - Crea un GameObject vacío llamado `EarthquakeGameManager`
   - Crea un GameObject vacío llamado `DebrisSpawner`
   - Crea un GameObject vacío llamado `SafeZone_Table1`
   - Crea un GameObject vacío llamado `SafeZone_Table2`

---

### PASO 2: AGREGAR COMPONENTES (5 min)

**GameObject: EarthquakeGameManager**
```
Add Component → EarthquakeGameManager
Inspector:
  - Professor Controller → Arrastra NPCProfessor
  - Debris Spawner → Arrastra DebrisSpawner
  - Safe Zones → Tamaño 2
    [0] → Arrastra SafeZone_Table1
    [1] → Arrastra SafeZone_Table2
  - Ui Timer → Arrastra Canvas/TimerText
  - Status Text → Arrastra Canvas/StatusText
  - Hit Count Text → Arrastra Canvas/HitCountText
  - Debris Start Delay → 3
  - Earthquake Duration → 30
  - Shake Intensity → 0.1
  - Shake Speed → 10
```

**GameObject: DebrisSpawner**
```
Add Component → DebrisSpawner
Inspector:
  - Debris Prefab → (IMPORTANTE: Ve al PASO 3 para crear esto)
  - Spawn Area Min → (-10, 8, -10)
  - Spawn Area Max → (10, 10, 10)
  - Spawn Rate → 2 (escombros por segundo)
  - Debris Force → 20 (velocidad de caída)
  - Debris Lifetime → 10 (segundos antes de desaparecer)
  - Max Debris Active → 50
```

**GameObject: SafeZone_Table1**
```
Position: (-3, 1, 0)
Add Component → Collider (para que el DebrisHitDetector lo detecte)
  - Es Trigger: ON
```

**GameObject: SafeZone_Table2**
```
Position: (3, 1, 0)
Add Component → Collider (para que el DebrisHitDetector lo detecte)
  - Es Trigger: ON
```

---

### PASO 3: CREAR DEBRIS PREFAB (10 min)

El "escombro" que cae es la parte crítica. Debe tener:
- Mesh visual (cubo, cilindro, o modelo 3D)
- Rigidbody (para que caiga)
- Collider (para detectar impactos)
- DebrisHitDetector script (auto-agregado por DebrisSpawner)

**OPCIÓN A: Usar un Cubo Simple**
```
1. Right-click en Hierarchy → 3D Object → Cube
2. Renombra a "DebrisPrefab"
3. Inspector:
   - Scale: (0.5, 0.5, 0.5)
   - Material: Gris o rojo
4. Add Component → Rigidbody:
   - Mass: 5
   - Gravity: ON
   - Constraints: Congelage Rotation → X, Y, Z
5. El Collider (Box) ya está por defecto
6. NO agregues DebrisHitDetector (lo agrega DebrisSpawner)
7. Drag-drop a Assets/ para crear prefab
8. Arrastra a DebrisSpawner > debrisPrefab
9. DELETE del Hierarchy (no necesita estar en escena)
```

**OPCIÓN B: Usar un Modelo 3D**
- Si tienes modelos de escombros (rocas, ladrillos), úsalos
- Pasos iguales, solo que con tu modelo 3D en lugar del cubo

---

### PASO 4: CAMBIAR NPCProfessor A EarthquakeProfessor (3 min)

En la escena EarthquakeLesson1:

1. **Selecciona** el GameObject NPCProfessor
2. **Elimina** el component NPCProfessor
3. **Add Component → EarthquakeProfessor**
4. Inspector:
   - Dialogue Text → Arrastra Canvas/DialogueText
   - Next Button → Arrastra Canvas/NextButton
   - Game Controller → Arrastra EarthquakeGameManager
   - Results Canvas → Arrastra Canvas/ResultsCanvas
   - Results Feedback → Arrastra Canvas/ResultsFeedbackText

---

### PASO 5: CREAR CANVAS PARA RESULTADOS (5 min)

Si aún no lo tienes, agrega a Canvas:

```
Canvas/
├─ TimerText (ya debe estar)
├─ StatusText (ya debe estar)
├─ HitCountText (CREAR)
│  └─ TextMeshPro con contenido "Impactos: 0"
├─ DialogueText (ya debe estar)
├─ NextButton (ya debe estar)
└─ ResultsCanvas (CREAR - Panel con fondo)
   ├─ ResultsFeedbackText
   │  └─ TextMeshPro para mostrar resultados
   └─ ButtonReturnToLobby
      └─ SimpleLobbyLoader (LoadMode: ReturnToLobby)
```

---

### PASO 6: CONFIGURAR BOTÓN "VOLVER A LOBBY" (2 min)

En Canvas/ResultsCanvas/ButtonReturnToLobby:

```
1. Add Component → SimpleLobbyLoader
2. Inspector:
   - Mode: ReturnToLobby
   - Lobby Scene Name: "Lobby"
3. Button component:
   - On Click () → +
   - Arrastra ButtonReturnToLobby
   - Dropdown: SimpleLobbyLoader > OnButtonClick()
```

---

### PASO 7: INICIAR DESDE SCRIPT (0.5 min)

En el Start() de tu manager principal (o al entrar a escena):

```csharp
EarthquakeGameManager gameManager = FindFirstObjectByType<EarthquakeGameManager>();
if (gameManager != null)
{
    gameManager.StartIntroduction();
}
```

O simplemente asegúrate de que `StartIntroduction()` se llama desde otro lugar.

---

## 🎮 FLUJO DEL JUEGO

```
USUARIO ABRE EarthquakeLesson1
        ↓
EarthquakeGameManager.StartIntroduction()
        ↓
EarthquakeProfessor muestra diálogos:
  - "Hola, aprenderemos qué hacer en un terremoto"
  - "DROP: Agáchate"
  - "COVER: Cúbrete bajo una mesa"
  - "HOLD ON: Mantente en posición"
  - "Presiona Continuar cuando estés listo"
        ↓
USUARIO PRESIONA CONTINUAR (OnNextClicked)
        ↓
EarthquakeGameManager.CompleteIntroduction()
        ↓
COMIENZA EL TERREMOTO:
  - Cámara shake (animación de temblor)
  - Después de 3 segundos → escombros empiezan a caer
  - Escombros cae durante 30 segundos
        ↓
SI USUARIO ESTÁ DEBAJO DE MESA:
  ✓ Los impactos NO cuentan (IsPlayerInSafeZone = true)
SI USUARIO ESTÁ AFUERA:
  ✗ Los impactos cuentan (totalHits++)
        ↓
DESPUÉS DE 30 SEGUNDOS:
  - Terremoto termina
  - DebrisSpawner detiene el spawn
  - Cámara vuelve a posición normal
        ↓
EarthquakeGameManager.ShowResults()
        ↓
Mostrar Canvas/ResultsCanvas con:
  - Impactos recibidos: X
  - Puntaje final: 100 - (X * 10)
  - Feedback (Excelente/Bien/Aceptable/Mal)
        ↓
USUARIO PRESIONA "VOLVER A LOBBY"
        ↓
SimpleLobbyLoader.OnButtonClick()
        ↓
SceneManager.LoadScene("Lobby")
        ↓
VUELVE A LOBBY
```

---

## ⚙️ CONFIGURACIÓN DE VALORES (Tunables)

En Inspector del EarthquakeGameManager, puedes ajustar:

| Parámetro | Valor Default | Recomendación |
|-----------|--------------|---------------|
| Debris Start Delay | 3s | 2-4s (cuándo empiezan a caer) |
| Earthquake Duration | 30s | 20-40s (duración total) |
| Shake Intensity | 0.1 | 0.05-0.3 (más alto = más movimiento) |
| Shake Speed | 10 | 5-15 (más alto = vibración más rápida) |
| Spawn Rate | 2 | 1-5 (escombros por segundo) |
| Debris Force | 20 | 10-30 (velocidad de caída) |
| Safe Zone Radius | 2m | 1.5-3 (distancia desde tabla) |

---

## 🧪 TESTING CHECKLIST

Después de crear la escena:

- [ ] **Entra a EarthquakeLesson1**
- [ ] **Diálogos del profesor se muestran correctamente** (DROP, COVER, HOLD ON)
- [ ] **Presiona Continuar → Comienza el shake**
- [ ] **Espera 3 segundos → Empiezan a caer escombros**
- [ ] **Escombros caen correctamente desde arriba**
- [ ] **Párate EN MEDIO → Recibe impactos (HitCount aumenta)**
- [ ] **Párate BAJO MESA → NO recibe impactos (HitCount NO aumenta)**
- [ ] **Espera 30 segundos → Terremoto termina**
- [ ] **ResultsCanvas aparece con puntaje y feedback**
- [ ] **Presiona "Volver" → Vuelve a Lobby**

---

## 🐛 TROUBLESHOOTING

### "No aparecen escombros"
```
Verificar:
1. DebrisSpawner tiene debrisPrefab asignado
2. debrisPrefab tiene Rigidbody
3. EarthquakeGameManager.StartIntroduction() fue llamado
4. Console muestra "[DebrisSpawner] Empezando a spawnear"
```

### "El jugador se congelaaaa"
```
Problema: Terremoto no termina
Solución:
1. Verificar que currentPhase transiciona correctamente
2. Que earthquakeDuration = 30 segundos
3. Que Update() está ejecutándose
```

### "Los impactos no se cuentan"
```
Verificar:
1. Debris prefab tiene Collider
2. DebrisHitDetector se agrega automáticamente
3. Jugador tiene tag "Player" o "Head" (opcional, pero recomendado)
4. Console muestra "[EarthquakeGameManager] Impacto recibido"
```

### "Safe zones no funcionan"
```
Verificar:
1. SafeZone GameObjects tienen Collider
2. Collider > Is Trigger: ON
3. EarthquakeGameManager > Safe Zones está poblado
4. Posiciones están debajo de donde puede estar el jugador
```

---

## 📊 SISTEMA DE PUNTUACIÓN

```csharp
Puntuación Final = 100 - (Impactos × 10)

Ejemplos:
- 0 impactos → 100/100 (EXCELENTE)
- 2 impactos → 80/100 (MUY BIEN)
- 5 impactos → 50/100 (BIEN)
- 10+ impactos → 0/100 (NECESITA PRACTICAR)
```

---

## 🎓 PRÓXIMOS PASOS DESPUÉS DE EARTHQUAKE1

1. **EarthquakeLesson2**: Más escombros, shake más fuerte
2. **EarthquakeLesson3**: Safe zones más pequeñas, duración más larga

Simplemente copia EarthquakeLesson1 y ajusta:
- `Shake Intensity` → más alto
- `Spawn Rate` → más alto
- `Earthquake Duration` → más largo
- Safe Zone radius → más pequeño (en código)

---

## 📚 ARCHIVOS CREADOS/MODIFICADOS

```
Assets/
├─ EarthquakeGameManager.cs      ✅ NUEVO (reformulado)
├─ EarthquakeProfessor.cs         ✅ NUEVO
├─ DebrisSpawner.cs               ✅ NUEVO
├─ DebrisHitDetector.cs            ✅ NUEVO
├─ SimpleLobbyLoader.cs            ✅ Ya existe
├─ FireGameManager.cs              ✅ REFORMULADO
└─ Scenes/
   ├─ FireExtinguisherLesson1.unity ✅ Funcional
   ├─ EarthquakeLesson1.unity        📝 CREAR (como copia de Extintor)
   ├─ EarthquakeLesson2.unity        📝 CREAR
   └─ EarthquakeLesson3.unity        📝 CREAR
```

---

## ✅ RESUMEN RÁPIDO

**Para cada lección de sismo:**
1. Copia curso de extintor
2. Reemplaza componentes:
   - `NPCProfessor` → `EarthquakeProfessor`
   - `FireGameManager` → `EarthquakeGameManager`
3. Elimina objetos de extintor (fuego, extintor físico)
4. Agrega `DebrisSpawner` + `SafeZones`
5. Crea `DebrisPrefab`
6. Ajusta variables en Inspector
7. ¡Listo!

**Tiempo total**: 30 minutos por escena.

