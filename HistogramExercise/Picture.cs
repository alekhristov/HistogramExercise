﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HistogramExercise
{
    internal class Picture
    {
        private string name;
        
        //Remove
        private string imageTopColor;

        public Picture(string name)
        {
            this.name = name;
            // ??
            this.ImageTopColor = imageTopColor;
        }
        public string ImageTopColor { get; set; }

        public Dictionary<BasicColor, int> GetImagePixelsAndGetTheirColors(Dictionary<BasicColor, int> dictOfColors, Dictionary<string, int> topColors, Histogram histogram)
        {
            using (var bitmap = new Bitmap(name))
            {
                GetPixels(bitmap, dictOfColors, topColors, histogram);
            }
            
            return dictOfColors;
        }

        //Get rid of th Histogram dependency.
        private void GetPixels(Bitmap bitmap, Dictionary<BasicColor, int> dictOfColors, Dictionary<string, int> topColors, Histogram histogram)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    System.Drawing.Color pixel = bitmap.GetPixel(x, y);

                    FindingTheNearestColor(pixel, dictOfColors, topColors);
                }
            }
            
            foreach (var color in topColors.OrderByDescending(a => a.Value).Take(1))
            {
                imageTopColor = color.Key;
                ImageTopColor = imageTopColor;
                
                //Get rid of th Histogram dependency.
                histogram.FillDataInExcelFile(imageTopColor, name);
            }
        }

        private void FindingTheNearestColor(System.Drawing.Color pixel, Dictionary<BasicColor, int> dictOfColors, Dictionary<string, int> topColors)
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
            
            if (!topColors.ContainsKey(currentColor))
            {
                topColors[currentColor] = 0;
            }                
            
            if (!dictOfColors.ContainsKey(nearestColor))
            {
                dictOfColors[nearestColor] = 0;
            }
            
            topColors[currentColor]++;
            dictOfColors[nearestColor]++;
        }
    }
}
