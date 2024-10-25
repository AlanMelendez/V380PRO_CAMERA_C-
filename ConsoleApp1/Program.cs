using OpenCvSharp;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CaptureVideoNotFile();
        }


        static async Task SendRequestAsync(string xmlFile)
        {
            string url = "http://192.168.100.19:8899/onvif/ptz";
            using (HttpClient client = new HttpClient())
            {
                string xmlContent = "";
                if (File.Exists(xmlFile))
                {
                    xmlContent = File.ReadAllText(xmlFile);
                }
                else
                {
                    Console.WriteLine($"File not found: {xmlFile}");
                }

                var content = new StringContent(xmlContent, System.Text.Encoding.UTF8, "text/xml");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Solicitud enviada con éxito.");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }

        static async Task CaptureVideoInFile()
        {
            // Captura de video RTSP
            string rtspUrl = "rtsp://192.168.100.19/live/ch00_1";
            using (var capture = new VideoCapture(rtspUrl))
            {
                Mat frame = new Mat();

                if (!capture.IsOpened())
                {
                    Console.WriteLine("No se pudo abrir la cámara.");
                    return;
                }

                Cv2.NamedWindow("IonicAppCamera", WindowFlags.Normal);

                // Obtener el tamaño del frame
                Size frameSize = new Size(capture.FrameWidth, capture.FrameHeight);

                // Definir el VideoWriter
                using (VideoWriter writer = new VideoWriter("video.avi", FourCC.XVID, 30, frameSize))
                {
                    bool isPtzCommandRunning = false; // Evitar múltiples solicitudes PTZ simultáneas hacia la cámara.

                    while (true)
                    {
                        // Leer el siguiente fotograma de la cámara
                        if (!capture.Read(frame))
                        {
                            Console.WriteLine("No se pudo capturar el frame.");
                            break;
                        }

                        // Comprobar si el frame está vacío
                        if (frame.Empty())
                        {
                            Console.WriteLine("El frame está vacío.");
                            break;
                        }

                        // Mostrar el fotograma en la ventana
                        Cv2.ImShow("IonicAppCamera", frame);

                        // Escribir el fotograma al archivo de video
                        writer.Write(frame);

                        // Esperar 10 ms y verificar si se ha presionado una tecla
                        int key = Cv2.WaitKey(10);

                        if (key == 105 && !isPtzCommandRunning) // 105 = 'i' (tecla 'i')
                        {
                            isPtzCommandRunning = true;
                            _ = Task.Run(async () =>
                            {
                                // Control PTZ con solicitudes HTTP en segundo plano
                                string xmlFilePath = @"C:\Users\Usuario\source\repos\ConsoleApp1\ConsoleApp1\xml\postup.xml";
                                await SendRequestAsync(xmlFilePath);
                                isPtzCommandRunning = false; // Marcar como finalizado
                            });
                        }
                        else if (key == 44 && !isPtzCommandRunning) // 44 = ',' (tecla ',')
                        {
                            isPtzCommandRunning = true;
                            _= Task.Run(async () =>
                            {
                                // Control PTZ con solicitudes HTTP en segundo plano
                                string xmlFilePath = @"C:\Users\Usuario\source\repos\ConsoleApp1\ConsoleApp1\xml\postdown.xml";
                                await SendRequestAsync(xmlFilePath);
                                isPtzCommandRunning = false; // Marcar como finalizado
                            });
                        }
                        else if (key == 27) // 27 = 'ESC' (para salir del bucle)
                        {
                            Console.WriteLine("Saliendo...");
                            break;
                        }
                    }
                }

                // Liberar los recursos
                Cv2.DestroyAllWindows();
            }
        }
        static async Task CaptureVideoNotFile()
        {
            // Captura de video RTSP
            string rtspUrl = "rtsp://192.168.100.19/live/ch00_1";
            using (var capture = new VideoCapture(rtspUrl))
            {
                Mat frame = new Mat();

                if (!capture.IsOpened())
                {
                    Console.WriteLine("Don't open the camera.");
                    return;
                }

                Cv2.NamedWindow("IonicAppCamera", WindowFlags.Normal);

                bool isPtzCommandRunning = false; // Para evitar múltiples solicitudes PTZ simultáneas hacia la camara.

                while (true)
                {
                    // Leer el siguiente fotograma de la cámara
                    if (!capture.Read(frame))
                    {
                        Console.WriteLine("No se pudo capturar el frame.");
                        break;
                    }

                    // Mostrar el fotograma en la ventana
                    Cv2.ImShow("IonicAppCamera", frame);

                    // Esperar 10 ms y verificar si se ha presionado una tecla
                    int key = Cv2.WaitKey(10);

                    if (key == 105 && !isPtzCommandRunning) // 105 = 'i' (tecla 'i')
                    {
                        isPtzCommandRunning = true;
                        _ = Task.Run(async () =>
                        {
                            // Control PTZ con solicitudes HTTP en segundo plano
                            string xmlFilePath = @"C:\Users\Usuario\source\repos\ConsoleApp1\ConsoleApp1\xml\postup.xml";
                            await SendRequestAsync(xmlFilePath);
                            isPtzCommandRunning = false; // Marcar como finalizado
                        });
                    }
                    else if (key == 44 && !isPtzCommandRunning) // 44 = ',' (tecla ',')
                    {
                        isPtzCommandRunning = true;
                        _= Task.Run(async () =>
                        {
                            // Control PTZ con solicitudes HTTP en segundo plano
                            string xmlFilePath = @"C:\Users\Usuario\source\repos\ConsoleApp1\ConsoleApp1\xml\postdown.xml";
                            await SendRequestAsync(xmlFilePath);
                            isPtzCommandRunning = false; // Marcar como finalizado
                        });
                    }
                    else if (key == 27) // 27 = 'ESC' (para salir del bucle)
                    {
                        Console.WriteLine("Saliendo...");
                        break;
                    }

                }
            }

            // Liberar los recursos
            Cv2.DestroyAllWindows();
        }
    }
}
