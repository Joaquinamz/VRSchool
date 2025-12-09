# QUICK START: CREAR EARTHQUAKESSON1 EN 30 MIN

## ⚡ VERSIÓN ULTRA-RÁPIDA

Si quieres un resultado inmediato sin leer la guía completa, sigue esto:

---

## PASO 1: DUPLICA LA ESCENA (2 min)

```
1. En Project → Scenes
2. Right-click en FireExtinguisherLesson1.unity
3. Duplicate
4. Renombra a EarthquakeLesson1.unity
5. Abre la escena (doble-click)
```

---

## PASO 2: ELIMINA LO QUE NO NECESITAS (5 min)

En Hierarchy, elimina (click derecho → Delete):
- ExtintorController
- FireGameManager
- FireMinigameManager
- Extintor (objeto físico)
- Fuegos si los ves

**Mantén**:
- Canvas (con todos los textos)
- NPCProfessor
- Luces
- Cámara

---

## PASO 3: CREA NUEVOS GAMEOBJECTS (3 min)

En Hierarchy, right-click → Create Empty:
1. `EarthquakeGameManager`
2. `DebrisSpawner`
3. `SafeZone_Table1` (posición -3, 1, 0)
4. `SafeZone_Table2` (posición 3, 1, 0)

---

## PASO 4: CREA PREFAB DE ESCOMBRO (5 min)

```
1. Hierarchy → Right-click → 3D Object → Cube
2. Renombra a "DebrisPrefab_Temp"
3. Inspector:
   - Scale: (0.5, 0.5, 0.5)
4. Add Component → Rigidbody
5. Right-click en Assets → Create Folder → "Prefabs"
6. Drag-drop DebrisPrefab_Temp a Assets/Prefabs/
7. En Hierarchy, DELETE DebrisPrefab_Temp
```

---

## PASO 5: AGREGAR COMPONENTES (8 min)

**EarthquakeGameManager**:
```
Add Component → EarthquakeGameManager
Inspector:
  Professor Controller → Arrastra NPCProfessor
  Debris Spawner → Arrastra DebrisSpawner
  Safe Zones → Size: 2
    [0] → SafeZone_Table1
    [1] → SafeZone_Table2
  Ui Timer → Canvas/TimerText
  Status Text → Canvas/StatusText
  Hit Count Text → Canvas/HitCountText
```

**DebrisSpawner**:
```
Add Component → DebrisSpawner
Inspector:
  Debris Prefab → Assets/Prefabs/DebrisPrefab.prefab
  Spawn Rate → 2
  Max Debris Active → 50
  (Dejar resto por defecto)
```

**SafeZone_Table1 y 2**:
```
Add Component → BoxCollider
  Is Trigger: ON
```

---

## PASO 6: CAMBIAR NPCProfessor (3 min)

En Hierarchy:
```
1. Selecciona NPCProfessor
2. Busca el component NPCProfessor en Inspector
3. Click en 3 puntos → Remove Component
4. Add Component → EarthquakeProfessor
5. Inspector:
   Dialogue Text → Canvas/DialogueText
   Next Button → Canvas/NextButton
   Game Controller → EarthquakeGameManager
   Results Canvas → (crear si no existe)
   Results Feedback → (crear si no existe)
```

---

## PASO 7: CREAR UI PARA RESULTADOS (2 min)

En Canvas, agrega:
```
Right-click → UI → Legacy → Text - TextMeshPro
Renombra a "HitCountText"
Text: "Impactos: 0"

Right-click → Panel → Image
Renombra a "ResultsCanvas"
(Anídalo en Canvas, haz que sea hijo)
Dentro del Panel, agrega TextMeshPro para resultados
```

---

## PASO 8: BOTÓN VOLVER (2 min)

En ResultsCanvas:
```
Right-click → Button - TextMeshPro
Renombra a "ButtonReturn"
Text: "Volver a Lobby"

Selecciona ButtonReturn:
Add Component → SimpleLobbyLoader
  Mode: ReturnToLobby
  Lobby Scene Name: "Lobby"

En Button component:
  On Click → +
  Arrastra ButtonReturn
  Dropdown: SimpleLobbyLoader > OnButtonClick()
```

---

## PASO 9: INICIAR LA LECCIÓN (1 min)

Crea un script vacío llamado `SceneStarter.cs`:

```csharp
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    void Start()
    {
        var gameManager = FindFirstObjectByType<EarthquakeGameManager>();
        if (gameManager != null)
            gameManager.StartIntroduction();
    }
}
```

Add Component en un GameObject cualquiera (ej: Canvas).

---

## PASO 10: TEST (2 min)

```
1. Play
2. Diálogos del profesor aparecen
3. Presiona Continuar
4. Comienza shake + escombros
5. Párate EN MEDIO → Impactos se cuentan
6. Párate BAJO MESA → NO se cuentan
7. Espera 30 seg → Resultados
8. Presiona Volver → Lobby
```

---

## ✅ LISTO

**¡Ya tienes EarthquakeLesson1 funcional!**

Para EarthquakeLesson2 y 3, repite los mismos pasos y ajusta:
- `Shake Intensity`: 0.15 (en lugar de 0.1)
- `Spawn Rate`: 3 (en lugar de 2)

---

## 🐛 Si falla

**"No aparecen escombros"**
```
Verifica:
1. DebrisSpawner tiene debrisPrefab asignado
2. El prefab tiene Rigidbody
3. Console muestra "[DebrisSpawner] Empezando a spawnear"
```

**"Terremoto no termina"**
```
Verifica:
1. earthquakeDuration = 30 segundos
2. Que Update() se ejecuta
3. Console muestra fases que avanzan
```

**"Los impactos no se cuentan"**
```
Verifica:
1. SafeZone colliders tienen Is Trigger: ON
2. Console muestra "[EarthquakeGameManager] Impacto"
```

---

## 📚 Referencia Completa

Para detalles, ver: `GUIA_COMPLETA_CURSO_SISMOS.md`

