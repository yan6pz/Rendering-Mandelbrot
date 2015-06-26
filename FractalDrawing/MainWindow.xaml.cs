using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;
using MandelbrotDrawing.Models;

namespace MandelbrotDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
        {
            var ip = source.GetHbitmap();

            var bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip
                                                                            ,IntPtr.Zero
                                                                            , Int32Rect.Empty
                                                                            ,BitmapSizeOptions.FromEmptyOptions());
          
         

            return bs;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var fractalCreating = new CustomBitmap();
            var argumentsParser = new InputArguments(WidthBox.Text, HeightBox.Text, CoordinatesBox.Text, ThreadsNumBox.Text);


            var drewObject = fractalCreating.DrawMandelbrot(argumentsParser);
            drewObject.Bitmap.Save(ImageNameBox.Text, ImageFormat.Jpeg);
            Image.Source = LoadBitmap(drewObject.Bitmap);
            stopWatch.Stop();

            AllTimeBox.Text = stopWatch.Elapsed.ToString();
            RenderTimeBox.Text = drewObject.TimeForRender.ToString();

        }
    }
}
