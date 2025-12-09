# VR School - Educational VR Project

Una aplicación educativa inmersiva en realidad virtual desarrollada con **Unity 2022+** y **XR Interaction Toolkit**, diseñada para enseñar lecciones de seguridad en escuelas mediante experiencias interactivas.

## 🎯 Características Principales

### 📍 Lecciones Implementadas

1. **Extinción de Incendios** - Aprende a usar un extintor de forma segura
   - Mecanismo de rociado realista
   - Sistema de fuegos múltiples con dificultad progresiva
   - Feedback de progreso en tiempo real

2. **Lecciones de Terremoto** - Simulación realista de terremotos
   - Escombros cayendo dinámicamente
   - Sistema de impactos y daño
   - Efectos visuales y de audio
   - Múltiples niveles de dificultad

3. **Sistema de Lobby** - Navegación entre lecciones
   - Selector de lecciones fácil de usar
   - Gestión de escenas mediante SimpleLobbyLoader

### ⚙️ Tecnologías

- **Engine**: Unity 2022 LTS
- **XR Framework**: XR Interaction Toolkit v3.1.1
- **Sistema de Entrada**: New Input System
- **UI**: TextMeshPro
- **Audio**: AudioSource con clips personalizados

## 📦 Requisitos Previos

Antes de importar, asegúrate de tener:

- **Unity 2022 LTS** o superior (https://unity.com/download)
- **Visual Studio Community** 2019 o superior (para edición de scripts)
- **Git** instalado (https://git-scm.com)
- Mínimo **50 GB** de espacio en disco
- GPU compatible con VR (recomendado)

## 🚀 Instalación

### Paso 1: Clonar el Repositorio

```bash
git clone https://github.com/Joaquinamz/VRSchool.git
cd VRSchool
```

### Paso 2: Abrir en Unity

1. Abre **Unity Hub**
2. Click en **"Add project from disk"**
3. Selecciona la carpeta `VRSchool`
4. Unity importará automáticamente todos los Assets (esto puede tardar 5-10 minutos)
5. Espera a que termine el proceso de compilación

### Paso 3: Configuración Inicial

Una vez que el proyecto se carga:

1. **Escena de Lobby**:
   - Abre `Assets/Scenes/LobbyVR.unity`
   - Esta es la escena inicial

2. **Escenas de Lecciones**:
   - `Assets/Scenes/FireExtinguisherLesson.unity` - Lección de extinción de incendios
   - `Assets/Scenes/EarthquakeLesson.unity` - Lección de terremoto

3. **Verificación de Assets**:
   - Asegúrate de que no haya errores en la consola
   - Algunos Assets pueden aparecer como "pink" inicialmente (modelo faltante) - de ocurrir,
   configurar el Shader del material a alguno de los "standard".

## 📂 Estructura del Proyecto

```
VRSchool/
├── Assets/
│   ├── Scenes/                    # Escenas del juego
│   │   ├── LobbyVR.unity
│   │   ├── FireExtinguisherLesson.unity
│   │   └── EarthquakeLesson.unity
│   ├── Scripts/                   # Scripts C#
│   │   ├── FireGameManager.cs
│   │   ├── EarthquakeGameManager.cs
│   │   ├── NPCProfessor.cs
│   │   └── ... (más scripts)
│   ├── Prefabs/                   # Prefabs reutilizables
│   └── school/                    # Modelos y assets 3D
├── ProjectSettings/               # Configuración de Unity
├── Packages/                      # Dependencias (XR Toolkit, etc)
└── README.md                      # Este archivo
```

## 🎮 Cómo Usar

### Reproducir el Proyecto

1. En Unity, abre la escena `LobbyVR.unity`
2. Presiona el botón **Play** (▶) en la parte superior central
3. Usa los controles para navegar:
   - **Movimiento**: Hand Trackers (Controles VR)
   - **Vista**: Headset VR
   - **Interactuar**: Gatillos de Hand Trackers

### Escenas Disponibles

| Escena | Descripción | Controles |
|--------|-------------|-----------|
| **LobbyVR** | Menú principal | Botones UI |
| **FireExtinguisherLesson** | Lección 1: Extintor | Click para apuntar, spray |
| **EarthquakeLesson** | Lección 2: Terremoto | Movimiento, esquivar escombros |

## 🔧 Scripts Principales

### FireGameManager.cs
Gestiona el flujo de la lección de extinción de incendios:
- `StartFirstFirePhase()` - Inicia el primer fuego
- `StartMultipleFires()` - Inicia múltiples fuegos simultáneos
- `ShowResults()` - Muestra resultados finales

### EarthquakeGameManager.cs
Gestiona la simulación de terremoto:
- `StartEarthquakePhase()` - Inicia el terremoto con escombros
- `RegisterDebrisHit()` - Registra impactos de escombros
- `CompleteEarthquake()` - Finaliza la lección

### NPCProfessor.cs
Sistema de diálogos del instructor:
- `ShowIntroduction()` - Muestra introducción
- `OnNextClicked()` - Maneja avance de diálogos

## 🎨 Personalización

### Cambiar Diálogos

En las escenas, edita los campos del script correspondiente:
- **NPCProfessor** para lección de fuego
- **EarthquakeProfessor** para lección de terremoto

### Ajustar Dificultad

En **EarthquakeGameManager** puedes modificar:
```csharp
spawnRate = 2f;           // Escombros por segundo
earthquakeDuration = 30f; // Duración del terremoto
shakeIntensity = 0.5f;    // Intensidad del temblor
```

### Audio Personalizado

En la escena, asigna archivos de audio en los campos:
- `earthquakeSound` - Clip de audio para terremoto (recomendado: .mp3)

## ⚙️ Requisitos del Sistema

### Mínimo Recomendado
- **CPU**: Intel i5-10400 / AMD Ryzen 5 3600
- **RAM**: 16 GB
- **GPU**: NVIDIA GTX 1660 / AMD RX 5600XT
- **Storage**: 100 GB SSD
- **Resolución**: 1920x1080 @ 60Hz

### VR
- **Headset**: Meta Quest 2/3, HTC Vive, Valve Index
- **Configurar en**: Edit → Project Settings → XR Plug-in Management

## 🐛 Troubleshooting

### Problema: Assets aparecen en rosa/magenta
**Solución**: Assets faltantes - ignore, son recursos opcionales. El juego funciona sin ellos.

### Problema: Botones no responden
**Solución**: 
1. Verifica que `EventSystem` existe en la escena
2. Canvas debe estar en modo `Screen Space - Overlay`
3. Ejecuta `Assets/UIButtonFixer.cs` si está disponible

### Problema: Scripts muestran errores
**Solución**: 
1. Abre `Window → TextMeshPro → Import TMP Essentials`
2. Espera a que se recompilen los scripts
3. Si persiste: `Assets → Reimport All`

## 📝 Licencia

Este proyecto es de código abierto bajo licencia MIT. Úsalo libremente para propósitos educativos.

## 👥 Soporte

Para reportar problemas o sugerencias, abre un **Issue** en GitHub:
https://github.com/Joaquinamz/VRSchool/issues

## 🚀 Próximas Mejoras Planeadas

- [ ] Más lecciones educativas
- [ ] Soporte mejorado para VR completo
- [ ] Sistema de puntuación y rankings
- [ ] Localizaciones (múltiples idiomas)
- [ ] Optimizaciones de rendimiento

---

**Última actualización**: Diciembre 2025  
**Versión**: 1.0.2  
**Desarrollador**: Joaquin A.
