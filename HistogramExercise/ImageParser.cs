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
        private string urlAddress;
        private List<string> listOfImagePaths;

        public ImageParser(string urlAddress)
        {
            this.UrlAddress = urlAddress;
            this.listOfImagePaths = new List<string>();
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

        private string LoadUrl()
        {
            string htmlString = string.Empty;
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

                htmlString = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return htmlString;
        }

        public List<string> ExtractImages()
        {
            string htmlString = LoadUrl();

            var imgPattern = @"<img.+?=[\""'](?<imgUrl>(?:https?|www|\/).+?)[\\""'].*?>";

            var matchedImages = Regex.Matches(htmlString, imgPattern);

            foreach (Match match in matchedImages)
            {
                var imgUrl = match.Groups["imgUrl"].Value;

                if (imgUrl.EndsWith("png") || imgUrl.EndsWith("jpg") || imgUrl.EndsWith("jpeg"))
                {
                    listOfImagePaths.Add(match.Groups["imgUrl"].Value);
                }
            }
            return listOfImagePaths;
        }
    }
}