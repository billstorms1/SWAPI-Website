using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class StarShipProfileModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public JObject Result;
        public StarShipModel Profile;

        public StarShipProfileModel(IMakeRequest makeRequest)
        {
            MakeRequest = makeRequest;
        }

        public void OnGet(string url)
        {
            var decodedPath = HttpUtility.UrlDecode(url);
            Result = MakeRequest.GetSpecificData(decodedPath);
            BuildProfile(Result);
        }

        public void BuildProfile(JObject result)
        {
            Profile = new StarShipModel
            {
                Name = result.SelectToken("name").ToString(),
                Model = result.SelectToken("model").ToString(),
                Manufacturer = result.SelectToken("manufacturer").ToString(),
                CostInCredits = result.SelectToken("cost_in_credits").ToString(),
                Length = result.SelectToken("length").ToString(),
                MaxAtmospheringSpeed = result.SelectToken("max_atmosphering_speed").ToString(),
                Crew = result.SelectToken("crew").ToString(),
                Passengers = result.SelectToken("passengers").ToString(),
                CargoCapacity = result.SelectToken("cargo_capacity").ToString(),
                Consumables = result.SelectToken("consumables").ToString(),
                HyperdriveRating = result.SelectToken("hyperdrive_rating").ToString(),
                Mglt = result.SelectToken("MGLT").ToString(),
                StarShipClass = result.SelectToken("starship_class").ToString(),
                Pilots = GetNames("pilots"),
                Films = GetNames("films"),
                Url = result.SelectToken("url").ToObject<Uri>()
            };
        }

        public List<string> GetNames(string item)
        {
            var items = new List<string>();
            foreach (var result in Result.SelectToken(item))
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