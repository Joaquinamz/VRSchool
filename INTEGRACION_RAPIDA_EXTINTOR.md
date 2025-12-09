# 🚀 GUÍA RÁPIDA: INTEGRACIÓN EXTINTOR DUAL-HITBOX

**Tiempo estimado**: 30 minutos para tener funcionando

---

## 📋 LISTA DE PASOS

### FASE 1: Preparar Scripts (5 min)

- [ ] ExtintorController.cs → Assets/
- [ ] BoquillaController.cs → Assets/
- [ ] FireBehavior.cs → Actualizado
- [ ] En Unity: Assets → Reimport All

---

### FASE 2: Crear Estructura Jerárquica (5 min)

**Crea esto en Hierarchy**:

```
FireExtinguisherLesson (Escena)
├─ XROrigin
├─ XRInteractionManager
├─ EventSystem
├─ [Aquí irá el extintor]
│  
└─ ExtintorPrincipal ← CREATE THIS
   ├─ CuerpoExtintor ← Cube Rojo
   └─ BoquillaExtintor ← Cube Naranja
```

---

## ⚙️ PASO A PASO

### PASO 1: Crear ExtintorPrincipal (Empty)

```
1. Hierarchy → Right click → Create Empty
2. Rename: "ExtintorPrincipal"
3. Position: (0, 1, 0)
4. NO AGREGAR COMPONENTES
```

---

### PASO 2: Crear CuerpoExtintor (Cube Rojo)

**Dentro de ExtintorPrincipal**:

```
1. Right click en ExtintorPrincipal → 3D Object → Cube
2. Rename: "CuerpoExtintor"
3. Position: (0, 0, 0)
4. Scale: (0.1, 0.3, 0.1)
```

**Material**:
```
Mesh Renderer → Material → Color: Rojo (255, 0, 0)
```

**Componentes** (Add Component):

1. **Rigidbody**
   ```
   Mass: 0.5
   Drag: 0.5
   Angular Drag: 0.5
   Gravity: ON
   Freeze Rotation X/Y/Z: ✓ (3 checks)
   ```

2. **BoxCollider**
   ```
   Center: (0, 0, 0)
   Size: (1, 1, 1) ← Se auto-ajusta
   Is Trigger: OFF
   ```

3. **XR Grab Interactable**
   ```
   Interaction Managers: [XRInteractionManager]
   Model Transform: [CuerpoExtintor]
   Grab Type: Single Hand
   Drop on Deselect: ON
   ```

4. **ExtintorController.cs**
   ```
   Espuma Particles: [Arrastra BoquillaExtintor → EspumaParticles]
   Boquilla: [Arrastra BoquillaExtintor]
   Damage Per Second: 30
   Damage Range: 5
   ```

---

### PASO 3: Crear BoquillaExtintor (Cube Naranja)

**Hermano de CuerpoExtintor (dentro de ExtintorPrincipal)**:

```
1. Right click en ExtintorPrincipal → 3D Object → Cube
2. Rename: "BoquillaExtintor"
3. Position: (0, 0.2, 0.08)
4. Scale: (0.05, 0.1, 0.05)
```

**Material**:
```
Mesh Renderer → Material → Color: Naranja (255, 165, 0)
```

**Componentes** (Add Component):

1. **Rigidbody**
   ```
   Mass: 0.1
   Body Type: Kinematic ← IMPORTANTE
   Gravity: OFF
   ```

2. **Remove → BoxCollider** (que viene por defecto)

3. **Add Component → SphereCollider**
   ```
   Center: (0, 0, 0)
   Radius: 0.08
   Is Trigger: ON ← IMPORTANTE
   ```

4. **XR Simple Interactable**
   ```
   Interaction Managers: [XRInteractionManager]
   Select Mode: Multiple
   ```

5. **BoquillaController.cs**
   ```
   (Sin campos que asignar, se busca automáticamente)
   ```

---

### PASO 4: Crear Particle System (Espuma)

**En BoquillaExtintor**:

```
1. Add Component → Particle System
2. Rename a "EspumaParticles" (en Hierarchy)
```

**Configuración Rápida**:

| Sección | Valor |
|---------|-------|
| **Duration** | 2 |
| **Looping** | ON |
| **Gravity Modifier** | -0.5 |
| **Emission Rate** | 50 |
| **Shape** | Cone |
| **Cone Angle** | 30 |
| **Velocity (Local Y)** | 2 |
| **Start Size** | 0.15 |
| **Start Lifetime** | 2 |

---

### PASO 5: Crear Fuegos

**En escena, crea 3-5 fuegos**:

```
1. Right click → 3D Object → Sphere
2. Rename: "Fuego1"
3. Position: (2, 0.5, 0)
4. Scale: (0.3, 0.3, 0.3)
```

**Componentes**:

1. **Material** → Color Naranja (255, 165, 0)

2. **Rigidbody**
   ```
   Mass: 1
   Body Type: Dynamic O Static
   Gravity: ON
   ```

3. **Sphere Collider**
   ```
   Radius: 0.15
   Is Trigger: OFF
   ```

4. **Light**
   ```
   Type: Point
   Color: Naranja (255, 165, 0)
   Intensity: 2.5
   Range: 5
   Baking: Realtime
   ```

5. **Particle System → "FlamesParticles"**
   ```
   Duration: 5
   Looping: ON
   Prewarm: ON
   Emission: 40
   Shape: Sphere
   Velocity (Y): 2
   Start Size: 0.4
   Start Lifetime: 2.5
   Color: Rojo→Naranja→Amarillo→Transparente
   ```

6. **FireBehavior.cs**
   ```
   Max Intensity: 100
   Emission Rate at Max: 40
   Has Smoke: false (opcional)
   ```

---

## 🧪 TEST RÁPIDO

```
1. Presiona PLAY
2. En VR: Agarra CUERPO ROJO con mano IZQ
   → Console debe mostrar: "🖐️ CUERPO AGARRADO"

3. Presiona BOQUILLA NARANJA con mano DER
   → Console debe mostrar: "💨 BOQUILLA PRESIONADA"
   → Deberías ver ESPUMA

4. Apunta a un FUEGO
   → Fuego debe reducirse

5. Suelta Trigger
   → Console: "🔓 BOQUILLA SOLTADA"
   → Espuma se detiene

6. ¿Funciona? ✅
   ¿NO funciona? → VER TROUBLESHOOTING
```

---

## 🐛 TROUBLESHOOTING RÁPIDO

### Error: "Component not found"
**Solución**: 
- Verifica que ExtintorController.cs está en CuerpoExtintor
- Verifica que BoquillaController.cs está en BoquillaExtintor

### Error: "No XRInteractionManager"
**Solución**:
- Verifica que la escena tiene XRInteractionManager
- Si no, importa XR Interaction Toolkit desde Package Manager

### Boquilla se agarra (se re-agarra al cuerpo)
**Solución**:
- Verifica que BoquillaExtintor tiene **XRSimpleInteractable** (NO XRGrabInteractable)
- Verifica que Rigidbody es **Kinematic**
- Verifica que BoquillaExtintor es **HERMANO** de CuerpoExtintor (no hijo)

### Espuma no sale
**Solución**:
- Verifica que ExtintorController → Espuma Particles apunta a EspumaParticles
- Verifica que Particle System tiene Play on Awake: ON
- En Play mode, agarra + presiona Trigger, abre Console

### Fuego no se apaga
**Solución**:
- Verifica que Fuego tiene FireBehavior.cs
- Verifica que está en rango (5 metros del extintor)
- En Console, dispara hacia el fuego, busca mensajes de daño

---

## 📊 CHECKLIST FINAL

**Extintor**:
- [ ] ExtintorPrincipal (Empty) creado
- [ ] CuerpoExtintor (Cube rojo, 0.1 x 0.3 x 0.1)
- [ ] BoquillaExtintor (Cube naranja, 0.05 x 0.1 x 0.05)
- [ ] CuerpoExtintor tiene: Rigidbody + BoxCollider + XRGrabInteractable + ExtintorController
- [ ] BoquillaExtintor tiene: Rigidbody (Kinematic) + SphereCollider (Trigger) + XRSimpleInteractable + BoquillaController
- [ ] BoquillaExtintor tiene Particle System "EspumaParticles"
- [ ] ExtintorController.cs tiene referencias asignadas

**Fuegos**:
- [ ] 3-5 Spheres (0.3 x 0.3 x 0.3) creadas
- [ ] Cada fuego tiene: Light + Particle System + FireBehavior.cs
- [ ] Material naranja/rojo
- [ ] Luz naranja, intensidad 2.5

**Testing**:
- [ ] Console limpio (sin errores)
- [ ] Agarras cuerpo → Ves log
- [ ] Presionas boquilla → Ves espuma
- [ ] Espuma daña fuego → Fuego se reduce
- [ ] Espuma se detiene al soltar → Fuego deja de reducirse

---

## ✅ RESULTADO ESPERADO

**En escena**:
- Cubo rojo (cuerpo)
- Cubo naranja arriba (boquilla)
- 3-5 esferas naranja con luz (fuegos)

**En gameplay**:
- Agarras rojo con IZQ
- Presionas naranja con DER
- Espuma dispara
- Fuegos se apagan

**En Console**:
```
🔧 Extintor listo - Modo dual-hitbox
💨 Boquilla lista para presionar
🔥 Fuego configurado

[Al agarrar]
🖐️ CUERPO AGARRADO

[Al presionar boquilla]
💨 BOQUILLA PRESIONADA
💨 Fuego 'Fuego1' daño: -0.5 (HP: 99.5/100)

[Al soltar]
🔓 BOQUILLA SOLTADA
```

---

## 🎓 CONCEPTOS CLAVE

**Por qué esto funciona**:

1. **XRGrabInteractable** en cuerpo = UNA mano agarra
2. **XRSimpleInteractable** en boquilla = OTRA mano presiona
3. **Rigidbody Kinematic** en boquilla = No se mueve físicamente
4. **ExtintorController + BoquillaController** = Comunicación entre ambos
5. **FireBehavior.TakeDamage()** = Fuegos reciben daño y se apagan

**Esto evita**:
- ❌ Re-agarre (boquilla NO es hijo de cuerpo)
- ❌ Conflictos (2 tipos de interactable diferentes)
- ❌ Confusión (BoquillaController busca ExtintorController automáticamente)

---

*Integración Rápida - Extintor Dual-Hitbox*
*29 de Noviembre, 2025*
*30 minutos para funcionar*
