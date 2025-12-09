# ✅ VERIFICATION CHECKLIST: Antes de Entrar en VR

**Objetivo:** Verificar que TODO está configurado antes de probar en VR

**Tiempo Estimado:** 10 minutos

---

## FASE 1: Compilación (2 min)

### Console Status

```
❌ PROBLEMA
Si ves en Console:
- Red messages (errores)
- "Assembly Reloading..."

✅ SOLUCIÓN
1. Window → General → Console
2. Espera a que termine "Assembly Reloading"
3. Cuenta errores rojos ¿Cuántos hay?
   
   0 errores → ✅ CONTINUAR
   > 0 errores → ❌ DETENER y revisar
```

### Scripts Detectados

```
En Project → Assets, verifica:
☐ ExtintorController.cs (existe)
☐ BoquillaController.cs (existe)
☐ BoquillaVinculacion.cs (existe)
☐ FireBehavior.cs (existe)

Si falta alguno:
→ Cópialo desde donde esté
→ Reimport All (Ctrl+Shift+R)
→ Espera a "Assembly Reloading" complete
```

---

## FASE 2: Jerarquía (3 min)

### Structure Verification

En **Hierarchy**, debe verse:

```
✅ ExtintorPrincipal (vacío)
   ├─ ☐ CuerpoExtintor (¿existe?)
   └─ ☐ BoquillaExtintor (¿existe?)

❌ INCORRECTO:
   Extintor (un solo objeto)
   BoquillaExtintor (suelto sin padre)
```

### Parent-Child Verification

```
Haz click en cada objeto y verifica en Inspector:

CuerpoExtintor:
☐ Parent: ExtintorPrincipal
☐ Position: aproximadamente (0, 0, 0)

BoquillaExtintor:
☐ Parent: ExtintorPrincipal
☐ Position: aproximadamente (0.1, -0.3, 0)
```

---

## FASE 3: Componentes CuerpoExtintor (2 min)

### Selecciona: CuerpoExtintor

En **Inspector**, verifica cada componente:

```
☐ Transform
  └─ Position: (0, 0, 0) o cercano
  
☐ Mesh Renderer
  └─ Material: (alguno)

☐ Collider (Box, Sphere, o Capsule)
  └─ Is Trigger: ✗ (NO marcado)
  └─ Material: (alguno o Default)

☐ Rigidbody ← CRÍTICO
  ├─ Body Type: Dynamic (NO Kinematic)
  ├─ Use Gravity: ✓ (MARCADO)
  ├─ Mass: 2 (aproximadamente)
  ├─ Drag: 0
  ├─ Angular Drag: 0.05
  ├─ Freeze Rotation: ✓ X, Y, Z (todos)
  └─ Collision Detection: Continuous
  
☐ XRGrabInteractable ← CRÍTICO
  ├─ Interaction Mode: Grab
  ├─ Movement Type: Instantaneous
  ├─ Can Move: ✓ (MARCADO)
  └─ Throw On Detach: ✓ (MARCADO)

☐ ExtintorController.cs ← CRÍTICO
  └─ (Sin campos públicos para asignar)
```

**Si algo no coincide:**
```
→ Corrige el valor
→ Guarda (Ctrl+S)
→ Vuelve a revisar
```

---

## FASE 4: Componentes BoquillaExtintor (2 min)

### Selecciona: BoquillaExtintor

En **Inspector**, verifica cada componente:

```
☐ Transform
  ├─ Position: (0.1, -0.3, 0) o cercano
  ├─ Scale: (0.3, 0.3, 1) o similar
  └─ Rotation: (0, 0, 0)
  
☐ Mesh Renderer
  └─ Material: (alguno diferente del cuerpo)

☐ Collider (Box, Sphere, o Capsule)
  ├─ Is Trigger: ✗ (NO marcado)
  ├─ Size/Radius: pequeño
  └─ Material: (alguno o Default)

☐ Rigidbody ← CRÍTICO
  ├─ Body Type: Dynamic
  ├─ Use Gravity: ✗ (NO marcado)
  ├─ Is Kinematic: ✓ (MARCADO) ← MUY IMPORTANTE
  ├─ Mass: 0.2 (no importa si Kinematic)
  └─ Constraints: Freeze All (X, Y, Z - los 3)
  
☐ XRGrabInteractable ← CRÍTICO
  ├─ Interaction Mode: Grab
  ├─ Movement Type: Instantaneous
  ├─ Can Move: ✗ (NO marcado) ← MUY IMPORTANTE
  └─ Throw On Detach: ✗ (NO marcado)

☐ BoquillaController.cs ← CRÍTICO
  └─ (Sin campos públicos para asignar)

☐ BoquillaVinculacion.cs ← CRÍTICO
  └─ (Sin campos públicos para asignar)
```

---

## FASE 5: Fuegos de Test (1 min)

### Crear fuego simple

En Hierarchy:
```
1. Right click → Create Empty → "TestFuego"
2. Position: (2, 1, 0) ← lejos del extintor
3. Add Component: 3D Object → Cube
4. Add Component: Light (optional, para efecto)
5. Add Component: FireBehavior.cs
```

En **Inspector de TestFuego → FireBehavior**:
```
☐ maxIntensity: 100
☐ emissionRateAtMax: 40
☐ Particle Systems: [auto-detect]
☐ Fire Light: [auto-detect]
```

---

## FASE 6: Test en Play Mode (2 min)

### Paso 1: Iniciar Play

```
1. Presiona PLAY (▶)
2. Mira la Console
3. Debería aparecer:
   ✅ "🔥 Fuego 'TestFuego' inicializado..."
   ✅ "🔧 Extintor listo - Modo dual-hitbox"
   ✅ "💨 Boquilla lista para presionar"

❌ Si NO aparece:
   → Revisar que los scripts están asignados
   → Revisar que FireBehavior existe
```

### Paso 2: Verificar Física

```
En Game View:
1. ¿CuerpoExtintor cae al suelo?
   ✅ SÍ → Rigidbody OK
   ❌ NO → Cambiar Use Gravity a TRUE

2. ¿BoquillaExtintor sigue al cuerpo?
   ✅ SÍ → BoquillaVinculacion OK
   ❌ NO → Revisar script está asignado
   
3. ¿BoquillaExtintor flotando?
   ✅ NO (está en posición) → OK
   ❌ SÍ (está cayendo) → Is Kinematic debe ser TRUE
```

### Paso 3: Verificar Interacción

```
En Console mientras PLAY:
1. Agarrar CuerpoExtintor:
   Esperar ver: "🖐️ CUERPO AGARRADO"
   ✅ VES ESTO → ExtintorController OK
   ❌ NO VES → Revisar XRGrabInteractable en cuerpo

2. Presionar BoquillaExtintor:
   Esperar ver: "💨 BOQUILLA PRESIONADA"
   ✅ VES ESTO → BoquillaController OK
   ❌ NO VES → Revisar XRGrabInteractable en boquilla

3. Ver espuma:
   ¿Aparecen partículas?
   ✅ SÍ → FireBehavior dispara
   ❌ NO → Revisar que extinguidor dispara
```

### Paso 4: Detener Play

```
Presiona STOP (⏹)
Si ves errores en Console:
→ Anotarlos
→ Revisar en PAUSED mode (click derecho en objeto)
```

---

## PROBLEMA: ¿Qué si algo falla?

### Cuerpo no cae

```
❌ SÍNTOMA: Objeto flota cuando presionas PLAY

🔍 DIAGRAMA:
   Rigidbody → Use Gravity?
                ├─ NO → ❌ CAMBIAR A SÍ
                └─ SÍ → Body Type?
                        ├─ Kinematic → ❌ CAMBIAR A Dynamic
                        └─ Dynamic → ✅ OK

✅ SOLUCIÓN:
1. Selecciona CuerpoExtintor
2. Inspector → Rigidbody
3. Use Gravity: ✓ MARCADO
4. Body Type: Dynamic
5. Presiona PLAY de nuevo
```

### Boquilla no sigue

```
❌ SÍNTOMA: Boquilla se queda atrás cuando agarras cuerpo

🔍 DIAGRAMA:
   BoquillaVinculacion?
   ├─ NO asignado → ❌ ASIGNAR
   ├─ Asignado pero falla → Body no encontrado?
   │   ├─ Verificar nombre: "CuerpoExtintor"
   │   └─ Verificar estructura en Hierarchy
   └─ Todo OK → ¿Script tiene LateUpdate?

✅ SOLUCIÓN:
1. Selecciona BoquillaExtintor
2. Inspector → BoquillaVinculacion
3. En Play mode, abre Console
4. Busca: "Boquilla vinculada a: CuerpoExtintor"
   ✅ SI → Vinculación OK
   ❌ NO → Revisar script
5. Si falta: Right click BoquillaVinculacion → Remove
6. Add Component → BoquillaVinculacion
```

### No se detecta interacción

```
❌ SÍNTOMA: No ves rayo de interacción, o no se agarra

🔍 DIAGRAMA:
   XRGrabInteractable?
   ├─ NO existe → ❌ AGREGAR
   ├─ Existe pero Collider falta → ❌ AGREGAR Collider
   ├─ Collider es Trigger → ❌ CAMBIAR Is Trigger a FALSE
   └─ Todo OK → Verifica XRInteractionManager

✅ SOLUCIÓN:
1. Selecciona objeto (CuerpoExtintor o BoquillaExtintor)
2. Inspector → Add Component → XRGrabInteractable
3. Espera a que compile
4. Verifica en Hierarchy que tienes Collider
   ❌ SI NO → Right click → Add Component → [Collider]
5. En Collider: Is Trigger ✗ (NO marcado)
6. Presiona PLAY
```

---

## CHECKLIST FINAL (Pre-VR)

Antes de poner los controles VR:

```
☐ 0 errores de compilación (Console vacía de rojo)
☐ Jerarquía correcta (padre + 2 hermanos)
☐ CuerpoExtintor tiene Dynamic + Use Gravity
☐ BoquillaExtintor tiene Kinematic + congelado
☐ Ambos tienen XRGrabInteractable
☐ Cuerpo tiene ExtintorController
☐ Boquilla tiene BoquillaController + BoquillaVinculacion
☐ Fuego de test tiene FireBehavior
☐ Play Mode: Cuerpo cae
☐ Play Mode: Boquilla sigue
☐ Play Mode: Se detecta interacción
☐ Play Mode: Se ven partículas
```

**Si TODO tiene ☐:** ✅ LISTO PARA VR

**Si algo falta ☐:** ❌ REVISAR ANTES

---

## Recomendación

```
Si todo está ✅, ve a:
→ INICIO_30_MINUTOS.md
→ Sección "TEST EN VR"
→ Instrucciones para probar con controles reales
```

