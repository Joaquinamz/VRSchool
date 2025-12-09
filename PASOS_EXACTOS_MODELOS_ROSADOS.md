# 🛠️ PASOS EXACTOS: Arreglar Modelos Rosados

## VERSIÓN SUPER RÁPIDA (30 SEGUNDOS)

```
1. En Unity: Assets → Reimport All
2. Espera 30 seg
3. ¿Se arreglaron? 
   SÍ → FIN ✅
   NO → Ve a VERSIÓN LENTA
```

---

## VERSIÓN LENTA (5 MINUTOS)

### PASO 1: Verificar Que El Asset Está Importado (30 seg)

```
En Project View (left panel):
1. Haz click en: Assets
2. Busca carpeta: "Kansai University (Takatsuki)"
3. ¿La ves?
   SÍ → Continúa a PASO 2
   NO → El asset no se importó, importalo primero
```

### PASO 2: Reimport All (30 seg)

```
En Editor:
1. Click en menu: Assets
2. Click en: Reimport All
3. O atajo: Ctrl + Shift + R
4. Verás en Console: "Assembly Reloading..."
5. Espera a que diga: "ready"
```

### PASO 3: Revisar Console Para Errores (30 seg)

```
En Editor:
1. Click en menu: Window
2. Click en: General
3. Click en: Console
4. Mira si hay TEXTO ROJO (errores)
5. Si hay rojo, cópialo (importante para diagnóstico)

Si NO hay rojo:
└─ Ir a PASO 4

Si HAY rojo:
└─ Ir a PASO 5 (Error en Shader)
```

### PASO 4: Verificar Si Se Arregló (30 seg)

```
En Editor:
1. Click en Scene tab (arriba a la izquierda)
2. Busca un objeto del asset Kansai
3. ¿Está rosado?
   NO → ✅ ¡ARREGLADO!
   SÍ → Continúa a PASO 6
```

### PASO 5: Si Hay Error En Console (1 min)

```
Leyendo el error:
❌ "Shader error in 'Custom/BothSides'..."
   → El shader BothSides.shader tiene un error
   → Posible solución: Descargar asset nuevamente

❌ "Cannot find shader 'Custom/BothSides'"
   → El shader no se compiló
   → Solución: Reimport nuevamente

❌ "Unknown identifier..."
   → Syntax error en el shader
   → Solución: Manual fix (ver PASO 7) o reemplazar

Anota exactamente qué dice el error
```

### PASO 6: Reasignar Shader Manualmente (2 min)

Si el shader no se aplicó automáticamente:

```
EN PROJECT VIEW:
1. Navega: Assets → Kansai University → Materials

2. Abre carpeta: Building (o cualquiera)

3. Selecciona TODOS los .mat:
   ├─ Click en primero (Concrete1.mat)
   ├─ Ctrl+A para seleccionar todos en carpeta
   ├─ O click uno, Shift+click el último

4. EN INSPECTOR (right panel):
   ├─ Busca: "Shader"
   ├─ Haz click en la rueda/menú junto a Shader
   ├─ O haz click directamente en el dropdown

5. BUSCA el shader:
   ├─ En el search box, escribe: "BothSides"
   ├─ O escribe: "Custom"
   ├─ Debería aparecer: "Custom/BothSides"

6. HADA CLICK en: Custom/BothSides

7. REPITE para cada carpeta:
   ├─ Building/
   ├─ Akikan/
   ├─ kandai/
   ├─ Chaara/
   └─ Wood/
```

### PASO 7: Si El Shader Está Roto (Temporal Fix)

```
OPCIÓN A: Usar Standard Shader (rápido pero diferente look)

1. Selecciona TODOS los materiales rosados
   ├─ Assets → Kansai University → Materials
   ├─ Ctrl+A para todos
   
2. EN INSPECTOR:
   ├─ Campo "Shader"
   ├─ Click en dropdown
   ├─ Search: "Standard"
   ├─ Select: "Standard"

RESULTADO:
✅ Modelos dejan de ser rosados
❌ Pero se ven diferentes (no usan BothSides)

OPCIÓN B: Remplazar Archivo Shader (si tienes backup)

1. Verifica que BothSides.shader NO es corrupto:
   ├─ Abre: Assets → Kansai University → Shader → BothSides.shader
   ├─ En el editor de texto
   ├─ ¿Ves caracteres raros o basura?
   ├─ ❌ SÍ → Está corrupto, reemplazar
   ├─ ✅ NO → No está corrupto, ir a OPCIÓN C

2. Si está corrupto:
   ├─ Descarga el asset nuevamente
   ├─ O copia BothSides.shader de backup
   ├─ O reemplaza con Standard shader (más fácil)

OPCIÓN C: Descargar Asset Nuevamente (completo)

1. Si nada funciona:
   ├─ Delete carpeta: Kansai University (Takatsuki)
   ├─ Asset Store → Search "Kansai University"
   ├─ Import nuevamente
   ├─ Espera a que Unity recompile todo
```

### PASO 8: Verificar Texturas (1 min)

```
Si los modelos NO están rosados pero ves BLANCO:

EN INSPECTOR:
1. Selecciona un objeto
2. Ver Material asignado
3. Material → Verificar albedo/textura

EN PROJECT:
1. Assets → Kansai University → Textures
2. ¿Hay archivos .png o .jpg?
   SÍ → Están ahí
   NO → No se importaron las texturas
   
Si no hay texturas:
├─ Reimport All nuevamente
├─ O verifica estructura de carpetas
```

---

## VERIFICACIÓN FINAL

```
CHECKLIST:
☐ Reimport All hecho
☐ Console revisada (sin errores rojos)
☐ Modelos NO están rosados
☐ Se ven colores y texturas
☐ Shaders: Custom/BothSides o Standard

SI TODOS TIENEN ☐:
→ ✅ ÉXITO - Modelos listos para usar

SI ALGO NO TIENE ☐:
→ Vuelve a ese paso
```

---

## TROUBLESHOOTING RÁPIDO

| Síntoma | Solución |
|---------|----------|
| TODO está rosado | Reimport All |
| Algunos rosados, otros no | Reasignar shader (PASO 6) |
| Error en console | Ver PASO 5 |
| Blanco/sin texturas | Reimport All + PASO 8 |
| Modelo ve normal but se ve extraño | Shader Standard Shader (OPCIÓN A) |

---

**CONSEJO:** Si pasa algo raro mientras haces Reimport:
```
1. Cierra escenas del asset (si las tienes abiertas)
2. Click derecho en carpeta: Reimport
3. Espera a que termine
4. Abre escena nuevamente
```

