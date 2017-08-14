using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HistogramExercise
{
    //Unite with ImageParser
    internal class ImageUrlDownloader
    {
        private const stirng DOWNLOADFILEPATH = @"C:\Users\alek.hristov\Desktop\HistogramTask\DownloadedPictures\";
        //Remove
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
                //https://msdn.microsoft.com/en-us/library/system.windows.forms.savefiledialog(v=vs.110).aspx use instead hardcoding the file paths.
                foreach (var image in listOfImagePaths)
                {
                    var primaryUrlEndIndex = urlAddress.IndexOf(@"/", 9);
                    var primaryUrl = urlAddress.Substring(0, primaryUrlEndIndex+1);
                    var index = image.LastIndexOf(@"/");
                    var filePath = image.Substring(index + 1);

                    if (!File.Exists(DOWNLOADFILEPATH + filePath))
                    {
                        if (image.StartsWith("/"))
                        {
                            webClient.DownloadFile(new Uri(primaryUrl + image.Substring(1)), DOWNLOADFILEPATH + filePath);
                        }
                        else if (image.StartsWith(@"http"))
                        {
                            webClient.DownloadFile(new Uri(image), DOWNLOADFILEPATH + filePath);
                        }
                    }
                }
            }
        }
    }
}
