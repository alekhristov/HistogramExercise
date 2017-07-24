﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HistogramExercise
{
    class ImageUrlDownloader
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

        public void DownloadImagesFromUrl(string urlAddress, List<string> listOfImagePaths)
        {
            using (WebClient webClient = new WebClient())
            {
                foreach (var image in listOfImagePaths)
                {
                    var index = image.LastIndexOf(@"/");
                    var filePath = image.Substring(index + 1);

                    if (!File.Exists(@"C:\Users\alek.hristov\Pictures\HistogramTask\" + filePath))
                    {
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
        }
    }
}
