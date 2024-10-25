# ConsoleApp1

## Description
ConsoleApp1 is a C# console application that captures video from an RTSP camera, displays the video in a window, and allows you to control the camera using PTZ (Pan-Tilt-Zoom) commands via HTTP requests.

## Requirements
- **.NET Framework 4.7.2**
- **Visual Studio** (recommended version: 2019 or higher)

## Dependencies
- **OpenCvSharp**: Library for image and video processing.
- **System.Net.Http**: To make HTTP requests.

## Installation

### 1. Clone the repository
git clone [https://github.com/your-user/your-repository.git cd your-repository](https://github.com/AlanMelendez/V380PRO_CAMERA_CSHARP)

### 2. Open the project in Visual Studio
Open the `ConsoleApp1.sln` file in Visual Studio.

### 3. Restore NuGet Packages
Visual Studio should automatically restore the necessary NuGet packages. If not, you can restore them manually:
nuget restore

### 4. Configure RTSP URL and XML files
Make sure to configure your camera's RTSP URL and the paths to the XML files for the PTZ commands in the `Program.cs` file:

## Usage
Run the application from Visual Studio or from the command line: dotnet run

### PTZ Commands
- **'i'**: Move the camera up.
- **','**: Move the camera down.
- **'ESC'**: Quit the application.

## Contributions
Contributions are welcome, feel free to share any changes. Please open an issue or a pull request to discuss any changes you would like to make.
