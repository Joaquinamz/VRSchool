# VERIFICACIÓN Y TESTING: FireGameManager Reformulado

## ✅ CAMBIOS REALIZADOS EN FireGameManager.cs

### PROBLEMA ORIGINAL
❌ El fuego no aparecía cuando presionabas "Continuar" en la introducción
❌ El juego se quedaba en "cargando eterno"
❌ No había logs claros para diagnosticar

### SOLUCIÓN IMPLEMENTADA

**1. Sistema de Fases Mejorado**
```csharp
public enum GamePhase 
{ 
    NotStarted,               // Estado inicial
    Introduction,             // Mostrando diálogos
    WaitingForFireSpawn,      // Preparando el fuego
    FirstFire,                // Fuego activo
    WaitingForPostFireDialog, // Esperando siguiente
    Minigame,                 // Múltiples fuegos
    Complete                  // Lección terminada
}
```

**2. Validaciones Defensivas**
- ✅ Verifica que `firePrefab` existe ANTES de spawnear
- ✅ Verifica que `FireBehavior` component existe en el prefab
- ✅ Timeout de 3 segundos si el fuego no aparece
- ✅ Manejo de excepciones con try-catch

**3. Logging Completo**
```
[FireGameManager] ✓ Inicializado
[FireGameManager] ✓ firePrefab está asignado correctamente
[FireGameManager] ✓ fireSpawnPoint: Asignado
[FireGameManager] ✓ CompleteIntroduction() llamado
[FireGameManager] 🔥 Spawneando fuego de PRÁCTICA
[FireGameManager] ✓ Fuego instanciado exitosamente
[FireGameManager] ✓ FireBehavior encontrado
[FireGameManager] ✓✓✓ FUEGO DE PRÁCTICA LISTO
```

**4. Secuencia de Ejecución Clara**
```
StartIntroduction()
  ↓
Mostrar diálogos
  ↓
Usuario presiona CONTINUAR
  ↓
CompleteIntroduction()
  ↓ Invoke(0.5s)
SpawnPracticeFire()
  ↓
FireGameManager.FirstFire (esperando que lo apaguen)
  ↓
Usuario apaga fuego
  ↓
CheckPracticeFireComplete()
  ↓
CompletePracticeFire()
  ↓
Mostrar diálogo post-fuego
```

---

## 🧪 TESTING STEP-BY-STEP

### ANTES DE TESTEAR
Verifica que:
1. La escena FireExtinguisherLesson1 está configurada
2. NPCProfessor está asignado en FireGameManager
3. **firePrefab está asignado en FireGameManager** (CRÍTICO)
4. El prefab tiene FireBehavior component

### TEST 1: ENTRA A LA ESCENA

```
1. Play
2. Console debe mostrar:
   [FireGameManager] ✓ Inicializado
   [FireGameManager] ✓ firePrefab está asignado correctamente
```

**Si ves error:**
```
[FireGameManager] ❌ CRÍTICO: firePrefab NO ESTÁ ASIGNADO en Inspector
```
→ Ve a Inspector > FireGameManager > arrastra Fire prefab

---

### TEST 2: INTRODUCCIÓN Y TRANSICIÓN

```
1. La escena muestra diálogos del profesor
2. Presiona "Continuar" cuando termine la introducción
3. Console debe mostrar:
   [FireGameManager] ✓ CompleteIntroduction() llamado
   [FireGameManager] 🔥 Spawneando fuego de PRÁCTICA
   [FireGameManager] ✓ Fuego instanciado exitosamente
   [FireGameManager] ✓ FireBehavior encontrado
   [FireGameManager] ✓✓✓ FUEGO DE PRÁCTICA LISTO
```

**Si ves ERROR:**
```
[FireGameManager] ❌ CRÍTICO: firePrefab es NULL
```
→ Asigna el prefab en Inspector

```
[FireGameManager] ❌ Fuego NO tiene FireBehavior
```
→ Añade FireBehavior al prefab de fuego

---

### TEST 3: FUEGO APARECE EN PANTALLA

```
1. Después de presionar CONTINUAR, deberías ver el fuego
2. Status text dice "Apaga el fuego de práctica con el extintor"
3. Timer comienza a avanzar
```

**Si NO aparece fuego:**
- Mira la console para errores
- Verifica que fireSpawnPoint está asignado (o usa default 0,1,5)
- Prueba con un prefab simple (cubo que cae)

---

### TEST 4: EXTINTOR APAGA FUEGO

```
1. Toma el extintor
2. Apunta al fuego
3. Pulsa el gatillo
4. El fuego debe reducir su intensidad
5. Cuando FireBehavior.currentIntensity <= 0:
   Console muestra:
   [FireGameManager] ✓ Fuego de práctica apagado
   [FireGameManager] ✓ Mostrando diálogo post-fuego
```

**Si NO se detecta:**
- Verifica que ExtintorController está funcionando
- Comprueba que FireBehavior.currentIntensity es pública
- Verifica que el extintor daña al fuego

---

### TEST 5: DIÁLOGO POST-FUEGO Y MINIJUEGO

```
1. Se muestra diálogo post-fuego
2. Usuario presiona CONTINUAR
3. Console muestra:
   [FireGameManager] 🎮 Iniciando minijuego
   [FireGameManager] ✓ Llamando FireMinigameManager.StartMultipleFires()
```

**Si falla minijuego:**
- Verifica que FireMinigameManager existe y tiene método StartMultipleFires()
- Mira logs para encontrar exact error

---

## 📋 CHECKLIST DE FUNCIONAMIENTO

```
Funcionalidad                          ✅ Working
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Inicialización                         [ ]
Validación de firePrefab               [ ]
Mostrar introducción                   [ ]
Transición a FirstFire                 [ ]
Spawnear fuego con delay               [ ]
Detectar que fuego fue apagado         [ ]
Mostrar diálogo post-fuego             [ ]
Iniciar minijuego                      [ ]
Timer funcionando                      [ ]
UI actualizándose                      [ ]
```

---

## 🔍 DEBUGGING AVANZADO

### Habilitar Verbose Logging

En FireGameManager.cs, línea ~120, hay un método llamado `Update()`. 
Agrega esto para más detalles:

```csharp
void Update()
{
    // ... código existente ...
    
    // DEBUG: Descomenta para ver estado cada frame
    // Debug.Log($"Fase: {currentPhase}, Timer: {gameTimer:F2}, Activo: {gameActive}");
}
```

### Inspeccionar Estado en Runtime

En la ventana "Game" o Console, crea un script de debugging:

```csharp
public class FireGameManagerDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            var mgr = FindFirstObjectByType<FireGameManager>();
            if (mgr != null)
            {
                Debug.Log($"Phase: {mgr.GetCurrentPhase()}");
                Debug.Log($"Timer: {mgr.GetGameTimer()}");
                Debug.Log($"Active: {mgr.IsGameActive()}");
            }
        }
    }
}
```

Presiona **D** para ver el estado actual en Console.

---

## 🚀 PRÓXIMOS PASOS

Si todo funciona:
1. ✅ Prueba todos los 3 cursos de extintor (Lesson1, 2, 3)
2. ✅ Crea los 3 cursos de sismos (usa GUIA_COMPLETA_CURSO_SISMOS.md)
3. ✅ Asegúrate de que los botones de Lobby funcionan

---

## 📞 SI ALGO SIGUE MAL

Revisa estos archivos en orden:
1. `NPCProfessor.cs` - ¿Llama correctamente a `gameController.CompleteIntroduction()`?
2. `FireGameManager.cs` - ¿Transiciona entre fases correctamente?
3. `FireBehavior.cs` - ¿Tiene `currentIntensity` pública?
4. Prefab de fuego - ¿Tiene FireBehavior y Rigidbody?

**Console es tu mejor amigo**: Busca **[FireGameManager]** para ver el flujo.

