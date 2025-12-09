# 📖 ÍNDICE RÁPIDO: ¿QUÉ DOCUMENTO LEER?

## 🎯 SEGÚN LO QUE NECESITES

### "El fuego no aparece cuando presiono Continuar"
→ Lee: `VERIFICACION_FIREGAMEMANAGER.md`
→ Troubleshooting: Línea ~200

### "Quiero crear una lección de sismo RÁPIDO"
→ Lee: `QUICKSTART_EARTHQUAKE_30MIN.md`
→ Tiempo: 30 minutos exactos

### "Quiero entender TODO sobre sismos"
→ Lee: `GUIA_COMPLETA_CURSO_SISMOS.md`
→ Incluye: Explicaciones completas, troubleshooting, configuración

### "¿Qué se hizo en este mensaje?"
→ Lee: `RESUMEN_FINAL_COMPLETO.md`
→ Resumen ejecutivo de cambios y próximos pasos

### "¿En qué estado está el proyecto?"
→ Lee: `00_RESUMEN_ESTADO_ACTUAL.md`
→ Estado de cada archivo, prioridades, tiempo estimado

---

## 📚 DOCUMENTACIÓN COMPLETA

| Documento | Propósito | Largo | Leer Si |
|-----------|-----------|-------|---------|
| `RESUMEN_FINAL_COMPLETO.md` | Resumen de lo hecho en este mensaje | 5 min | Primero |
| `QUICKSTART_EARTHQUAKE_30MIN.md` | Crear lección de sismo en 30 min | 10 min | Prisa |
| `GUIA_COMPLETA_CURSO_SISMOS.md` | Guía detallada de sismos | 20 min | Detalles |
| `VERIFICACION_FIREGAMEMANAGER.md` | Testing y debugging de extintor | 15 min | Problemas |
| `00_RESUMEN_ESTADO_ACTUAL.md` | Estado actual y prioridades | 10 min | Planning |

---

## 🚀 FLUJO RECOMENDADO

1. **Lee primero**: `RESUMEN_FINAL_COMPLETO.md` (5 min)
   → Entiende qué se hizo

2. **Verifica**: `VERIFICACION_FIREGAMEMANAGER.md` (15 min)
   → Asegúrate de que extintor funciona

3. **Crea**: `QUICKSTART_EARTHQUAKE_30MIN.md` (30 min × 3 = 90 min)
   → Crea las 3 lecciones de sismo

4. **Configura**: Botones en Lobby (10 min)
   → Agrega SimpleLobbyLoader a cada botón

5. **Testea**: End-to-end (15 min)
   → Verifica todo funciona

---

## 🔍 BÚSQUEDA POR PROBLEMA

### "No aparece fuego"
Documentos:
- `VERIFICACION_FIREGAMEMANAGER.md` → TEST 2
- `GUIA_COMPLETA_CURSO_SISMOS.md` → Troubleshooting

### "No aparecen escombros"
Documentos:
- `GUIA_COMPLETA_CURSO_SISMOS.md` → Troubleshooting
- `QUICKSTART_EARTHQUAKE_30MIN.md` → "Si falla"

### "Los botones no funcionan"
Documentos:
- `00_RESUMEN_ESTADO_ACTUAL.md` → FASE 3
- Script: `SimpleLobbyLoader.cs`

### "El juego se cuelga"
Documentos:
- `VERIFICACION_FIREGAMEMANAGER.md` → TEST 5
- `00_RESUMEN_ESTADO_ACTUAL.md` → Debugging

### "¿Cuánto tiempo me lleva?"
Documentos:
- `00_RESUMEN_ESTADO_ACTUAL.md` → "Tiempo total estimado"
- `QUICKSTART_EARTHQUAKE_30MIN.md` → 30 minutos

---

## 💻 POR SI NECESITAS CÓDIGO

### "Necesito el código de GameManager"
→ Archivo: `Assets/FireGameManager.cs`
→ También: `Assets/EarthquakeGameManager.cs`

### "Necesito SimpleLobbyLoader"
→ Archivo: `Assets/SimpleLobbyLoader.cs`
→ Métodos clave: `LoadCourse()`, `ReturnToLobby()`

### "Necesito profesor para sismos"
→ Archivo: `Assets/EarthquakeProfessor.cs`
→ Diálogos: Array de strings en `Start()`

### "Necesito spawnear escombros"
→ Archivo: `Assets/DebrisSpawner.cs`
→ Método: `StartSpawning()`, `StopSpawning()`

---

## 🎯 ACCIONES RÁPIDAS

### "Verificar compilación"
```
Ctrl+Shift+B (en Unity, o File → Build)
Debería mostrar: 0 errores
```

### "Ver logs de Debug"
```
Window → General → Console
Busca [FireGameManager], [EarthquakeGameManager], etc.
```

### "Duplicar escena"
```
Project → Scenes
Right-click en escena → Duplicate
```

### "Crear prefab"
```
Hierarchy: Right-click en objeto → Drag a Assets/
```

---

## 📋 CHECKLIST FINAL

Antes de terminar, verifica:

- [ ] Leí `RESUMEN_FINAL_COMPLETO.md`
- [ ] Probé FireGameManager (debería no colgarse)
- [ ] Creé EarthquakeLesson1 (usando QUICKSTART)
- [ ] Creé EarthquakeLesson2 y 3
- [ ] Configuré botones en Lobby
- [ ] Verifiqué que todo funciona end-to-end
- [ ] Guardé todos los cambios (Ctrl+S)
- [ ] Probé play para verifiear

---

## 🆘 SOPORTE

**Si algo no funciona:**
1. Busca el problema en esta tabla
2. Lee el documento recomendado
3. Implementa la solución del troubleshooting
4. Si aún falla, mira Console ([nombre] = log relevante)

**Si no encuentras el problema:**
1. Copia el log exacto de Console
2. Busca en documentación si hay error similar
3. Verifica Inspector values

---

## ✨ NOTAS FINALES

- Todos los scripts tienen **0 errores de compilación** ✅
- Cada script tiene **logging completo** para debugging
- Documentación **paso-a-paso** para cada tarea
- Tiempo total: ~2 horas para proyecto completo

**¡Ahora a implementar! 🚀**

