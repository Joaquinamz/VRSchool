# 🏗️ SETUP DE ESCENAS SIMPLES - SIN MODELOS COMPLEJOS

**Problema**: El modelo de escuela de Asset Store tiene dependencias faltantes y errores de iluminación.

**Solución**: Crear escenas simples desde cero que funcionan perfectamente con nuestros scripts.

---

## ⚠️ QUÉ HACER CON EL MODELO DESCARGADO

### Opción A: Eliminar el modelo (Recomendado para empezar)
1. En Project (carpeta Assets/)
2. Haz clic derecho en la carpeta `school/`
3. Delete → Delete
4. Presiona Ctrl+S para guardar

### Opción B: Mantener pero no usar
- Muévelo a una carpeta `_Unused/` dentro de Assets/
- No lo importarás en las escenas

---

## 🎯 PLAN: CREAR 3 ESCENAS FUNCIONALES SIMPLES

### Escena 1: LobbyVR (YA EXISTE)
- ✅ Existe en Assets/LobbyVR.unity
- Solo necesita: Canvas con botones
- Veremos después

### Escena 2: FireExtinguisherLesson (CREAR)
- Profesor (GameObject simple)
- Canvas con diálogos
- Plano para el suelo
- Extintor (modelo simple o GameObject)
- Fuegos (Particle Systems)

### Escena 3: EarthquakeLesson (CREAR)
- Profesor (GameObject simple)
- Canvas con diálogos
- Plano para el suelo
- Mesas (Cubes simples)
- Escombros (Cubes con Rigidbody)

---

## 📋 PASO A PASO: CREAR FireExtinguisherLesson

### PASO 1: Crear la escena
1. En Unity, haz clic en **File → New Scene**
2. Selecciona **Basic (Built-in)**
3. Se abre una escena nueva
4. **File → Save As**
5. **Nombre**: `FireExtinguisherLesson`
6. **Carpeta**: Assets/
7. **Guardar**

### PASO 2: Eliminar la cámara predeterminada
1. En Hierarchy, selecciona **Main Camera**
2. Delete (presiona Delete)
3. El XR Rig de XR Interaction Toolkit manejará la vista

### PASO 3: Crear el suelo
1. **Hierarchy → Create Empty**
2. **Nombre**: `Ground`
3. Haz clic en **Add Component**
4. Busca **Plane**
5. Configura:
   - **Position**: (0, 0, 0)
   - **Scale**: (5, 1, 5)
   - **Material**: Usa el material por defecto gris

### PASO 4: Crear el Profesor (GameObject vacío)
1. **Hierarchy → Create Empty**
2. **Nombre**: `Profesor`
3. **Position**: (0, 1.5, 2)
4. **Add Component → Model Importer** (si tienes modelo) O déjalo solo con Transform
5. Asigna el script **InstructorController.cs**

### PASO 5: Crear Canvas para diálogos
1. **Hierarchy → UI → Canvas**
2. **Nombre**: `DialogueCanvas`
3. Haz clic en Canvas
4. En Inspector → **Canvas Scaler**
   - **UI Scale Mode**: `Scale With Screen Size`
   - **Reference Resolution**: 1920 x 1080

5. **Crear Panel dentro de Canvas**:
   - Click derecho en DialogueCanvas
   - **UI → Panel – TextMeshPro**
   - **Nombre**: `DialoguePanel`
   - **Anchors**: Stretch, Stretch (llenar pantalla)

6. **Crear Texto dentro de Panel**:
   - Click derecho en DialoguePanel
   - **UI → Text – TextMeshPro**
   - **Nombre**: `DialogueText`
   - **Texto**: "Presiona el botón para comenzar"
   - **Font Size**: 36

7. **Crear Botón "Siguiente"**:
   - Click derecho en DialogueCanvas
   - **UI → Button – TextMeshPro**
   - **Nombre**: `NextButton`
   - **Posición**: (0, -400, 0) - abajo a la derecha
   - En el Text hijo, cambia el texto a "Siguiente"

### PASO 6: Crear un GameObject para el Extintor
1. **Hierarchy → Create Empty**
2. **Nombre**: `ExtintorObject`
3. **Position**: (0, 1, 0)
4. **Add Component → Cube** (por ahora, modelo simple)
5. **Scale**: (0.1, 0.3, 0.1) - forma de extintor
6. **Add Material rojo**:
   - Click derecho en Assets/Materials/
   - **Create → Material**
   - **Nombre**: `Red`
   - En Inspector, cambiar **Base Map** a rojo (255, 0, 0)
   - Arrastra el material al Cube

7. **Asigna el script WorkingExtinguisher.cs**
8. En Inspector:
   - **damageRange**: 5
   - **damagePerSecond**: 30

### PASO 7: Crear Fuegos (Particle Systems)
1. **Hierarchy → Effects → Particle System**
2. **Nombre**: `Fire_1`
3. **Position**: (2, 0.5, 0)
4. En Inspector, configura:
   - **Duration**: 30
   - **Looping**: ON
   - **Start Size**: 0.5
   - **Color over Lifetime**: Naranja/Rojo
   - **Renderer → Material**: Usa material de fuego (o crea uno naranja)

5. **Add Component → FireBehavior.cs**
6. En Inspector:
   - **maxIntensity**: 100
   - **initialIntensity**: 100
   - **particleSystem**: Arrastra el Particle System aquí

7. **Duplica este fuego** (Ctrl+D o Cmd+D):
   - `Fire_2` en (-2, 0.5, 0)
   - `Fire_3` en (0, 0.5, 2)
   - `Fire_4` en (0, 0.5, -2)
   - `Fire_5` en (2, 0.5, 2)

### PASO 8: Crear Contenedor de Fuegos
1. **Hierarchy → Create Empty**
2. **Nombre**: `Fires`
3. Arrastra todos los Fire_X dentro de Fires
4. **Add Component → FireGameManager.cs**
5. En Inspector, rellena:
   - **Fire Prefab**: Arrastra `Fire_1` aquí
   - **Timer Text**: Arrastra el `DialogueText` (o crea un Text específico para timer)
   - **Score Text**: Crea otro Text en Canvas para puntuación
   - **Fire Count Text**: Crea otro Text para contar fuegos

### PASO 9: Crear Canvas para Resultados
1. **Hierarchy → UI → Canvas**
2. **Nombre**: `ResultsCanvas`
3. Dentro, crea:
   - **Panel → Nombre: ResultsPanel**
   - **Text → Nombre: TitleText** ("¡ÉXITO!" o "TIEMPO AGOTADO")
   - **Text → Nombre: ScoreText** ("Puntuación: X")
   - **Text → Nombre: TimeText** ("Tiempo: Xs")
   - **Text → Nombre: StatsText** ("Éxitos: X")
   - **Button → Nombre: RetryButton** (Texto: "Reintentar")
   - **Button → Nombre: LobbyButton** (Texto: "Volver al Lobby")

4. **Add Component → ResultsScreen.cs**
5. En Inspector, arrastra:
   - **Results Canvas**: El canvas que acabas de crear
   - **Title Text**: TitleText
   - **Score Text**: ScoreText
   - **Time Text**: TimeText
   - **Stats Text**: StatsText
   - **Retry Button**: RetryButton
   - **Lobby Button**: LobbyButton

### PASO 10: Crear CourseManager en Lobby
1. Ve a la escena **LobbyVR.unity**
2. **Hierarchy → Create Empty**
3. **Nombre**: `CourseManager`
4. **Add Component → CourseManager.cs**
5. ¡Ya está! (El Singleton se encargará del resto)

### PASO 11: Agregar XR Rig (si no existe)
1. **Hierarchy → Create → XR → XR Origin (VR)**
   - O si ya existe, verifica que esté en position (0, 0, 0)

---

## 🎯 CREAR EarthquakeLesson (SIMILAR)

Sigue los mismos pasos que FireExtinguisherLesson, pero:

### Diferencias:

**En lugar de Extintor**, crea:
- **Mesas** (Cubes de 2x0.1x1, color marrón)
- Coloca 3-4 mesas dispersas

**En lugar de Fuegos**, crea:
- **Escombros** (Cubes pequeños, color gris)
- Agrégales **Rigidbody** con **Use Gravity: ON**
- **Add Component → EarthquakeSimulator.cs**

**Agrega el GameManager**:
- **Hierarchy → Create Empty → Nombre: EarthquakeManager**
- **Add Component → EarthquakeGameManager.cs**

**Crea Canvas para el jugador**:
- **Texto**: "¡Agáchate bajo la mesa!"
- **Botón**: Para confirmar cuando está listo

---

## 🧑 CREAR ESTUDIANTES (StudentAI)

Para EarthquakeLesson:

1. **Hierarchy → Create Cube**
2. **Nombre**: `Student_1`
3. **Scale**: (0.3, 1, 0.3)
4. **Position**: Alrededor del escenario
5. **Add Component → StudentAI.cs**
6. **Add Component → NavMeshAgent**
7. En Inspector:
   - **Speed**: 3.5
   - **Stopping Distance**: 0.5

---

## 🔧 CONFIGURAR LOBBYMANAGER EN LOBBY

1. Ve a **LobbyVR.unity**
2. **Hierarchy → Create Empty → Nombre: LobbyUI**
3. **Add Component → LobbyManager.cs**
4. En Inspector, crea los botones:
   - **Fire Extinguisher Button**: Crea un Button en Canvas
   - **Earthquake Button**: Crea otro Button
   - **Difficulty A Button**: Crea Button
   - **Difficulty B Button**: Crea Button
   - **Difficulty C Button**: Crea Button
   - **Difficulty Random Button**: Crea Button
   - **Module Name Text**: Crea Text
   - **Confirm Button**: Crea Button
   - **Difficulty Selection Canvas**: Crea Canvas para selección de dificultad

---

## ✅ BUILD SETTINGS - AGREGAR ESCENAS

1. **File → Build Settings**
2. Haz clic en **Add Open Scenes** para la escena actual
3. Manualmente agrega:
   - `LobbyVR`
   - `FireExtinguisherLesson`
   - `EarthquakeLesson`
4. **Scenes in Build** debe tener 3 escenas

---

## 🧪 TESTING

### Test 1: Play en Lobby
1. Abre LobbyVR.unity
2. Presiona **Play**
3. Debes ver 2 botones (Extintor, Sismo)
4. Haz clic en "Extintor"
5. Aparece panel de dificultad
6. Selecciona "Fácil"
7. Se carga FireExtinguisherLesson automáticamente ✅

### Test 2: Play en Extintor
1. Abre FireExtinguisherLesson.unity
2. Presiona **Play**
3. Ves diálogos del profesor
4. Presiona "Siguiente"
5. Empiezan los fuegos
6. Agarra extintor y apunta a fuegos
7. Cuando termina, ves pantalla de resultados ✅

---

## ⚠️ SI SIGUE DANDO ERRORES

### Error: "Lighting is not baked"
- **Window → Rendering → Lighting**
- Haz clic en **Generate Lighting**
- O desactiva **Baked Lights** si no los necesitas

### Error: "Missing Components"
- Es probable que haya referencias rotas
- Presiona **Ctrl+Shift+L** para limpiar
- Recrea los GameObjects

### Error: "XR Rig not found"
- Agrega un **XR Origin (VR)** en la jerarquía
- Position: (0, 0, 0)

---

## 🎉 RESUMEN

Con esta guía tienes:
- ✅ LobbyVR funcional (seleccionar módulos)
- ✅ FireExtinguisherLesson lista (minijuego de fuegos)
- ✅ EarthquakeLesson lista (minijuego de sismo)
- ✅ Transiciones automáticas entre escenas
- ✅ Sistema de dificultad A/B/C
- ✅ Interfaz de resultados

**¡NO necesitas modelos complejos! Las escenas simples funcionan perfecto.**

---

*Setup de Escenas Simples*
*28 de Noviembre, 2025*
