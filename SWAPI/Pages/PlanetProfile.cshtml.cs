using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class PlanetProfileModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public JObject Result;
        public PlanetModel Profile;
        

        public PlanetProfileModel(IMakeRequest makeRequest)
        {
            MakeRequest = makeRequest;
            Topic = "planets";
        }
        public void OnGet(string url)
        {
            var decodedPath = HttpUtility.UrlDecode(url);
            Result = MakeRequest.GetSpecificData(decodedPath);
            BuildProfile(Result);
        }

        public PlanetModel BuildProfile(JObject result)
        {
            Profile =  new PlanetModel 
            {
                Name = result.SelectToken("name").ToString(),
                RotationPeriod = result.SelectToken("rotation_period").ToString(),
                OrbitalPeriod = result.SelectToken("orbital_period").ToString(),
                Diameter = result.SelectToken("diameter").ToString(),
                Climate = result.SelectToken("climate").ToString(),
                Gravity = result.SelectToken("gravity").ToString(),
                Terrain = result.SelectToken("terrain").ToString(),
                Films = GetNames("films"),
                SurfaceWater = result.SelectToken("surface_water").ToString(),
                Population = result.SelectToken("population").ToString(),
                Residents = GetNames("residents"),
                Url = result.SelectToken("url").ToObject<Uri>()
            };

            return Profile;
        }

        public List<string> GetNames(string item)
        {
            var items = new List<string>();
            foreach (var result in Result.SelectToken(item))
            {
                var link = result.ToString();
                var itemResult = MakeRequest.GetSpecificData(link);
               
                if(item == "films") //Stupid change in convention in the API
                    items.Add(itemResult.SelectToken("title").ToString());
                else
                    items.Add(itemResult.SelectToken("name").ToString());
            }
            
            return items;
        }
    }
}