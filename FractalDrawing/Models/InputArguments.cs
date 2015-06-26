using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotDrawing.Models
{
     public class InputArguments
    {
        private  double realMin = -2;
        private  double realMax = 2;
        private  double imaginaryMin = -2;
        private  double imaginaryMax = 2;

        private readonly int width=640;
        private readonly int height=480;

        private readonly int numberOfThreads = 1;

         public int Width { get; set; }
         public int Height { get; set; }

         public List<double> Coordinates { get; set; }
         public int Threads { get; set; }
         //public string ImagePath { get; set; }

         public InputArguments(string widthParam,string heightParam,string coordinates,string threadNum)
         {
            this.Coordinates = this.ValidateArguments(coordinates);

            this.Width = int.TryParse(widthParam, out width) ? width : 640;

            this.Height = int.TryParse(heightParam, out height) ? height : 480;

            this.Threads = int.TryParse(threadNum, out numberOfThreads) ? numberOfThreads : 1;

         }


        private  List<double> ValidateArguments(string complexPlaneSize)
        {
            var complexPlaneCoordinates = complexPlaneSize.Split(':');

            if (complexPlaneCoordinates.Length != 4)
            {
                complexPlaneSize = "-2.0:2.0:-2.0:2.0";
                complexPlaneCoordinates = complexPlaneSize.Split(':');
            }

            var result = new List<double>();
            result.Add(double.TryParse(complexPlaneCoordinates[0], out realMin) ? realMin : -2.0);
            result.Add(double.TryParse(complexPlaneCoordinates[1], out realMax) ? realMax : 2.0);
            result.Add(double.TryParse(complexPlaneCoordinates[2], out imaginaryMin) ? imaginaryMin : -2.0);
            result.Add(double.TryParse(complexPlaneCoordinates[3], out imaginaryMax) ? imaginaryMax : 2.0);
          
            return result;
        }
    }
}
