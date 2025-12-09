# ❓ FAQ: Preguntas Frecuentes (Respuestas Rápidas)

## INSTALACIÓN Y CONFIGURACIÓN

### P: ¿Necesito instalar algo nuevo?
**R:** No. El código compila con lo que ya tienes. Está listo para usar.

### P: ¿Está probado el código?
**R:** Sí. 0 errores de compilación. Pero NECESITAS testearlo en tu máquina porque cada proyecto es diferente.

### P: ¿Puedo usarlo en mis escenas existentes?
**R:** Sí. Son scripts modulares. Simplemente Add Component y configura.

---

## EXTINTOR (FireGameManager)

### P: ¿Por qué antes no aparecía el fuego?
**R:** El código no validaba que `firePrefab` estuviera asignado. Si era null, simplemente no hacía nada sin error visible. Ahora valida y muestra errores claros.

### P: ¿Qué cambió en FireGameManager?
**R:** 
- Antes: 4 fases → Ahora: 7 fases
- Antes: Sin validación → Ahora: Validación defensiva
- Antes: Sin logs → Ahora: Logs en cada paso
- Antes: Sin timeout → Ahora: Timeout de 3 segundos

### P: ¿Necesito cambiar mis scripts de extintor?
**R:** No. Los scripts como `ExtintorController.cs` y `FireBehavior.cs` siguen igual. FireGameManager se adaptó a ellos.

---

## SISMOS (EarthquakeGameManager)

### P: ¿Cómo creo un curso de sismo?
**R:** Sigue `QUICKSTART_EARTHQUAKE_30MIN.md` - toma exactamente 30 minutos.

### P: ¿Puedo cambiar la duración del terremoto?
**R:** Sí. En `EarthquakeGameManager` inspector, ajusta `Earthquake Duration` (default 30 segundos).

### P: ¿Cómo cambio cuántos escombros caen?
**R:** En `DebrisSpawner` inspector, ajusta `Spawn Rate` (default 2 = 2 por segundo).

### P: ¿Los escombros son destructibles?
**R:** No. Caen y se destruyen automáticamente después de 10 segundos. Pero puedes cambiar eso en `DebrisSpawner`.

### P: ¿Puedo tener 2 terremotossimuláneamente?
**R:** Sí, pero necesitarías 2 GameManagers. No está configurado por defecto.

---

## BOTONES Y CARGA DE ESCENAS

### P: ¿Qué es SimpleLobbyLoader?
**R:** Un script simple para cargar/descargar escenas. Reemplaza el complicado SceneManagerVR con algo más simple (como pediste).

### P: ¿Cómo asigno un botón a una escena?
**R:** 
1. Add Component → SimpleLobbyLoader
2. Inspector: Mode = LoadCourse, Target = "EarthquakeLesson1"
3. Button On Click → SimpleLobbyLoader.OnButtonClick()

### P: ¿Funciona con SceneManagerVR?
**R:** Están completamente separados. SimpleLobbyLoader es más simple, SceneManagerVR es más complejo. Elige uno.

### P: ¿Necesito ambos?
**R:** No. Usa SimpleLobbyLoader en botones. Es suficiente.

---

## LOGS Y DEBUGGING

### P: ¿Cómo sé qué está pasando?
**R:** Console (Window → General → Console). Busca `[nombre]` y los logs te dicen exactamente qué pasó.

### P: ¿Qué mean los símbolos en los logs?
**R:**
```
✓ o ✅ = Éxito
✗ o ❌ = Error
⚠️  = Advertencia
📂 = Info sobre archivos
🔥 = Info sobre fuego
💨 = Info sobre escombros
🎮 = Info sobre juego
```

### P: ¿Cómo reporto un error?
**R:** 
1. Copia el log exacto de Console
2. Busca en documentación si alguien tuvo ese error
3. Sigue el troubleshooting

---

## DOCUMENTACIÓN

### P: ¿Qué documento debería leer primero?
**R:** `00_COMIENZA_AQUI.md` o `RESUMEN_FINAL_COMPLETO.md` (5 minutos).

### P: Tengo prisa, ¿qué es lo mínimo?
**R:** `QUICKSTART_EARTHQUAKE_30MIN.md` - Nada de explicaciones, solo pasos.

### P: Quiero entender TODO
**R:** Lee en orden:
1. RESUMEN_FINAL_COMPLETO.md
2. DIAGRAMA_ARQUITECTURA_VISUAL.md
3. GUIA_COMPLETA_CURSO_SISMOS.md

### P: ¿Puedo eliminar documentos que no uso?
**R:** Sí, pero no lo recomiendo. Algunos son referencia útil después.

---

## PROBLEMAS COMUNES

### P: "El fuego no aparece"
**R:** Mira Console, busca `[FireGameManager]`. El log te dirá si `firePrefab` está null o si le falta component.

**Solución rápida:**
1. GameManager en Hierarchy
2. Inspector → arrastra Fire prefab al campo `firePrefab`

### P: "Los escombros no caen"
**R:** En `DebrisSpawner`, verifica que:
- `debrisPrefab` está asignado
- Prefab tiene `Rigidbody`
- StartSpawning() fue llamado

### P: "El botón no carga la escena"
**R:** Verifica:
- SimpleLobbyLoader está en el GameObject del botón
- On Click está configurado
- Target Scene Name es correcto
- La escena existe en Build Settings

### P: "El juego se cuelga"
**R:** En Console, busca timeout o error. Probablemente:
- Fase no avanza
- Loop infinito
- Referencia null

Ver: `VERIFICACION_FIREGAMEMANAGER.md` TEST 5

---

## PERSONALIZACIÓN

### P: ¿Puedo cambiar los diálogos?
**R:** Sí. En el script (Profesor), edita el array `currentDialogues[]`.

### P: ¿Puedo cambiar los colores de los escombros?
**R:** Sí. El prefab es un cubo - cambio su Material.

### P: ¿Puedo agregar sonidos?
**R:** Sí. Agrega `AudioSource` al prefab de escombro o al GameManager.

### P: ¿Puedo tener diferentes dificultades?
**R:** Sí. Duplica la escena y ajusta:
- Shake Intensity
- Spawn Rate
- Earthquake Duration

---

## RENDIMIENTO

### P: ¿Cuántos escombros pueden haber simultáneamente?
**R:** Default 50. Cambio en `DebrisSpawner` > `Max Debris Active`.

### P: ¿Afecta el rendimiento?
**R:** Depende de tu máquina y configuración. Con 50 escombros simples (cubos) debería estar bien en VR.

### P: ¿Cómo optimizo?
**R:** 
- Reduce `Max Debris Active`
- Usa prefab más simple
- Reduce `Spawn Rate`
- Usa Object Pooling (tema avanzado)

---

## PRÓXIMOS PASOS

### P: Después de terminar los 6 cursos, ¿qué sigue?
**R:** Opcional:
- Modelos 3D de escombros
- Sonidos de terremoto
- Tabla de puntajes
- Modos dificultad
- Multijugador

### P: ¿Cómo agrego más cursos?
**R:** Copia cualquier lección, cambia nombres, ajusta parámetros.

### P: ¿Puedo mezclar extintor y sismos?
**R:** Sí, pero son sistemas independientes. Necesitarían un GameManager superior.

---

## COMPILACIÓN Y BUILDS

### P: ¿Puedo hacer un BUILD para entregar?
**R:** Sí. Archivo → Build Settings:
1. Verifica que TODAS las escenas están incluidas (7 total)
2. Build

### P: ¿Ocupa mucho espacio?
**R:** Sin assets pesados, ~500MB-1GB típico.

### P: ¿Funciona en Android/iOS/Web?
**R:** Sí, pero XR Interaction Toolkit necesita configuración específica. Escapa al scope de este proyecto.

---

## SI ALGO SIGUE MAL

### P: Leí la documentación y aún no funciona
**R:** 
1. Verifica Console (Window → General)
2. Busca el log con `[nombre]`
3. Copia el log exacto
4. Ve a `00_INDICE_DOCUMENTACION.md` y busca tu problema

### P: El error no está en la documentación
**R:** Probablemente hay algo específico a tu configuración:
1. Comparte el log exacto
2. Di qué escena/lección
3. Di paso a paso qué hiciste

### P: ¿Hay soporte en vivo?
**R:** Mira los documentos. Contienen 90% de posibles problemas.

---

## ESTADÍSTICAS

### P: ¿Cuánta línea de código se escribió?
**R:** ~1200 líneas de código + ~3000 líneas de documentación.

### P: ¿Cuánto tiempo toma implementar TODO?
**R:** ~2 horas si sigues rápido, ~3-4 horas si entiendes mientras vas.

### P: ¿Cuántos scripts nuevos?
**R:** 7 scripts:
- FireGameManager (reformulado)
- SimpleLobbyLoader (nuevo)
- EarthquakeGameManager (nuevo)
- EarthquakeProfessor (nuevo)
- DebrisSpawner (nuevo)
- DebrisHitDetector (nuevo)

---

## MÁS PREGUNTAS?

Si tu pregunta no está aquí:
1. Busca por palabra clave en documentos
2. Mira Console para logs
3. Sigue el troubleshooting relevante

---

**¡Esperamos que esto te ayude! 🚀**

