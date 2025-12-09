# ✅ ACTUALIZACIÓN: LobbyManager Ahora Inteligente

## Qué cambió:

`LobbyManager.cs` ahora:

1. **Detecta automáticamente el tipo de escena según el curso**:
   - Si seleccionas "Extintor" → Carga `FireExtinguisherLesson`
   - Si seleccionas "Sismo" → Carga `ClassroomScene`

2. **Es más robusto** (maneja mayúsculas, espacios, etc.)

3. **Muestra logs claros**:
   ```
   [LobbyManager] Cargando escena: FireExtinguisherLesson
   ```

---

## Verificación en Build Settings

**IMPORTANTE: La escena DEBE estar en Build Settings**

1. File → Build Settings
2. Scroll a "Scenes In Build"
3. Debe haber:
   - Scene 0: `1` (o tu Lobby)
   - Scene 1: `FireExtinguisherLesson` (o similar)

Si tu escena no está ahí:
- Arrastra el archivo `.unity` desde Project al recuadro "Scenes In Build"

---

## Ahora funciona así:

```
Click "Extintor A" en Lobby
  ↓
LobbyManager lee que es "Extintor"
  ↓
Determina que debe cargar "FireExtinguisherLesson"
  ↓
SceneManager.LoadScene("FireExtinguisherLesson")
  ↓
Se descarga Lobby ("1")
Se carga FireExtinguisherLesson
  ↓
NPCProfessor.Start() lee GameManager.selectedCourse = "Extintor"
  ↓
ShowIntroduction() muestra diálogos de Extintor
  ↓
¡El flujo comienza!
```

---

**Ahora sí, presiona PLAY en Lobby y prueba. Debería funcionar.**
