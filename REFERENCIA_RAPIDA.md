# ⚡ REFERENCIA RÁPIDA: Configura Extintor en 5 Min

## El Setup Necesario

```
ExtintorPrincipal (vacío)
├─ CuerpoExtintor (rojo, cae)
└─ BoquillaExtintor (naranja, sigue)
```

---

## CuerpoExtintor - Checklist Inspector

```
☐ Mesh/Model: [Tu cilindro]
☐ Rigidbody:
  ☑ Use Gravity: TRUE
  ☑ Body Type: Dynamic
  ☑ Mass: 2
  ☑ Freeze Rotation: X, Y, Z (todos)
☐ Collider: [Alguno, no trigger]
☐ XRGrabInteractable:
  ☑ Interaction Mode: Grab
  ☑ Movement Type: Instantaneous
  ☑ Can Move: TRUE
☐ ExtintorController.cs [Script asignado]
```

---

## BoquillaExtintor - Checklist Inspector

```
☐ Mesh/Model: [Tu cilindro pequeño]
☐ Transform.Position: (0.1, -0.3, 0) [Offset del cuerpo]
☐ Rigidbody:
  ☑ Use Gravity: FALSE
  ☑ Body Type: Dynamic
  ☑ Is Kinematic: TRUE
  ☑ Constraints: Freeze All (X, Y, Z)
☐ Collider: [Alguno, no trigger]
☐ XRGrabInteractable:
  ☑ Interaction Mode: Grab
  ☑ Movement Type: Instantaneous
  ☑ Can Move: FALSE
☐ BoquillaVinculacion.cs [Script asignado]
☐ BoquillaController.cs [Script asignado]
```

---

## Test en Play

```
1. ¿Cae el cuerpo?
   NO → Cambiar Rigidbody a Dynamic

2. ¿Boquilla sigue al cuerpo?
   NO → Verificar BoquillaVinculacion asignado

3. ¿Puedes interactuar?
   NO → Verificar XRGrabInteractable presente
```

---

## Los Scripts Necesarios

- ✅ `ExtintorController.cs` - En CuerpoExtintor
- ✅ `BoquillaController.cs` - En BoquillaExtintor
- ✅ `BoquillaVinculacion.cs` - En BoquillaExtintor
- ✅ `FireBehavior.cs` - En cada fuego

**TODOS DEBEN ESTAR EN:** `Assets/`

---

## Fuegos para Testear

```
TestFuego (Game Object)
├── 3D Cube [visual]
├── FireBehavior.cs [script]
└── Light [para efecto]
```

---

## Problemas Comunes

| Síntoma | Solución |
|---------|----------|
| Cuerpo flotante | Rigidbody: Dynamic, Use Gravity ✓ |
| Boquilla inerte | Add BoquillaVinculacion.cs |
| No interactúa | Agregar XRGrabInteractable |
| Gira descontrolado | Rigidbody: Freeze Rotation ✓✓✓ |
| No cae al suelo | Verificar si hay Plane con Collider abajo |

---

## Links Útiles

1. `SOLUCION_3_PROBLEMAS.md` ← Detalles completos
2. `CONFIGURACION_DUAL_PASO_A_PASO.md` ← Explicación detallada
3. `CHECKLIST_3_PROBLEMAS.md` ← Troubleshooting
4. `INICIO_30_MINUTOS.md` ← Guía paso a paso

