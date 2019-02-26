using System;
using System.Web;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class VehicleProfileModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IGetNames GetNames;
        public JObject Result;
        public VehicleModel Profile;

        public VehicleProfileModel(IMakeRequest makeRequest, IGetNames getNames)
        {
            MakeRequest = makeRequest;
            GetNames = getNames;
        }

        public void OnGet(string url)
        {
            var decodedPath = HttpUtility.UrlDecode(url);
            Result = MakeRequest.GetSpecificData(decodedPath);
            BuildProfile(Result);
        }

        public void BuildProfile(JObject result)
        {
            Profile = new VehicleModel
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
                VehicleClass = result.SelectToken("vehicle_class").ToString(),
                Pilots = GetNames.Get("pilots", result),
                Films = GetNames.Get("films", result),
                Url = result.SelectToken("url").ToObject<Uri>()
            };
        }
    }
}