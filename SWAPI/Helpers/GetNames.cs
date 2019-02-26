using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SWAPI.Helpers
{
    public interface IGetNames
    {
        List<string> Get(string item, JObject data);
    }

    public class GetNames : IGetNames
    {
        public IMakeRequest MakeRequest;

        public GetNames(IMakeRequest makeRequest)
        {
            MakeRequest = makeRequest;
        }

        public List<string> Get(string item, JObject data)
        {
            var items = new List<string>();
            foreach (var result in data.SelectToken(item))
            {
                var link = result.ToString();
                var itemResult = MakeRequest.GetSpecificData(link);

                if (item == "films") //Stupid change in convention in the API
                    items.Add(itemResult.SelectToken("title").ToString());
                else
                    items.Add(itemResult.SelectToken("name").ToString());
            }

            return items;
        }
    }
}
