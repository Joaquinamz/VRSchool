# 🎨 Visual Guide: Anatomía del Extintor Dual-Hitbox

## Jerarquía en Árbol

```
┌─────────────────────────────────┐
│ ExtintorPrincipal               │
│ (Empty - Solo contenedor)       │
│                                 │
│ No components                   │
└──────────────┬──────────────────┘
               │
        ┌──────┴──────┐
        │             │
        ▼             ▼
   ┌─────────┐   ┌──────────┐
   │ CUERPO  │   │ BOQUILLA │
   │ HERMANO │   │ HERMANO  │
   └─────────┘   └──────────┘
   (Dinámico)    (Cinemático)
```

---

## Comparación: ANTES vs DESPUÉS

### ❌ ANTES (No funcionaba)

```
Extintor (Un solo objeto)
├── Mesh
├── Rigidbody (Kinematic)
├── XRGrabInteractable
└── Collider

PROBLEMA:
- Presionar boquilla → Se re-agarra el objeto
- No hay forma de tener 2 manos simultáneas
- Lógica imposible
```

### ✅ DESPUÉS (Funciona)

```
ExtintorPrincipal (vacío)
│
├── CuerpoExtintor (agarre)
│   ├── Mesh: Cilindro rojo grande
│   ├── Rigidbody: Dynamic + Gravity
│   ├── Collider: Grande
│   ├── XRGrabInteractable: Can Move ✓
│   └── ExtintorController.cs
│
└── BoquillaExtintor (presión)
    ├── Mesh: Cilindro pequeño
    ├── Rigidbody: Kinematic + Congelado
    ├── Collider: Pequeño
    ├── XRGrabInteractable: Can Move ✗
    ├── BoquillaController.cs
    └── BoquillaVinculacion.cs [sincroniza]

VENTAJAS:
✓ Dos manos simultáneas
✓ Boquilla no cae
✓ Lógica clara y mantenible
```

---

## Flujo de Eventos (Cómo Funciona)

```
┌─────────────────────────────────────────────┐
│ USUARIO INTERACTÚA                          │
└──────────────────┬──────────────────────────┘
                   │
        ┌──────────┴──────────┐
        │                     │
        ▼                     ▼
    MANO IZQ             MANO DER
    Agarra               Presiona
    Cuerpo               Boquilla
        │                     │
        ▼                     ▼
   ExtintorController    BoquillaController
   .OnGrabbed()          .OnPressed()
        │                     │
        └──────────┬──────────┘
                   │
                   ▼
          Ambas llaman a:
        DispararEspuma()
                   │
                   ▼
          Partículas salen
          Espuma colisiona
          Fuego recibe daño
                   │
                   ▼
          FireBehavior.TakeDamage()
                   │
                   ▼
          ¿Intensidad = 0?
           /          \
         SÍ            NO
         │             │
         ▼             ▼
      Extinguish() Continue
         │
         ▼
      Luz apaga
      Partículas paran
      OnFireExtinguished()
```

---

## Rigidbody: Diferencias

### CuerpoExtintor (Dynamic)

```
Puede CAER, puede MOVERSE, tiene GRAVEDAD

   ▼▼▼ GRAVEDAD
   
┌──────────────┐
│ CUERPO       │
│ Dynamic      │
│ Gravity ✓    │
└──────────────┘
    CUERPO
    (Cae y se mueve con mano)
    
Rigidbody Settings:
├─ Mass: 2 (afecta física)
├─ Use Gravity: TRUE
├─ Body Type: Dynamic
├─ Drag: 0 (sin fricción aérea)
└─ Angular Drag: 0.05 (poca rotación)
```

### BoquillaExtintor (Kinematic)

```
NO CABE, NO SE MUEVE, NO tiene GRAVEDAD
Se mueve mediante SCRIPT (BoquillaVinculacion)

   
┌──────────────┐
│ BOQUILLA     │
│ Kinematic    │
│ Gravity ✗    │
└──────────────┘
  ↑ Script lo mueve
  
Rigidbody Settings:
├─ Mass: 0.2 (no afecta)
├─ Use Gravity: FALSE
├─ Body Type: Dynamic
├─ Is Kinematic: TRUE
└─ Constraints: Freeze All
```

---

## Colisiones: Diferencias

### Collider del Cuerpo (Physical)

```
[Agarra con mano]
        │
    ▼▼▼ COLISIONA con:
        - Suelo (rebota/cae)
        - Otros objetos (física real)
        - Boquilla no interfiere
        
Es GRANDE para que la puedas agarrar fácil
```

### Collider de la Boquilla (Minimal)

```
[Presiona con mano]
        │
    ▼▼▼ COLISIONA con:
        - Detector de presión
        - Partículas de espuma
        
Es PEQUEÑO, solo para detectar presión
NO BLOQUEA física del cuerpo
```

---

## Interactables: Diferencias

### XRGrabInteractable (Cuerpo)

```
┌──────────────────────────────────┐
│ XRGrabInteractable               │
│ Cuerpo                           │
├──────────────────────────────────┤
│ Interaction Mode: Grab           │
│ Movement Type: Instantaneous     │
│ Can Move: TRUE ← IMPORTANTE      │
│ Throw On Detach: TRUE            │
│ Track Position: TRUE             │
│ Track Rotation: TRUE             │
└──────────────────────────────────┘

RESULTADO:
- Se agarra con mano
- Se mueve con mano
- Se puede lanzar
- Físicamente realista
```

### XRGrabInteractable (Boquilla)

```
┌──────────────────────────────────┐
│ XRGrabInteractable               │
│ Boquilla                         │
├──────────────────────────────────┤
│ Interaction Mode: Grab           │
│ Movement Type: Instantaneous     │
│ Can Move: FALSE ← IMPORTANTE     │
│ Throw On Detach: FALSE           │
│ Track Position: FALSE            │
│ Track Rotation: FALSE            │
└──────────────────────────────────┘

RESULTADO:
- Se detecta presión
- NO se mueve (congelada)
- NO se lanza
- Solo para detectar interacción
```

---

## Scripts: Responsabilidades

### ExtintorController.cs (CuerpoExtintor)

```
┌──────────────────────────────┐
│ ExtintorController           │
├──────────────────────────────┤
│ Responsabilidades:           │
│                              │
│ 1. Escuchar agarre del cuerpo│
│ 2. Comunicar con boquilla    │
│ 3. Controlar espuma          │
│ 4. Calcular daño            │
│ 5. Manejar eventos           │
└──────────────────────────────┘

public methods:
├─ DispararEspuma() → Activa partículas
├─ DetenerEspuma() → Desactiva partículas
└─ OnGrabbed() → Cuando se agarra
```

### BoquillaController.cs (BoquillaExtintor)

```
┌──────────────────────────────┐
│ BoquillaController           │
├──────────────────────────────┤
│ Responsabilidades:           │
│                              │
│ 1. Escuchar presión          │
│ 2. Llamar a extintor         │
│ 3. Disparar espuma           │
└──────────────────────────────┘

public methods:
├─ OnPressed() → Cuando presionas
└─ OnReleased() → Cuando sueltas
```

### BoquillaVinculacion.cs (BoquillaExtintor)

```
┌──────────────────────────────┐
│ BoquillaVinculacion          │
├──────────────────────────────┤
│ Responsabilidades:           │
│                              │
│ 1. Encontrar el cuerpo       │
│ 2. Calcular offset inicial   │
│ 3. Sincronizar cada frame    │
│ 4. Mantener posición relativa│
└──────────────────────────────┘

void LateUpdate():
├─ Posición = Cuerpo + Offset
└─ Rotación = Cuerpo × Offset
```

### FireBehavior.cs (En cada fuego)

```
┌──────────────────────────────┐
│ FireBehavior                 │
├──────────────────────────────┤
│ Responsabilidades:           │
│                              │
│ 1. Guardar intensidad        │
│ 2. Recibir daño              │
│ 3. Actualizar visuales       │
│ 4. Extinguirse               │
└──────────────────────────────┘

public methods:
├─ TakeDamage(float) → Recibe daño
├─ ReduceIntensity(float) → Compat
└─ GetCurrentIntensity() → Estado
```

---

## Debugging Visual

```
¿Qué VES?              ¿Qué SIGNIFICA?       ¿QUÉ HACER?
─────────────────────────────────────────────────────────

Cuerpo flotante    →  Gravity OFF o         → Cambiar a Dynamic
                      Kinematic ON             + Use Gravity ✓

Boquilla atrás    →   No sincroniza         → Agregar
                                               BoquillaVinculacion

No interactúa     →   Falta componente      → Add XRGrabInteractable

Espuma no sale    →   FireBehavior no       → Agregar en fuego
                      existe                   

Cuerpo gira loco  →   Freeze Rotation OFF   → Marcar X, Y, Z

Boquilla cae      →   Rigidbody dinámico    → Is Kinematic ✓
                      + gravity

No detecta        →   Collider es trigger   → Is Trigger ✗
interacción
```

---

## Estados de la Boquilla

```
ESTADO 1: En descanso
┌─────────────────────┐
│ Boquilla           │
│ Position: Offset   │
│ Rotation: Relativa │
│ Interaction: Sí    │
└─────────────────────┘

ESTADO 2: Presionada
┌─────────────────────┐
│ Boquilla + 👆      │
│ Sigue al cuerpo    │
│ Dispara espuma     │
│ Sigue sincronizada │
└─────────────────────┘
```

