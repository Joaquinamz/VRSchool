# ⚡ CHECKLIST RÁPIDO - Botón No Funciona

## 🎯 1. ¿Existe SceneManager en Lobby?

```
Hierarchy (Lobby Scene)
├─ Canvas
├─ LobbyManager
└─ SceneManager ← ¿EXISTE?
   └─ SceneManagerVR component
```

**SI**: Ir a paso 2  
**NO**: 
```
1. Click derecho en Hierarchy → 3D Object > Empty
2. Nombre: "SceneManager"
3. Add Component → SceneManagerVR
4. Vuelve aquí y repite desde paso 1
```

---

## 🎯 2. ¿Está configurado el OnClick del botón?

```
Selecciona: Tu botón en Hierarchy
Inspector → Button component
↓
"On Click ()" 
├─ Verifica que hay al menos 1 evento
└─ Si está vacío:
   ✓ Click en "+"
   ✓ Arrastra el botón al campo Object
   ✓ Dropdown: SceneLoaderButton > OnButtonPressed()
```

**SI**: Ir a paso 3  
**NO**: Configura (arriba)

---

## 🎯 3. ¿Tiene el botón SceneLoaderButton?

```
Selecciona: Tu botón en Hierarchy
Inspector:
├─ Canvas Renderer ✓
├─ Image ✓
├─ Button ✓
└─ SceneLoaderButton ← ¿EXISTE?
```

**SI**: Ir a paso 4  
**NO**:
```
1. Click en "Add Component"
2. Busca "SceneLoaderButton"
3. Selecciona
4. Vuelve aquí
```

---

## 🎯 4. ¿Está llena la configuración de SceneLoaderButton?

```
Inspector → SceneLoaderButton:
├─ Load Mode: Replace (o ReturnLobby) ← ¿Configurado?
└─ Target Scene Name: "FireExtinguisherLesson1" ← ¿Lleno?
```

**SI Load MODE vacío**: Selecciona "Replace"  
**SI TARGET SCENE vacío**: Escribe el nombre de la escena  
**SI AMBOS LLENOS**: Ir a paso 5

---

## 🎯 5. ¿Las escenas están en Build Settings?

```
File → Build Settings → Scenes In Build

¿Ves:
0. Lobby ✓
1. FireExtinguisherLesson1 ✓
2. FireExtinguisherLesson2 ✓
... etc
?
```

**SI**: Ir a paso 6  
**NO**: Agrega las escenas que faltan

---

## 🎯 6. ¿Presionaste el botón?

```
▶ Play
Presiona el botón
```

**¿Qué ves en Console?**

| Log que ves | Acción |
|------------|--------|
| Nada | ❌ On Click no funciona, ve a Paso 2 |
| `[SceneLoaderButton] 🔘 Botón presionado` | ✅ Sigue leyendo |
| `SceneManagerVR NO ENCONTRADO` | ❌ Crea SceneManager, ve a Paso 1 |
| `Target Scene Name está vacío` | ❌ Llena Target Scene, ve a Paso 4 |
| `📂 Cargando (Replace): ...` | ✅ FUNCIONA, espera a cargar |

---

## ✅ SI VES: "📂 Cargando (Replace): FireExtinguisherLesson1"

**¡FELICITACIONES!** Tu botón **SÍ FUNCIONA**

Espera ~1 segundo a que la escena cargue. Si ves la escena cargada:
- ✅ **Todo está bien**
- ✅ El sistema funciona
- ✅ Configura el resto de botones igual

---

## 🆘 SI AÚN NO FUNCIONA

Ejecuta en Console (Editor):

```csharp
// Copia esto en Console y presiona Enter:
FindFirstObjectByType<SceneManagerVR>()
```

**Resultado esperado**: `[SceneManagerVR]...` (amarillo/tipo)  
**Resultado malo**: `null` (rojo)

**Si sale null**: SceneManager NO existe en Lobby

---

## 📝 QUICK REFERENCE

```
Botón NO reacciona:
  → Paso 2 (On Click)

On Click funciona pero SceneManagerVR NO encontrado:
  → Paso 1 (Crear SceneManager)

On Click funciona pero Target Scene vacío:
  → Paso 4 (Llenar Target Scene)

On Click funciona y carga escena:
  → ✅ TODO BIEN
```

---

**¿Todavía no funciona?** → Lee `DEBUG_BOTON_NO_REACCIONA.md`
