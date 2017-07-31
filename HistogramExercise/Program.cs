using System;
using System.Collections.Generic;
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
    //	- export histogram in excel file(method in the class)
    //	- import excel file to histogram(method in the class)
    internal class Program
    {
        private static void Main(string[] args)
        {
            string urlAddress = "http://yesofcorsa.com/red/";

            var listOfImagePaths = new List<string>();
            var dictOfColors = new Dictionary<BasicColor, int>();

            try
            {
                var imageParser = new ImageParser(urlAddress);
                listOfImagePaths = imageParser.ExtractImages();

                var imageUrlDownloader = new ImageUrlDownloader(urlAddress);
                imageUrlDownloader.DownloadImagesFromUrl(listOfImagePaths);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            string[] fileEntries = Directory.GetFiles(@"C:\Users\alek.hristov\Pictures\HistogramTask\");

            var histogram = new Histogram();
            histogram.CreateExcelFile();

            foreach (string pictureName in fileEntries)
            {
                var topColor = new Dictionary<string, int>();
                var picture = new Picture(pictureName);
                picture.GetImagePixelsAndGetTheirColors(dictOfColors, topColor, histogram);
                histogram.CreateColorHistrogram(pictureName);
            }

            var topFiveColors = dictOfColors.OrderByDescending(a => a.Value).Take(5).ToList();
        }
    }
}