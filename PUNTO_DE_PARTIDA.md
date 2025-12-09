# 🎯 PUNTO DE PARTIDA: Plan de 6 Horas

## 📌 ANTES DE COMENZAR: LEER ESTO

Has recibido **2 documentos principales:**

1. **SOLUCION_ERROR_PREFAB.md** ← LEE PRIMERO (30 min)
   - Soluciona el error de "Missing Prefab" 
   - Limpia tu proyecto
   - Una vez hecho: cierra y reabre Unity

2. **PASO_A_PASO_6HORAS.md** ← LEE SEGUNDO (aplicar 6 horas)
   - Guía paso a paso en el editor
   - Copia exacta de scripts
   - Testing validaciones

---

## ⏱️ TIMELINE RECOMENDADO

```
ANTES (30 MINUTOS):
├─ Leer SOLUCION_ERROR_PREFAB.md
├─ Eliminar archivos problemáticos
├─ Limpiar Console
└─ Reiniciar Unity

DESPUÉS (6 HORAS):

BLOQUE 1: Setup Inicial (30 min)
├─ 1.1: GameManager.cs
├─ 1.2: Escena Lobby
└─ 1.3: LobbyManager.cs

BLOQUE 2: Classroom Scene (1h 30min)
├─ 2.1: Crear escena
├─ 2.2: Setup básico
├─ 2.3: NPC Profesor
└─ 2.4: NPCProfessor.cs

BLOQUE 3: Sistema Extintor (1h 30min)
├─ 3.1: Prefab Fire
├─ 3.2: FireBehavior.cs
├─ 3.3: FireGameManager.cs
└─ 3.4: UI de Gameplay

BLOQUE 4: Sistema Sismo (1h 30min)
├─ 4.1: Prefab Mesa
├─ 4.2: Prefab Debris
├─ 4.3: EarthquakeManager.cs
└─ 4.4: UI Terremoto

BLOQUE 5: Resultados (45 min)
├─ 5.1: Canvas Resultados
└─ 5.2: ResultsUIController.cs

BLOQUE 6: Integración (1 hora)
├─ 6.1: Actualizar NPCProfessor.cs
├─ 6.2: Conectar Escenas
├─ 6.3: Configurar Dificultades
└─ 6.4: Conectar Botones

BLOQUE 7: Testing (1 hora)
├─ 7.1: Testear Lobby
├─ 7.2: Testear Extintor
└─ 7.3: Testear Sismo

TOTAL: 6 HORAS
```

---

## 🎮 QUÉ PODRÁ HACER AL FINAL

✅ Entrar a Lobby y ver 8 botones (Extintor A/B/C/Random, Sismo A/B/C/Random)

✅ Click en cualquier botón → cargar ClassroomScene

✅ Ver profesor hablando y presionar "Siguiente" para pasar diálogos

✅ CURSO DE EXTINTOR:
   - Aparece 1 fuego (entrenar)
   - Apagarlo
   - Profesor felicita
   - Aparecen múltiples fuegos (minijuego)
   - Apagar todos
   - Ver puntuación y feedback

✅ CURSO DE SISMO:
   - Cámara tiembla durante 20-30 seg
   - Debris cae
   - Profesor da instrucciones
   - Ver resultados

✅ RESULTADOS:
   - Ver puntuación
   - Botón "Reintentar" → volver a juego
   - Botón "Volver a Lobby" → volver a selección

---

## ⚠️ REQUISITOS PREVIOS

Asegúrate de tener:

```
✅ Unity 2022+ instalado
✅ XR Interaction Toolkit instalado
   (Window → TextMesh Pro → Import TMP Resources)
✅ Proyecto VRDemo abierto
✅ Carpeta Assets/Scenes/ existente
✅ Carpeta Assets/Prefab/ existente
```

Si no existen las carpetas:
- Click derecho en Assets → Create → Folder
- Nombre: Scenes
- Click derecho en Assets → Create → Folder
- Nombre: Prefab

---

## 📚 ESTRUCTURA DE ARCHIVOS AL FINAL

```
Assets/
├─ Scripts/
│  ├─ GameManager.cs
│  ├─ LobbyManager.cs
│  ├─ NPCProfessor.cs
│  ├─ FireGameManager.cs
│  ├─ FireBehavior.cs
│  ├─ EarthquakeManager.cs
│  └─ ResultsUIController.cs
│
├─ Scenes/
│  ├─ Lobby.unity
│  └─ ClassroomScene.unity
│
└─ Prefab/
   ├─ Fire.prefab
   ├─ Debris.prefab
   ├─ Table.prefab
   └─ (ExtintorPrincipal.prefab - ya existe)
```

---

## 🚀 COMENZAR AHORA

### OPCIÓN A: Limpieza Rápida (Recomendado)

```
1. En Assets, DELETE: 1.unity
2. En Assets, DELETE: 1FireExtinguisherLesson.unity
3. Cierra Unity
4. Reabre Unity
5. Abre el documento: PASO_A_PASO_6HORAS.md
6. Comienza FASE 1, Paso 1.1
```

### OPCIÓN B: Limpieza Detallada

```
1. Lee SOLUCION_ERROR_PREFAB.md completamente
2. Ejecuta cada paso
3. Valida tu proyecto
4. Comienza PASO_A_PASO_6HORAS.md
```

---

## 🔥 TIPS DE ÉXITO

**Mientras implementas:**

1. **Después de cada paso, presiona PLAY (triángulo arriba)**
   - Verifica que no hay errores en Console (esquina abajo izq)
   - Si hay error: LEE EL MENSAJE, generalmente dice qué está mal

2. **Guarda constantemente (Ctrl+S)**
   - Cada vez que hagas cambios importantes

3. **Si algo falla:**
   - Mira el error completo en Console
   - Busca el nombre del script que falla
   - Verifica que está asignado correctamente en Inspector

4. **Mantén orden:**
   - Scripts en Assets/Scripts/
   - Escenas en Assets/Scenes/
   - Prefabs en Assets/Prefab/

5. **Duplica en caso de emergencia:**
   - Si un prefab se daña, bórralo
   - Crea uno nuevo basado en las instrucciones

---

## 📞 TROUBLESHOOTING RÁPIDO

| Problema | Solución |
|----------|----------|
| **Error: "Script not found"** | Verifica que el .cs está en Assets y que el nombre del script coincide |
| **Botón no responde** | Asegúrate de haber hecho drag del script al objeto en Hierarchy |
| **Fuego no desaparece** | Verifica que FireBehavior.cs tiene `currentIntensity` público |
| **Escena no carga** | Verifica Build Settings (File → Build Settings) tiene las 2 escenas |
| **NPC no habla** | Verifica que DialogueCanvas está activo y asignado en NPCProfessor.cs |

---

## 📖 DOCUMENTACIÓN COMPLETA

Para referencia avanzada (después de terminar):
- **ARQUITECTURA_COMPLETA_PROYECTO.md**: Explicación del sistema completo
- **QUICK_FIX_MODELOS_ROSADOS.md**: Si hay problemas con shaders

---

## ✨ PRÓXIMO PASO

**Abre ahora:** SOLUCION_ERROR_PREFAB.md

Sigue los primeros 30 minutos de limpieza y luego comienza con PASO_A_PASO_6HORAS.md

**¡Vamos! 🚀**

