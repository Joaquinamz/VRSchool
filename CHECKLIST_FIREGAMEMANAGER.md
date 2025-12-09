# ✅ CHECKLIST: NPCProfessor → FireGameManager

## 🎯 Verificación Rápida

### En la escena "FireExtinguisherLesson1":

```
[ ] 1. Existe objeto "NPCProfessor" en Hierarchy
      └─ ¿Aparece en la lista de objetos?
      
[ ] 2. Existe objeto "FireGameManager" en Hierarchy
      └─ ¿Aparece en la lista de objetos?
      
[ ] 3. NPCProfessor tiene componente "NPCProfessor"
      └─ Selecciona NPCProfessor → Inspector
      └─ ¿Ves componente NPCProfessor (Script)?
      
[ ] 4. Campo "Game Controller" en NPCProfessor
      └─ Inspector → NPCProfessor component
      └─ Campo "Game Controller"
      └─ ¿Ves "FireGameManager" asignado?
         SI:  ✅ Listo
         NO:  Arrastra FireGameManager aquí
```

## 🧪 Test Rápido

```
[ ] ▶ Play
[ ] Abre Console (Window > General > Console)
[ ] Debería ver: "[NPCProfessor] ✓ FireGameManager encontrado"
[ ] Presiona siguiente en los diálogos
[ ] Debería aparecer un fuego pequeño
[ ] ✅ Si aparece, TODO FUNCIONA
```

## 📋 Si aún no funciona:

Sigue en este orden:

1. **Lee**: `FIX_NPCPROFESSOR_FIREGAMEMANAGER.md` (2 min)
2. **Verifica**: Que FireGameManager existe en Hierarchy
3. **Asigna**: Arrastra FireGameManager al campo Game Controller
4. **Prueba**: ▶ Play y presiona siguiente
5. **Consulta**: `ASIGNACIONES_INSPECTOR.md` si tienes dudas

---

**Tiempo total**: ~5 minutos  
**Dificultad**: ⭐ Muy Fácil
