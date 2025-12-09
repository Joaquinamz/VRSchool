# Ajustes Finales del Extintor - Guía de Configuración

## Resumen de Cambios

Se han realizado 4 ajustes críticos al sistema del extintor:

### 1. ✅ No dispara al iniciar (isFiring = false en Start)
- El extintor ahora comienza **SIN disparar** automáticamente
- Solo dispara cuando presiones la boquilla correctamente
- Se detiene al soltar el cuerpo o la boquilla

### 2. ✅ Físicas realistas (Rigidbody)
- Se agregó **Rigidbody automático** si no existe
- Configuración:
  - `Mass: 2f` (peso realista)
  - `Drag: 0.5f` (fricción)
  - `Use Gravity: true` (cae naturalmente)
  - `Is Kinematic: false` (interactúa con física)
  - `Rotation Constraints: Freeze Rotation` (no rota)
- El extintor ahora se cae como un objeto real

### 3. ✅ Respawn automático si cae fuera del mapa
- Si el extintor se aleja **más de 30 unidades** de su posición inicial
- Se respawnea automáticamente en su posición de inicio
- Velocidad se resetea a cero (no conserva momentum)
- Verificación cada frame en `Update()`

**Configuración personalizable:**
```
En Inspector > ExtintorController > Respawn Distance = 30
```
- Aumentar si el mapa es muy grande
- Disminuir si quieres que respawnee más cerca

### 4. ✅ Tiempo mostrado en Canvas Results
- Se agregó campo `timeText` en FireMinigameManager
- Ahora muestra: `"Tiempo: XX.Xs"` en la pantalla final
- Tomado del mismo contador que el minijuego

---

## Configuración en Unity

### ExtintorController (Cambios Automáticos)
No requiere configuración manual - se auto-configura en `Start()`:
```
✓ Rigidbody se crea si no existe
✓ isFiring inicia en false
✓ Respawn Position se guarda automáticamente
```

Si tu extintor YA tiene Rigidbody:
- Se usará el existente
- Verifica que tenga `Use Gravity = true`

### FireMinigameManager (Canvas Results)
Necesitas referenciar el TextMeshProUGUI para tiempo:

1. **Selecciona**: `FireMinigameManager` (en la escena)
2. **Inspector**: Busca campo `Time Text` (bajo Canvas References)
3. **Drag & Drop**: El TextMeshProUGUI donde quieras mostrar el tiempo
   - Ejemplo: `Canvas_Results > Panel > TimeText`

Si no tienes ese TextMeshProUGUI, créalo:
```
Canvas_Results > Create Empty Child > Nombre: "TimeText"
  └─ Add Component > TextMeshProUGUI
```

---

## Validación en Juego

### Extintor NO dispara al iniciar
```
✓ Abre la escena de fuego
✓ No deberías ver espuma saliendo al iniciar
✓ Toma el extintor con la mano
✓ Presiona la boquilla → Dispara
✓ Suelta la boquilla → Se detiene
```

### Extintor tiene física
```
✓ Suelta el extintor en el aire
✓ Debería caer con gravedad
✓ Debería rebota/rodar naturalmente
```

### Respawn funciona
```
✓ Suelta el extintor
✓ Aléjate de él más de 30 unidades
✓ Debería reaparecer en posición inicial
✓ Console debería mostrar: "Extintor respawneado"
```

### Tiempo en Resultados
```
✓ Completa el minijuego
✓ Canvas Results se muestra
✓ Debería ver "Tiempo: XX.Xs" en pantalla
```

---

## Logs en Console para Debug

Busca estos mensajes para confirmar que todo funciona:

```
🔧 Extintor listo - Sin disparo inicial
✅ Rigidbody creado automáticamente [si no existía]
⚠️ Extintor muy lejos, respawneando...
✅ Extintor respawneado en posición inicial
🔥 Daño al fuego más cercano (Fire_0): XXX
💨 Disparando espuma
🔓 Disparo detenido
```

---

## Configuración Avanzada

### Cambiar Respawn Distance
```csharp
// En ExtintorController.cs
[SerializeField] private float respawnDistance = 30f;  // Cambiar aquí
```

### Cambiar Mass (peso) del Extintor
```csharp
// En ExtintorController.cs - Start()
rigidbody.mass = 2f;  // Más alto = más pesado, cae más rápido
```

### Cambiar Damage Range (alcance de daño)
```csharp
// En ExtintorController.cs
[SerializeField] private float damageRange = 5f;  // Cambiar aquí
```

---

## Checklist de Implementación

- [ ] ExtintorController.cs actualizado (física automática)
- [ ] FireMinigameManager.cs actualizado (campo timeText)
- [ ] Canvas Results tiene TextMeshProUGUI para tiempo
- [ ] FireMinigameManager referencia ese TextMeshProUGUI
- [ ] Prueba: Extintor no dispara al iniciar
- [ ] Prueba: Extintor se cae con gravedad
- [ ] Prueba: Extintor respawnea al alejarse 30+ unidades
- [ ] Prueba: Tiempo aparece en pantalla final
- [ ] Verifica que no hay errores en Console

---

## Troubleshooting

### "El extintor sigue disparando al iniciar"
- Verifica que `espumaParticles` esté parado: `espumaParticles.Stop()` en Start()
- Revisa que isFiring = false en la línea correcta

### "El extintor no se cae"
- Inspector > ExtintorController > Rigidbody
- Verifica: `Use Gravity = true`
- Verifica: `Is Kinematic = false`
- Verifica: `Constraints` NO incluya "Freeze Position Y"

### "El extintor no respawnea"
- Verifica: No está agarrado (`isHeld = false`)
- Verifica: Distancia > 30 unidades
- Busca en Console: "Extintor muy lejos"
- Si no ve el mensaje, aumenta `respawnDistance`

### "No veo el tiempo en resultados"
- Verifica: `timeText` está asignado en Inspector
- Verifica: Ese TextMeshProUGUI existe en la escena
- Si es null, ignora y el tiempo no se muestra (pero el juego funciona)

---

**Versión**: 1.0 - Ajustes Finales del Extintor  
**Última actualización**: Diciembre 2025
