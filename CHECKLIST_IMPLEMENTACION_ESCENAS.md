# ✅ CHECKLIST DE VERIFICACIÓN E IMPLEMENTACIÓN

**Versión**: 1.0  
**Última actualización**: Diciembre 2025  

---

## PRE-IMPLEMENTACIÓN (Verificación)

### Scripts Verificados
- [ ] ✅ SceneManagerVR.cs existe en Assets/
- [ ] ✅ SceneLoaderButton.cs existe en Assets/
- [ ] ✅ LobbyManager.cs actualizado en Assets/
- [ ] ✅ 0 errores de compilación
- [ ] ✅ Todos los scripts importan correctamente

### Documentación Verificada
- [ ] ✅ INICIO_RAPIDO_ESCENAS.md creado
- [ ] ✅ GUIA_CARGA_DESCARGA_ESCENAS.md creado
- [ ] ✅ Otras 7 guías creadas
- [ ] ✅ Total: 9 archivos de documentación

---

## IMPLEMENTACIÓN (Pasos)

### PASO 1: Crear SceneManager en Lobby (1 minuto)
```
[ ] Abre escena "Lobby" en Unity
[ ] Click derecho en Hierarchy → 3D Object > Empty
[ ] Renombra a: "SceneManager"
[ ] Add Component → SceneManagerVR
[ ] Verifica que aparece en Inspector
[ ] ✅ Listo (auto-configurado)
```

Inspector debería verse así:
```
SceneManager (GameObject)
├─ Transform
├─ SceneManagerVR
│  ├─ Lobby Scene Name: "Lobby"
│  ├─ Transition Delay: 0.5
│  └─ Debug Mode: true
└─ (automáticamente marcado como DontDestroyOnLoad)
```

---

### PASO 2: Configurar Botones en Lobby (4 minutos)

**Botón 1: Extintor Lección 1**
```
[ ] Selecciona "btnExtintorA" en Hierarchy
[ ] Add Component → SceneLoaderButton
[ ] Inspector > SceneLoaderButton:
    [ ] Load Mode = Replace
    [ ] Target Scene Name = "FireExtinguisherLesson1"
[ ] Inspector > Button:
    [ ] On Click () → + (agregar evento)
    [ ] Arrastra el mismo GameObject del botón
    [ ] Dropdown: SceneLoaderButton > OnButtonPressed()
[ ] ✅ Botón configurado
```

**Botón 2: Extintor Lección 2**
```
[ ] Selecciona "btnExtintorB"
[ ] Add Component → SceneLoaderButton
[ ] Load Mode = Replace
[ ] Target Scene Name = "FireExtinguisherLesson2"
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

**Botón 3: Extintor Lección 3**
```
[ ] Selecciona "btnExtintorC"
[ ] Add Component → SceneLoaderButton
[ ] Load Mode = Replace
[ ] Target Scene Name = "FireExtinguisherLesson3"
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

**Botón 4: Sismo Lección 1**
```
[ ] Selecciona "btnSismoA"
[ ] Add Component → SceneLoaderButton
[ ] Load Mode = Replace
[ ] Target Scene Name = "EarthQuakeLesson1"
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

**Botón 5: Sismo Lección 2**
```
[ ] Selecciona "btnSismoB"
[ ] Add Component → SceneLoaderButton
[ ] Load Mode = Replace
[ ] Target Scene Name = "EarthQuakeLesson2"
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

**Botón 6: Sismo Lección 3**
```
[ ] Selecciona "btnSismoC"
[ ] Add Component → SceneLoaderButton
[ ] Load Mode = Replace
[ ] Target Scene Name = "EarthQuakeLesson3"
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

---

### PASO 3: Configurar Botón "Volver" en Cursos (3 minutos)

**FireExtinguisherLesson1 → Botón Volver**
```
[ ] Abre escena "FireExtinguisherLesson1"
[ ] Identifica el botón "Volver a Lobby"
[ ] Add Component → SceneLoaderButton
[ ] Inspector > SceneLoaderButton:
    [ ] Load Mode = ReturnLobby
    [ ] Target Scene Name = (dejalo vacío)
[ ] Inspector > Button:
    [ ] On Click () → +
    [ ] Arrastra el botón
    [ ] Dropdown: SceneLoaderButton > OnButtonPressed()
[ ] ✅ Botón configurado
```

**FireExtinguisherLesson2 → Botón Volver**
```
[ ] Abre escena "FireExtinguisherLesson2"
[ ] Add Component → SceneLoaderButton (ReturnLobby)
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

**FireExtinguisherLesson3 → Botón Volver**
```
[ ] Abre escena "FireExtinguisherLesson3"
[ ] Add Component → SceneLoaderButton (ReturnLobby)
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

**EarthQuakeLesson1 → Botón Volver**
```
[ ] Abre escena "EarthQuakeLesson1"
[ ] Add Component → SceneLoaderButton (ReturnLobby)
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

**EarthQuakeLesson2 → Botón Volver**
```
[ ] Abre escena "EarthQuakeLesson2"
[ ] Add Component → SceneLoaderButton (ReturnLobby)
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

**EarthQuakeLesson3 → Botón Volver**
```
[ ] Abre escena "EarthQuakeLesson3"
[ ] Add Component → SceneLoaderButton (ReturnLobby)
[ ] Button On Click → OnButtonPressed()
[ ] ✅ Botón configurado
```

---

### PASO 4: Configurar Build Settings (1 minuto)

```
[ ] Ve a File > Build Settings
[ ] Asegúrate que aparecen en "Scenes In Build":
    [ ] Index 0: Lobby
    [ ] Index 1: FireExtinguisherLesson1
    [ ] Index 2: FireExtinguisherLesson2
    [ ] Index 3: FireExtinguisherLesson3
    [ ] Index 4: EarthQuakeLesson1
    [ ] Index 5: EarthQuakeLesson2
    [ ] Index 6: EarthQuakeLesson3
[ ] ✅ Build Settings configurado
```

---

## TESTING (Validación)

### Test 1: Carga desde Lobby
```
[ ] Presiona Play ▶
[ ] Lobby carga correctamente
[ ] Presiona botón "Extintor Lección 1"
[ ] Pantalla se carga (transición ~1s)
[ ] FireExtinguisherLesson1 aparece
[ ] Console muestra: [SceneManagerVR] ✓ Escena cargada
[ ] ✅ Test PASÓ
```

### Test 2: Retorno a Lobby
```
[ ] Estás en FireExtinguisherLesson1
[ ] Presiona botón "Volver a Lobby"
[ ] Pantalla transiciona (~1s)
[ ] Vuelves a ver Lobby con los 6 botones
[ ] Console muestra: [SceneManagerVR] ✓ Escena descargada
[ ] ✅ Test PASÓ
```

### Test 3: Cambio de Curso
```
[ ] Estás en Lobby
[ ] Presiona botón "Sismo Lección 1"
[ ] Carga EarthQuakeLesson1
[ ] Presiona "Volver"
[ ] Vuelves a Lobby
[ ] Presiona "Extintor Lección 2" (diferente)
[ ] Carga FireExtinguisherLesson2
[ ] ✅ Test PASÓ
```

### Test 4: Repetir Múltiples Veces
```
[ ] Presiona Extintor 1 → Funciona
[ ] Vuelves → Funciona
[ ] Presiona Sismo 1 → Funciona
[ ] Vuelves → Funciona
[ ] Repite 5 veces → Sin errores
[ ] Console limpia (sin excepciones)
[ ] ✅ Test PASÓ
```

### Test 5: Build Settings Validation
```
[ ] Verifica que cada nombre de escena coincida exactamente
    [ ] "Lobby" (case-sensitive)
    [ ] "FireExtinguisherLesson1" (exacto)
    [ ] "FireExtinguisherLesson2" (exacto)
    [ ] Etc.
[ ] ✅ Validación PASÓ
```

---

## POST-IMPLEMENTACIÓN (Verificación Final)

### Compilation
```
[ ] 0 errores en Console
[ ] 0 warnings relacionados a SceneManager
[ ] Todos los scripts se compilan correctamente
```

### Functionality
```
[ ] Lobby → Curso funciona (6 botones)
[ ] Curso → Lobby funciona (botón Volver)
[ ] Sin lag significativo (<2s transición)
[ ] SceneManager persiste entre escenas
[ ] Logs [SceneManagerVR] aparecen en Console
```

### Documentation
```
[ ] Leíste INICIO_RAPIDO_ESCENAS.md
[ ] Tienes acceso a las 8 guías
[ ] Sabes dónde buscar información
[ ] Entiendes el flujo básico
```

---

## TROUBLESHOOTING QUICK REFERENCE

### Problema: "Escena no carga"
```
Checklist:
[ ] ¿Escena en Build Settings?
[ ] ¿Nombre de escena exacto (case-sensitive)?
[ ] ¿SceneManagerVR en Lobby?
[ ] ¿Target Scene Name completo en Inspector?
```

### Problema: "Botón no responde"
```
Checklist:
[ ] ¿SceneLoaderButton agregado?
[ ] ¿On Click configurado?
[ ] ¿OnButtonPressed() seleccionado?
[ ] ¿Botón está interactible (Button component)?
```

### Problema: "Se ve lag"
```
Checklist:
[ ] Ajusta Transition Delay (más alto = más tiempo)
[ ] Reduce objetos en escena (especialmente scripts)
[ ] Verifica Performance (Profiler)
```

### Problema: "SceneManager desaparece"
```
Checklist:
[ ] Es NORMAL - está en DontDestroyOnLoad
[ ] Debería aparecer en siguiente escena
[ ] Busca en Hierarchy cuando cargues nueva escena
```

---

## COLUMNA DE VERIFICACIÓN FINAL

```
┌─────────────────────────────────────┬─────┐
│ Elemento                             │ ✅  │
├─────────────────────────────────────┼─────┤
│ SceneManagerVR.cs compilado         │ [ ] │
│ SceneLoaderButton.cs compilado      │ [ ] │
│ LobbyManager.cs actualizado         │ [ ] │
│ SceneManager en Lobby creado        │ [ ] │
│ 6 botones Lobby configurados        │ [ ] │
│ 6 botones Volver en Cursos          │ [ ] │
│ Build Settings completo             │ [ ] │
│ Test 1: Carga desde Lobby           │ [ ] │
│ Test 2: Retorno a Lobby             │ [ ] │
│ Test 3: Cambio de Curso             │ [ ] │
│ Test 4: Múltiples transiciones      │ [ ] │
│ Test 5: Build Settings              │ [ ] │
│ Documentación leída                 │ [ ] │
│ Sin errores en Console              │ [ ] │
│ Sistema funcionando                 │ [ ] │
└─────────────────────────────────────┴─────┘
```

---

## ¿CUÁNDO ESTARÁ LISTO?

```
Tiempo estimado:
  Paso 1 (SceneManager):        1 minuto
  Paso 2 (6 Botones Lobby):     4 minutos
  Paso 3 (6 Botones Volver):    3 minutos
  Paso 4 (Build Settings):      1 minuto
  Testing (5 tests):            3 minutos
  ────────────────────────────────────
  TOTAL:                        ~15 minutos

Esto asume que:
- Tienes Unity abierto
- Las escenas ya existen
- Los botones ya están creados en Canvas
```

---

## ¿QUÉ HACER SI ALGO FALLA?

### Opción 1: Revisar Documentación
```
Consulta:
  • GUIA_CARGA_DESCARGA_ESCENAS.md (sección Troubleshooting)
  • DIAGRAMA_ESCENAS_VISUAL.md (sección Troubleshooting)
  • EJEMPLOS_CODIGO_ESCENAS.md (sección Errores Comunes)
```

### Opción 2: Verificar Logs
```
Abre Console en Unity:
  • Busca [SceneManagerVR] para ver qué está pasando
  • Si ves error, copia el mensaje y búscalo en Troubleshooting
```

### Opción 3: Verificar Configuración
```
SceneManager > Inspector > SceneManagerVR:
  • Debug Mode = true (para ver más logs)
  • Transition Delay = 0.5 (si hay lag, aumenta)
```

---

## SIGUIENTE PASO

```
Una vez completado este checklist:

[ ] ✅ TODO ESTÁ LISTO

Ahora puedes:
  ✓ Usar el sistema de escenas en producción
  ✓ Agregar nuevas escenas (repite Paso 2 o 3)
  ✓ Consultar documentación si necesitas cambios
```

---

**Versión**: 1.0  
**Última actualización**: Diciembre 2025  
**Tiempo total**: ~15 minutos  
**Dificultad**: ⭐ Muy Fácil  
