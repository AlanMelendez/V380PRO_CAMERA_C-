# ConsoleApp1

## Descripción
ConsoleApp1 es una aplicación de consola en C# que captura video desde una cámara RTSP, muestra el video en una ventana y permite controlar la cámara mediante comandos PTZ (Pan-Tilt-Zoom) a través de solicitudes HTTP.

## Requisitos
- **.NET Framework 4.7.2**
- **Visual Studio** (versión recomendada: 2019 o superior)

## Dependencias
- **OpenCvSharp**: Librería para procesamiento de imágenes y video.
- **System.Net.Http**: Para realizar solicitudes HTTP.

## Instalación

### 1. Clonar el repositorio
git clone https://github.com/tu-usuario/tu-repositorio.git cd tu-repositorio


### 2. Abrir el proyecto en Visual Studio
Abre el archivo `ConsoleApp1.sln` en Visual Studio.

### 3. Restaurar paquetes NuGet
Visual Studio debería restaurar automáticamente los paquetes NuGet necesarios. Si no es así, puedes restaurarlos manualmente:
nuget restore


### 4. Configurar la URL RTSP y los archivos XML
Asegúrate de configurar la URL RTSP de tu cámara y las rutas a los archivos XML para los comandos PTZ en el archivo `Program.cs`:


## Uso
Ejecuta la aplicación desde Visual Studio o desde la línea de comandos: dotnet run

### Comandos PTZ
- **'i'**: Mover la cámara hacia arriba.
- **','**: Mover la cámara hacia abajo.
- **'ESC'**: Salir de la aplicación.

## Contribuciones
Las contribuciones son bienvenidas, sientete libre de compartir cualquier cambio. Por favor, abre un issue o un pull request para discutir cualquier cambio que te gustaría hacer.


