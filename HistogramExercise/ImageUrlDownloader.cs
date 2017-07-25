using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HistogramExercise
{
    internal class ImageUrlDownloader
    {
        private string urlAddress;

        public ImageUrlDownloader(string urlAddress)
        {
            this.UrlAddress = urlAddress;
        }

        public string UrlAddress
        {
            get { return this.urlAddress; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Parameter cannot be null");
                }
                this.urlAddress = value;
            }
        }

        public void DownloadImagesFromUrl(List<string> listOfImagePaths)
        {
            using (WebClient webClient = new WebClient())
            {
                foreach (var image in listOfImagePaths)
                {
                    var primaryUrlEndIndex = urlAddress.IndexOf(@"/", 8);
                    var primaryUrl = urlAddress.Substring(0, primaryUrlEndIndex);
                    var index = image.LastIndexOf(@"/");
                    var filePath = image.Substring(index + 1);

                    if (!File.Exists(@"C:\Users\alek.hristov\Pictures\HistogramTask\" + filePath))
                    {
                        if (image.StartsWith("/"))
                        {
                            webClient.DownloadFile(new Uri(primaryUrl + image.Substring(1)), @"C:\Users\alek.hristov\Pictures\HistogramTask\" + filePath);
                        }
                        else if (image.StartsWith(@"http://"))
                        {
                            webClient.DownloadFile(new Uri(image), @"C:\Users\alek.hristov\Pictures\HistogramTask\" + filePath);
                        }
                    }
                }
            }
        }
    }
}