using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotDrawing.Models
{
    public class ImageAndTime
    {
        public Bitmap Bitmap { get; set; }
        public TimeSpan TimeForRender { get; set; }
    }
}
