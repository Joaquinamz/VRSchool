# ⚡ BOTONES - Guía Rápida (TL;DR)

## El problema resumido:
Botones dejan de funcionar cuando se asignan a LobbyManager.

## Por qué pasa:
- Botón no tiene componente `Button` correctamente configurado
- O botón no se asignó en el orden correcto en el array
- O hay un error en la inicialización que está silenciado

## La solución resumida:

### 1. Abre Console
```
Window → General → Console
```

### 2. Presiona PLAY

### 3. Mira los logs

**¿Ves esto?**
```
[LobbyManager] ✓ Botón Extintor 0 asignado
✓ Botón Extintor 1 asignado
✓ Botón Extintor 2 asignado
✓ Botón Extintor 3 asignado
```

✅ **Los botones están asignados correctamente.**

**¿O ves esto?**
```
[LobbyManager] ❌ Botón Extintor 0 es NULL
```

❌ **Element 0 no tiene botón asignado en Inspector.**

### 4. Si ves errores, sigue estos pasos:

1. **Selecciona objeto LobbyManager en Hierarchy**
2. **En Inspector, busca componente LobbyManager**
3. **Expande "Extintor Buttons"**
4. **Para cada Element vacío:**
   - Arrastra botón del Hierarchy
   - O del Project si es un prefab

### 5. Asigna en este orden:

```
Element 0 → Botón "A"
Element 1 → Botón "B"
Element 2 → Botón "C"
Element 3 → Botón "Random"
```

Repite para "Sismo Buttons"

### 6. Presiona PLAY de nuevo

Deberías ver todos los checkmarks verdes (✓).

### 7. Presiona botón en game view

Console debería mostrar:
```
[LobbyManager.SelectCourse] CLICK DETECTADO
[LobbyManager] ✓ GameManager actualizado
[LobbyManager] Cargando escena: FireExtinguisherLesson
[LobbyManager] ✓ Escena cargada exitosamente
```

---

## Si aún no funciona:

Abre: `BOTONES_FIX_LOBBYMANAGER.md` (guía completa con troubleshooting)
