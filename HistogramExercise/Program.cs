using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace HistogramExercise
{
    //    HTML Parser, който търси всички URL-и на картинки, картинките могат да са.JPG, .PNG;
    //    След това трябва да бъдат свалени в папка на компютъра.

    //    За всяка записана картинка, създаваме един Histogram generator, който е записан в един excel file.
    //    http://csharp.net-informations.com/excel/csharp-excel-chart.htm

    //    Histogram Generator, минаваме през всички свалени картинки и правим 5 папки на диска, в които ще има всички картинки, които съдържат най-използваните цветове. Трябва да се намерят петте топ цвята.
    //    В петте папки записваме по поне една картинка(в папка Blue - отиват картинките с топ-цвят синьо).

    //To design a class, which contains all the color clusters, and has Name, Color, Fields with coefficients(0.3, 0.59, 0.11), и проверката да стане с foreach(за mindistance) - всичко ми е fields
    //Class Picture - imagePath, imageTopColor, class в Picture - Histogram(); GetPixels() минава в class Picture

    //Class Histogram:
    //	- create histogram with input pic file path
    //	- export histogram in excell file(method in the class)
    //	- import excell file to histogram(method in the class)
    internal class Program
    {
        private static void Main(string[] args)
        {
            string urlAddress = "http://www.chelseafc.com/";

            var listOfImagePaths = new List<string>();
            var dictOfColors = new Dictionary<string, int>();

            try
            {
                var imageParser = new ImageParser(urlAddress);
                var htmlString = imageParser.HtmlParser(urlAddress);
                listOfImagePaths = imageParser.ImageHtmlExtractor(urlAddress, listOfImagePaths, htmlString);

                var imageUrlDownloader = new ImageUrlDownloader(urlAddress);
                imageUrlDownloader.DownloadImagesFromUrl(urlAddress, listOfImagePaths);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            string[] fileEntries = Directory.GetFiles(@"C:\Users\alek.hristov\Pictures\HistogramTask\");

            foreach (string image in fileEntries)
            {
                using (var bitmap = new Bitmap(image))
                {
                    GetPixels(bitmap, dictOfColors);
                }
            }

            var topFiveColors = dictOfColors.OrderByDescending(a => a.Value).Take(5).ToList();
        }

        public static void GetPixels(Bitmap bitmap, Dictionary<string, int> dictOfColors)
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

        private static void FindingTheNearestColor(System.Drawing.Color pixel, Dictionary<string, int> dictOfColors)
        {
            double distance = double.MaxValue;
            double minDistance = double.MaxValue;
            var colorName = string.Empty;

            double r = pixel.R;
            double g = pixel.G;
            double b = pixel.B;

            var black = new Color(0, 0, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((black.R - r) * (0.3), 2) + Math.Pow((black.G - g) * (0.59), 2) + Math.Pow((black.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "black";
            }
            var white = new Color(255, 255, 255);
            distance = Math.Sqrt(Math.Abs(Math.Pow((white.R - r) * (0.3), 2) + Math.Pow((white.G - g) * (0.59), 2) + Math.Pow((white.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "white";
            }
            var red = new Color(255, 0, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((red.R - r) * (0.3), 2) + Math.Pow((red.G - g) * (0.59), 2) + Math.Pow((red.B - b) * (0.11), 2)));

            if (Math.Abs(distance) < minDistance)
            {
                minDistance = distance;
                colorName = "red";
            }
            var blue = new Color(0, 0, 255);
            distance = Math.Sqrt(Math.Abs(Math.Pow((blue.R - r) * (0.3), 2) + Math.Pow((blue.G - g) * (0.59), 2) + Math.Pow((blue.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "blue";
            }
            var yellow = new Color(255, 255, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((yellow.R - r) * (0.3), 2) + Math.Pow((yellow.G - g) * (0.59), 2) + Math.Pow((yellow.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "yellow";
            }
            var orange = new Color(255, 165, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((orange.R - r) * (0.3), 2) + Math.Pow((orange.G - g) * (0.59), 2) + Math.Pow((orange.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "orange";
            }
            var grey = new Color(128, 128, 128);
            distance = Math.Sqrt(Math.Abs(Math.Pow((grey.R - r) * (0.3), 2) + Math.Pow((grey.G - g) * (0.59), 2) + Math.Pow((grey.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "grey";
            }
            var green = new Color(0, 128, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((green.R - r) * (0.3), 2) + Math.Pow((green.G - g) * (0.59), 2) + Math.Pow((green.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "green";
            }
            var purple = new Color(128, 0, 128);
            distance = Math.Sqrt(Math.Abs(Math.Pow((purple.R - r) * (0.3), 2) + Math.Pow((purple.G - g) * (0.59), 2) + Math.Pow((purple.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "purple";
            }
            var pink = new Color(128, 0, 128);
            distance = Math.Sqrt(Math.Abs(Math.Pow((pink.R - r) * (0.3), 2) + Math.Pow((pink.G - g) * (0.59), 2) + Math.Pow((pink.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "pink";
            }
            var brown = new Color(160, 82, 45);
            distance = Math.Sqrt(Math.Abs(Math.Pow((brown.R - r) * (0.3), 2) + Math.Pow((brown.G - g) * (0.59), 2) + Math.Pow((brown.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "brown";
            }

            if (!dictOfColors.ContainsKey(colorName))
            {
                dictOfColors[colorName] = 0;
            }
            if (dictOfColors.ContainsKey(colorName))
            {
                dictOfColors[colorName]++;
            }
        }
    }
}