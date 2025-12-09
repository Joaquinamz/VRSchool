# ✅ CHECKLIST RÁPIDO: Tu Ruta Hacia el Éxito

## Antes de Empezar (AHORA)

```
☐ 1. Abre: COMIENZA_AQUI_AHORA.md
☐ 2. Lee los primeros 2 minutos
☐ 3. Responde: ¿Tengo Unity 2022+? SÍ/NO
☐ 4. Responde: ¿Tengo XR Toolkit? SÍ/NO
☐ 5. Si dijiste NO en alguno: Instala primero
☐ 6. Si dijiste SÍ a todo: Continúa
```

---

## Fase de Limpieza (30 MINUTOS)

```
☐ 1. Abre: SOLUCION_ERROR_PREFAB.md
☐ 2. En Assets, DELETE: 1.unity
☐ 3. En Assets, DELETE: 1FireExtinguisherLesson.unity
☐ 4. Espera a que reimporte
☐ 5. Cierra Unity completamente
☐ 6. Reabre Unity
☐ 7. Verifica: Console NO tiene errores rojos
☐ 8. Si sigue habiendo errores: Lee SOLUCION_ERROR_PREFAB.md completamente
```

---

## Fase de Implementación (6 HORAS)

### ANTES DE COMENZAR
```
☐ 1. Abre: PASO_A_PASO_6HORAS.md
☐ 2. Ten a mano VSCode o Notepad para copiar scripts
☐ 3. Ten a mano Unity Editor minimizado
☐ 4. ¿Estás listo? COMIENZA FASE 1
```

### FASE 1: Setup Inicial (30 MINUTOS)
```
Paso 1.1: Crear GameManager.cs
  ☐ Create Empty en Hierarchy → Rename: GameManager
  ☐ Add Component → GameManager (script nuevo)
  ☐ Copia contenido de GameManager.cs del documento
  ☐ Guarda (Ctrl+S)
  ☐ Arrastra al objeto GameManager
  ✅ VALIDACIÓN: Ves "GameManager" en inspector

Paso 1.2: Crear Escena Lobby
  ☐ File → New Scene
  ☐ Rename a: Lobby
  ☐ Save as: Assets/Scenes/Lobby.unity
  ☐ Crea Canvas
  ☐ Crea 8 botones bajo Panel_Selection
  ✅ VALIDACIÓN: Jerarquía tiene 8 botones

Paso 1.3: LobbyManager.cs
  ☐ Crea script LobbyManager.cs
  ☐ Copia contenido completo del documento
  ☐ Guarda
  ☐ Create Empty → Rename: LobbyManager
  ☐ Add Component → LobbyManager
  ☐ Arrastra los 8 botones a los arrays
  ✅ VALIDACIÓN: Los 8 botones están asignados
```

**PAUSA: Presiona PLAY y verifica**
```
☐ No hay errores en Console
☐ Ves el Canvas con botones
☐ Click un botón → Intenta cargar escena (error normal, falta escena)
☐ Si todo OK: Continúa a FASE 2
```

---

### FASE 2: ClassroomScene (1H 30MIN)
```
Paso 2.1: Crear escena
  ☐ File → New Scene
  ☐ Rename: ClassroomScene
  ☐ Save: Assets/Scenes/ClassroomScene.unity
  ✅ VALIDACIÓN: Ves escena vacía

Paso 2.2: Setup básico
  ☐ Crea Plane → Rename: Floor
  ☐ Crea Cube → Rename: Walls
  ☐ Crea Light
  ✅ VALIDACIÓN: Escena tiene iluminación

Paso 2.3: NPC Profesor
  ☐ Crea Capsule → Rename: Professor
  ☐ Add Canvas como hijo
  ☐ Crea Panel → Rename: DialoguePanel
  ☐ Crea 2 TextMeshPro
  ☐ Crea 1 Button
  ✅ VALIDACIÓN: Puedes ver el UI bajo el profesor

Paso 2.4: NPCProfessor.cs
  ☐ Crea script NPCProfessor.cs
  ☐ Copia contenido completo
  ☐ Guarda
  ☐ Selecciona Professor
  ☐ Add Component → NPCProfessor
  ☐ Arrastra TextMeshPro al campo Dialogue Text
  ☐ Arrastra Button al campo Next Button
  ✅ VALIDACIÓN: Al presionar PLAY ves textos

**PAUSA: Presiona PLAY**
☐ Profesor habla (texto aparece)
☐ Botón "Siguiente" funciona
☐ Cambia el texto después de cada click
☐ Si todo OK: Continúa a FASE 3
```

---

### FASE 3: Sistema Extintor (1H 30MIN)
```
Paso 3.1: Prefab Fire
  ☐ Crea Sphere → Rename: Fire
  ☐ Add Collider
  ☐ Add Rigidbody (Dynamic, Gravity ON)
  ☐ Crea material rojo
  ☐ Arrastra Fire a Assets/Prefab/
  ☐ Borra Fire de Hierarchy
  ✅ VALIDACIÓN: Fire.prefab existe en Assets/Prefab/

Paso 3.2: FireBehavior.cs
  ☐ Crea script FireBehavior.cs
  ☐ Copia contenido completo
  ☐ Selecciona Fire prefab (en Assets/Prefab/)
  ☐ Add Component → FireBehavior
  ✅ VALIDACIÓN: Prefab tiene script

Paso 3.3: FireGameManager.cs
  ☐ Crea script FireGameManager.cs
  ☐ Copia contenido completo
  ☐ Create Empty en ClassroomScene → FireGameManager
  ☐ Add Component → FireGameManager
  ☐ Arrastra Fire prefab al campo Fire Prefab
  ✅ VALIDACIÓN: Script está asignado

Paso 3.4: Canvas UI
  ☐ Crea Canvas → GameplayUI
  ☐ Crea 2 TextMeshPro bajo él
  ☐ Arrastra al FireGameManager.cs
  ✅ VALIDACIÓN: Campos asignados

**PAUSA: Presiona PLAY**
☐ Presionas Siguiente en diálogos
☐ Aparece un fuego
☐ Si todo OK: Continúa a FASE 4
```

---

### FASE 4: Sistema Sismo (1H 30MIN)
```
Paso 4.1: Prefab Mesa
  ☐ Crea Cube plano → Table
  ☐ Crea 4 Cubes pequeños bajo él (patas)
  ☐ Arrastra Table a Assets/Prefab/
  ☐ Borra de Hierarchy
  ✅ VALIDACIÓN: Table.prefab existe

Paso 4.2: Prefab Debris
  ☐ Crea Cube pequeño → Debris
  ☐ Add Rigidbody (Dynamic)
  ☐ Arrastra a Assets/Prefab/
  ☐ Borra de Hierarchy
  ✅ VALIDACIÓN: Debris.prefab existe

Paso 4.3: EarthquakeManager.cs
  ☐ Crea script EarthquakeManager.cs
  ☐ Copia contenido completo
  ☐ Create Empty → EarthquakeManager
  ☐ Add Component → EarthquakeManager
  ☐ Arrastra Main Camera, Debris prefab, Table prefab
  ✅ VALIDACIÓN: Script está asignado y campos llenos

**PAUSA: Presiona PLAY**
☐ Presionas Siguiente en diálogos
☐ Cámara tiembla
☐ Debris cae
☐ Si todo OK: Continúa a FASE 5
```

---

### FASE 5: Resultados (45 MIN)
```
Paso 5.1: Canvas Resultados
  ☐ Crea Canvas → ResultsUI (DESACTIVADO)
  ☐ Crea Panel
  ☐ Crea 2 TextMeshPro (puntuación, feedback)
  ☐ Crea 2 Botones (Retry, Lobby)
  ✅ VALIDACIÓN: Canvas está desactivado

Paso 5.2: ResultsUIController.cs
  ☐ Crea script ResultsUIController.cs
  ☐ Copia contenido completo
  ☐ Selecciona ResultsUI
  ☐ Add Component → ResultsUIController
  ☐ Arrastra los elementos del Canvas
  ✅ VALIDACIÓN: Script está asignado
```

---

### FASE 6: Integración (1 HORA)
```
Paso 6.1: Actualizar NPCProfessor.cs
  ☐ Abre NPCProfessor.cs
  ☐ Reemplaza con la versión actualizada del documento
  ☐ Guarda
  ✅ VALIDACIÓN: Compila sin errores

Paso 6.2: Conectar Escenas
  ☐ File → Build Settings
  ☐ Agrega Lobby.unity (Scene 0)
  ☐ Agrega ClassroomScene.unity (Scene 1)
  ✅ VALIDACIÓN: 2 escenas en Build Settings

Paso 6.3: Dificultades
  ☐ Lee sección 6.3 de PASO_A_PASO_6HORAS.md
  ☐ Agrega métodos a FireGameManager.cs
  ✅ VALIDACIÓN: Script tiene método ContinueToMultipleFires()

Paso 6.4: Botones conectados
  ☐ Actualiza OnNextClicked() en NPCProfessor.cs
  ☐ Guarda
  ✅ VALIDACIÓN: Compila sin errores
```

---

### FASE 7: Testing (1 HORA)
```
Paso 7.1: Test Lobby
  ☐ Abre escena Lobby
  ☐ Presiona PLAY
  ☐ Ves 8 botones ✅
  ☐ Click en "Extintor A" ✅
  ☐ Carga ClassroomScene ✅
  ✅ VALIDACIÓN: Lobby funciona

Paso 7.2: Test Extintor
  ☐ En ClassroomScene presiona PLAY
  ☐ Presionas Siguiente 5 veces ✅
  ☐ Aparece 1 fuego ✅
  ☐ Si fuego desaparece: ✅
  ☐ Profesor habla de nuevo
  ☐ Presionas Siguiente
  ☐ Aparecen múltiples fuegos ✅
  ☐ Si todos desaparecen: ✅
  ☐ Ves resultados ✅
  ✅ VALIDACIÓN: Todo funciona

Paso 7.3: Test Sismo
  ☐ En Lobby, click "Sismo B" ✅
  ☐ En ClassroomScene presiona PLAY
  ☐ Presionas Siguiente 5 veces ✅
  ☐ Cámara tiembla ✅
  ☐ Debris cae ✅
  ☐ Duración 25 segundos ✅
  ☐ Temblor para ✅
  ☐ Profesor habla evacuación
  ☐ Presionas Siguiente
  ☐ Ves resultados ✅
  ✅ VALIDACIÓN: Todo funciona
```

---

## Post-Implementación (5 MINUTOS)

```
☐ 1. Presionas PLAY en Lobby
☐ 2. Seleccionas "Extintor C"
☐ 3. Completas el juego
☐ 4. Presionas "Reintentar"
☐ 5. Vuelves a jugar
☐ 6. Presionas "Volver a Lobby"
☐ 7. Vuelves a Lobby
☐ 8. Seleccionas "Sismo Random"
☐ 9. Completas el juego
☐ 10. Todo funciona ✅
```

---

## Validación Final

```
☐ Lobby tiene 8 botones funcionales
☐ Cada botón carga la escena correcta
☐ Profesor habla en cada escena
☐ Extintor: 1 fuego + múltiples fuegos
☐ Sismo: temblor + evacuación
☐ Resultados se muestran con puntuación
☐ "Reintentar" funciona
☐ "Volver a Lobby" funciona
☐ Console tiene 0 errores rojos
☐ Todo integrado y funcional ✅
```

---

## Si Algo Falla

```
ERROR EN CONSOLE:
├─ Lee el mensaje completo
├─ Busca el nombre del script en PASO_A_PASO_6HORAS.md
├─ Verifica que lo copiaste EXACTAMENTE
├─ Verifica que lo asignaste al objeto correcto
└─ Intenta de nuevo

BOTÓN NO RESPONDE:
├─ Verifica que el script está asignado (Add Component)
├─ Verifica que el script se compila sin errores
├─ Verifica que el onClick está conectado
└─ Prueba de nuevo

ESCENA NO CARGA:
├─ Verifica que está en Build Settings
├─ Verifica que el nombre es exacto
├─ Mira el console para errores
└─ Prueba de nuevo
```

---

## 🎉 Cuando TODO Funciona

```
✅ Proyecto completo
✅ Todos los cursos funcionan
✅ Sistema de puntuación activo
✅ Reintentos funcionan
✅ Volver a Lobby funciona
✅ 0 errores en Console

CELEBRA! 🎊

Tu proyecto VR educativo está listo para usar.
```

---

## 📞 Ayuda Rápida

| Problema | Solución |
|----------|----------|
| "Script not found" | Verifica que el .cs está en Assets/Scripts/ |
| Canvas no aparece | Verifica que está activo (checkbox ON) |
| Botón no responde | Verifica que agregaste onClick listener en código |
| Escena no carga | Verifica que SceneManager.LoadScene tiene nombre exacto |
| Fuego no desaparece | Verifica que FireBehavior tiene currentIntensity público |
| Nada se ve | Verifica que Canvas está en ScreenSpace Overlay |

---

## 🚀 Comienza YA

```
AHORA MISMO:
1. Abre: COMIENZA_AQUI_AHORA.md
2. Lee 2 minutos
3. Vuelve aquí
4. Comienza FASE 1

EN 6 HORAS:
¡Tu proyecto funciona!
```

**¡VAMOS! 🚀**

