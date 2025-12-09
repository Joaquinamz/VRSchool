# 🎬 GUÍA RÁPIDA: PRIMEROS 5 MINUTOS - CREAR ESCENA EXTINTOR

Si no quieres leer todo, **aquí está lo mínimo** para que funcione en 5 minutos.

---

## ⚡ PASO 1: Crea la escena (30 segundos)

```
File → New Scene → Basic (Built-in) → Save As
Nombre: FireExtinguisherLesson
```

---

## ⚡ PASO 2: Borra la cámara (10 segundos)

```
Hierarchy:
├ Main Camera ← DELETE
├ Directional Light
└ Canvas
```

---

## ⚡ PASO 3: Agrega el suelo (30 segundos)

```
Hierarchy → 3D Object → Plane
Nombre: Ground
Scale: (5, 1, 5)
Position: (0, 0, 0)
```

---

## ⚡ PASO 4: Agrega Profesor (vacío)

```
Hierarchy → Create Empty
Nombre: Profesor
Position: (0, 1.5, 2)
Add Component → InstructorController.cs
```

---

## ⚡ PASO 5: Agrega Extintor

```
Hierarchy → 3D Object → Cube
Nombre: ExtintorObject
Scale: (0.1, 0.3, 0.1)
Position: (0, 1, 0)
Add Component → WorkingExtinguisher.cs
```

---

## ⚡ PASO 6: Agrega 5 Fuegos (Particle Systems)

```
Hierarchy → Effects → Particle System
Nombre: Fire_1
Position: (2, 0.5, 0)
Add Component → FireBehavior.cs

Duplica 4 veces más (Ctrl+D):
- Fire_2 en (-2, 0.5, 0)
- Fire_3 en (0, 0.5, 2)
- Fire_4 en (0, 0.5, -2)
- Fire_5 en (2, 0.5, 2)
```

---

## ⚡ PASO 7: Agrega GameManager

```
Hierarchy → Create Empty
Nombre: FireGameManager
Add Component → FireGameManager.cs
```

---

## ⚡ PASO 8: Configura referencias

**En FireGameManager (Inspector)**:
1. Arrastra Fire_1 al campo **Fire Prefab**
2. Haz clic en **Add Component → TextMeshProUGUI** para crear textos
3. Arrastra los textos a Timer Text, Score Text, etc.

---

## ✅ ¡LISTO! 

Presiona **Play** y debería funcionar.

Si no:
1. ¿Ves errores en Console?
2. ¿Falta algún componente?
3. Lee SETUP_ESCENA_SIMPLE.md para detalles completos

---

*Quick Start - 5 minutos*
