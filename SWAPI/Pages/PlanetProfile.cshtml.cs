using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;
using System;
using System.Web;

namespace SWAPI.Pages
{
    public class PlanetProfileModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IGetNames GetNames;
        public JObject Result;
        public PlanetModel Profile;
        

        public PlanetProfileModel(IMakeRequest makeRequest, IGetNames getNames)
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

        public PlanetModel BuildProfile(JObject result)
        {
            Profile = new PlanetModel
            {
                Name = result.SelectToken("name").ToString(),
                RotationPeriod = result.SelectToken("rotation_period").ToString(),
                OrbitalPeriod = result.SelectToken("orbital_period").ToString(),
                Diameter = result.SelectToken("diameter").ToString(),
                Climate = result.SelectToken("climate").ToString(),
                Gravity = result.SelectToken("gravity").ToString(),
                Terrain = result.SelectToken("terrain").ToString(),
                Films = GetNames.Get("films", result),
                SurfaceWater = result.SelectToken("surface_water").ToString(),
                Population = result.SelectToken("population").ToString(),
                Residents = GetNames.Get("residents", result),
                Url = result.SelectToken("url").ToObject<Uri>()
            };

            return Profile;
        }
    }
}