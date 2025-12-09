# 🔧 FIX: Botones no funcionan cuando se asignan a LobbyManager

## ❌ El Problema

Cuando arrastra un botón funcional al array `extintorButtons` en LobbyManager, deja de funcionar.

**Razón**: Los botones necesitan estar correctamente configurados con el componente `Button` y asignados EXACTAMENTE en el orden correcto.

---

## ✅ La Solución (Paso a Paso)

### Paso 1: Verifica que el botón sea funcional

Antes de asignarlo a LobbyManager, verifica que el botón funcione solo:

1. Selecciona el botón en Hierarchy
2. En Inspector, busca componente **Button**
3. Verifica que tenga:
   - ✓ `Is Interactable` checkbox marcado (ON)
   - ✓ Navigation: `Automatic`
   - ✓ Transition: `Color Tint` (o lo que uses)

Si no tiene componente Button:
- Add Component → Button → Image

---

### Paso 2: Asigna los botones a LobbyManager

En la escena Lobby:

1. Selecciona objeto que tiene `LobbyManager.cs` (probablemente se llama "LobbyManager" o similar)
2. En Inspector, busca componente **LobbyManager**
3. Expande el array:
   - **Extintor Buttons** (tamaño: 4)
   - **Sismo Buttons** (tamaño: 4)

---

### Paso 3: IMPORTANTE - Asigna en el orden CORRECTO

**Para Extintor Buttons:**
```
Element 0 (Extintor A)    ← Arrastra botón A
Element 1 (Extintor B)    ← Arrastra botón B
Element 2 (Extintor C)    ← Arrastra botón C
Element 3 (Extintor Random) ← Arrastra botón Random
```

**Para Sismo Buttons:**
```
Element 0 (Sismo A)       ← Arrastra botón A
Element 1 (Sismo B)       ← Arastra botón B
Element 2 (Sismo C)       ← Arrastra botón C
Element 3 (Sismo Random)  ← Arrastra botón Random
```

---

### Paso 4: Verifica Console cuando presionas PLAY

Con el nuevo `LobbyManager`, verás logs que te dicen exactamente qué pasó:

**Si está correcto, verás:**
```
[LobbyManager] Inicializando...
[LobbyManager] ✓ Botón Extintor 0 asignado
[LobbyManager] ✓ Botón Extintor 1 asignado
[LobbyManager] ✓ Botón Extintor 2 asignado
[LobbyManager] ✓ Botón Extintor 3 asignado
[LobbyManager] ✓ Botón Sismo 0 asignado
[LobbyManager] ✓ Botón Sismo 1 asignado
[LobbyManager] ✓ Botón Sismo 2 asignado
[LobbyManager] ✓ Botón Sismo 3 asignado
[LobbyManager] Inicialización completada
```

**Si hay problema, verás:**
```
[LobbyManager] ❌ Botón Extintor 0 es NULL (no asignado en Inspector)
```

---

## 🐛 Troubleshooting

### Síntoma: "Botón Extintor 0 es NULL"

**Solución**:
1. En Inspector de LobbyManager
2. Expande "Extintor Buttons"
3. ¿Element 0 está vacío (sin círculo azul)?
4. Arrastra botón A al recuadro vacío

---

### Síntoma: Click en botón pero nada pasa

**Solución**:
1. Abre Console (Window → General → Console)
2. ¿Ves el log `[LobbyManager.SelectCourse] CLICK DETECTADO`?
   - ✓ SÍ → El click funcionó, pero hay problema en SelectCourse() o SceneManager
   - ❌ NO → El click no está llegando a LobbyManager
     - Verifica que el botón NO tiene otro listener asignado
     - Verifica que Canvas tiene `GraphicRaycaster`
     - Verifica que Canvas tiene `EventSystem`

---

### Síntoma: Botón se ve presionado pero no carga escena

**Solución**:
1. Abre Console
2. Busca log: `[LobbyManager] Cargando escena:`
3. ¿Qué dice que va a cargar?
4. Verifica que esa escena existe en Build Settings:
   - File → Build Settings
   - Scroll a "Scenes In Build"
   - ¿Ves la escena listada?

---

## 📝 Checklist Final

**En LobbyManager.cs:**
- [ ] Script tiene manejo de errores (ya lo hice)
- [ ] Console muestra logs al iniciar

**En Inspector (LobbyManager):**
- [ ] Extintor Buttons:
  - [ ] Element 0: Botón A asignado (círculo azul)
  - [ ] Element 1: Botón B asignado (círculo azul)
  - [ ] Element 2: Botón C asignado (círculo azul)
  - [ ] Element 3: Botón Random asignado (círculo azul)
- [ ] Sismo Buttons: (mismo proceso)

**En Console (después de PLAY):**
- [ ] Ves logs `✓ Botón XXX asignado`
- [ ] NO ves logs `❌ Botón XXX es NULL`

**Cuando presionas botón:**
- [ ] Console muestra `[LobbyManager.SelectCourse] CLICK DETECTADO`
- [ ] Escena cambia automáticamente

---

## 🔍 Debug Rápido

Presiona PLAY y abre Console. Haz esto en orden:

1. **¿Ves "Inicialización completada"?**
   - SÍ → LobbyManager se inicializó
   - NO → Hay error en Start()

2. **¿Ves "Botón Extintor 0 asignado"?**
   - SÍ → Botón está en Inspector
   - NO → Element 0 está vacío o null

3. **Presiona botón en game view. ¿Ves "CLICK DETECTADO"?**
   - SÍ → Click llegó a LobbyManager
   - NO → Botón no tiene listener o Canvas está deshabilitado

4. **¿Ves "Escena ... cargada exitosamente"?**
   - SÍ → Todo funciona, escena está cambiando
   - NO → Escena no está en Build Settings

---

**Si pasas todos estos pasos, el botón debería funcionar correctamente.**
