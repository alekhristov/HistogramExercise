using System;
using System.Collections.Generic;
using System.Drawing;

namespace HistogramExercise
{
    internal class Picture
    {
        private string name;

        public Picture(string name)
        {
            this.name = name;
        }

        public Dictionary<BasicColor, int> GetImagePixelsAndGetTheirColors(Dictionary<BasicColor, int> dictOfColors)
        {
            using (var bitmap = new Bitmap(name))
            {
                GetPixels(bitmap, dictOfColors);
            }
            return dictOfColors;
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
            BasicColor nearestColor = BasicColor.BasicColorsArray[0];

            double r = pixel.R;
            double g = pixel.G;
            double b = pixel.B;

            foreach (var basicColor in BasicColor.BasicColorsArray)
            {
                distance = Math.Sqrt(Math.Abs(Math.Pow((basicColor.Color.R - r) * (0.3), 2) + Math.Pow((basicColor.Color.R - g) * (0.59), 2) + Math.Pow((basicColor.Color.R - b) * (0.11), 2)));

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestColor = basicColor;
                }
            }
            if (!dictOfColors.ContainsKey(nearestColor))
            {
                dictOfColors[nearestColor] = 0;
            }
            if (dictOfColors.ContainsKey(nearestColor))
            {
                dictOfColors[nearestColor]++;
            }
        }
    }
}