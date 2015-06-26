using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MandelbrotDrawing.Models;
using Color = System.Windows.Media.Color;

namespace MandelbrotDrawing
{
        public class CustomBitmap
        {
          

            public CustomBitmap()
            {
                
            }
            public CustomBitmap(int width, int height)
            {
                this.Bitmap = new Bitmap(width, height, PixelFormat.Format48bppRgb);
            }

            public Bitmap Bitmap
            {
                get;
                set;
            }


            public ImageAndTime DrawMandelbrot(InputArguments arguments)
            {

                var palette = GenerateColorPalette();
                var img = new CustomBitmap(arguments.Width, arguments.Height);

                var realScale = (Math.Abs(arguments.Coordinates[0]) + Math.Abs(arguments.Coordinates[1])) / arguments.Width; // Отстояние на което да преместим пикселите в реални числа
                var imaginaryScale = (Math.Abs(arguments.Coordinates[2]) + Math.Abs(arguments.Coordinates[3])) / arguments.Height; // Отстояние на което да преместим пикселите в комплексни числа
                
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var rectangle = new Rectangle(0, 0, img.Bitmap.Width, img.Bitmap.Height);
                var data = img.Bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format48bppRgb);
                var height = arguments.Height;
                var width = arguments.Width;
                var coordinatesF = arguments.Coordinates[0];
                var coordinatesS = arguments.Coordinates[2];
                var numberOfThreads = arguments.Threads;

                Parallel.For(0, width, new ParallelOptions { MaxDegreeOfParallelism = numberOfThreads }, x =>
                    {
                        for (var y = 0; y < height; y++)
                        {
                            //скалиране на комплексното число
                            var c = new Complex(x * realScale + coordinatesF, y * imaginaryScale + coordinatesS);
                            var z = c;
                            foreach (var t in palette)
                            {
                                if (z.Magnitude >= 2.0)
                                {
                                    
                                    img.SetPixel(x, y, t,data);
                        
                                    break; 
                                }
                                z = c + Complex.Pow(z, 2); // Z = Zlast^2 + C
                            }
                        }
                    });
                img.Bitmap.UnlockBits(data); 
                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed);

                return new ImageAndTime{Bitmap=img.Bitmap,TimeForRender = stopwatch.Elapsed};
            }

            private List<Color> GenerateColorPalette()
            {
                var retVal = new List<Color>();
                for (int i = 0; i <= 255; i++)
                {
                    retVal.Add(Color.FromArgb(255, (byte)i, (byte)i, 255));
                }
                return retVal;
            }

            private unsafe void SetPixel(int x, int y, Color color,BitmapData data)
            {

                var scan0 = data.Scan0;

                byte* imagePointer = (byte*)scan0.ToPointer(); // Пойнтер към първия пиксел на картината
                int offset = (y * data.Stride) + (6 * x); // 6*x защото имаме 48bits/px = 6bytes/px
                byte* px = (imagePointer + offset); // Пойнтер към пиксела, който ни трябва
                px[0] = color.B;
                px[1] = color.G;
                px[2] = color.R; 

               
            }
        }
    }

