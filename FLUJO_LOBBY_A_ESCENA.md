# 🔄 FLUJO: De Lobby a Escena del Juego

## La Pregunta:

**"¿Si presiono botón Extintor A, se descargará la escena '1' (Lobby) y cargará FireExtinguisherLesson?"**

**Respuesta**: ✅ SÍ, exactamente eso debería pasar.

---

## 🎯 Cómo funciona actualmente:

### Paso 1: Presionas botón en Lobby

```
Usuario en escena "1" (Lobby)
     ↓
Click en botón "Extintor A"
```

### Paso 2: LobbyManager intercepta el click

```csharp
extintorButtons[0].onClick.AddListener(() => SelectCourse("Extintor", "A"));
```

Se ejecuta: `SelectCourse("Extintor", "A")`

### Paso 3: Se guardan valores en GameManager

```csharp
GameManager.instance.selectedCourse = "Extintor";
GameManager.instance.selectedDifficulty = "A";
```

### Paso 4: Se descarga Lobby y carga la nueva escena

```csharp
SceneManager.LoadScene("ClassroomScene");
```

**AQUÍ ESTÁ EL PROBLEMA:** 

El código intenta cargar `"ClassroomScene"`, pero probablemente tu escena se llama **`"FireExtinguisherLesson"`** (u otro nombre).

### Paso 5: Nueva escena se carga

Se descarga: Escena "1" (Lobby)
Se carga: Escena determinada por el nombre en LoadScene()

---

## ✅ SOLUCIÓN: Actualizar LobbyManager

Tienes dos opciones:

### Opción A: Renombrar tu escena a "ClassroomScene"

1. En Project/Assets/Scenes
2. Find: `FireExtinguisherLesson.unity`
3. Right-click → Rename: `ClassroomScene.unity`
4. ✅ Listo. El código ya apunta a eso.

### Opción B: Actualizar LobbyManager para que apunte a tu escena

Si tu escena realmente se llama `FireExtinguisherLesson`, actualiza LobbyManager.cs:

```csharp
// Cambiar esta línea:
SceneManager.LoadScene("ClassroomScene");

// A esto:
SceneManager.LoadScene("FireExtinguisherLesson");
```

---

## 📋 Build Settings (IMPORTANTE)

**Independientemente de la opción que elijas, la escena DEBE estar registrada en Build Settings:**

1. File → Build Settings
2. En "Scenes In Build" debe haber:
   - Scene 0: `1` (o tu Lobby) ← donde estás ahora
   - Scene 1: `ClassroomScene` O `FireExtinguisherLesson` ← donde vas

Si tu escena NO está aquí, ¡no se puede cargar!

### Cómo verificar:

1. File → Build Settings
2. Scroll down a "Scenes In Build"
3. ¿Ves tu escena listada? (Con un número de índice)
4. Si NO, arrastra el archivo .unity desde Project al recuadro "Scenes In Build"

---

## 🔍 Resumen del flujo CORRECTO

```
LOBBY (Escena "1")
  ├─ Usuario ve 2 grupos de botones:
  │  ├─ Extintor [A] [B] [C] [Random]
  │  └─ Sismo [A] [B] [C] [Random]
  │
  └─ Click en "Extintor A"
     │
     ├─ LobbyManager.SelectCourse("Extintor", "A")
     │
     ├─ GameManager.selectedCourse = "Extintor"
     │ GameManager.selectedDifficulty = "A"
     │
     ├─ SceneManager.LoadScene("FireExtinguisherLesson") ← O ClassroomScene
     │
     ├─ DESCARGA: Escena "1" (Lobby)
     │ CARGA: Escena FireExtinguisherLesson
     │
     └─ NUEVA ESCENA (FireExtinguisherLesson)
        │
        ├─ NPCProfessor.Start() se ejecuta
        │  └─ Lee GameManager.selectedCourse = "Extintor" ✓
        │
        ├─ ShowIntroduction()
        │  └─ Muestra diálogos de Extintor
        │
        └─ ... (resto del flujo)
```

---

## ⚙️ Lo que tienes que hacer AHORA

### Opción Recomendada: OPCIÓN A (más simple)

1. **Renombra tu escena a "ClassroomScene":**
   - En Project
   - Find: tu archivo .unity de la escena del juego
   - Right-click → Rename: `ClassroomScene.unity`
   - ✅ Listo. El código ya funciona.

2. **Verifica que está en Build Settings:**
   - File → Build Settings
   - "Scenes In Build" debe tener tu escena en index 1
   - Si no, arrastra tu escena .unity al recuadro

3. **Presiona PLAY en Lobby:**
   - Selecciona "Extintor A"
   - Verifica que se carga la nueva escena
   - Mira en Console los logs

### Si prefieres OPCIÓN B (personalizado)

1. **Abre `LobbyManager.cs`:**
   ```csharp
   SceneManager.LoadScene("ClassroomScene");
   ```
   Cambiar a:
   ```csharp
   SceneManager.LoadScene("FireExtinguisherLesson");
   ```

2. **Verifica que está en Build Settings:**
   - File → Build Settings
   - Tu escena debe estar listada en "Scenes In Build"

3. **Presiona PLAY**

---

## 🆘 Troubleshooting

### Error: "Scene not found: ClassroomScene"

**Causa**: La escena no existe o no está en Build Settings

**Solución**:
1. File → Build Settings
2. En "Scenes In Build", escala down
3. ¿Ves tu escena? Si NO:
   - Arrastra tu escena .unity desde Project al recuadro
4. Verifica que el nombre sea EXACTO (case-sensitive)

### Se abre Lobby de nuevo (no cambia de escena)

**Causa**: `SceneManager.LoadScene()` apunta a escena que no existe

**Solución**:
1. Abre `LobbyManager.cs`
2. Verifica que el nombre coincida EXACTAMENTE con el archivo .unity
3. Verifica que la escena está en Build Settings

### Funciona pero no inicia flujo de Extintor

**Causa**: `NPCProfessor` no está detectando `selectedCourse = "Extintor"`

**Solución**:
1. Mira Console (Window → General → Console)
2. Busca: `[NPCProfessor] Curso seleccionado:`
3. ¿Qué dice?
   - Si dice `'Extintor'` → Correcto, sigue el flujo
   - Si dice `''` (vacío) → `LobbyManager` no está asignando valor
   - Si dice `'Sismo'` → Presionaste botón Sismo por error

---

## ✅ Checklist Final

- [ ] Tu escena de juego existe (FireExtinguisherLesson u otro nombre)
- [ ] La escena está guardada en Assets/Scenes/
- [ ] La escena está registrada en Build Settings (Scene 1)
- [ ] LobbyManager.cs tiene el nombre EXACTO de tu escena
- [ ] NPCProfessor.cs está en la nueva escena
- [ ] FireGameManager.cs está en la nueva escena
- [ ] Canvas de diálogos está en la nueva escena
- [ ] Todos los campos en Inspector están asignados

---

**Una vez esto esté correcto, presiona PLAY, ve al Lobby, clickea "Extintor A", y debería cambiar de escena automáticamente.**
