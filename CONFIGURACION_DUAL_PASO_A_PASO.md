# 🔧 Configuración Dual-Hitbox: Paso a Paso

## 1. Jerarquía de Objetos (CRÍTICO)

```
ExtintorPrincipal (vacío - solo contenedor)
├── CuerpoExtintor (el cilindro rojo)
│   ├── Mesh
│   └── Collider
└── BoquillaExtintor (el pico - HERMANO, NO hijo del cuerpo)
    ├── Mesh
    └── Collider
```

**IMPORTANTE:** La boquilla debe ser HERMANA del cuerpo, no hija. Ambos son hijos de ExtintorPrincipal.

---

## 2. Configuración del CuerpoExtintor

### Inspector → Componentes

**Rigidbody (CRÍTICO para que caiga)**
```
Mass: 2
Drag: 0
Angular Drag: 0.05
Use Gravity: ✓ (MARCADO - para que caiga)
Freeze Rotation: ✓ (marcar los 3 ejes - para que no gire)
Body Type: Dynamic (NO Kinematic)
Collision Detection: Continuous
```

**Collider (Capsule o Box)**
```
Material: Physic Material (crea uno con 0.5 friction)
```

**XRGrabInteractable (el script de agarre)**
```
Interaction Mode: Grab
Movement Type: Instantaneous (el cuerpo se agarra)
Can Move: ✓
Track Position: ✓
Track Rotation: ✓
Throw On Detach: ✓ (para que se lance si lo sueltas rápido)
Throw Velocity Scale: 1.5
Throw Angular Velocity Scale: 1
```

**ExtintorController.cs (tu script)**
```
(Se asigna automáticamente)
```

---

## 3. Configuración de la BoquillaExtintor

### Inspector → Componentes

**Transform**
```
Position: (ajusta para que sobresalga del cuerpo)
  X: 0.1 (hacia adelante)
  Y: -0.3 (hacia abajo)
  Z: 0
Rotation: (0, 0, 0)
Scale: (0.3, 0.3, 1) (pequeña)
```

**Rigidbody (PARA VINCULACIÓN)**
```
Mass: 0.2 (muy ligero)
Use Gravity: ✗ (NO - se mueve con el cuerpo)
Is Kinematic: ✓ (MARCADO - se mueve con el padre)
Body Type: Dynamic
Constraints: Freeze All (congela posición y rotación)
```

**Collider (Sphere o Capsule)**
```
Radius/Size: pequeño
Material: Default
```

**XRGrabInteractable (para detectar presión)**
```
Interaction Mode: Grab
Movement Type: Instantaneous
Can Move: ✗ (NO se mueve, solo detecta presión)
Throw On Detach: ✗ (no se lanza)
```

**BoquillaController.cs (tu script)**
```
(Se asigna automáticamente)
```

---

## 4. Vinculación Física (CRÍTICO)

Para que la boquilla se mueva CON el cuerpo:

### OPCIÓN A: Joint (Recomendado)
1. En BoquillaExtintor, agregar: `Add Component → Configurable Joint`
2. Asignar:
   ```
   Connected Body: CuerpoExtintor (arrastra desde jerarquía)
   Anchor: (0, 0, 0)
   Connected Anchor: (posición relativa en cuerpo)
   ```

### OPCIÓN B: Parent Object
1. Hacer que BoquillaExtintor sea HIJO de CuerpoExtintor en jerarquía
2. PERO cambiar sus Rigidbodies a Kinematic:
   - CuerpoExtintor: Rigidbody Dynamic (para caer)
   - BoquillaExtintor: Rigidbody Kinematic (se mueve con padre)

### OPCIÓN C: Script de Vinculación (más control)
Agregar este script a BoquillaExtintor:
```csharp
public class BoquillaVinculacion : MonoBehaviour
{
    private Transform cuerpo;
    private Vector3 offsetPosicion;
    private Quaternion offsetRotacion;

    void Start()
    {
        cuerpo = transform.parent.Find("CuerpoExtintor");
        if (cuerpo == null)
        {
            cuerpo = transform.parent.GetChild(0);
        }
        
        offsetPosicion = transform.localPosition;
        offsetRotacion = transform.localRotation;
    }

    void LateUpdate()
    {
        if (cuerpo != null)
        {
            // Seguir al cuerpo manteniendo offset
            transform.position = cuerpo.position + cuerpo.TransformDirection(offsetPosicion);
            transform.rotation = cuerpo.rotation * offsetRotacion;
        }
    }
}
```

---

## 5. Configuración de FireBehavior (fuegos)

En cada fuego (Prefab o instancia):
```
FireBehavior.cs
├── maxIntensity: 100
├── emissionRateAtMax: 40
├── Particle Systems: (detecta automáticamente)
├── Light: (detecta automáticamente)
```

---

## 6. Test Rápido

**Antes de testear en VR:**

1. **Click en Play** en Editor
2. **Suelta el cuerpo** desde la jerarquía:
   - ❌ **Si NO cae**: Rigidbody está en Kinematic (cambiar a Dynamic)
   - ✅ **Si cae**: Configuración correcta

3. **Observa la boquilla:**
   - ❌ **Si se queda atrás**: Falta Joint o vinculación
   - ✅ **Si sigue al cuerpo**: Vinculación correcta

4. **Intenta interactuar:**
   - ❌ **Si no se pueden interactuar**: Falta XRGrabInteractable
   - ✅ **Si se detecta presión**: Todo bien

---

## 7. Diagram Physico

```
┌─────────────────────────────────┐
│    ExtintorPrincipal (vacío)    │ ← Contenedor (sin Rigidbody)
└──────────────┬──────────────────┘
               │
        ┌──────┴──────┐
        │             │
    HERMANO 1      HERMANO 2
    ┌────────┐     ┌──────────┐
    │ Cuerpo │     │ Boquilla │
    │ Dynamic│     │ Kinematic│
    │ Rigidbody    │ Rigidbody│
    │ Can Move: ✓  │ Can Move:✗
    │ Grab: Sí     │ Grab: Sí (presión)
    └────────┘     └──────────┘
         │              │
    Genera daño    Detecta presión
```

---

## 8. Troubleshooting

| Problema | Causa | Solución |
|----------|-------|----------|
| Cuerpo no cae | Rigidbody Kinematic | Cambiar a Dynamic |
| Boquilla no sigue | Sin Joint/vinculación | Agregar Configurable Joint |
| No se agarra cuerpo | XRGrabInteractable falta/mal config | Revisar Interaction Mode = Grab |
| No detecta presión boquilla | Boquilla sin XRGrabInteractable | Agregar componente |
| Cuerpo gira sin control | Freeze Rotation no marcado | Marcar los 3 ejes en Rigidbody |

---

## 9. Scripts Necesarios (VERIFICAR)

En CuerpoExtintor:
- ✓ ExtintorController.cs
- ✓ XRGrabInteractable (componente)

En BoquillaExtintor:
- ✓ BoquillaController.cs
- ✓ XRGrabInteractable (componente)
- ? BoquillaVinculacion.cs (si usas OPCIÓN C)

En FireGameManager:
- ✓ FireGameManager.cs
- ✓ FireBehavior.cs (en cada fuego)

