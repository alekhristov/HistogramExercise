﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HistogramExercise
{
    internal class Picture
    {
        private string name;

        public Picture(string name)
        {
            this.name = name;
        }

        public Dictionary<BasicColor, int> GetImagePixelsAndGetTheirColors(Dictionary<BasicColor, int> dictOfColors, Dictionary<string, int> topColor, Histogram histogram)
        {
            using (var bitmap = new Bitmap(name))
            {
                GetPixels(bitmap, dictOfColors, topColor, histogram);
            }
            return dictOfColors;
        }

        private void GetPixels(Bitmap bitmap, Dictionary<BasicColor, int> dictOfColors, Dictionary<string, int> topColors, Histogram histogram)
        {
            long r = 0;
            long g = 0;
            long b = 0;
            var histDict = new Dictionary<double, long>();
            for (int i = 0; i <= 255; i++)
            {
                histDict.Add(i, 0);
            }

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    System.Drawing.Color pixel = bitmap.GetPixel(x, y);
                    r += pixel.R;
                    g += pixel.G;
                    b += pixel.B;

                    FindingTheNearestColor(pixel, dictOfColors, topColors);
                }
            }
            foreach (var color in topColors.OrderByDescending(a => a.Value).Take(1))
            {
                string imageTopColor = color.Key;
                histogram.FillDataInExcelFile(imageTopColor, name);
            }
        }

        private void FindingTheNearestColor(System.Drawing.Color pixel, Dictionary<BasicColor, int> dictOfColors, Dictionary<string, int> topColor)
        {
            double distance = double.MaxValue;
            double minDistance = double.MaxValue;
            BasicColor nearestColor = BasicColor.BasicColorsArray[0];
            var currentColor = string.Empty;

            double r = pixel.R;
            double g = pixel.G;
            double b = pixel.B;

            foreach (var basicColor in BasicColor.BasicColorsArray)
            {
                distance = Math.Sqrt(Math.Pow((basicColor.Color.R - r) * (0.3), 2) + Math.Pow((basicColor.Color.G - g) * (0.59), 2) + Math.Pow((basicColor.Color.B - b) * (0.11), 2));

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestColor = basicColor;
                    currentColor = (basicColor.Name).ToString();
                }
            }
            if (!topColor.ContainsKey(currentColor))
            {
                topColor[currentColor] = 0;
            }
            if (topColor.ContainsKey(currentColor))
            {
                topColor[currentColor]++;
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