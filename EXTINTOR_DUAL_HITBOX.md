# 🎯 EXTINTOR CON DOS HITBOX SEPARADAS - GUÍA COMPLETA

**Objetivo**: Crear un extintor donde:
- ✅ UNA MANO agarra el CUERPO (XRGrabInteractable)
- ✅ OTRA MANO presiona la BOQUILLA (XRSimpleInteractable)
- ❌ Sin problemas de "re-agarre"

---

## 🏗️ ARQUITECTURA DEL MODELO

### Jerarquía Final

```
ExtintorPrincipal (Empty/Null Transform)
├─ CuerpoExtintor (Cube, rojo, 0.1 x 0.3 x 0.1)
│  ├─ Component: XRGrabInteractable ← PARA AGARRAR
│  ├─ Component: Rigidbody (Dynamic, Mass: 0.5)
│  ├─ Component: BoxCollider (es trigger: OFF)
│  └─ Hijos: NINGUNO
│
└─ BoquillaExtintor (Cube pequeño, 0.05 x 0.1 x 0.05)
   ├─ Component: XRSimpleInteractable ← PARA PRESIONAR
   ├─ Component: SphereCollider (es trigger: ON)
   ├─ Component: Rigidbody (Kinematic, no physics)
   └─ Component: BoquillaController.cs ← SCRIPT NUEVO
```

### 🔑 PUNTO CLAVE: Jerarquía

**IMPORTANTE**: BoquillaExtintor NO es hijo de CuerpoExtintor. Son hermanos bajo ExtintorPrincipal.

```
❌ MALO (causa re-agarre):
ExtintorPrincipal
└─ CuerpoExtintor
   └─ BoquillaExtintor ← Sigue al cuerpo, se re-agarra

✅ BUENO (no causa re-agarre):
ExtintorPrincipal
├─ CuerpoExtintor
└─ BoquillaExtintor ← Independiente, no sigue automáticamente
```

---

## 📐 PASO 1: CREAR EL MODELO (Geometry)

### Crear CuerpoExtintor

1. **Hierarchy → Create Empty → Rename "ExtintorPrincipal"**
2. **Position: (0, 1, 0)**
3. **Right click → 3D Object → Cube → Rename "CuerpoExtintor"**

Configurar CuerpoExtintor:
```
Position: (0, 0, 0) ← Dentro de ExtintorPrincipal
Scale: (0.1, 0.3, 0.1)
Color: Rojo (255, 0, 0)
```

**Inspector → CuerpoExtintor**:
```
Position: (0, 0, 0)
Rotation: (0, 0, 0)
Scale: (0.1, 0.3, 0.1)
```

**Mesh Renderer**:
- Material: Rojo (crea uno si es necesario)

---

### Crear BoquillaExtintor

1. **Right click en ExtintorPrincipal → 3D Object → Cube → Rename "BoquillaExtintor"**

Configurar BoquillaExtintor:
```
Position: (0, 0.2, 0.08) ← ENCIMA y ADELANTE del cuerpo
Scale: (0.05, 0.1, 0.05)
Color: Naranja (255, 165, 0)
```

**Inspector → BoquillaExtintor**:
```
Position: (0, 0.2, 0.08)
Rotation: (0, 0, 0)
Scale: (0.05, 0.1, 0.05)
```

**Mesh Renderer**:
- Material: Naranja

---

## 🔗 PASO 2: AGREGAR FÍSICAS

### CuerpoExtintor - Rigidbody

**Add Component → Rigidbody**:
```
Mass: 0.5
Drag: 0.5
Angular Drag: 0.5
Gravity: ON
Freeze Rotation X/Y/Z: ON ← IMPORTANTE: evita rotaciones
Collision Detection: Discrete
```

### CuerpoExtintor - BoxCollider

**Ya existe por defecto**:
```
Center: (0, 0, 0)
Size: (1, 1, 1) ← Se auto-ajusta al cubo
Is Trigger: OFF ← IMPORTANTE: OFF para física
Convex: OFF
```

---

### BoquillaExtintor - Rigidbody

**Add Component → Rigidbody**:
```
Mass: 0.1
Body Type: Kinematic ← IMPORTANTE: no se mueve por física
Gravity: OFF
Collision Detection: Discrete
```

### BoquillaExtintor - SphereCollider

**Remove → BoxCollider** (que viene por defecto con el Cube)

**Add Component → SphereCollider**:
```
Center: (0, 0, 0)
Radius: 0.08 ← Cubre el área de presión
Is Trigger: ON ← IMPORTANTE: es trigger para detectar presión
```

---

## ⚙️ PASO 3: AGREGAR INTERACCIÓN XR

### CuerpoExtintor - XRGrabInteractable

**Add Component → XR Grab Interactable**:
```
Interaction Managers: [Arrastra XRInteractionManager]
Model Transform: [Arrastra CuerpoExtintor]
Select Mode: Multiple
Grab Type: Single Hand ← CLAVE: UNA mano solo
Drop on Deselect: ON
```

### BoquillaExtintor - XRSimpleInteractable

**Add Component → XR Simple Interactable**:
```
Interaction Managers: [Arrastra XRInteractionManager]
Select Mode: Multiple
```

---

## 🎨 PASO 4: AGREGAR PARTICLE SYSTEM

### Crear Particle System en BoquillaExtintor

**BoquillaExtintor → Add Component → Particle System → Rename "EspumaParticles"**

Configuración:
```
General
├─ Duration: 2
├─ Looping: ON
└─ Start Lifetime: 2

Emission
├─ Rate over Time: 50 ← Cuando está activo
└─ Bursts: NONE

Shape
├─ Shape: Cone ← Para simular spray
├─ Angle: 30
├─ Radius: 0.02
└─ Length: 0.5

Initial Velocity Module
├─ Velocity (Space=World): (0, 0, 2) ← Sale hacia adelante
└─ Speed Modifier: 1

Size Module
├─ Start Size: 0.15
└─ Size over Lifetime: Linear (0 → 1 → 0)

Velocity over Lifetime
├─ Velocity: (-0.1, -0.5, 0) ← Cae mientras avanza
```

**Particle System Renderer**:
- Material: Búsca "ParticleSystemForLiquidStandardUnlit"
  (O crea uno blanco simple)
- Render Mode: Billboard

---

## 💻 PASO 5: CREAR SCRIPTS

### Script 1: ExtintorController.cs (Cuerpo)

**Crear archivo: Assets/ExtintorController.cs**

```csharp
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtintorController : MonoBehaviour
{
    [SerializeField] private ParticleSystem espumaParticles;
    [SerializeField] private GameObject boquilla;
    
    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;
    private float damagePerSecond = 30f;
    private float damageRange = 5f;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        // Eventos de agarre
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
        
        Debug.Log("🔧 Extintor listo - Modo dual-hitbox");
    }

    // Cuando el usuario agarra el CUERPO
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log("🖐️ CUERPO AGARRADO - Espera a que presionen la boquilla");
    }

    // Cuando el usuario suelta el CUERPO
    private void OnReleased(SelectExitEventArgs args)
    {
        isHeld = false;
        espumaParticles.Stop();
        Debug.Log("🖐️ CUERPO SOLTADO");
    }

    // Llamado desde BoquillaController
    public void DispararEspuma()
    {
        if (!isHeld) return; // Solo funciona si cuerpo está agarrado
        
        espumaParticles.Play();
        ApplyDamageToFires();
    }

    public void DetenerEspuma()
    {
        espumaParticles.Stop();
    }

    // Daña fuegos cercanos
    private void ApplyDamageToFires()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        
        foreach (Collider col in colliders)
        {
            FireBehavior fire = col.GetComponent<FireBehavior>();
            if (fire != null)
            {
                fire.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
```

---

### Script 2: BoquillaController.cs (Boquilla)

**Crear archivo: Assets/BoquillaController.cs**

```csharp
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoquillaController : MonoBehaviour
{
    private XRSimpleInteractable simpleInteractable;
    private ExtintorController extintorPrincipal;
    private bool isPressedDown = false;

    void Start()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        
        // Buscar el extintor principal (padre)
        Transform padre = transform.parent;
        extintorPrincipal = padre.GetComponent<ExtintorController>();
        
        if (extintorPrincipal == null)
        {
            Debug.LogError("❌ BoquillaController: No encontré ExtintorController en el padre");
            return;
        }

        // Eventos de presión
        simpleInteractable.selectEntered.AddListener(OnPressed);
        simpleInteractable.selectExited.AddListener(OnReleased);
        
        Debug.Log("💨 Boquilla lista para presionar");
    }

    // Cuando el usuario PRESIONA la boquilla
    private void OnPressed(SelectEnterEventArgs args)
    {
        if (isPressedDown) return; // Evita duplicados
        
        isPressedDown = true;
        Debug.Log("💨 BOQUILLA PRESIONADA - Disparando espuma");
        
        // Llamar al extintor para disparar
        extintorPrincipal.DispararEspuma();
    }

    // Cuando el usuario SUELTA la boquilla
    private void OnReleased(SelectExitEventArgs args)
    {
        isPressedDown = false;
        Debug.Log("🔓 BOQUILLA SOLTADA - Deteniendo espuma");
        
        // Detener espuma
        extintorPrincipal.DetenerEspuma();
    }
}
```

---

### Script 3: FireBehavior.cs (Ya lo tienes, pero aquí va completo)

```csharp
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    [SerializeField] private float maxIntensity = 100f;
    private float currentIntensity;
    private ParticleSystem fireParticles;

    void Start()
    {
        currentIntensity = maxIntensity;
        fireParticles = GetComponent<ParticleSystem>();
    }

    public void TakeDamage(float damage)
    {
        currentIntensity -= damage;
        
        if (currentIntensity <= 0)
        {
            Extinguish();
        }
        else
        {
            // Reducir intensidad del fuego visualmente
            float intensity = currentIntensity / maxIntensity;
            fireParticles.emission.rateOverTime = 50 * intensity;
        }
    }

    private void Extinguish()
    {
        fireParticles.Stop();
        Debug.Log("🔥 Fuego apagado!");
        
        // Opcional: destruir o desactivar
        // gameObject.SetActive(false);
    }
}
```

---

## 🔧 PASO 6: CONFIGURAR EN INSPECTOR

### ExtintorPrincipal

```
Position: (0, 1, 0)
Rotation: (0, 0, 0)
Scale: (1, 1, 1)
Components: NINGUNO
```

### CuerpoExtintor

```
✅ Mesh (Cube)
✅ Material (Rojo)
✅ Rigidbody (masa 0.5, freeze rotation)
✅ BoxCollider (is trigger OFF)
✅ XRGrabInteractable (Single Hand)
✅ ExtintorController.cs
   ├─ Espuma Particles: [Arrastra BoquillaExtintor → EspumaParticles]
   └─ Boquilla: [Arrastra BoquillaExtintor]
```

### BoquillaExtintor

```
✅ Mesh (Cube pequeño)
✅ Material (Naranja)
✅ Rigidbody (Kinematic, no gravity)
✅ SphereCollider (is trigger ON)
✅ XRSimpleInteractable
✅ BoquillaController.cs
```

---

## 🎮 PRUEBA RÁPIDA

1. **Presiona Play**
2. **En VR: Agarra el CUERPO ROJO con mano izquierda**
3. **En Console deberías ver**: `🖐️ CUERPO AGARRADO`
4. **En VR: Presiona con mano derecha la BOQUILLA NARANJA**
5. **En Console deberías ver**: `💨 BOQUILLA PRESIONADA`
6. **¿Ves espuma?**

---

## ⚠️ SOLUCIÓN A PROBLEMA DE "RE-AGARRE"

El problema anterior ocurría porque:

```
❌ ANTES:
BoquillaExtintor era hijo de CuerpoExtintor
→ Al agarrar cuerpo, la boquilla se movía con él
→ La boquilla terminaba siendo "agarrada" también
→ No se podía presionar correctamente
```

```
✅ AHORA:
BoquillaExtintor es hermano de CuerpoExtintor
→ Al agarrar cuerpo, la boquilla NO se mueve
→ Rigidbody: Kinematic en boquilla la mantiene en lugar
→ XRSimpleInteractable solo permite "presionar", no "agarrar"
→ Dos manos pueden trabajar independientemente
```

**Clave**: `XRGrabInteractable` en cuerpo + `XRSimpleInteractable` en boquilla

---

## 🔥 BONUS: CREAR FUEGOS DETALLADOS

### El Problema

Tienes "charcos gigantes que emiten partículas blancas".

### La Solución

Necesitas fuegos REALISTAS:
- Modelo pequeño (esfera, no charco)
- Partículas GRANDES y ANARANJADAS (no blancas)
- Luz dinámica (emisor de luz roja)
- Sonido (opcional)

---

## 🎨 PASO 1: CREAR MODELO DE FUEGO

### Crear Fuego Base

```
1. Right click → 3D Object → Sphere → Rename "Fuego1"
2. Position: (2, 0.5, 0)
3. Scale: (0.3, 0.3, 0.3) ← Pequeño, no gigante
```

**Inspector → Fuego1**:
```
Scale: (0.3, 0.3, 0.3)
Material: Color naranja/rojo

Components:
├─ Mesh Renderer
├─ Sphere Collider (is trigger OFF para física)
└─ Rigidbody (Body Type: Static)
```

---

### Crear Particle System para Fuego

**Fuego1 → Add Component → Particle System**

**General**:
```
Duration: 5
Looping: ON
Prewarm: ON ← Para que empiece con llamas visibles
Play on Awake: ON
```

**Emission**:
```
Rate over Time: 30 ← Bastantes partículas
```

**Shape**:
```
Shape: Sphere
Radius: 0.2
Randomize Direction: 0.3
```

**Velocity Module**:
```
Velocity: (0, 1, 0) ← Sube en Y
Speed: 0.5
```

**Size Module**:
```
Start Size: 0.5 ← MÁS GRANDE que las blancas
Size over Lifetime: Curve (Grande al inicio, pequeño al final)
```

**Color Module**:
```
Color over Lifetime: 
├─ 0%: Rojo puro (255, 0, 0)
├─ 50%: Naranja (255, 165, 0)
└─ 100%: Transparente
```

**Particle System Renderer**:
```
Material: "Default-Particle" O crea uno naranja
Render Mode: Billboard
Max Particles: 100
```

---

### Agregar Luz Dinámica

**Fuego1 → Add Component → Light**

```
Light Type: Point
Color: Naranja (255, 165, 0)
Intensity: 2
Range: 3
Baking: Realtime
```

---

### Agregar Script de Fuego

**Fuego1 → Add Component → FireBehavior.cs**

```csharp
[SerializeField] private float maxIntensity = 100f;
```

---

## 📊 COMPARACIÓN: Antes vs Después

| Feature | Antes (Charco) | Después (Fuego) |
|---------|----------------|-----------------|
| Modelo | Cubo gigante | Esfera pequeña (0.3) |
| Partículas | Blancas, pequeñas | Naranjas/rojas, GRANDES |
| Color | Blanco | Degradado rojo→naranja |
| Luz | Ninguna | Punto naranja 2x |
| Realismo | Bajo | Alto |

---

## ✅ CHECKLIST FINAL

Extintor:
- [ ] ExtintorPrincipal (Empty)
- [ ] CuerpoExtintor (rojo, XRGrabInteractable)
- [ ] BoquillaExtintor (naranja, XRSimpleInteractable)
- [ ] BoquillaExtintor tiene Particle System
- [ ] Ambos scripts cargados
- [ ] No hay re-agarre (prueba ambas manos)

Fuegos:
- [ ] Esferas pequeñas (no charcos)
- [ ] Partículas naranjas/rojas GRANDES
- [ ] Luz dinámica
- [ ] FireBehavior.cs asignado
- [ ] Al disparar extintor, fuegos se reducen

---

## 🚀 RESULTADO ESPERADO

```
Usuario:
1. Agarra cuerpo rojo con mano IZQ
2. Presiona boquilla naranja con mano DER
3. Espuma dispara desde boquilla
4. Fuego se reduce y se apaga

Console:
🖐️ CUERPO AGARRADO
💨 BOQUILLA PRESIONADA
💨 BOQUILLA SOLTADA
🔥 Fuego apagado!
```

---

*Extintor Dual Hitbox - Guía Completa*
*29 de Noviembre, 2025*
*Sin re-agarre, dos manos funcionales*
