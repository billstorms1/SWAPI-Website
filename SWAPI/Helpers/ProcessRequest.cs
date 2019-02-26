using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SWAPI.Models;

namespace SWAPI.Helpers
{
    public interface IProcessRequest
    {
        List<string> GetResults(List<JObject> resultBlobs, string node);
        List<NameModel> CreateNameList(List<JObject> resultBlobs);
        List<NameModel> CreateTitleList(List<JObject> resultBlobs);
    }

    public class ProcessRequest : IProcessRequest
    {
        public NameModel NameList = new NameModel();

        public List<NameModel> CreateNameList(List<JObject> resultBlobs)
        {
            var names = new List<NameModel>();
            foreach (var blob in resultBlobs)
            foreach (var result in blob.SelectToken("results"))
                names.Add(new NameModel
                {
                    Name = result.SelectToken("name").ToString(),
                    Url = result.SelectToken("url").ToObject<Uri>()
                });

            return names;
        }

        public List<NameModel> CreateTitleList(List<JObject> resultBlobs)
        {
            var names = new List<NameModel>();
            foreach (var blob in resultBlobs)
            foreach (var result in blob.SelectToken("results"))
                names.Add(new NameModel
                {
                    Name = result.SelectToken("title").ToString(),
                    Url = result.SelectToken("url").ToObject<Uri>()
                });

            return names;
        }

        public List<string> GetResults(List<JObject> resultBlobs, string node)
        {
            var results = new List<string>();
            foreach (var blob in resultBlobs)
                if (blob.SelectToken("results").SelectToken(node).ToString() == node)
                {
                    var count = blob.SelectToken("results").Children();
                    results.Add(blob.SelectTokens("results").ToString());
                }

            return results;
        }
    }
}