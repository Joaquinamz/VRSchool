# 🐛 TROUBLESHOOTING - SOLUCIÓN DE PROBLEMAS

## ERRORES COMUNES Y SOLUCIONES

---

## 1. "Script no encuentra referencias" 

### Error en consola:
```
NullReferenceException: Object reference not set to an instance of an object
```

### Causas posibles:
- ❌ No asignaste la referencia en Inspector
- ❌ Nombre del GameObject no coincide con FindObjectOfType
- ❌ Script está en escena distinta

### Solución:
```
1. Abre Inspector del GameObject
2. Busca el campo vacío (warning icon)
3. Arrastra el objeto correcto
4. Verifica "Type" coincida (Transform, Canvas, etc)
```

**Ejemplo:**
```csharp
// Si esto falla:
instructor = FindObjectOfType<InstructorController>();

// Haz esto en Inspector:
[SerializeField] private InstructorController instructor;
// Y arrastra el Profesor GameObject
```

---

## 2. Los fuegos no desaparecen

### Síntomas:
- Extintor dispara pero fuegos no se apagan
- Console: Fuego recibe damage pero intensidad no baja

### Causas:
- ❌ `damageRange` muy pequeño
- ❌ `foamParticle` no asignado
- ❌ Fuego no tiene `FireBehavior.cs`

### Solución:

**Paso 1:** Verifica WorkingExtinguisher
```csharp
[SerializeField] private float damageRange = 5f; // Aumenta a 10
```

**Paso 2:** En Play mode, Debug
```csharp
// Añade en Update():
Debug.Log($"Fuegos en rango: {activeFiresInRange.Count}");

// En consola debe mostrar > 0 cuando apuntas al fuego
```

**Paso 3:** Verifica prefab Fire
- ¿Tiene `BoxCollider`?
- ¿Tiene `FireBehavior.cs`?
- ¿Tiene `ParticleSystem`?

---

## 3. El profesor no aparece en pantalla

### Síntomas:
- Canvas está pero texto no se ve
- Profesor GameObject existe pero no visible

### Causas:
- ❌ Canvas es ScreenSpace (debe ser WorldSpace)
- ❌ Profesor está fuera de vista de cámara
- ❌ Texto es color blanco en fondo blanco

### Solución:

**Para Canvas:**
```
Canvas > Inspector > Render Mode: World Space
Canvas > Position: (0, 1.5, 2) // Frente a cámara
```

**Para Profesor:**
```
Profesor > Position: (0, 0, 3)
Profesor > Scale: (1, 1.8, 1)
```

**Para Texto:**
```
DialogueText > Color: Black o contraste fuerte
```

---

## 4. "Minijuego no inicia"

### Síntomas:
- Presionas "Siguiente" pero no pasa nada
- Console vacío o sin errores

### Causas:
- ❌ `CourseManager.Instance` es null
- ❌ `fireGameManagerPrefab` no asignado
- ❌ `StartGamePhase()` no se llama

### Debugging:

```csharp
// En InstructorController.cs, añade debug:
if (CourseManager.Instance != null)
{
    Debug.Log("✅ CourseManager found");
    CourseManager.Instance.StartGamePhase();
}
else
{
    Debug.LogError("❌ CourseManager NOT found!");
}
```

**Solución:**
1. Asegúrate CourseManager está en escena
2. Verifica prefab asignado en CourseManager
3. Prefab debe tener FireGameManager.cs

---

## 5. Los estudiantes en sismo no se mueven

### Síntomas:
- Estudiantes están congelados
- Console: "NavMeshAgent" warnings

### Causas:
- ❌ NavMesh no está baked
- ❌ StudentAI.cs no tiene NavMeshAgent
- ❌ ExitPoint no está asignado

### Solución:

**Paso 1:** Bake NavMesh
```
Window > AI > Navigation > Bake
```

**Paso 2:** Verifica Student Prefab
```
Student > Inspector
[✓] NavMeshAgent component
[✓] Radius: 0.5
[✓] Speed: 3.5
[✓] Stopping Distance: 0.5
```

**Paso 3:** En EarthquakeGameManager
```csharp
[SerializeField] private Transform exitPoint; // Asigna en Inspector
```

---

## 6. Puntuación incorrecta

### Síntomas:
- Puntuación no aumenta
- Resultado muestra 0 puntos

### Causas:
- ❌ Evento `OnFireExtinguished` no se invoca
- ❌ Score stays at 0

### Solución:

**Verifica FireBehavior.cs:**
```csharp
void Extinguish()
{
    FireGameManager fireGame = FindObjectOfType<FireGameManager>();
    if (fireGame != null)
    {
        fireGame.OnFireExtinguished(); // IMPORTANTE
    }
}
```

**Debugging en FireGameManager:**
```csharp
public void OnFireExtinguished()
{
    firesExtinguished++;
    currentScore += pointsPerFireExtinguished;
    Debug.Log($"✅ Score: {currentScore}"); // Ver en console
}
```

---

## 7. El jugador no se puede agachar

### Síntomas:
- Input no funciona
- Cámara no baja al presionar
- Console: "Crouch input not found"

### Causas:
- ❌ Input Action no está configurado
- ❌ Input no está mapeado a tecla/botón
- ❌ `crouchInput` no asignado en PlayerEarthquakeBehavior

### Solución:

**Opción A: Usar InputAction existente**
```
Project Settings > Input Manager
Buscar "Crouch" o "Jump"
Mapear a Espacio o Tecla X
```

**Opción B: Crear InputAction nueva**
```
Assets > Create > Input Actions
Nombre: "PlayerControls"
Nueva Action: "Crouch"
Binding: Space / Joystick Right Stick Click

Luego en PlayerEarthquakeBehavior:
[SerializeField] private InputActionReference crouchInput;
// Arrastra de asset creado
```

**Debugging:**
```csharp
private void OnCrouchInput(InputAction.CallbackContext context)
{
    Debug.Log("🛑 CROUCH ACTIVATED");
    // Si no ves esto, input no funciona
}
```

---

## 8. Colisiones no detectan

### Síntomas:
- Fuego no toma daño de extintor
- Escombros pasan a través del jugador
- Console: "Collision not detected"

### Causas:
- ❌ Colliders sin Rigidbody
- ❌ Rigidbody seteado a Kinematic
- ❌ isTrigger activado cuando no debería

### Checklist de Colliders:

**Fuego:**
```
[ ] BoxCollider ON
[ ] isTrigger: OFF
[ ] Rigidbody: Kinematic
[ ] FireBehavior.cs attached
```

**Escombro:**
```
[ ] BoxCollider ON
[ ] isTrigger: OFF
[ ] Rigidbody: Dynamic
[ ] Gravity: ON
[ ] Tag: "Debris"
```

**Mesas:**
```
[ ] BoxCollider ON
[ ] isTrigger: OFF
[ ] Rigidbody: NOT needed
[ ] Tag: "Table"
```

---

## 9. Transición entre escenas no funciona

### Síntomas:
- Presionas "Continuar" pero nada pasa
- Scene no cambia
- Console: SceneManager error

### Causas:
- ❌ Escena no está en Build Settings
- ❌ Nombre de escena incorrecto
- ❌ SceneLoading no activado

### Solución:

**Paso 1:** Verifica Build Settings
```
File > Build Settings
Arrastra escenas a la lista
0: LobbyVR
1: FireExtinguisherLesson
2: EarthquakeLesson
```

**Paso 2:** Verifica nombre exacto
```csharp
// En CourseManager.cs
SceneManager.LoadScene("LobbyVR"); // Exacto como aparece en Build Settings
```

**Debugging:**
```csharp
void Start()
{
    string[] scenesInBuild = new string[SceneManager.sceneCountInBuildSettings];
    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
    {
        scenesInBuild[i] = Path.GetFileNameWithoutExtension(
            SceneUtility.GetScenePathByBuildIndex(i)
        );
        Debug.Log($"Escena {i}: {scenesInBuild[i]}");
    }
}
```

---

## 10. Sismo no tiembla

### Síntomas:
- EarthquakeSimulator inicia pero cámara no se mueve
- Console: Earthquake started pero sin shaking

### Causas:
- ❌ `mainCamera` es null
- ❌ `ApplyCameraShake()` no actualiza posición
- ❌ `isEarthquakeActive` es false

### Solución:

**En EarthquakeSimulator.cs:**
```csharp
void Start()
{
    mainCamera = Camera.main;
    if (mainCamera == null)
    {
        Debug.LogError("❌ MAIN CAMERA NOT FOUND!");
        return;
    }
    originalCameraPos = mainCamera.transform.localPosition;
    Debug.Log("✅ Camera found and saved");
}
```

**Debugging en Update:**
```csharp
private void Update()
{
    if (!isEarthquakeActive) return;
    
    Debug.Log($"Shaking... {earthquakeTimeRemaining:F1}s remaining");
    ApplyCameraShake();
}
```

---

## 11. Prefab no instancia correctamente

### Síntomas:
- Instantiate funciona pero objeto no aparece
- Objeto aparece en (0, 0, 0) o fuera de vista

### Causas:
- ❌ Posición inicial incorrecta
- ❌ Scale = 0
- ❌ Padre incorrecto

### Solución:

**Verificar Instantiate:**
```csharp
// Así es correcto:
Vector3 randomPos = spawnCenter + Random.insideUnitSphere * spawnRadius;
randomPos.y = 1f; // No generar bajo tierra
GameObject fireObj = Instantiate(firePrefab, randomPos, Quaternion.identity);

Debug.Log($"Fire instantiated at: {randomPos}"); // Ver en console
```

---

## 12. "No hay audio"

*Este es un TODO para la siguiente fase*

**Cuando hagas AudioManager.cs:**
```csharp
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;
    
    public void PlaySFX(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Audio/SFX/{clipName}");
        if (clip != null)
            sfxSource.PlayOneShot(clip);
        else
            Debug.LogError($"Audio no encontrado: {clipName}");
    }
}
```

---

## 📋 DEBUGGING UNIVERSAL

### Comandos útiles en Console:

```csharp
// Ver instancia de CourseManager
Debug.Log(CourseManager.Instance != null ? "✅ Found" : "❌ Not found");

// Ver estado actual
Debug.Log($"Current state: {CourseManager.Instance.GetCurrentState()}");

// Ver puntuación
Debug.Log($"Score: {fireGameManager.currentScore}");

// Ver objetos activos
GameObject[] objects = FindObjectsOfType<GameObject>();
Debug.Log($"Total objects: {objects.Length}");

// Ver todas las escenas
for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
{
    Debug.Log($"Scene {i}: {SceneUtility.GetScenePathByBuildIndex(i)}");
}
```

---

## 🆘 ÚLTIMA OPCIÓN: RESETEAR

Si todo falla, pueden reiniciar desde aquí:

1. Elimina todas las escenas creadas
2. Mantén solo los scripts (.cs)
3. Crea nuevas escenas paso a paso
4. Verifica cada paso en Play mode

O contacta conmigo para debuggear juntos.

---

## ✅ CHECKLIST FINAL

Antes de decir "no funciona":

```
[ ] ¿Script está attached a GameObject?
[ ] ¿Todas las referencias en Inspector están asignadas?
[ ] ¿La escena está en Build Settings?
[ ] ¿Hay errores en Console?
[ ] ¿Verificaste Play mode paso por paso?
[ ] ¿El prefab existe en Assets/Prefabs/?
[ ] ¿Las capas y tags están correctas?
[ ] ¿El NavMesh está baked (si es sismo)?
```

---

*Guía de troubleshooting - VR Educativo*
*Última actualización: 28 Nov 2025*
