# 🔧 TROUBLESHOOTING - ERRORES COMUNES AL SETUP

---

## ❌ Error: "Failed to reserve memory for scene-based lightmaps"

**Causa**: El modelo de escuela tiene iluminación baked muy pesada

**Soluciones**:
1. **Opción A**: Elimina la carpeta `Assets/school/` por completo
2. **Opción B**: 
   - **Window → Rendering → Lighting**
   - Desactiva **Auto Generate**
   - Presiona **Delete** en Baked Lighting
   - Haz clic en **Generate**

---

## ❌ Error: "Found a Transform component that is not assigned to a GameObject"

**Causa**: El modelo tiene componentes huérfanos

**Soluciones**:
1. Presiona **Ctrl+Shift+L** para limpiar referencias
2. O simplemente **no uses ese modelo** - crea escenas simples desde cero

---

## ❌ Error: "Missing Prefab with guid: 89e15e70..."

**Causa**: El modelo hace referencia a prefabs que no existen

**Soluciones**:
1. **Elimina completamente la carpeta `school/`**
2. Crea tus propios Prefabs simples:
   ```
   Assets/Prefabs/Fire.prefab
   Assets/Prefabs/Debris.prefab
   Assets/Prefabs/Student.prefab
   ```

---

## ❌ Error: "Your current multi-scene setup has inconsistent Lighting settings"

**Causa**: Cada escena tiene configuración de iluminación diferente

**Soluciones**:
1. **Window → Rendering → Lighting**
2. En la pestaña **Scene**, configura:
   - **Skybox Material**: Mismo para todas las escenas
   - **Ambient Light**: Mismo para todas
   - **Realtime GI**: Mismo para todas (ON o OFF)

3. Repite para cada escena (Lobby, Extintor, Sismo)

---

## ❌ Mi escena aparece en ROSA / MORADO

**Causa**: Material faltante o error de shader

**Soluciones**:
1. **Elimina el modelo problemático**
2. O reemplaza materiales:
   - Selecciona el objeto rosa
   - En Inspector → Materials
   - Haz clic en el material
   - Cambia Shader a **Standard**
   - Asigna colores manualmente

---

## ❌ Error: "XR Rig not found" o "Player controller missing"

**Causa**: No hay XR Origin en la escena

**Soluciones**:
1. **Hierarchy → Create → XR → XR Origin (VR)**
2. O busca en Assets si hay un prefab XR
3. Position debe ser **(0, 0, 0)**

---

## ❌ Los botones no funcionan

**Causa**: No están conectados a ningún evento

**Soluciones**:
1. Selecciona el botón en Hierarchy
2. En Inspector → Button component
3. En la sección **On Click ()**:
   - Haz clic en **+**
   - Arrastra el GameObject con el script
   - En el dropdown, selecciona **LobbyManager.OnModuleSelected()**

---

## ❌ Los diálogos no aparecen

**Causa**: El TextMeshPro no está conectado a InstructorController

**Soluciones**:
1. Selecciona el GameObject con **InstructorController**
2. En Inspector, arrastra el Text (TextMeshProUGUI) al campo **Dialogue Text**
3. Recarga la escena (Ctrl+R en Play)

---

## ❌ Los fuegos no desaparecen cuando los apago

**Causa**: El Particle System no se detiene correctamente

**Soluciones**:
1. Selecciona cada Fire_X
2. En Particle System:
   - **Stop Action**: Loop
   - Cambia a **Destroy**
3. Recarga

---

## ❌ El contador de fuegos no funciona

**Causa**: FireGameManager no sabe cuál es el prefab de fuego

**Soluciones**:
1. Selecciona **FireGameManager** en Hierarchy
2. En Inspector:
   - Arrastra **Fire_1** al campo **Fire Prefab**
   - Copia de Fire_1, y sigue las instrucciones exactas

---

## ❌ "CourseManager is null" en Console

**Causa**: CourseManager no está en LobbyVR.unity

**Soluciones**:
1. Ve a **LobbyVR.unity**
2. **Hierarchy → Create Empty**
3. **Nombre**: `CourseManager`
4. **Add Component → CourseManager.cs**
5. **DontDestroyOnLoad**: Ya está en el script ✅

---

## ❌ Las transiciones entre escenas no funcionan

**Causa**: Las escenas no están en Build Settings

**Soluciones**:
1. **File → Build Settings**
2. Haz clic en **Add Open Scenes** 3 veces (una por cada escena)
3. O arrastra manualmente las escenas:
   - LobbyVR
   - FireExtinguisherLesson
   - EarthquakeLesson

---

## ❌ "Scene not found: FireExtinguisherLesson"

**Causa**: La escena no tiene el nombre exacto

**Soluciones**:
1. Abre la escena
2. **File → Save As**
3. Nombre exacto: `FireExtinguisherLesson`
4. En Build Settings, verifica que esté listada con ese nombre

---

## ✅ SI TODO FALLA...

1. **Elimina la carpeta `Assets/school/` completamente**
2. **File → New Scene → Basic (Built-in)**
3. **Sigue QUICKSTART_5MIN.md paso a paso**
4. Ignora el modelo de Asset Store por ahora

---

## 🆘 ÚLTIMO RECURSO

Si después de todo esto aún hay errores:

1. **File → New Project**
2. Importa solo nuestros scripts C#
3. Crea escenas desde cero con los pasos en SETUP_ESCENA_SIMPLE.md
4. Debería funcionar perfectamente

Los scripts C# están listos. El problema es solo el modelo de escuela.

---

*Troubleshooting - Errores Comunes*
*28 de Noviembre, 2025*
