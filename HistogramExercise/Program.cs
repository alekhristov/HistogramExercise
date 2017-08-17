﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HistogramExercise
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string urlAddress = "http://yesofcorsa.com/red/";
            //string urlAddress = "https://www.wallpaper.com/";

            var listOfImagePaths = new List<string>();
            var listOfHistogramPaths = new List<string>();
            var dictOfColors = new Dictionary<BasicColor, int>();
            var imageTopColor = new Dictionary<string, string>();

            try
            {
                var imageParser = new ImageParser(urlAddress, listOfImagePaths);
                imageParser.ExtractImages();
                imageParser.DownloadImagesFromUrl();
            }

            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            string[] fileEntries = Directory.GetFiles(@"C:\Users\alek.hristov\Desktop\HistogramTask\DownloadedPictures");

            //var histogram = new Histogram();
            Histogram.CreateCommonExcelFile();
            var counter = 1;

            foreach (string pictureName in fileEntries)
            {
                var topColor = new Dictionary<string, int>();
                var picture = new Picture(pictureName);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Getting image pixels and colors of image: {pictureName}...");
                picture.GetImagePixelsAndGetTheirColors(dictOfColors, topColor);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Creating color histogram of image: {pictureName}...");
                Histogram.CreateColorHistrogram(pictureName, listOfHistogramPaths);

                if (!imageTopColor.ContainsKey(pictureName))
                {
                    imageTopColor[pictureName] = picture.ImageTopColor;
                }

                var histogram = new Histogram(pictureName, string.Format($@"C:\Users\alek.hristov\Desktop\HistogramTask\{counter}-Histogram.xlsx"));
                counter++;
                histogram.ExportToExcel();
            }

            Histogram.ExportHistogramsToExcel(listOfHistogramPaths);

            CreateTopColorsFolders(dictOfColors);
            SavePicturesToNewFolders(imageTopColor);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("CONGRATULATIONS YOUR TASK IS COMPLETED!!!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SavePicturesToNewFolders(Dictionary<string, string> imageTopColor)
        {
            string[] filesindirectory = Directory.GetDirectories(@"C:\Users\alek.hristov\Desktop\HistogramTask");

            foreach (var kvp in imageTopColor)
            {
                foreach (var subdir in filesindirectory)
                {
                    string folderColor = subdir.Substring(subdir.LastIndexOf(@"\") + 1);
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