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
        public IGetNames GetNames;
        public JObject Result;
        public CharacterModel Profile;

        public CharacterProfileModel(IMakeRequest makeRequest, IGetNames getNames)
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
            Profile = new CharacterModel
            {
                Name = result.SelectToken("name").ToString(),
                Height = result.SelectToken("height").ToString(),
                Gender = result.SelectToken("gender").ToString(),
                Mass = result.SelectToken("mass").ToString(),
                BirthYear = result.SelectToken("birth_year").ToString(),
                EyeColor = result.SelectToken("eye_color").ToString(),
                SkinColor = result.SelectToken("skin_color").ToString(),
                Films =GetNames.Get("films", result),
                Species = GetNames.Get("species", result).FirstOrDefault(),
                HairColor = result.SelectToken("hair_color").ToString(),
                HomeWorld = MakeRequest.GetSpecificData(result.SelectToken("homeworld").ToString()).SelectToken("name").ToString(),
                Vehicles = GetNames.Get("vehicles", result),
                StarShips = GetNames.Get("starships", result),
                Url = result.SelectToken("url").ToObject<Uri>()
            };
        }
    }
}
