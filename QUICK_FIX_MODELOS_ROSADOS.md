# ⚡ QUICK FIX: Modelos Rosados en 30 Segundos

## 🚨 Problema
Tu asset Kansai University (Takatsuki) aparece todo ROSADO/MAGENTA

## ✅ Solución (3 pasos)

### Paso 1: Reimport
```
En Unity:
1. Assets → Reimport All
2. O: Ctrl + Shift + R
3. Espera 30 segundos
```

### Paso 2: Verifica Console
```
Window → General → Console
¿Ves errores rojos? 
  NO → Siguiente paso
  SÍ → Lee SOLUCION_MODELOS_ROSADOS.md (Paso 3)
```

### Paso 3: ¿Siguen rosados?
```
NO → ✅ ¡LISTO!

SÍ → Haz lo siguiente:
  1. Assets → Kansai University → Materials
  2. Selecciona UNA carpeta (Building, etc)
  3. Selecciona todos los .mat (Ctrl+A)
  4. En Inspector, busca "Shader"
  5. Cambia a: Custom/BothSides
```

---

## 🔍 Si Aún Está Rosado

### Paso 4: Verifica Que El Shader Existe
```
Assets → Kansai University → Shader → BothSides.shader

❌ NO existe → Descarga de nuevo el asset
✅ Existe → Ir a Paso 5
```

### Paso 5: Usa Standard Shader (Temporal)
```
1. Selecciona todos los materiales rosados
2. Inspector → Shader dropdown
3. Selecciona: Standard (built-in)
4. ¡Deberían cambiar de color!
```

---

## 🎯 Resumen

| Paso | Acción | Resultado |
|------|--------|-----------|
| 1 | Reimport All | 80% de los casos se arregla aquí |
| 2 | Revisar Console | Identifica si hay error |
| 3 | Reasignar Custom/BothSides | Arregla materiales |
| 4 | Usar Standard Shader | Si nada funciona |

---

**CONSEJO:** Si aparecen rosados = "Missing Shader" en lenguaje de Unity
= El shader no se compiló
= Reimport Always arregla esto

