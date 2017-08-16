using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HistogramExercise
{
    internal class ImageParser
    {
        private const string DOWNLOADFILEPATH = @"C:\Users\alek.hristov\Desktop\HistogramTask\DownloadedPictures\";

        public ImageParser(string urlAddress, List<string> listOfImagePaths)
        {
            this.UrlAddress = urlAddress;
            this.ListOfImagePaths = listOfImagePaths;
        }

        public string UrlAddress
        {
            get; private set;
        }
        public List<string> ListOfImagePaths
        {
            get; private set;
        }

        private string LoadUrl()
        {
            string htmlString = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlAddress);
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

                htmlString = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return htmlString;
        }

        //Method should be void
        public void ExtractImages()
        {
            string htmlString = LoadUrl();

            var imgPattern = @"<img.+?=[\""'](?<imgUrl>(?:https?|www|\/).+?)[\\""'].*?>";

            var matchedImages = Regex.Matches(htmlString, imgPattern);

            foreach (Match match in matchedImages)
            {
                var imgUrl = match.Groups["imgUrl"].Value;

                if (imgUrl.EndsWith("png") || imgUrl.EndsWith("jpg") || imgUrl.EndsWith("jpeg"))
                {
                    ListOfImagePaths.Add(match.Groups["imgUrl"].Value);
                }
            }
        }
        public void DownloadImagesFromUrl()
        {
            using (WebClient webClient = new WebClient())
            {
                //https://msdn.microsoft.com/en-us/library/system.windows.forms.savefiledialog(v=vs.110).aspx use instead hardcoding the file paths.
                foreach (var image in ListOfImagePaths)
                {
                    //Use instead of string for the url address, instance of Url class (.Net) so you can avoid hardcoded indexes like that.
                    var primaryUrlEndIndex = UrlAddress.IndexOf(@"/", 9);
                    var primaryUrl = UrlAddress.Substring(0, primaryUrlEndIndex + 1);
                    var index = image.LastIndexOf(@"/");
                    var filePath = image.Substring(index + 1);

                    if (!File.Exists(DOWNLOADFILEPATH + filePath))
                    {
                        if (image.StartsWith("/"))
                        {
                            webClient.DownloadFile(new Uri(primaryUrl + image.Substring(1)), DOWNLOADFILEPATH + filePath);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Downloading image {image}");
                        }
                        else if (image.StartsWith(@"http"))
                        {
                            webClient.DownloadFile(new Uri(image), DOWNLOADFILEPATH + filePath);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Downloading image {image}");
                        }
                    }
                }
            }
        }
    }
}
