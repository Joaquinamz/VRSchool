# 🚀 INICIO RÁPIDO - Sistema de Carga de Escenas

**Tiempo estimado**: 5 minutos

---

## ¿Qué necesito hacer?

### Paso 1️⃣: Crear SceneManager en Lobby (1 min)

```
1. Abre escena "Lobby" en Unity
2. Click derecho en Hierarchy → 3D Object > Empty
3. Renombra: "SceneManager"
4. Click en "Add Component" → Busca "SceneManagerVR" → Selecciona
5. ¡Listo! El resto se configura automáticamente
```

---

### Paso 2️⃣: Elegir tu Estrategia (30 seg)

**Opción A: Usar LobbyManager** ✅ RECOMENDADO
- Más simple si ya lo tienes configurado
- LobbyManager.cs ya está actualizado
- Solo asegúrate de arrastrar los botones

**Opción B: Usar SceneLoaderButton**
- Más flexible
- Un script por botón
- Sigue Paso 3 abajo

---

### Paso 3️⃣: Configurar Botones en Lobby (2 min)

**Si usas Opción B (SceneLoaderButton):**

Para cada uno de los 6 botones:

```
1. Selecciona el botón (ej: "btnExtintorA")
2. Add Component → SceneLoaderButton
3. Inspector:
   • Load Mode = Replace
   • Target Scene Name = FireExtinguisherLesson1
4. En Button component:
   • On Click () → + (agregar evento)
   • Arrastra el GameObject del botón
   • Dropdown: SceneLoaderButton > OnButtonPressed()
```

---

### Paso 4️⃣: Botón "Volver" en Cursos (2 min)

En CADA escena de curso (FireExtinguisherLesson1, etc):

```
1. Identifica o crea botón "Volver a Lobby"
2. Add Component → SceneLoaderButton
3. Inspector:
   • Load Mode = ReturnLobby
   • Target Scene Name = (vacío)
4. En Button component:
   • On Click () → +
   • Arrastra el botón
   • Dropdown: SceneLoaderButton > OnButtonPressed()
```

---

## ✅ Verificación

```
▶ Presiona Play
✓ Presiona botón Extintor 1 → Carga escena
✓ Presiona "Volver" → Regresa a Lobby
✓ Repite → Funciona sin problemas
✓ Console muestra: [SceneManagerVR] ...
```

**Si todo funciona ↑**: ¡Ya está listo! 🎉

---

## 📚 Guías Completas

Si necesitas **detalles**, consulta:

- **IMPLEMENTACION_RAPIDA_ESCENAS.md** ← Explicación paso a paso
- **GUIA_CARGA_DESCARGA_ESCENAS.md** ← Guía completa con ejemplos
- **DIAGRAMA_ESCENAS_VISUAL.md** ← Diagramas visuales

---

## 🔧 Opciones Avanzadas

### Ajustar Tiempo de Transición
```
En Lobby, selecciona "SceneManager"
Inspector > SceneManagerVR
  • Transition Delay = 0.5 (en segundos)
```

### Ver Logs Detallados
```
Inspector > SceneManagerVR
  • Debug Mode = true ✓
```

### Usar desde Código C#
```csharp
// Cargar curso
SceneManagerVR.LoadScene_Static("FireExtinguisherLesson1");

// Volver a Lobby
SceneManagerVR.ReturnToLobby_Static();
```

---

## ❌ Troubleshooting Rápido

| Problema | Solución |
|----------|----------|
| "No carga la escena" | Verifica nombre en Build Settings |
| "Botón no responde" | Configura On Click correctamente |
| "Se ve lag" | Aumenta Transition Delay |
| "Errores en Console" | Verifica que SceneManager existe |

---

## 📋 Build Settings

**Requerido**: Todas las escenas aquí

```
File > Build Settings > Scenes In Build

0. Lobby
1. FireExtinguisherLesson1
2. FireExtinguisherLesson2
3. FireExtinguisherLesson3
4. EarthQuakeLesson1
5. EarthQuakeLesson2
6. EarthQuakeLesson3
```

---

## 📦 Archivos Creados

```
SceneManagerVR.cs          ← Gestor central
SceneLoaderButton.cs       ← Script para botones
LobbyManager.cs            ← Actualizado
```

**0 errores de compilación** ✅

---

**¿Preguntas?** Consulta la guía completa → **GUIA_CARGA_DESCARGA_ESCENAS.md**

**¡Tiempo estimado total: 5 minutos!** ⏱️
