# 🎮 CANVAS VR - GUÍA COMPLETA

**Problema**: Los Canvas en VR aparecen enormes/pequeños y el texto no se escala correctamente.

---

## ⚠️ POR QUÉ PASA

En VR, los Canvas tienen escala diferente a Desktop:
- Desktop: 1 unidad = 1 pixel en pantalla
- VR: 1 unidad = 1 metro en el mundo 3D

Por eso los Canvas aparecen enormes si usas escala 1.

---

## ✅ SOLUCIÓN: CANVAS VR CORRECTO

### PASO 1: Crear Canvas (VR)

```
Hierarchy → UI → Canvas
```

### PASO 2: Configurar Canvas Scaler

Selecciona el **Canvas**:

**Inspector → Canvas Scaler**:
```
UI Scale Mode: Constant Physical Size (IMPORTANTE)
Physical Unit: Centimeters
Fallback Screen DPI: 96
Default Physical DPI: 96
```

### PASO 3: Tamaño del Canvas

Selecciona el **Canvas**:

**Inspector → Rect Transform**:
```
Scale X: 0.01  ← REDUCIR 100 veces
Scale Y: 0.01  ← REDUCIR 100 veces
Scale Z: 1     ← Dejar igual

Position X: 0
Position Y: 1.5  ← Altura de ojos
Position Z: 2    ← Distancia al jugador

Width: 1920  (píxeles lógicos)
Height: 1080 (píxeles lógicos)
```

### PASO 4: Crear Panel dentro del Canvas

```
Click derecho en Canvas
UI → Panel (TextMeshPro)
Nombre: DialoguePanel
```

**Configurar Panel**:
- Anchor: Stretch, Stretch (llenar canvas)
- Left: 0, Right: 0, Top: 0, Bottom: 0

### PASO 5: Crear Texto

```
Click derecho en Panel
UI → Text (TextMeshPro)
Nombre: DialogueText
```

**Configurar Texto**:
```
Tamaño de Fuente: 36  (NO 100+)
Alignment: Center + Middle
Wrapping Enabled: ON
```

---

## 📏 TAMAÑOS RECOMENDADOS PARA VR

### Canvas Scaler
```
UI Scale Mode: Constant Physical Size
Physical Unit: Centimeters
```

### Canvas Rect Transform
```
Scale: (0.01, 0.01, 1)
Position: (0, 1.5, 2)  - A la altura de los ojos
```

### Texto
```
Font Size: 36-48
Line Spacing: 1.2
```

### Botones
```
Button Size: (200, 80)  - En píxeles
Text Font Size: 36
```

---

## 🎯 EJEMPLO: Canvas Dialogo Correcto

### Jerarquía
```
DialogueCanvas (Scale: 0.01, 0.01, 1)
├─ DialoguePanel (Stretch)
│  ├─ DialogueText (TextMeshProUGUI, size 40)
│  ├─ NextButton (Button)
│  │  └─ Text (TextMeshProUGUI, size 32)
│  └─ Background (Image)
```

### Valores exactos

**DialogueCanvas**:
```
Position: (0, 1.5, 2)
Scale: (0.01, 0.01, 1)
Canvas Scaler:
  - UI Scale Mode: Constant Physical Size
  - Physical Unit: Centimeters
```

**DialoguePanel**:
```
Anchors: Stretch, Stretch
OffsetMin: (0, 0)
OffsetMax: (0, 0)
Color: Black with alpha 0.8
```

**DialogueText**:
```
Anchor: Middle Center
Position: (0, 0, 0)
Size: (800, 300)
Font Size: 40
Color: White
Alignment: Center, Middle
Wrapping: On
```

**NextButton**:
```
Anchor: Bottom Center
Position: (0, -200, 0)
Size: (300, 80)
Button Component:
  - Transition: Color Tint
  - Normal Color: Blue
  - Highlighted Color: Cyan
```

---

## ⚡ QUICK FIX: Si tu Canvas está GIGANTE

1. Selecciona el Canvas
2. **Rect Transform → Scale**: Cambia a **(0.01, 0.01, 1)**
3. **Canvas Scaler → UI Scale Mode**: Cambia a **Constant Physical Size**
4. Presiona Play
5. ✅ Debe estar del tamaño correcto

---

## 🎮 CANVAS PARA DIFERENTES USOS

### Canvas para Diálogos
```
Scale: (0.01, 0.01, 1)
Position: (0, 1.5, 2)  - Frente a los ojos
Text Size: 40-50
```

### Canvas para UI Menú (Lobby)
```
Scale: (0.02, 0.02, 1)
Position: (0, 1.2, 3)  - Más lejos
Botones Size: 200x80
```

### Canvas para Resultados
```
Scale: (0.015, 0.015, 1)
Position: (0, 1.5, 2.5)
Tamaños medianos
```

---

## 🎨 ESCALA VR vs DESKTOP

| Elemento | Desktop | VR |
|----------|---------|-----|
| Canvas Scale | (1, 1, 1) | (0.01, 0.01, 1) |
| Texto Size | 60 | 40 |
| Botón Size | 300x100 | 200x80 |
| Distancia | N/A | 2 metros |

---

## ✅ CHECKLIST: Canvas VR Correcto

- [ ] Canvas Scaler: **Constant Physical Size**
- [ ] Canvas Scale: **(0.01, 0.01, 1)**
- [ ] Canvas Position: **(0, 1.5, 2)**
- [ ] Texto Font Size: **36-48**
- [ ] Texto Wrapping: **ON**
- [ ] Botones tienen OnClick listeners
- [ ] Presionas Play y ves tamaños normales

---

*Canvas VR - Guía Completa*
*29 de Noviembre, 2025*
