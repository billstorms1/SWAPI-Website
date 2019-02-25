using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWAPI.Pages
{
    public class CharacterProfileModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public JObject Result;
        public CharacterModel Profile;

        public CharacterProfileModel(IMakeRequest makeRequest)
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
            Profile = new CharacterModel
            {
                Name = result.SelectToken("name").ToString(),
                Height = result.SelectToken("height").ToString(),
                Gender = result.SelectToken("gender").ToString(),
                Mass = result.SelectToken("mass").ToString(),
                BirthYear = result.SelectToken("birth_year").ToString(),
                EyeColor = result.SelectToken("eye_color").ToString(),
                SkinColor = result.SelectToken("skin_color").ToString(),
                Films =GetNames("films"),
                Species = GetNames("species").FirstOrDefault(),
                HairColor = result.SelectToken("hair_color").ToString(),
                HomeWorld = MakeRequest.GetSpecificData(result.SelectToken("homeworld").ToString()).SelectToken("name").ToString(),
                Vehicles = GetNames("vehicles"),
                StarShips = GetNames("starships"),
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
               
                if(item == "films") //Stupid change in convention in the API
                    items.Add(itemResult.SelectToken("title").ToString());
                else
                    items.Add(itemResult.SelectToken("name").ToString());
            }
            
            return items;
        }
    }
}
