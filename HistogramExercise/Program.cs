using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HistogramExercise
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //string urlAddress = "http://yesofcorsa.com/red/";
            string urlAddress = "https://www.wallpaper.com/";

            var listOfImagePaths = new List<string>();
            var listOfHistogramPaths = new List<string>();
            var dictOfColors = new Dictionary<BasicColor, int>();
            var imageTopColor = new Dictionary<string, string>();

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
                histogram.CreateColorHistrogram(pictureName, listOfHistogramPaths);

                if (!imageTopColor.ContainsKey(pictureName))
                {
                    imageTopColor[pictureName] = picture.ImageTopColor;
                }
            }
            histogram.ExportHistogramsToExcel(listOfHistogramPaths);

            CreateTopColorsFolders(dictOfColors);
            SavePicturesToNewFolders(imageTopColor);
        }

        private static void SavePicturesToNewFolders(Dictionary<string, string> imageTopColor)
        {
            string[] filesindirectory = Directory.GetDirectories(@"C:\Users\alek.hristov\Desktop\HistogramTask");

            foreach (var kvp in imageTopColor)
            {
                foreach (var subdir in filesindirectory)
                {
                    string folderColor = subdir.Substring(subdir.LastIndexOf(@"\")+1);
                    if (kvp.Value == folderColor) 
                    {
                        string sourcePath = kvp.Key;
                        string targetPath = $@"{subdir}{kvp.Key.Substring(kvp.Key.LastIndexOf(@"\"))}";

                        File.Copy(sourcePath, targetPath, true);
                    }
                }
            }
        }

        private static void CreateTopColorsFolders(Dictionary<BasicColor, int> dictOfColors)
        {
            foreach (var kvp in dictOfColors.OrderByDescending(a => a.Value).Take(5))
            {
                var topColorName = kvp.Key.Name;

                string activeDir = @"C:\Users\alek.hristov\Desktop\HistogramTask";
                string newPath = Path.Combine(activeDir, $"{topColorName}");
                Directory.CreateDirectory(newPath);
            }
        }
    }
}