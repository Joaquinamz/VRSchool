# Implementación Rápida - Carga de Escenas (5 minutos)

## Paso 1: Crear SceneManager (1 minuto)

### En la escena Lobby:
1. Click derecho en Hierarchy → `3D Object > Empty`
2. Renombra a: `SceneManager`
3. Click en `Add Component` → Busca `SceneManagerVR` → Selecciona
4. Listo, ya está configurado automáticamente

---

## Paso 2: Opción A - Usar LobbyManager Actualizado (RECOMENDADO)

Si ya tienes LobbyManager en tu Lobby:
- ✅ Ya está actualizado para usar SceneManagerVR
- Solo asegúrate de arrastrar los botones en Inspector
- Funciona exactamente igual que antes

---

## Paso 3: Opción B - Usar SceneLoaderButton en cada botón (ALTERNATIVO)

Si prefieres script por botón (más flexible):

### Para cada botón en Lobby:

1. **Selecciona el botón** (ej: "btnExtintorA")
2. **Add Component → SceneLoaderButton**
3. **Inspector > SceneLoaderButton**:
   - Load Mode: `Replace`
   - Target Scene Name: `FireExtinguisherLesson1`
4. **Inspector > Button**:
   - On Click () → `+`
   - Arrastra el mismo GameObject (donde está el botón)
   - Dropdown: `SceneLoaderButton > OnButtonPressed()`

Repite para los 6 botones con sus respectivas escenas.

---

## Paso 4: Botón "Volver a Lobby" (en cada curso)

En cada escena de curso (FireExtinguisherLesson1, etc.):

1. **Identifica o crea un botón "Volver"**
2. **Add Component → SceneLoaderButton**
3. **Inspector > SceneLoaderButton**:
   - Load Mode: `ReturnLobby` ← IMPORTANTE
   - Target Scene Name: (dejalo vacío)
4. **Inspector > Button**:
   - On Click () → `+`
   - Arrastra el GameObject con el botón
   - Dropdown: `SceneLoaderButton > OnButtonPressed()`

---

## Verificación Rápida

Abre la escena y prueba:
```
✓ Lobby se carga correctamente
✓ Presionas botón → Carga el curso
✓ En el curso, presionas "Volver" → Vuelve a Lobby
✓ Console muestra: [SceneManagerVR] mensajes
```

Si todo funciona, ¡ya está listo! 🎉

---

## Diferencia con Script Antiguo

### SceneLoaderExtintor.cs (antiguo)
```csharp
public void LoadByName(string cursoExtintor1)
{
    SceneManager.LoadScene(cursoExtintor1);  // Solo carga
}
```

### SceneManagerVR.cs (nuevo)
```csharp
public void LoadSceneReplace(string sceneName)     // Reemplaza
public void LoadSceneAdditive(string sceneName)    // Carga adicional
public void UnloadScene(string sceneName)          // Descarga
public void ReturnToLobby()                        // Vuelve a Lobby
```

**Ventajas**:
- ✅ Flexible (Replace, Additive, Return)
- ✅ Manejo de transiciones
- ✅ Singleton (acceso global)
- ✅ Compatible con botones
- ✅ Compatible con código C#

---

¿Preguntas? Consulta `GUIA_CARGA_DESCARGA_ESCENAS.md` para detalles completos.
