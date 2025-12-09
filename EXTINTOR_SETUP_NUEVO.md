# 🔧 SETUP DEL EXTINTOR - GUÍA COMPLETA

**Problema**: El extintor anterior solo funcionaba con una mano.

**Solución**: WorkingExtinguisher.cs NUEVO soporta ambas manos + Input System.

---

## ✅ NUEVO EXTINTOR (Dual-Hand + Input System)

### CARACTERÍSTICAS

- ✅ Se agarra con CUALQUIER mano (left o right)
- ✅ Funciona con Input System (no legacy Input)
- ✅ Trigger presiona para disparar espuma
- ✅ Apaga múltiples fuegos cercanos
- ✅ Funciona en VR con XR Controllers

---

## 📋 PASO A PASO: SETUP CORRECTO

### PASO 1: Preparar el Extintor

En tu escena FireExtinguisherLesson:

```
Hierarchy:
├─ ExtintorObject (Cube, scale 0.1, 0.3, 0.1, color rojo)
```

### PASO 2: Agregar Componentes

**Selecciona ExtintorObject**:

1. **Add Component → XR Grab Interactable**
   - Grab Type: Single Hand
   - Drop on Deselect: ON

2. **Add Component → WorkingExtinguisher.cs** (el nuevo)

3. **Add Component → Particle System** (para la espuma)
   - Nombre: `FoamParticles`
   - Start Lifetime: 2 segundos
   - Start Size: 0.2
   - Emission: 50 particles/sec
   - Shape: Cone (para simular salida de extintor)

### PASO 3: Configurar WorkingExtinguisher

**Inspector → WorkingExtinguisher script**:

```
Referencias:
┌─────────────────────────────────────────┐
│ Foam Particle: [Arrastra FoamParticles] │
│ Grip Input Action: [Vacío]              │
│ Trigger Input Action: [Vacío]           │
└─────────────────────────────────────────┘

Configuración:
┌─────────────────────────────────────────┐
│ Damage Per Second: 30                   │
│ Damage Range: 5                         │
└─────────────────────────────────────────┘
```

### PASO 4: Configurar Input System

**Importante**: Debes tener un Input Action Map.

Si YA TIENES:
- Ve a Project → Busca `XRI Default Input Actions`
- Haz doble clic para abrir
- En `XRI RightHand → Grip → Value` copia su nombre

En WorkingExtinguisher:
- **Trigger Input Action**: Busca `XRI RightHand → Trigger → Value`

**Si NO tienes Input Actions**:
1. **Window → TextMesh Pro → Import TMP Examples & Extras**
   (Esto trae los Input Actions de XRI)

O crea tus propias Actions:
1. **Assets → Create → Input Actions**
2. **Acción → Add Action → "Trigger"**
3. **Binding → Add Binding → Gamepad/Right Trigger**

### PASO 5: Referencias de Fuegos

**NO NECESITAS ASIGNARLAS MANUALMENTE**

El script busca todos los FireBehavior automáticamente con:
```csharp
FindObjectsByType<FireBehavior>()
```

---

## 🎮 CÓMO FUNCIONA EN GAMEPLAY

### Usuario agarra el extintor:
```
1. Mano izquierda O derecha agarra el Cube
2. OnGrab() se dispara
3. isHeld = true
4. Debug.Log: "🖐️ Extintor AGARRADO"
```

### Usuario presiona Trigger:
```
1. Presiona trigger del control
2. OnTriggerPressed() se dispara
3. isTriggerPressed = true
4. ParticleSystem empieza a jugar
5. ApplyDamageToFires() daña fuegos cercanos
```

### Resultado:
```
- Fuego 1: -30 intensidad/seg
- Fuego 2: -30 intensidad/seg
- (Todos los fuegos en rango reciben daño)
```

### Usuario suelta trigger:
```
1. Suelta trigger
2. OnTriggerReleased() se dispara
3. isTriggerPressed = false
4. ParticleSystem se detiene
5. Ya no hay daño
```

### Usuario suelta extintor:
```
1. Suelta el control
2. OnRelease() se dispara
3. isHeld = false
4. ParticleSystem se detiene
5. Extintor se queda en el suelo
```

---

## 🔍 DEBUGGING

### En Console, debes ver:

Al agarrar:
```
🔧 Extintor listo con soporte dual-hand
🖐️ Extintor AGARRADO (ambas manos soportadas)
```

Al presionar Trigger:
```
💨 TRIGGER PRESIONADO
```

Al disparar:
```
(Silencio - solo Debug.Log de fuegos apagándose)
```

Al soltar:
```
🔓 TRIGGER SOLTADO
🖐️ Extintor SOLTADO
```

### Si NO ves mensajes:

1. Presiona **Ctrl+`** para abrir Console
2. Verifica que Errors está habilitado
3. Agarres el extintor en VR
4. ¿Ves los mensajes?

---

## 🐛 ERRORES COMUNES

### Error: "Component not found"
**Solución**: 
- Verifica que asignaste WorkingExtinguisher.cs al Cube
- Verifica que agregaste Particle System

### Error: "No Input Action"
**Solución**:
- En PlaySettings, verifica que Input System está activado
- Window → TextMesh Pro → Import TMP Examples (trae Actions)

### Extintor no daña fuegos
**Problema**: Fuegos están fuera de rango (5 metros)
**Solución**: 
- Aumenta `Damage Range` a 10 o 15
- O acerca más los fuegos al extintor

### Particle System no funciona
**Problema**: El Particle System está desactivado o no visible
**Solución**:
- Selecciona ExtintorObject → Particle System
- Presiona Play
- Agarra extintor y presiona Trigger
- ¿Ves partículas?
- Si no, aumenta "Emission Rate" a 100

---

## ✅ CHECKLIST: Extintor Funcional

- [ ] ExtintorObject tiene XRGrabInteractable
- [ ] ExtintorObject tiene WorkingExtinguisher.cs
- [ ] ExtintorObject tiene Particle System
- [ ] WorkingExtinguisher → Foam Particle: asignado
- [ ] Damage Per Second: 30 (o más)
- [ ] Damage Range: 5 (o más)
- [ ] Input System configurado en PlaySettings
- [ ] Cuando agarras, ves Debug.Log
- [ ] Cuando presionas Trigger, funciona espuma

---

## 🎯 TEST RÁPIDO

1. Abre FireExtinguisherLesson.unity
2. Presiona Play
3. En VR: Agarra extintor con mano izquierda O derecha
4. Presiona Trigger
5. ¿Ves espuma?
6. Apunta a un fuego
7. ¿El fuego se apaga?

**SI SÍ A TODAS**: ✅ ¡FUNCIONA!

---

## 📊 COMPARACIÓN: Antiguo vs Nuevo

| Feature | Antiguo | Nuevo |
|---------|---------|-------|
| 1 mano | ✅ | ✅ |
| 2 manos | ❌ | ✅ |
| Input System | ❌ | ✅ |
| Interacción boquilla | Compleja | Simple (Trigger) |
| Daño automático | ❌ | ✅ |
| Búsqueda de fuegos | Manual | Automática |

---

*Setup del Extintor - Versión Nueva*
*29 de Noviembre, 2025*
