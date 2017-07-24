using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistogramExercise
{
    class Picture
    {
        private string name;
        private Dictionary<BasicColor, int> dictOfColors;

        public Picture(string name)
        {
            this.name = name;
            this.dictOfColors = new Dictionary<BasicColor, int>();
        }

        public void GetImagePixels(string name)
        {
            using (var bitmap = new Bitmap(name))
            {
                GetPixels(bitmap, dictOfColors);
            }
        }

        private void GetPixels(Bitmap bitmap, Dictionary<BasicColor, int> dictOfColors)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    System.Drawing.Color pixel = bitmap.GetPixel(x, y);

                    FindingTheNearestColor(pixel, dictOfColors);
                }
            }
        }

        private void FindingTheNearestColor(System.Drawing.Color pixel, Dictionary<BasicColor, int> dictOfColors)
        {
            double distance = double.MaxValue;
            double minDistance = double.MaxValue;
            var colorName = string.Empty;

            double r = pixel.R;
            double g = pixel.G;
            double b = pixel.B;

            foreach (var basicColor in BasicColor.BasicColorsArray)
            {
                distance = Math.Sqrt(Math.Abs(Math.Pow((basicColor.Color.R - r) * (0.3), 2) + Math.Pow((basicColor.Color.R - g) * (0.59), 2) + Math.Pow((basicColor.Color.R - b) * (0.11), 2)));

                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }
    }
}
