# 🔧 FIX: NPCProfessor - FireGameManager no asignado

## ❌ Error
```
[NPCProfessor] ❌ FireGameManager no asignado
UnityEngine.Debug:LogError (object)
NPCProfessor:EndIntroduction () (at Assets/NPCProfessor.cs:99)
```

## 🎯 Solución Rápida (2 minutos)

### Paso 1: Encuentra el GameObject FireGameManager

```
En Hierarchy (escena FireExtinguisherLesson1):
├─ Canvas
├─ NPCProfessor ← El que dice el error
└─ FireGameManager ← Busca esto
```

**Si NO lo ves**:
- ❌ El objeto no existe
- ✅ Crea uno: Click derecho → 3D Object > Empty
- ✅ Nombre: "FireGameManager"
- ✅ Add Component → FireGameManager

### Paso 2: Asigna a NPCProfessor

```
1. Selecciona "NPCProfessor" en Hierarchy
2. Inspector → NPCProfessor component
3. Campo: "Game Controller" ← Verifica que está VACÍO
4. Si está vacío:
   ✓ Arrastra "FireGameManager" aquí
   ✓ O click en círculo pequeño → Selecciona FireGameManager
5. Ahora debe verse:
   └─ Game Controller: FireGameManager (GameObject)
```

### Paso 3: Prueba

```
▶ Play
→ Presiona siguiente hasta que termine los diálogos
→ Debería iniciar el fuego de práctica
→ Si funciona: ✅ LISTO
```

---

## 📋 Checklist

```
[ ] Existe "FireGameManager" en Hierarchy
[ ] NPCProfessor tiene componente "NPCProfessor"
[ ] NPCProfessor.Game Controller está asignado a FireGameManager
[ ] Presionas siguiente y aparece el fuego
```

---

## 🔍 Verificación Adicional

Si aún no funciona, verifica en Console:

```
▶ Play
Abre Console (Window > General > Console)
```

**Deberías ver**:
```
[NPCProfessor] ✓ FireGameManager encontrado automáticamente
```

**Si ves**:
```
[NPCProfessor] ❌ FireGameManager NO ENCONTRADO en la escena
```

Significa que el objeto literalmente no existe. Crea uno (Paso 1 arriba).

---

## 🆘 Último Recurso

Si tienes duda de qué es FireGameManager:

**Es el script que:**
- Controla la práctica del primer fuego
- Hace que aparezca el fuego
- Detecta cuándo lo apagaste
- Muestra diálogos posteriores

**Ubicación típica**:
```
Escena FireExtinguisherLesson1
├─ Canvas
├─ NPCProfessor
├─ FireGameManager ← AQUÍ (puede estar vacío, solo necesita el script)
├─ Extintor
└─ ... otros objetos
```

---

**Tiempo total**: ~2 minutos  
**Dificultad**: ⭐ Muy Fácil
