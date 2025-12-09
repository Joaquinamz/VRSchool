# ⚡ ASIGNACIONES EN INSPECTOR - Checklist Rápido

## 🎯 Paso 1: En objeto `Professor` (NPCProfessor.cs)

Inspector → busca componente NPCProfessor

```
✓ dialogueText
  → Arrastra el TextMeshPro del canvas que muestra diálogos

✓ nextButton
  → Arrastra el Button "Siguiente" del canvas

✓ gameController
  → Arrastra el objeto completo "FireGameManager" (de Hierarchy)
```

---

## 🎯 Paso 2: En objeto `FireGameManager` (FireGameManager.cs)

Inspector → busca componente FireGameManager

```
✓ professorController
  → Arrastra objeto Professor

✓ firePrefab
  → Arrastra prefab Fire (debe existir)

✓ fireSpawnPoint
  → (Opcional) Arrastra vacío o déjalo null

✓ uiFiresRemaining
  → Arrastra TextMeshPro que muestra "Fuegos: X/X"

✓ uiTimer
  → Arrastra TextMeshPro que muestra "Tiempo: Xs"

✓ statusText
  → (Opcional) puede quedar null

✓ resultsCanvas
  → Arrastra Canvas_Results (EL CANVAS, no un objeto dentro)

✓ scoreText
  → Arrastra TextMeshPro dentro de Canvas_Results

✓ feedbackText
  → Arrastra TextMeshPro dentro de Canvas_Results
```

---

## ⚠️ LOS MÁS IMPORTANTES

**ABSOLUTAMENTE NECESARIOS:**
1. `gameController` en NPCProfessor ← ¡SIN ESTO NO INICIA!
2. `resultsCanvas` en FireGameManager ← ¡SIN ESTO NO MUESTRA RESULTADOS!
3. `scoreText` y `feedbackText` ← ¡SIN ESTOS SALE ERROR AL TERMINAR!

---

## 🔍 Cómo verificar que está bien

1. Selecciona `Professor` en Hierarchy
2. En Inspector, abre el componente `NPCProfessor`
3. Mira los 3 campos:
   - ¿`dialogueText` tiene un círculo azul? ✓
   - ¿`nextButton` tiene un círculo azul? ✓
   - ¿`gameController` tiene un círculo azul? ✓

Si alguno está vacío (sin círculo), entonces **NO ESTÁ ASIGNADO**.

---

**Mismo proceso para FireGameManager.**

Si todos tienen círculos azules, ¡presiona PLAY y deberían funcionar!
