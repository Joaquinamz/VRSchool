# ✅ CHECKLIST PARA SETUP EXITOSO

Usa este checklist mientras configuras las escenas.

---

## 📋 FASE 1: PREPARACIÓN (5 minutos)

- [ ] Abriste Unity
- [ ] Creaste proyecto (o abriste el existente)
- [ ] No tienes errores de compilación (Ctrl+R)
- [ ] Leíste QUICKSTART_5MIN.md

---

## 📋 FASE 2: CREAR FireExtinguisherLesson (15 minutos)

### Escena
- [ ] **File → New Scene → Basic (Built-in)**
- [ ] **File → Save As → FireExtinguisherLesson**
- [ ] Guardar en Assets/

### GameObjects Básicos
- [ ] Eliminar Main Camera
- [ ] Crear Ground (Plane, scale 5,1,5)
- [ ] Crear Profesor (Empty, position 0,1.5,2)
- [ ] Crear ExtintorObject (Cube, scale 0.1,0.3,0.1)

### Fuegos
- [ ] Crear Fire_1 (Particle System, position 2,0.5,0)
- [ ] Duplicar 4 veces más (Fire_2 a Fire_5)
- [ ] Colocar en posiciones diferentes

### Contenedor
- [ ] Crear Fires (Empty)
- [ ] Mover todos Fire_X dentro de Fires

### Scripts
- [ ] Asignar InstructorController al Profesor
- [ ] Asignar WorkingExtinguisher al ExtintorObject
- [ ] Asignar FireBehavior a cada Fire_X
- [ ] Asignar FireGameManager al contenedor Fires

### UI
- [ ] Crear Canvas con Panel
- [ ] Crear TextMeshPro para diálogos
- [ ] Crear Botón "Siguiente"
- [ ] Crear Textos para Timer, Score, Fire Count

### Resultados
- [ ] Crear Canvas para resultados
- [ ] Crear Panel de resultados
- [ ] Crear Textos: Título, Score, Time, Stats
- [ ] Crear Botones: Retry, Lobby
- [ ] Asignar ResultsScreen.cs

---

## 📋 FASE 3: CREAR EarthquakeLesson (15 minutos)

Repite los pasos de FireExtinguisherLesson pero:

### En lugar de Extintor
- [ ] Crear 3-4 Mesas (Cubes pequeños)

### En lugar de Fuegos
- [ ] Crear Escombros (Cubes, agregar Rigidbody)
- [ ] Asignar EarthquakeSimulator.cs

### GameManager
- [ ] Crear EarthquakeManager (Empty)
- [ ] Asignar EarthquakeGameManager.cs

### Estudiantes
- [ ] Crear Student_1 (Cube, scale 0.3,1,0.3)
- [ ] Asignar StudentAI.cs
- [ ] Asignar NavMeshAgent
- [ ] Duplicar 3-4 veces más

---

## 📋 FASE 4: CONFIGURAR LOBBY (10 minutos)

### Abrir LobbyVR.unity
- [ ] File → Open Scene → LobbyVR

### CourseManager
- [ ] Crear CourseManager (Empty)
- [ ] Asignar CourseManager.cs
- [ ] ¡Listo! (Singleton se encarga del resto)

### UI Lobby
- [ ] Crear LobbyUI (Empty)
- [ ] Asignar LobbyManager.cs
- [ ] Crear Canvas para módulos
- [ ] Crear Botones: Extintor, Sismo
- [ ] Crear Canvas para dificultad
- [ ] Crear Botones: A, B, C, Random
- [ ] Crear Botón Confirmar

### Referencias
- [ ] En LobbyManager Inspector, asignar todos los botones

---

## 📋 FASE 5: BUILD SETTINGS (5 minutos)

### Agregar escenas
- [ ] **File → Build Settings**
- [ ] Haz clic en **Add Open Scenes** 3 veces
- [ ] O arrastra manualmente:
  - [ ] LobbyVR (Index 0)
  - [ ] FireExtinguisherLesson (Index 1)
  - [ ] EarthquakeLesson (Index 2)

---

## 📋 FASE 6: TESTING (10 minutos)

### Test Lobby
- [ ] Abre LobbyVR.unity
- [ ] Presiona **Play**
- [ ] ¿Ves 2 botones (Extintor, Sismo)?
- [ ] Haz clic en "Extintor"
- [ ] ¿Aparece panel de dificultad?
- [ ] Selecciona "Fácil"
- [ ] ¿Se carga FireExtinguisherLesson?

### Test Extintor
- [ ] Ya debe estar cargada
- [ ] ¿Ves diálogos del profesor?
- [ ] Presiona "Siguiente"
- [ ] ¿Empiezan los fuegos?
- [ ] ¿Puedes agarrar extintor?
- [ ] ¿Se apagan los fuegos?
- [ ] ¿Ves pantalla de resultados?

### Test Resultados
- [ ] ¿Botón "Reintentar" funciona?
- [ ] ¿Botón "Volver a Lobby" funciona?
- [ ] ¿Vuelves al Lobby correctamente?

---

## 🆘 SI ALGO NO FUNCIONA

### Error: "Scene not found"
- [ ] Verifica Build Settings
- [ ] Verifica nombre exacto de escena

### Error: "Component missing"
- [ ] Verifica que asignaste todos los scripts
- [ ] Verifica console para detalles

### Error: "Reference not assigned"
- [ ] En Inspector, asigna referencias arrastrando

### Botones no responden
- [ ] Selecciona botón → Button component
- [ ] Haz clic en **+** en On Click ()
- [ ] Arrastra GameObject
- [ ] Selecciona método correcto

**Si aún no funciona, lee**: TROUBLESHOOTING_DETALLADO.md

---

## ✅ CUANDO TODO FUNCIONA

- [ ] ✅ Play en Lobby
- [ ] ✅ Seleccionar módulo
- [ ] ✅ Seleccionar dificultad
- [ ] ✅ Carga escena
- [ ] ✅ Juego funciona
- [ ] ✅ Resultados muestran
- [ ] ✅ Volver a Lobby funciona

**¡PROYECTO COMPLETADO!**

---

## 🎉 BONUS: FINE-TUNING (Opcional)

Cuando todo funciona, puedes:

- [ ] Cambiar parámetros A/B/C en FireGameManager
- [ ] Cambiar parámetros A/B/C en EarthquakeGameManager
- [ ] Agregar modelos 3D
- [ ] Cambiar texturas y colores
- [ ] Agregar sonidos
- [ ] Crear prefabs para reutilizar

---

## 📞 SOPORTE

**Si tienes dudas**:
1. Abre QUICKSTART_5MIN.md (rápido)
2. Lee SETUP_ESCENA_SIMPLE.md (detallado)
3. Busca en TROUBLESHOOTING_DETALLADO.md (errores específicos)

---

*Checklist para Setup*
*VR Educativo v2.0*
*28 de Noviembre, 2025*
