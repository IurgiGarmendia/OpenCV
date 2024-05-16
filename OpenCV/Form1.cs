using OpenCvSharp;
using PDFtoImage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PDFtoImage;
using Conversion = PDFtoImage.Compatibility.Conversion;
using System.IO;

namespace OpenCV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string imagePath = @"C:\Activa\Siemens\D2342266_005.jpg";
            Mat src = new Mat(imagePath, ImreadModes.Grayscale);

            Mat dst = new Mat();

            Cv2.Canny(src, dst, 50, 200);
            //using (new OpenCvSharp.Window("src image", src, WindowFlags.FreeRatio))
            using (new OpenCvSharp.Window("dst image", dst, WindowFlags.GuiNormal))
            {
                Cv2.WaitKey();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string imagePath = @"C:\Activa\Siemens\image.jpg";
            Mat image = Cv2.ImRead(imagePath, ImreadModes.Color);

            // Convertir la imagen a escala de grises
            Mat grayImage = new Mat();
            Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);

            // Aplicar umbralización (binarización) para obtener una imagen binaria
            Mat binaryImage = new Mat();
            //Cv2.Threshold(grayImage, binaryImage, 127, 255, ThresholdTypes.Binary);
            Cv2.Canny(grayImage, binaryImage, 50, 200);
            // Mostrar la imagen con los contornos dibujados
            Cv2.NamedWindow("Canny", WindowFlags.GuiNormal);
            Cv2.ImShow("Canny", binaryImage);
            Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();

            // Encontrar contornos en la imagen binaria
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binaryImage, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // Dibujar contornos en la imagen original
            Scalar color = Scalar.Red;
            int thickness = 10;
            for (int i = 0; i < contours.Length; i++)
            {
                Cv2.DrawContours(image, contours, i, color, thickness);
            }

            

            // Mostrar la imagen con los contornos dibujados
            Cv2.NamedWindow("Contours", WindowFlags.GuiNormal);
            Cv2.ImShow("Contours", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();


            int j = 0;
            foreach (OpenCvSharp.Point[] puntu in contours)
            {
                if (puntu[0].X > 2000 && puntu[0].Y>1000)
                {
                   j++;
                }

            }




            OpenCvSharp.Point[][] contoursIurgi = new OpenCvSharp.Point[j][];
            int k = 0;
            foreach (OpenCvSharp.Point[] puntu in contours)
            {
                if (puntu[0].X > 2000 && puntu[0].Y>1000) { 
                    contoursIurgi[k]=puntu;
                    k++;
                }

            }


            Cv2.FillPoly(image, contoursIurgi, color);
            Cv2.NamedWindow("Contours", WindowFlags.GuiNormal);
            Cv2.ImShow("Contours", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pdfPath = @"C:\Activa\Siemens\D2930454_001_D2930454-001-dwg1.pdf";
            string jpgPath= @"C:\Activa\Siemens\image.jpg";
            FileStream pdfStream = new FileStream(pdfPath, FileMode.Open);
            PDFtoImage.Conversion.SaveJpeg(jpgPath, pdfStream);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //https://www.quora.com/How-can-I-detect-a-rectangle-using-OpenCV-code-in-Python
            string imagePath = @"C:\Activa\Siemens\image.jpg";
            Mat image = Cv2.ImRead(imagePath, ImreadModes.Color);

            // Convertir la imagen a escala de grises
            Mat grayImage = new Mat();
            Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);

            // Aplicar umbralización (binarización) para obtener una imagen binaria
            Mat binaryImage = new Mat();
            //Cv2.Threshold(grayImage, binaryImage, 127, 255, ThresholdTypes.Binary);
            Cv2.Canny(grayImage, binaryImage, 50, 200);
            // Mostrar la imagen con los contornos dibujados
            Cv2.NamedWindow("Canny", WindowFlags.GuiNormal);
            Cv2.ImShow("Canny", binaryImage);
            Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();

            // Encontrar contornos en la imagen binaria
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binaryImage, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            //OpenCvSharp.Point[][] contoursIurgi = new OpenCvSharp.Point[200][];
            //int w = 0;
            //// Iterate through the contours 
            //foreach (OpenCvSharp.Point[] contour in contours)
            //{
            //    // Approximate the contour to a polygon 
            //    var approx =Cv2.ApproxPolyDP(contour, 0.04 * Cv2.ArcLength(contour, true), true);

            //    ////Check if the polygon has 4 vertices(a rectangle)
            //    if (approx.Length == 4)
            //    {
            //        contoursIurgi[w] = approx;
            //        w++;

            //    }
            //}

            //////Dibujar contornos en la imagen original
            //Scalar color = Scalar.Red;
            //    int thickness = 10;
            ////remove null values
            //contoursIurgi = contoursIurgi.Where(c => c != null).ToArray();
            //for (int i = 0; i < contoursIurgi.Length; i++)
            //    {
            //        Cv2.DrawContours(image, contoursIurgi, i, color, thickness);
            //    }
            //    // Draw the rectangle on the original image 
            //    //Cv2.DrawContours(image, approx, 0, (0, 255, 0), 2);

            ////# Display the result 
            //Cv2.NamedWindow("Rectangles", WindowFlags.GuiNormal);
            //Cv2.ImShow("Rectangles", image);
            //Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();


            // Filtrar los contornos para seleccionar solo los rectángulos
            for (int i = 0; i < contours.Length; i++)
            {
                RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                Point2f[] vertices = Cv2.BoxPoints(rect);

                // Convertir los puntos a Point
                OpenCvSharp.Point[] intPoints = new OpenCvSharp.Point[vertices.Length];
                for (int j = 0; j < vertices.Length; j++)
                {
                    intPoints[j] = new OpenCvSharp.Point((int)vertices[j].X, (int)vertices[j].Y);
                }

                // Si el contorno tiene cuatro esquinas, es un rectángulo
                if (intPoints.Length == 4)
                {
                    // Dibujar el rectángulo
                    for (int j = 0; j < intPoints.Length; j++)
                    {
                        Cv2.Line(image, intPoints[j], intPoints[(j + 1) % 4], Scalar.Red, 2);
                    }
                }
            }
            // Mostrar la imagen con los rectángulos dibujados
            Cv2.NamedWindow("Rectangles", WindowFlags.GuiNormal);
            Cv2.ImShow("Rectangles", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            string imagePath = @"C:\Activa\Siemens\image.jpg";
            Mat image = Cv2.ImRead(imagePath, ImreadModes.Color);

            // Convertir la imagen a escala de grises
            Mat grayImage = new Mat();
            Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);

            // Aplicar umbralización (binarización) para obtener una imagen binaria
            Mat binaryImage = new Mat();
            //Cv2.Threshold(grayImage, binaryImage, 127, 255, ThresholdTypes.Binary);
            Cv2.Canny(grayImage, binaryImage, 50, 200);
            // Mostrar la imagen con los contornos dibujados
            Cv2.NamedWindow("Canny", WindowFlags.GuiNormal);
            Cv2.ImShow("Canny", binaryImage);
            Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();

            // Encontrar contornos en la imagen binaria
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binaryImage, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);



            //// Encontrar el contorno con el área más grande
            //double maxArea = 0;
            //int maxContourIndex = -1;
            //for (int i = 0; i < contours.Length; i++)
            //{
            //    double area = Cv2.ContourArea(contours[i]);
            //    if (area > maxArea)
            //    {
            //        maxArea = area;
            //        maxContourIndex = i;
            //    }
            //}

            int maxContourIndex = -1;
            int xIxkinaMin=5000000;
            int yIxkinaMin=5000000;
            for (int i = 0; i < contours.Length; i++)
            {
                int xIxkina = contours[i][0].X;
                int yIxkina = contours[i][0].Y;
                if (xIxkina < xIxkinaMin &&  yIxkina < yIxkinaMin)
                {
                    xIxkinaMin = xIxkina;
                    yIxkinaMin = yIxkina;
                    maxContourIndex = i;
                }
            }
            

            //// Dibujar el rectángulo más grande si se encontró algún contorno
            //if (maxContourIndex != -1)
            //{
            //    RotatedRect rect = Cv2.MinAreaRect(contours[maxContourIndex]);
            //    Point2f[] vertices = Cv2.BoxPoints(rect);

            //    // Convertir los puntos a Point
            //    OpenCvSharp.Point[] intPoints = new OpenCvSharp.Point[vertices.Length];
            //    for (int j = 0; j < vertices.Length; j++)
            //    {
            //        intPoints[j] = new OpenCvSharp.Point((int)vertices[j].X, (int)vertices[j].Y);
            //    }

            //    // Dibujar el rectángulo
            //    for (int j = 0; j < intPoints.Length; j++)
            //    {
            //        Cv2.Line(image, intPoints[j], intPoints[(j + 1) % 4], Scalar.Red, 2);
            //    }
            //}


            // Dibujar contornos en la imagen original
            Scalar color = Scalar.Red;
            int thickness = 10;
            //for (int i = 0; i < contours.Length; i++)
            //{
                Cv2.DrawContours(image, contours, maxContourIndex, color, thickness);
            //}


            // Mostrar la imagen con los rectángulos dibujados
            Cv2.NamedWindow("Rectangles", WindowFlags.GuiNormal);
            Cv2.ImShow("Rectangles", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();



            OpenCvSharp.Point[][] contoursIurgi = new OpenCvSharp.Point[contours.Length - 1][];

            int z = 0;
            for (int i = 0; i < contours.Length; i++)
            {
                if (i != maxContourIndex)
                {
                    contoursIurgi[z] = contours[i];
                    z++;
                }
                else
                    z = z;
            }



            Cv2.FillPoly(image, contoursIurgi, color);
            Cv2.NamedWindow("Contours", WindowFlags.GuiNormal);
            Cv2.ImShow("Contours", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

            //-------------------------------------------

            //// Dibujar el contorno más grande en una imagen de máscara
            //Mat mask = new Mat(image.Size(), MatType.CV_8UC1, Scalar.Black);
            //Cv2.DrawContours(mask, contours, maxContourIndex, Scalar.White, thickness: -1);

            //// Crear una imagen para almacenar los contornos filtrados
            //Mat result = new Mat(image.Size(), MatType.CV_8UC3, Scalar.Black);

            //// Filtrar los contornos basados en su jerarquía
            //for (int i = 0; i < contours.Length; i++)
            //{
            //    if (i != maxContourIndex && hierarchy[i].Parent != maxContourIndex)
            //    {
            //        Cv2.DrawContours(result, contours, i, Scalar.White, thickness: -1);
            //    }
            //}

            //// Mostrar la imagen resultante
            //Cv2.NamedWindow("Result", WindowFlags.GuiNormal);
            //Cv2.ImShow("Result", result);
            //Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();









        }

        private void button6_Click(object sender, EventArgs e)
        {
            string classpath = @"C:\Activa\Siemens\custom haar cascade\classifier\cascade.xml";
            string imagePath = @"C:\Activa\Siemens\image.jpg";

            // Cargar la imagen
            Mat image = Cv2.ImRead(imagePath);

            // Convertir la imagen a escala de grises
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

            // Cargar el clasificador en cascada de Haar
            var faceCascade = new CascadeClassifier(classpath);

            // Detectar rostros en la imagen
            Rect[] faces = faceCascade.DetectMultiScale(
                gray,
                scaleFactor: 1.1,
                minNeighbors: 5,
                flags: HaarDetectionTypes.ScaleImage,
                minSize: new OpenCvSharp.Size(500, 300)
                );

            // Dibujar rectángulos alrededor de los rostros detectados
            foreach (var face in faces)
            {
                Cv2.Rectangle(image, face, Scalar.Red, 2);
            }

            // Mostrar la imagen con los rostros detectados
            Cv2.NamedWindow("Detected box", WindowFlags.GuiNormal);
            Cv2.ImShow("Detected box", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Ruta a la carpeta con las imágenes originales
            string inputFolder = @"C:\Activa\Siemens\p_sin tratar";
            // Ruta a la carpeta donde se guardarán las imágenes normalizadas
            string outputFolder = @"C:\Activa\Siemens\p tratados";
            // Tamaño deseado
            int desiredWidth = 30;
            int desiredHeight = 30;

            foreach (string file in Directory.GetFiles(inputFolder))
            {
                Mat img = Cv2.ImRead(file);
                if (img.Empty())
                {
                    Console.WriteLine($"Error al leer la imagen: {file}");
                    continue;
                }

                // Calcular el factor de escala para mantener la relación de aspecto
                double aspectRatio = Math.Min((double)desiredWidth / img.Width, (double)desiredHeight / img.Height);
                int newWidth = (int)(img.Width * aspectRatio);
                int newHeight = (int)(img.Height * aspectRatio);

                // Redimensionar la imagen
                Mat resizedImg = new Mat();
                Cv2.Resize(img, resizedImg, new OpenCvSharp.Size(newWidth, newHeight));

                // Crear una imagen de destino con el tamaño deseado y color de fondo (negro)
                Mat normalizedImg = new Mat(new OpenCvSharp.Size(desiredWidth, desiredHeight), MatType.CV_8UC3, Scalar.White);

                // Calcular las coordenadas para centrar la imagen redimensionada
                int xOffset = (desiredWidth - newWidth) / 2;
                int yOffset = (desiredHeight - newHeight) / 2;

                // Copiar la imagen redimensionada al centro de la imagen de destino
                Rect roi = new Rect(xOffset, yOffset, newWidth, newHeight);
                resizedImg.CopyTo(new Mat(normalizedImg, roi));

                // Guardar la imagen normalizada
                string outputFileName = Path.Combine(outputFolder, Path.GetFileName(file));
                Cv2.ImWrite(outputFileName, normalizedImg);
            }

            Console.WriteLine("Normalización completada.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Ruta a la carpeta con las imágenes originales
            string inputFolder = @"C:\Activa\Siemens\p_sin tratar";
            // Ruta a la carpeta donde se guardarán las imágenes normalizadas
            string outputFolder = @"C:\Activa\Siemens\p tratados";


            // Ruta de la carpeta con las imágenes
            string carpetaImagenes = inputFolder;
            // Ruta de la carpeta donde guardar las imágenes redimensionadas
            string carpetaDestino = outputFolder;

            // Definir la nueva anchura deseada
            int nuevaAnchura = 500; // por ejemplo, 500 píxeles

            // Crear la carpeta destino si no existe
            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            // Obtener todos los archivos de imagen en la carpeta
            string[] archivosImagen = Directory.GetFiles(carpetaImagenes, "*.*", SearchOption.TopDirectoryOnly);

            foreach (string archivoImagen in archivosImagen)
            {
                // Ignorar archivos que no son imágenes
                if (!archivoImagen.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                    !archivoImagen.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) &&
                    !archivoImagen.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                    !archivoImagen.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) &&
                    !archivoImagen.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Cargar la imagen
                Mat imagen = Cv2.ImRead(archivoImagen);

                // Obtener las dimensiones originales de la imagen
                int alturaOriginal = imagen.Rows;
                int anchuraOriginal = imagen.Cols;

                // Calcular la relación de aspecto y la nueva altura manteniendo esa relación
                float relacionAspecto = (float)alturaOriginal / anchuraOriginal;
                int nuevaAltura = (int)(nuevaAnchura * relacionAspecto);

                // Redimensionar la imagen
                Mat imagenRedimensionada = new Mat();
                Cv2.Resize(imagen, imagenRedimensionada, new OpenCvSharp.Size(nuevaAnchura, nuevaAltura), 0, 0, InterpolationFlags.Area);

                // Generar el nombre del archivo destino
                string nombreArchivoDestino = Path.Combine(carpetaDestino, Path.GetFileName(archivoImagen));

                // Guardar la imagen redimensionada
                Cv2.ImWrite(nombreArchivoDestino, imagenRedimensionada);

                // Liberar los recursos de las imágenes
                imagen.Dispose();
                imagenRedimensionada.Dispose();
            }
        }
    }
}
