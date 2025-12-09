# ⏰ PLAN INMEDIATO (30 MINUTOS)

**Objetivo**: Tener un extintor dual-hitbox funcional con fuegos realistas

**Tiempo**: 30 minutos exactos

---

## MINUTO 0-2: LEE ESTO PRIMERO

```
❌ NO crees nada sin leer
✅ Lee las 4 líneas siguientes
```

El problema que tenías:
- Extintor solo agarraba con una mano
- Boquilla se re-agarraba cuando agarrabas el cuerpo

La solución:
- Dos cubos HERMANOS (no padre-hijo)
- CuerpoExtintor: XRGrabInteractable (agarre)
- BoquillaExtintor: XRSimpleInteractable (presión)
- Scripts que se comunican automáticamente

---

## MINUTO 2-5: PREPARA (3 MINUTOS)

### En Windows Explorer

Verifica que tienes estos archivos en `c:\Users\Juaquin\VRDemo\Assets\`:

```
✅ ExtintorController.cs ← NUEVO
✅ BoquillaController.cs ← NUEVO
✅ FireBehavior.cs ← ACTUALIZADO
```

Si NO están, cópialos desde donde los puse.

### En Unity

1. **Project → Assets**
2. **Click derecho → Reimport All**
3. **Espera a que termine**

---

## MINUTO 5-15: CREAR ESTRUCTURA (10 MINUTOS)

### PASO 1: Crear objeto padre vacío

1. En Hierarchy → Right click → Create Empty
2. Nombre: **ExtintorPrincipal**
3. Position: (0, 0, 0)
4. **NO agregar componentes** (es solo contenedor)

### PASO 2: Crear CuerpoExtintor (hermano 1)

1. En Hierarchy → Click derecho en ExtintorPrincipal → Create Empty Child
2. Nombre: **CuerpoExtintor**
3. Agregar modelo (3D Cube o tu modelo)
4. Agregar **Rigidbody:**
   ```
   ☑ Use Gravity: TRUE (IMPORTANTE)
   ☑ Body Type: Dynamic (NO Kinematic)
   ☑ Mass: 2
   ☑ Freeze Rotation: ✓ X, Y, Z (CONGELAR rotación)
   ☑ Collision Detection: Continuous
   ```
5. Agregar **XRGrabInteractable:**
   ```
   ☑ Interaction Mode: Grab
   ☑ Movement Type: Instantaneous
   ☑ Can Move: ✓
   ```
6. Agregar **ExtintorController.cs** (arrastra el script)

### PASO 3: Crear BoquillaExtintor (hermano 2)

1. En Hierarchy → Click derecho en ExtintorPrincipal → Create Empty Child
2. Nombre: **BoquillaExtintor**
3. Posición: X: 0.1, Y: -0.3, Z: 0 (pequeño offset)
4. Agregar modelo (pequeño cilindro o cono)
5. Escala: (0.3, 0.3, 1)
6. Agregar **Rigidbody:**
   ```
   ☑ Use Gravity: FALSE (NO cae)
   ☑ Body Type: Dynamic
   ☑ Is Kinematic: ✓ TRUE
   ☑ Constraints: Freeze All (congelado)
   ```
7. Agregar **XRGrabInteractable:**
   ```
   ☑ Interaction Mode: Grab
   ☑ Movement Type: Instantaneous
   ☑ Can Move: ✗ (NO - solo detecta)
   ```
8. Agregar **BoquillaVinculacion.cs** (arrastra el script)
9. Agregar **BoquillaController.cs** (arrastra el script)

**RESULTADO VISUAL:**
```
ExtintorPrincipal (vacío)
├── CuerpoExtintor (cilindro rojo - cae)
└── BoquillaExtintor (cilindro pequeño - sigue al cuerpo)
```

---

## MINUTO 15-25: TEST EN PLAY MODE (10 MINUTOS)

### ANTES DE TESTEAR

1. **Verifica Console:**
   - Click en Window → General → Console
   
2. **Crea un fuego para testear:**
   - Right click en Hierarchy → Create Empty → Rename "TestFuego"
   - Add Component → 3D Object → Cube (para visualizar)
   - Add Component → Particle System (para efecto)
   - Add Component → FireBehavior.cs
   - Position: (2, 0, 0) ← lejos del extintor

3. **Presiona PLAY**

### TEST 1: ¿Cae el cuerpo?

```
✅ Cuerpo cae al suelo → Rigidbody está OK
❌ Cuerpo flotando → Cambia a Dynamic, Use Gravity ✓
```

### TEST 2: ¿La boquilla sigue al cuerpo?

```
✅ Boquilla sigue → BoquillaVinculacion OK
❌ Boquilla inerte → Verifica script está asignado
```

### TEST 3: ¿Se detecta interacción?

```
✅ Puedes agarrar cuerpo → XRGrabInteractable está OK
❌ No se agarra → Verifica Interactable está assignado
```

### TEST 4: ¿Dispara espuma?

```
✅ Ves partículas → Todo funciona
❌ No ves nada → Verifica que FireBehavior está en el fuego
```

---

## MINUTO 25-30: TROUBLESHOOTING (5 MINUTOS)

### Si ves errores en Console:

**Error: "No encuentro ExtintorController"**
- ❌ Falta ExtintorController.cs en CuerpoExtintor
- ✅ Solución: Arrastra ExtintorController.cs a CuerpoExtintor

**Error: "No encuentro BoquillaController"**
- ❌ Falta BoquillaController.cs en BoquillaExtintor
- ✅ Solución: Arrastra BoquillaController.cs a BoquillaExtintor

**Error: "No encuentro XRGrabInteractable"**
- ❌ Falta componente en alguno
- ✅ Solución: Add Component → XRGrabInteractable en ambos

---
````

## ⚠️ SI ALGO SALE MAL

### Error: No veo logs
```
1. Presiona Ctrl+` para abrir Console
2. Si está vacía, verifica que los scripts están en Assets/
3. Presiona Ctrl+Shift+R para recompilar
```

### Boquilla se re-agarra
```
1. Verifica: BoquillaExtintor está DENTRO de ExtintorPrincipal
2. Verifica: BoquillaExtintor tiene Rigidbody Kinematic (NO Dynamic)
3. Verifica: BoquillaExtintor tiene XRSimpleInteractable (NO XRGrabInteractable)
```

### No funciona el Trigger
```
1. En BoquillaExtintor, verifica SphereCollider (Is Trigger: ON)
2. En BoquillaExtintor, verifica que tiene BoquillaController.cs
```

---

## ✅ CHECKLIST DE 30 MINUTOS

- [ ] Scripts importados y compilados (min 0-2)
- [ ] ExtintorPrincipal creado (min 2-5)
- [ ] CuerpoExtintor creado y configurado (min 5-15)
- [ ] BoquillaExtintor creado y configurado (min 15-22)
- [ ] EspumaParticles creado (min 22-28)
- [ ] Test 1, 2, 3 pasados (min 28-30)

---

## 🎮 GAMEPLAY ESPERADO

```
Usuario:
1. Agarra cubo ROJO con mano IZQ
2. Presiona cubo NARANJA con mano DER
3. Sale ESPUMA desde la boquilla
4. ¡Éxito!

Console:
🔧 Extintor listo - Modo dual-hitbox
💨 Boquilla lista para presionar
🖐️ CUERPO AGARRADO
💨 BOQUILLA PRESIONADA
```

---

## 📚 REFERENCIAS

Si necesitas MÁS DETALLE:
- Leer: **EXTINTOR_DUAL_HITBOX.md** (arquitectura completa)
- Leer: **INTEGRACION_RAPIDA_EXTINTOR.md** (paso a paso largo)
- Leer: **RESUMEN_DUAL_HITBOX.md** (visión general)

---

```
⏱️ TIEMPO: 30 minutos
🎯 RESULTADO: Extintor dual-hitbox funcional
🚀 ESTADO: ¡A TRABAJAR!
```

---

*Plan Inmediato - 30 Minutos*
*29 de Noviembre, 2025*
