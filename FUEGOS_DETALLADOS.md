# 🔥 GUÍA DETALLADA: CREAR FUEGOS REALISTAS

**Problema**: Tienes "charcos gigantes que emiten partículas blancas"

**Solución**: Fuegos REALISTAS - esferas pequeñas, partículas naranjas/rojas, con luz dinámica

---

## 📐 PASO 1: MODELO BASE DEL FUEGO

### Crear la Esfera de Fuego

1. **Hierarchy → Right click → 3D Object → Sphere → Rename "Fuego1"**
2. **Posiciona en escena**: Position (2, 0.5, 0)

### Configurar Tamaño

**Inspector → Fuego1**:
```
Position: (2, 0.5, 0)
Rotation: (0, 0, 0)
Scale: (0.3, 0.3, 0.3) ← PEQUEÑO, no 1,1,1
```

### Material

**Inspector → Mesh Renderer → Materials**:
- Material: Crea uno rojo/naranja
  - Color: (1, 0.5, 0) - Naranja
  - Emmisive: (1, 0.3, 0) - Brilla un poco

---

## 📦 PASO 2: CONFIGURAR COLLIDER

**Inspector → Sphere Collider**:
```
Center: (0, 0, 0)
Radius: 0.15 (ajustado al tamaño de la esfera)
Is Trigger: OFF ← Para física
Convex: OFF
```

**Add Component → Rigidbody**:
```
Mass: 1
Drag: 0
Angular Drag: 0
Gravity: ON
Body Type: Dynamic O Static (depende)
```

---

## 🎆 PASO 3: PARTICLE SYSTEM - FLAMES

**Add Component → Particle System**

Renombra a "FlamesParticles" para claridad.

### General

```
Duration: 5
Looping: ON
Prewarm: ON ← Importante: empieza con llamas
Play on Awake: ON
Gravity Modifier: -0.5 ← Las llamas suben
```

### Emission

```
Rate over Time: 40 ← Bastante particles
Rate over Distance: 0
```

### Shape

```
Shape: Sphere
Radius: 0.15 ← Cubre el fuego
Radius Thickness: 1
Align to Direction: OFF
Randomize Direction: 0.3 ← Radiante
```

### Velocity Module

```
Velocity (Space: Local):
├─ X: 0
├─ Y: 2 ← Sube bastante
└─ Z: 0
Speed Modifier: 0.8
```

### Size Module

```
Start Size: 0.4 ← GRANDE (visible)
Size over Lifetime:
├─ 0%: 0.8 (tamaño inicial)
├─ 50%: 1.0
└─ 100%: 0.2 ← Se achica al final

Size over Speed:
├─ ON
└─ Curve: Multiplicador
```

### Color Module (IMPORTANTE)

```
Color over Lifetime:
├─ 0%: (1, 0, 0, 1) - Rojo puro (opaco)
├─ 20%: (1, 0.5, 0, 1) - Naranja (opaco)
├─ 60%: (1, 1, 0, 1) - Amarillo (opaco)
└─ 100%: (1, 1, 0, 0) - Amarillo (transparente)
```

### Rotation Module

```
Initial Rotation: 0 to 360 (random)
Rotation over Lifetime: 1 rotación/seg
```

### Lifetime Module

```
Start Lifetime: 2.5 ← Tiempo que vive cada partícula
```

---

## 🎨 PASO 4: PARTICLE SYSTEM RENDERER

**Inspector → Particle System (bajando)**

```
Rendering Mode: Billboard ← Siempre mira a cámara
Material: "Default-Particle"
Shader: Standard Unlit Particle
Color: White (1, 1, 1, 1)

Max Particles: 100
Render Alignment: View ← Mira a cámara
Sort Mode: By Distance
Normal Maps: None
```

Si el material no se ve bien:
- Crea uno nuevo → Material → Shader: "Particles/Standard Unlit"
- Color: (1, 1, 1)
- Texture: Usa el default

---

## 💡 PASO 5: LUZ DINÁMICA

**Add Component → Light**

```
Light Type: Point
Color: (1, 0.6, 0) - Naranja
Intensity: 2.5 ← Bastante intensidad
Range: 5 ← Ilumina alrededor
Baking: Realtime
Shadows: Soft ← Si tu hardware lo soporta
```

---

## 🌫️ PASO 6: PARTICLE SYSTEM - SMOKE (Opcional)

Para que parezca que hay humo, agrega otro Particle System.

**Right click en Fuego1 → Add Empty → Rename "SmokeParticles" → Add Component → Particle System**

### Smoke Configuration

```
General
├─ Duration: 8
├─ Looping: ON
├─ Prewarm: ON
└─ Play on Awake: ON

Emission
├─ Rate over Time: 15 ← Menos que flames
└─ Rate over Distance: 0

Shape
├─ Shape: Sphere
├─ Radius: 0.15
└─ Randomize Direction: 0.5

Velocity Module
├─ Velocity: (0, 1.5, 0) ← Sube
└─ Speed: 0.5

Size Module
├─ Start Size: 0.6 ← Un poco más que flames
└─ Size over Lifetime: 0.5 → 1.5 → 0 (crecer y después desaparecer)

Color Module
├─ 0%: (0.5, 0.5, 0.5, 0.5) - Gris semi-transparente
├─ 50%: (0.7, 0.7, 0.7, 0.3)
└─ 100%: (1, 1, 1, 0) - Desaparece

Lifetime: 4 segundos
```

---

## 📊 PASO 7: SCRIPT FireBehavior.cs

**Asegúrate que Fuego1 tiene el script**:

**Add Component → FireBehavior.cs**

```csharp
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    [SerializeField] private float maxIntensity = 100f;
    private float currentIntensity;
    private ParticleSystem flamesParticles;
    private ParticleSystem smokeParticles;
    private Light fireLight;

    void Start()
    {
        currentIntensity = maxIntensity;
        
        // Buscar particles
        flamesParticles = GetComponentInChildren<ParticleSystem>();
        
        ParticleSystem[] allParticles = GetComponentsInChildren<ParticleSystem>();
        if (allParticles.Length > 1)
        {
            smokeParticles = allParticles[1];
        }
        
        // Buscar luz
        fireLight = GetComponentInChildren<Light>();
        
        Debug.Log($"🔥 Fuego configurado: Flames={flamesParticles != null}, Smoke={smokeParticles != null}, Light={fireLight != null}");
    }

    public void TakeDamage(float damage)
    {
        currentIntensity -= damage;
        
        // Actualizar intensidad visual
        UpdateFireIntensity();
        
        if (currentIntensity <= 0)
        {
            Extinguish();
        }
    }

    private void UpdateFireIntensity()
    {
        float intensityRatio = currentIntensity / maxIntensity;
        
        // Reducir emisión de particles
        if (flamesParticles != null)
        {
            var emission = flamesParticles.emission;
            emission.rateOverTime = 40 * intensityRatio;
        }
        
        // Reducir intensidad de luz
        if (fireLight != null)
        {
            fireLight.intensity = 2.5f * intensityRatio;
        }
        
        // Cambiar color según intensidad
        if (flamesParticles != null)
        {
            Color colorLlama = Color.white;
            if (intensityRatio > 0.5f)
                colorLlama = Color.red;
            else if (intensityRatio > 0.2f)
                colorLlama = new Color(1, 0.5f, 0); // Naranja
            else
                colorLlama = new Color(1, 1, 0); // Amarillo
            
            // Nota: cambiar color en runtime requiere material instancia
        }
    }

    private void Extinguish()
    {
        Debug.Log("✅ Fuego extinguido!");
        
        // Detener particles
        if (flamesParticles != null)
            flamesParticles.Stop();
        if (smokeParticles != null)
            smokeParticles.Stop();
        
        // Apagar luz
        if (fireLight != null)
            fireLight.enabled = false;
        
        // Cambiar estado del objeto
        this.enabled = false;
    }
}
```

---

## 🎮 TEST RÁPIDO

1. **Play**
2. **En escena, verás un fuego realista**:
   - Esfera naranja pequeña (0.3)
   - Llamas rojas/naranjas subiendo
   - Luz naranja iluminando alrededor
   - Humo (si agregaste)

3. **Toma el extintor**
4. **Apunta a Fuego1**
5. **Presiona la boquilla**
6. **¿El fuego se reduce?**
7. **¿Las llamas se hacen más pequeñas?**
8. **¿La luz se oscurece?**

---

## 📋 CHECKLIST

- [ ] Esfera pequeña (0.3)
- [ ] Material naranja/rojo
- [ ] Flames Particle System (40 emisión)
- [ ] Smoke Particle System (15 emisión)
- [ ] Colors: Rojo → Naranja → Amarillo → Transparente
- [ ] Light dinámica (naranja, intensidad 2.5)
- [ ] FireBehavior.cs asignado
- [ ] Al disparar extintor, fuego se reduce
- [ ] Al terminar, fuego se apaga y luz se apaga

---

## 🎨 VARIACIONES

### Fuego Pequeño (Fácil)
```
Scale: (0.2, 0.2, 0.2)
Flames Emission: 20
Light Intensity: 1.5
Damage to extinguish: 50 HP
```

### Fuego Mediano (Normal)
```
Scale: (0.3, 0.3, 0.3)
Flames Emission: 40
Light Intensity: 2.5
Damage to extinguish: 100 HP
```

### Fuego Grande (Difícil)
```
Scale: (0.5, 0.5, 0.5)
Flames Emission: 60
Light Intensity: 3.5
Damage to extinguish: 150 HP
```

---

## ✨ RESULTADO ESPERADO

```
ANTES (Charco gigante + partículas blancas):
❌ Poco realista
❌ Difícil de apuntar
❌ No se ve que se apague

DESPUÉS (Esfera realista + llamas dinámicas):
✅ Realista
✅ Fácil de apuntar
✅ Visible cuando se apaga
✅ Iluminación dinámica
✅ Efecto de humo opcional
```

---

*Guía de Fuegos Realistas - VR*
*29 de Noviembre, 2025*
