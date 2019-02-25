using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Web;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;

namespace SWAPI.Helpers
{
    public interface IRequestHandler
    {
        List<JObject> MakeGeneralRequest(string query);
    }

    public class RequestHandler : WebRequest, IRequestHandler
    {
        public static List<JObject> ResultBlobs;

        public RequestHandler()
        {
            ResultBlobs = new List<JObject>();
        }

        private JObject MakeRequest(string query)
        {
            var request = (HttpWebRequest) Create(query);
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            var content = string.Empty;
            using (var response = (HttpWebResponse) request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                        using (var sr = new StreamReader(stream))
                        {
                            content = sr.ReadToEnd();
                        }
                }
            }

            return JObject.Parse(content);
        }

        public List<JObject> MakeGeneralRequest(string query)
        {
            var resultBlob = MakeRequest(query);
            var prevpage = resultBlob.SelectToken("previous");
            var nextPageValue = resultBlob.SelectToken("next");

            if (prevpage == null || prevpage.ToString() == "")
                ResultBlobs = new List<JObject>();

            ResultBlobs.Add(resultBlob);

            if (nextPageValue != null && nextPageValue.ToString() != string.Empty)
                MakeGeneralRequest(nextPageValue.ToString());

            return ResultBlobs;
        }
    }
}