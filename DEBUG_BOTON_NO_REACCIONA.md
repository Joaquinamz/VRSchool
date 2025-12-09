# 🔧 GUÍA DE DEBUG - El Botón No Reacciona

## Problema Reportado
El botón está configurado pero **no reacciona al presionarlo**.

---

## ✅ VERIFICACIÓN PASO A PASO

### Paso 1: Verificar que el Botón Ejecuta el Método

1. **Abre Console en Unity** (Window > General > Console)
2. **Presiona el botón**
3. **Busca estos logs**:
   ```
   [SceneLoaderButton] 🔘 Botón presionado: ...
   ```

**Resultado esperado**: Ves el log "Botón presionado"

**Si NO ves nada**:
- ❌ El evento On Click NO está configurado correctamente
- Ve al Paso 3 (Reconfigurar On Click)

---

### Paso 2: Verificar que SceneManagerVR Existe

1. **Presiona el botón**
2. **En Console, busca**:
   ```
   [SceneLoaderButton] ✓ ... listo (Modo: Replace, Escena: ...)
   ```

3. **Si ves error**:
   ```
   [SceneLoaderButton] ❌ SceneManagerVR NO ENCONTRADO
   ```

**Si SceneManagerVR no existe**:
- ❌ No creaste el GameObject "SceneManager" en Lobby
- ✅ Crea GameObject vacío: Click derecho → 3D Object > Empty
- ✅ Renombra a "SceneManager"
- ✅ Add Component → SceneManagerVR
- ✅ Presiona el botón nuevamente

---

### Paso 3: Reconfigurar On Click Correctamente

**El error más común es que el evento On Click NO está bien configurado.**

#### Opción A: Usando Dropdown (Más Fácil)

1. **Selecciona el botón en Hierarchy**
2. **Inspector → Button component**
3. **Section: "On Click ()"**
4. **Haz click en "+" (agregar evento)**
5. **En el nuevo evento**:
   - **Campo izquierdo (GameObject)**: Arrastra el **MISMO GameObject del botón**
   - **Dropdown (Function)**: Selecciona `SceneLoaderButton > OnButtonPressed()`
6. **✅ Listo**

**Resultado esperado**:
```
On Click ()
├─ ListElement 0
│  ├─ Object: Button (SceneLoaderButton)
│  └─ Function: SceneLoaderButton.OnButtonPressed()
```

#### Opción B: Verificar que está configurado

1. **Selecciona el botón**
2. **Inspector → Button component → On Click ()**
3. **Verifica que ves**:
   ```
   ListElement 0
   ├─ Object: [Tu Button GameObject aquí]
   └─ Function: SceneLoaderButton > OnButtonPressed ()
   ```

**Si NO ves nada**: Agrega el evento (Paso A arriba)

---

### Paso 4: Verificar que SceneLoaderButton está Configurado

1. **Selecciona el botón**
2. **Inspector → SceneLoaderButton component**
3. **Verifica**:
   ```
   ✓ Load Mode: Replace (o ReturnLobby)
   ✓ Target Scene Name: FireExtinguisherLesson1 (o nombre correcto)
   ```

**Si Target Scene está vacío**:
- ❌ Escribe el nombre de la escena exactamente (case-sensitive)
- ❌ O selecciona ReturnLobby si es botón de "Volver"

---

## 🧪 TESTING CON TECLAS (Debug Mode)

Para verificar que el sistema funciona **sin presionar botones**:

### Paso 1: Agregar Script de Debug

1. **En Lobby, selecciona Canvas**
2. **Add Component → SceneLoaderDebug**
3. **✅ Listo**

### Paso 2: Probar con Teclas

En el editor, presiona:

```
E → Carga FireExtinguisherLesson1
A → Carga EarthQuakeLesson1
R → Vuelve a Lobby
L → Verifica si SceneManagerVR existe
B → Lista todos los GameObjects en Lobby
```

**Resultado esperado**:
```
[DEBUG] Presionaste E - Cargando FireExtinguisherLesson1
[SceneManagerVR] Cargando escena (reemplazar): FireExtinguisherLesson1
```

**Si esto funciona**: El sistema está bien, el problema es solo el botón

---

## 🔍 TROUBLESHOOTING

### Problema 1: "Botón presionado" pero "SceneManagerVR NO ENCONTRADO"

```
Solución:
1. Crea GameObject "SceneManager" en Lobby
2. Add Component → SceneManagerVR
3. Presiona el botón nuevamente
```

### Problema 2: No ves ningún log (ni siquiera "Botón presionado")

```
Solución: On Click NO está configurado

1. Selecciona el botón
2. Inspector → Button → On Click ()
3. Si está vacío, agrega evento:
   ✓ Arrastra el botón al campo Object
   ✓ Selecciona SceneLoaderButton > OnButtonPressed()
4. Presiona el botón nuevamente
```

### Problema 3: Ves "Botón presionado" pero "Target Scene Name está vacío"

```
Solución:
1. Selecciona el botón
2. Inspector → SceneLoaderButton
3. Target Scene Name: Escribe el nombre de la escena
   ✓ Para Lobby: "FireExtinguisherLesson1", etc.
   ✓ Para Cursos: Si es "Volver", LoadMode debe ser ReturnLobby
```

### Problema 4: El botón dice que está en "Additive" pero yo quiero "Replace"

```
Solución:
1. Selecciona el botón
2. Inspector → SceneLoaderButton → Load Mode
3. Cambia a "Replace" (para botones de Lobby)
4. O "ReturnLobby" (para botón de "Volver")
```

---

## 📋 CHECKLIST DE VERIFICACIÓN

```
[ ] Existe GameObject "SceneManager" en Lobby
[ ] SceneManager tiene componente SceneManagerVR
[ ] El botón tiene componente SceneLoaderButton
[ ] SceneLoaderButton.Load Mode está configurado
[ ] SceneLoaderButton.Target Scene Name está lleno
[ ] Button.On Click tiene un evento
[ ] Button.On Click apunta a SceneLoaderButton.OnButtonPressed()
[ ] Build Settings tiene todas las escenas
[ ] Console NO muestra errores de compilación
```

---

## 📊 DIAGNÓSTICO RÁPIDO

### Ejecuta este test:

1. **Presiona el botón**
2. **Abre Console**
3. **¿Qué ves?**:

| Log | Problema | Solución |
|-----|----------|----------|
| Nada | On Click no configurado | Configura On Click |
| "Botón presionado" + SceneManagerVR NO ENCONTRADO | SceneManager no existe | Crea GameObject "SceneManager" con SceneManagerVR |
| "Botón presionado" + Target Scene vacío | Target Scene Name no lleno | Escribe el nombre de escena |
| "Botón presionado" + Cargando escena | ✅ FUNCIONA | Todo bien, espera a que cargue |

---

## 🆘 ÚLTIMA OPCIÓN: Reset Completo

Si nada funciona, haz esto:

1. **Elimina el SceneLoaderButton del botón**
2. **Presiona el botón → Verifica que no reacciona**
3. **Add Component → SceneLoaderButton nuevamente**
4. **Configura en Inspector**:
   - Load Mode: Replace
   - Target Scene Name: FireExtinguisherLesson1
5. **Button.On Click() → +**
6. **Arrastra el botón, selecciona OnButtonPressed()**
7. **Presiona el botón**

Si aún no funciona, es un problema con Unity/Build Settings.

---

## 📞 INFORMACIÓN A REPORTAR

Si aún no funciona, reporta:

```
1. Console log completo (copia/pega aquí)
2. Estructura del botón:
   • Canvas
   └─ Button (nombre exacto)
      └─ SceneLoaderButton (fields mostrados en Inspector)
3. Build Settings:
   • ¿Están todas las escenas?
   • ¿Nombres exactos?
```

---

**Versión**: 1.0 - Debug Guide  
**Última actualización**: Diciembre 2025
