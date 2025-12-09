# 🎮 START HERE: Extintor Dual-Hitbox en 5 Pasos

**Tiempo total: 30 minutos**

---

## El Problema (Lo que ya pasó)

Tu extintor:
- ❌ Solo se agarraba con UNA mano
- ❌ Cuando agarrabas el cuerpo, la boquilla se re-agarraba
- ❌ No había forma de presionar mientras agarrabas

## La Solución (Lo que hicimos)

Creamos un sistema de **Dual-Hitbox** con:
- ✅ Dos hitboxes independientes (cuerpo + boquilla)
- ✅ Dos manos simultáneas SIN re-agarre
- ✅ 3 scripts nuevos: ExtintorController, BoquillaController, BoquillaVinculacion
- ✅ 8 documentos de guía

---

## 5 PASOS: Configura en 30 Min

### PASO 1: Entiende la Estructura (2 min)

```
ABRE: REFERENCIA_RAPIDA.md

Lee el primer diagrama:
ExtintorPrincipal (vacío)
├─ CuerpoExtintor (agarra con mano 1)
└─ BoquillaExtintor (presiona con mano 2)
```

### PASO 2: Crea la Jerarquía (5 min)

```
EN UNITY:
1. Right click Hierarchy → Create Empty → "ExtintorPrincipal"
2. Right click ExtintorPrincipal → Create Empty Child → "CuerpoExtintor"
3. Right click ExtintorPrincipal → Create Empty Child → "BoquillaExtintor"

RESULTADO: Jerarquía correcta
```

### PASO 3: Configura Componentes (15 min)

```
PARA CUERPOEXTINTOR:
☐ Add Mesh (Cube o tu cilindro)
☐ Add Collider (Box/Sphere/Capsule)
☐ Add Rigidbody:
  ✓ Use Gravity: TRUE
  ✓ Body Type: Dynamic
  ✓ Mass: 2
  ✓ Freeze Rotation: X, Y, Z
☐ Add XRGrabInteractable (Can Move: TRUE)
☐ Drag ExtintorController.cs

PARA BOQUILLAEXTINTOR:
☐ Add Mesh (pequeño Cube)
☐ Position: (0.1, -0.3, 0)
☐ Add Collider (pequeño)
☐ Add Rigidbody:
  ✓ Is Kinematic: TRUE
  ✓ Constraints: Freeze All
☐ Add XRGrabInteractable (Can Move: FALSE)
☐ Drag BoquillaController.cs
☐ Drag BoquillaVinculacion.cs
```

### PASO 4: Verifica en Play Mode (5 min)

```
PRESIONA: PLAY (▶)

DEBE PASAR:
✅ Cuerpo cae al suelo
✅ Boquilla no cae
✅ Boquilla sigue al cuerpo

SI ALGO FALLA: Ve a CHECKLIST_3_PROBLEMAS.md
```

### PASO 5: Testea Interacción (3 min)

```
EN PLAY MODE:
1. Agarra CuerpoExtintor
   Console debe mostrar: "🖐️ CUERPO AGARRADO"
   
2. Presiona BoquillaExtintor
   Console debe mostrar: "💨 BOQUILLA PRESIONADA"

SI VES ESTO: ✅ TODO FUNCIONA

SI NO VES: Revisar VERIFICATION_ANTES_VR.md
```

---

## LISTO ✅

Tu extintor está listo para:
1. ✅ Dual-hand interaction
2. ✅ Sin re-agarre
3. ✅ Física realista

Ahora puedes:
- 🔥 Crear fuegos realistas (FUEGOS_DETALLADOS.md)
- 🎮 Testear en VR (INICIO_30_MINUTOS.md)
- 📚 Entender todo (SOLUCION_3_PROBLEMAS.md)

---

## 📚 Documentación

Si necesitas:

| Necesitas | Documento |
|-----------|-----------|
| Setup rápido | REFERENCIA_RAPIDA.md |
| Setup detallado | INICIO_30_MINUTOS.md |
| Entender qué pasó | SOLUCION_3_PROBLEMAS.md |
| Troubleshooting | CHECKLIST_3_PROBLEMAS.md |
| Diagramas | VISUAL_GUIDE_EXTINTOR.md |
| Verificar todo | VERIFICATION_ANTES_VR.md |
| Todo junto | README_DOCUMENTACION_EXTINTOR.md |

---

## ⚡ TL;DR (Ultra Rápido)

```
1. Crea jerarquía: ExtintorPrincipal > CuerpoExtintor + BoquillaExtintor
2. Configura Rigidbodies: Cuerpo=Dynamic, Boquilla=Kinematic
3. Agrega XRGrabInteractable a ambos
4. Arrastra scripts: ExtintorController, BoquillaController, BoquillaVinculacion
5. PLAY → Debe funcionar

❌ Si falla: VERIFICATION_ANTES_VR.md (paso a paso)
✅ Si funciona: ¡Prueba en VR!
```

---

## 🎯 Próximos Pasos

```
Ahora que el extintor funciona:

1. CREAR FUEGOS
   → FUEGOS_DETALLADOS.md

2. TESTEAR EN VR
   → INICIO_30_MINUTOS.md (sección TEST EN VR)

3. ENTENDER LA ARQUITECTURA
   → SOLUCION_3_PROBLEMAS.md

4. CUSTOMIZAR PARÁMETROS
   → ExtintorController.cs (comentarios)
```

---

**¡A EXTINTOR DUAL-HAND FUNCIONAL! 🔥🎮**

