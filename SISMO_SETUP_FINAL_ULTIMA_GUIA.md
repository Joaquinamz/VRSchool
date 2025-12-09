# GUÍA FINAL CURSO SISMOS - RESPUESTAS A TUS DUDAS

## ✅ PREGUNTA 1: ¿Cuál GameManager debe tener EarthquakeProfessor?

### RESPUESTA CORRECTA:
**`EarthquakeGameManager` DEBE tener referencia a `EarthquakeProfessor`**

- ❌ NO: Añadir `NPCProfessor` a `EarthquakeGameManager`
- ✅ SÍ: Añadir `EarthquakeProfessor` a `EarthquakeGameManager`

### CONFIGURACIÓN EN INSPECTOR:
```
EarthquakeGameManager (Script)
├─ professorController = [Arrastra aquí objeto con EarthquakeProfessor]
├─ debrisSpawner = [Arrastra aquí objeto con DebrisSpawner]
└─ safeZones = [Array con 3 mesas/escritorios]
```

---

## ✅ PREGUNTA 2: Canvas NO muestra diálogos de Sismo

### PROBLEMA: Canvas está desactivado o no tiene referencia

### SOLUCIÓN PASO A PASO:

1. **EN ESCENA**: Busca el GameObject que tiene `EarthquakeProfessor`

2. **VERIFICA QUE TENGA**:
   - Script `EarthquakeProfessor` ✓
   - En Inspector: `dialogueText` = TextMeshPro del Canvas
   - En Inspector: `nextButton` = Botón "Siguiente"

3. **EN CANVAS**:
   - Must-Have: TextMeshPro para mostrar texto
   - Must-Have: Button "Siguiente"
   - **IMPORTANTE**: Canvas debe estar ACTIVO al empezar

4. **EN EARTHGUAKEPROFESSOR** (Script):
   - Línea 49+: `ShowIntroduction()` configura el texto
   - Línea 58: `ShowNextLine()` actualiza el Canvas

### VERIFICAR QUE FUNCIONA:
```
Play → Abre Consola (Ctrl+Shift+C en Unity)
→ Busca logs [EarthquakeProfessor]
→ Debe ver: "Mostrando diálogo de introducción de terremoto"
```

---

## ✅ PREGUNTA 3: Canvas NO Muestra Textos - Debugging

### TEST 1: ¿Se llama ShowIntroduction()?
```csharp
// En EarthquakeProfessor.cs, línea ~49
Debug.Log("[EarthquakeProfessor] Texto a mostrar: " + currentDialogues[0]);
if (dialogueText != null)
    dialogueText.text = currentDialogues[0];
else
    Debug.LogError("[EarthquakeProfessor] ❌ dialogueText es NULL");
```

### TEST 2: ¿Canvas está activo?
```csharp
// En Start() de EarthquakeProfessor
if (Canvas != null && !Canvas.gameObject.activeSelf)
{
    Debug.LogWarning("[EarthquakeProfessor] ⚠️ Canvas estaba inactivo - activando");
    Canvas.gameObject.SetActive(true);
}
```

### TEST 3: ¿TextMeshPro está asignado?
En Inspector → EarthquakeProfessor:
```
dialogueText = ??? (Debe tener algo aquí, no estar vacío)
```

Si está vacío → **Arrastra TextMeshPro del Canvas aquí**

---

## ✅ PROBLEMA 1: DEBRIS NO SE GENERA

### CAUSA MÁS COMÚN:
`DebrisSpawner` NO está en la escena O no tiene referencia en `EarthquakeGameManager`

### SOLUCIÓN RÁPIDA:
```
En EarthquakeGameManager Inspector:
┗ debrisSpawner = [DEBE tener GameObject con DebrisSpawner]

Si es NULL:
1. Busca GameObject "DebrisSpawner" en jerarquía
2. Si no existe, CREAR:
   - GameObject → Nombre: "DebrisSpawner"
   - Add Component → DebrisSpawner
   - Asignar en EarthquakeGameManager.debrisSpawner
3. En DebrisSpawner Inspector:
   ├─ debrisPrefab = (tu prefab de escombro)
   └─ spawnFrequency = 0.5 (cada 0.5 segundos)
```

### DEBUGGING:
Abre Consola y busca:
- ✅ `[DebrisSpawner] Iniciado - spawneando cada` → Está funcionando
- ❌ `[DebrisSpawner] ❌ debrisPrefab es NULL` → Asigna prefab en Inspector

---

## ✅ PROBLEMA 2: TERREMOTO NO PROGRESA (Se queda cargando)

### CAUSA: `earthquakeActive` nunca se pone TRUE

### SOLUCIÓN:
En `EarthquakeGameManager.cs`, verifica:

1. **CompleteIntroduction() se llama?**
```csharp
// Debe haber esto:
public void CompleteIntroduction()
{
    Debug.Log("[EarthquakeGameManager] ✓ Iniciando terremoto");
    currentPhase = GamePhase.Earthquake_Starting;
    earthquakeActive = true;  // ← CRÍTICO
    earthquakeTimer = 0f;
    StartCoroutine(EarthquakeSequence());
}
```

2. **EarthquakeProfessor llama a CompleteIntroduction()?**
```csharp
// En EarthquakeProfessor OnNextClicked() final:
if (gameController != null)
    gameController.CompleteIntroduction();  // ← DEBE estar aquí
```

3. **Verifica en Consola**:
```
Presiona siguiente en diálogo
→ Busca: [EarthquakeGameManager] ✓ Iniciando terremoto
→ Si NO aparece → Problema en OnNextClicked()
```

---

## ✅ PROBLEMA 3: Debris Cae Pero No Impacta

### CAUSA: DebrisHitDetector NO está en debris prefab

### SOLUCIÓN:
1. Abre tu prefab de debris
2. Add Component → DebrisHitDetector
3. En DebrisHitDetector:
   - gameManager = [Arrastra EarthquakeGameManager]
   - damageAmount = 10

### DEBUGGING:
```csharp
// En DebrisHitDetector.cs
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("[DebrisHitDetector] ✓ Debris impactó al jugador");
        gameManager.RegisterHit();
    }
}
```

---

## ✅ PROBLEMA 4: Shaking (Temblor) No Se Ve

### CAUSA: Cámara NO se está moviendo

### SOLUCIÓN:
En `EarthquakeGameManager.cs`, Update():
```csharp
void Update()
{
    if (!earthquakeActive) return;
    
    earthquakeTimer += Time.deltaTime;
    
    // SHAKE: Mover la cámara
    Camera mainCam = Camera.main;
    if (mainCam != null)
    {
        float shakeX = Mathf.Sin(earthquakeTimer * shakeSpeed) * shakeIntensity;
        float shakeY = Mathf.Cos(earthquakeTimer * shakeSpeed) * shakeIntensity;
        mainCam.transform.Translate(new Vector3(shakeX, shakeY, 0));
    }
    
    UpdateUI();
}
```

---

## ✅ CHECKLIST ANTES DE PLAY

### En Inspector:

**EarthquakeGameManager:**
- [ ] professorController ≠ NULL
- [ ] debrisSpawner ≠ NULL
- [ ] uiTimer ≠ NULL
- [ ] statusText ≠ NULL
- [ ] safeZones.Length > 0

**EarthquakeProfessor:**
- [ ] dialogueText ≠ NULL
- [ ] nextButton ≠ NULL
- [ ] gameController ≠ NULL (debe ser EarthquakeGameManager)
- [ ] Canvas está ACTIVO

**DebrisSpawner:**
- [ ] debrisPrefab ≠ NULL
- [ ] gameManager ≠ NULL

**DebrisHitDetector (en prefab):**
- [ ] gameManager ≠ NULL
- [ ] Player tiene tag "Player"

---

## ✅ FLUJO ESPERADO (Paso a Paso)

```
1. PLAY
   └─ Consola: [EarthquakeProfessor] Mostrando diálogo de terremoto

2. CLICK "Siguiente" (última línea)
   └─ Consola: [EarthquakeGameManager] ✓ Iniciando terremoto

3. 0-3 SEGUNDOS
   └─ Canvas muestra: "¡Terremoto en curso!"
   └─ Cámara tiembla (pequeño movimiento)

4. 3-33 SEGUNDOS
   └─ Debris empieza a caer
   └─ Canvas actualiza contador: "Impactos: X"
   └─ Si debris toca Player → contador sube

5. 33+ SEGUNDOS
   └─ Terremoto termina
   └─ Canvas muestra resultados
   └─ Consola: [EarthquakeGameManager] ✓ Terremoto completado
```

---

## ✅ SOLUCIONES RÁPIDAS POR SÍNTOMA

| SÍNTOMA | CAUSA | SOLUCIÓN |
|---------|-------|----------|
| Canvas no muestra texto | dialogueText NULL | Arrastra TextMeshPro en Inspector |
| Debris no aparece | debrisPrefab NULL | Arrastra prefab en DebrisSpawner |
| Terremoto no empieza | CompleteIntroduction() no se llama | Verifica OnNextClicked en EarthquakeProfessor |
| Cámara no tiembla | shakeIntensity = 0 | Aumenta a 0.1 en Inspector |
| Contador no actualiza | hitCountText NULL | Arrastra TextMeshPro en Inspector |
| Debris atraviesa al jugador | Sin Collider | Añade Collider al debris prefab |

---

## ✅ CONSOLE LOGS PARA DEBUGGING

Busca estos en Consola durante Play:

**Inicio:**
- ✅ `[EarthquakeProfessor] Mostrando diálogo de introducción de terremoto`

**Al click siguiente:**
- ✅ `[EarthquakeGameManager] ✓ Iniciando terremoto`

**Durante terremoto (cada frame):**
- ✅ `[EarthquakeGameManager] UI actualizada - Tiempo: XX.X`

**Cuando debris impacta:**
- ✅ `[DebrisHitDetector] ✓ Debris impactó al jugador`

**Al terminar:**
- ✅ `[EarthquakeGameManager] ✓ Terremoto completado`

---

## 🚨 ÚLTIMA COSA: Verifica Permisos Canvas

Si Canvas NO aparece, podría ser:
1. Canvas está en layer diferente a Main Camera
2. Canvas está muy lejos de la cámara
3. Canvas tiene Alpha = 0

**FIX RÁPIDO:**
```
En Inspector Canvas:
├─ Sorting Order = 100 (para que esté arriba)
├─ Alpha = 1 (visible)
└─ Layer = UI
```

---

**Fin de guía. Suerte con sismos. 🎯**
