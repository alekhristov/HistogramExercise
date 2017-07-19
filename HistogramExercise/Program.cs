using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HistogramExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            //string urlAddress = "http://www.liverpoolfc.com/";

            var listOfImages = new List<string>();
            var dictOfColors = new Dictionary<string, int>();

            //GetImagesFromUrl(urlAddress, listOfImages);

            //DownloadImagesFromUrl(urlAddress, listOfImages);

            //Bitmap img = new Bitmap(@"C:\Users\alek.hristov\Pictures\HistogramTask\eden-hazard.thumbnail.png");
            //for (int i = 0; i < img.Width; i++)
            //{
            //    for (int j = 0; j < img.Height; j++)
            //    {
            //        Color pixel = img.GetPixel(i, j);
            //    }
            //}

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
                    Color pixel = bitmap.GetPixel(x, y);

                    FindingTheNearestColor(pixel, dictOfColors);
                }
            }
        }

        private static void FindingTheNearestColor(Color pixel, Dictionary<string, int> dictOfColors)
        {
            double distance = double.MaxValue;
            double minDistance = double.MaxValue;
            var colorName = string.Empty;

            double r = pixel.R;
            double g = pixel.G;
            double b = pixel.B;

            var black = new Colors(0, 0, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((black.R - r)*(0.3),2) + Math.Pow((black.G - g) * (0.59), 2) + Math.Pow((black.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "black";
            }
            var white = new Colors(255, 255, 255);
            distance = Math.Sqrt(Math.Abs(Math.Pow((white.R - r) * (0.3), 2) + Math.Pow((white.G - g) * (0.59), 2) + Math.Pow((white.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "white";
            }
            var red = new Colors(255, 0, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((red.R - r) * (0.3), 2) + Math.Pow((red.G - g) * (0.59), 2) + Math.Pow((red.B - b) * (0.11), 2)));

            if (Math.Abs(distance) < minDistance)
            {
                minDistance = distance;
                colorName = "red";
            }
            var blue = new Colors(0, 0, 255);
            distance = Math.Sqrt(Math.Abs(Math.Pow((blue.R - r) * (0.3), 2) + Math.Pow((blue.G - g) * (0.59), 2) + Math.Pow((blue.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "blue";
            }
            var yellow = new Colors(255, 255, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((yellow.R - r) * (0.3), 2) + Math.Pow((yellow.G - g) * (0.59), 2) + Math.Pow((yellow.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "yellow";
            }
            var orange = new Colors(255, 165, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((orange.R - r) * (0.3), 2) + Math.Pow((orange.G - g) * (0.59), 2) + Math.Pow((orange.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "orange";
            }
            var grey = new Colors(128, 128, 128);
            distance = Math.Sqrt(Math.Abs(Math.Pow((grey.R - r) * (0.3), 2) + Math.Pow((grey.G - g) * (0.59), 2) + Math.Pow((grey.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "grey";
            }
            var green = new Colors(0, 128, 0);
            distance = Math.Sqrt(Math.Abs(Math.Pow((green.R - r) * (0.3), 2) + Math.Pow((green.G - g) * (0.59), 2) + Math.Pow((green.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "green";
            }
            var purple = new Colors(128, 0, 128);
            distance = Math.Sqrt(Math.Abs(Math.Pow((purple.R - r) * (0.3), 2) + Math.Pow((purple.G - g) * (0.59), 2) + Math.Pow((purple.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "purple";
            }
            var pink = new Colors(128, 0, 128);
            distance = Math.Sqrt(Math.Abs(Math.Pow((pink.R - r) * (0.3), 2) + Math.Pow((pink.G - g) * (0.59), 2) + Math.Pow((pink.B - b) * (0.11), 2)));

            if (distance < minDistance)
            {
                minDistance = distance;
                colorName = "pink";
            }
            var brown = new Colors(160, 82, 45);
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

        private static void DownloadImagesFromUrl(string urlAddress, List<string> listOfImages)
        {
            using (WebClient webClient = new WebClient())
            {
                foreach (var image in listOfImages)
                {
                    var index = image.LastIndexOf(@"/");
                    var filePath = image.Substring(index + 1);

                    if (image.StartsWith("/"))
                    {
                        webClient.DownloadFile(new Uri(urlAddress + image.Substring(1)), @"C:\Users\alek.hristov\Pictures\HistogramTask\" + filePath);
                    }
                    else if (image.StartsWith(@"http://"))
                    {
                        webClient.DownloadFile(new Uri(image), @"C:\Users\alek.hristov\Pictures\HistogramTask\" + filePath);
                    }

                }

            }
        }

        private static void GetImagesFromUrl(string urlAddress, List<string> listOfImages)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                var imgPattern = @"<img.+?src=[\""'](?<imgUrl>(?:https?|www|\/).+?)[\\""'].*?>";

                var matchedImages = Regex.Matches(data, imgPattern);

                foreach (Match match in matchedImages)
                {
                    var imgUrl = match.Groups["imgUrl"].Value;

                    if (imgUrl.EndsWith("png") || imgUrl.EndsWith("jpg") || imgUrl.EndsWith("jpeg"))
                    {
                        listOfImages.Add(match.Groups["imgUrl"].Value);
                    }
                }
            }
        }
    }
}
