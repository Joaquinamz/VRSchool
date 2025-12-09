# ✅ Checklist: Solucionar 3 Problemas

## Problema 1: Cuerpo no cae tras tocar el suelo ❌→✅

### Causa
El Rigidbody del CuerpoExtintor está en modo **Kinematic** o falta configuración.

### Solución (5 pasos)

1. **Selecciona en Jerarquía:** `ExtintorPrincipal > CuerpoExtintor`

2. **En Inspector → Rigidbody, revisa:**
   ```
   ☐ Body Type: Dynamic (NO Kinematic)
   ☐ Use Gravity: ✓ MARCADO
   ☐ Mass: 2 (aproximadamente)
   ☐ Drag: 0
   ☐ Angular Drag: 0.05
   ☐ Collision Detection: Continuous
   ☐ Constraints → Freeze Rotation: ✓✓✓ (los 3 ejes)
   ```

3. **Test en Play Mode:**
   - Click derecho en CuerpoExtintor
   - "Toggle Active"
   - Debería caer instantáneamente al suelo

4. **Si sigue sin caer:**
   - Verifica que el Collider NO esté marcado como "Is Trigger"
   - Verifica que el suelo tenga un Collider (puede ser Plane con Box Collider)

5. **Si cae correctamente:**
   - ✅ Pasar al Problema 2

---

## Problema 2: Boquilla se queda inerte y no sigue al cuerpo ❌→✅

### Causa
La Boquilla NO está vinculada al Cuerpo. Se creó como objeto independiente sin física.

### Solución (3 opciones, elige 1)

#### OPCIÓN RÁPIDA: Hacer hijo del cuerpo
1. En Jerarquía: Arrastra `BoquillaExtintor` DENTRO de `CuerpoExtintor`
2. Ahora es hijo automáticamente
3. En Inspector de BoquillaExtintor → Rigidbody:
   ```
   ☐ Body Type: Dynamic
   ☐ Is Kinematic: ✓ MARCADO (importante)
   ☐ Constraints: Freeze All (todo congelado)
   ```
4. ✅ Probado y funciona

#### OPCIÓN FÍSICA: Configurable Joint (recomendado si quieres que se tambalee)
1. Mantén BoquillaExtintor como HERMANO (NO hijo)
2. En BoquillaExtintor → Add Component: **Configurable Joint**
3. Asigna:
   ```
   Connected Body: (arrastra CuerpoExtintor aquí)
   Anchor: (0, 0, 0)
   Connected Anchor: (0, -0.3, 0.1) ← ajusta según tu modelo
   X Motion: Free / Y Motion: Free / Z Motion: Free
   Angular X/Y/Z Motion: Free
   ```
4. Esto permite movimiento natural pero vinculado

#### OPCIÓN SCRIPT: BoquillaVinculacion.cs (más control)
1. Mantén BoquillaExtintor como HERMANO
2. En BoquillaExtintor → Add Component: **BoquillaVinculacion**
3. El script automáticamente:
   - ✅ Busca el CuerpoExtintor
   - ✅ Calcula el offset inicial
   - ✅ En cada frame, sincroniza posición y rotación
4. **Ventaja:** Funciona sin necesidad de Joint
5. **Desventaja:** Menos realista (no tiene inercia)

**RECOMENDACIÓN:** Usar OPCIÓN 1 (hijo del cuerpo) por simplicidad

---

## Problema 3: No se puede interactuar con la boquilla ❌→✅

### Causa
Falta componente XRGrabInteractable o está mal configurado.

### Solución (5 pasos)

1. **Selecciona:** `ExtintorPrincipal > BoquillaExtintor` (en jerarquía)

2. **En Inspector → Add Component:**
   ```
   Busca: XRGrabInteractable
   (debe venir del XR Interaction Toolkit)
   ```

3. **Configura XRGrabInteractable:**
   ```
   ☐ Interaction Mode: Grab
   ☐ Movement Type: Instantaneous
   ☐ Can Move: ✗ (NO marcar - no debe moverse)
   ☐ Track Position: ✓ (opcional)
   ☐ Track Rotation: ✓ (opcional)
   ☐ Throw On Detach: ✗ (NO)
   ☐ Use Dynamic Attach: ✗ (NO)
   ```

4. **Verifica que el Collider esté OK:**
   ```
   ☐ La boquilla tiene un Collider (Sphere, Capsule, o Box)
   ☐ El Collider NO está marcado como "Is Trigger"
   ☐ El Collider es lo suficientemente grande para detectar mano
   ```

5. **Test en Play Mode con VR:**
   - Acerca la mano a la boquilla
   - Debería aparecer highlight/rayo de interacción
   - Al agarrar, debería activar `BoquillaController.OnPressed()`
   - Debería disparar espuma (si hay fuegos)

---

## Summary Rápido

| Componente | Debe tener | Configuración |
|-----------|-----------|-----------------|
| **CuerpoExtintor** | Rigidbody | Dynamic, Use Gravity ✓, Freeze Rotation ✓✓✓ |
| **CuerpoExtintor** | XRGrabInteractable | Interaction Mode: Grab, Can Move: ✓ |
| **CuerpoExtintor** | ExtintorController | (auto) |
| **BoquillaExtintor** | Rigidbody | Dynamic O Kinematic (según vinculación) |
| **BoquillaExtintor** | XRGrabInteractable | Interaction Mode: Grab, Can Move: ✗ |
| **BoquillaExtintor** | BoquillaController | (auto) |
| **BoquillaExtintor** | BoquillaVinculacion? | Solo si es hermano (OPCIÓN 3) |
| **Fuegos** | FireBehavior | (auto) |

---

## Test Final

Una vez configurado, esto debe pasar:

1. ✅ Sueltas el cuerpo → Cae al suelo
2. ✅ Agarras el cuerpo → Se mueve con la mano
3. ✅ Agarras el cuerpo → La boquilla lo sigue
4. ✅ Agarras la boquilla → Se detecta presión
5. ✅ Presionando boquilla + cuerpo agarrado → Espuma dispara
6. ✅ Sueltas → Fuegos se apagan (si hay daño)

¿Cuál de estos pasos está fallando? 🤔

