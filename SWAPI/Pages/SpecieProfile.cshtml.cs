using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;
using System;
using System.Web;

namespace SWAPI.Pages
{
    public class SpecieProfileModel : PageModel
    {
        public IMakeRequest MakeRequest;
        public IGetNames GetNames;
        public JObject Result;
        public SpecieModel Profile;

        public SpecieProfileModel(IMakeRequest makeRequest, IGetNames getNames)
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
            Profile = new SpecieModel
            {
                Name = result.SelectToken("name").ToString(),
                Classification = result.SelectToken("classification").ToString(),
                Designation = result.SelectToken("designation").ToString(),
                AverageHeight = result.SelectToken("average_height").ToString(),
                SkinColors = result.SelectToken("skin_colors").ToString(),
                HairColors = result.SelectToken("hair_colors").ToString(),
                EyeColors = result.SelectToken("eye_colors").ToString(),
                AverageLifeSpan = result.SelectToken("average_lifespan").ToString(),
                HomeWorld = MakeRequest.GetSpecificData(result.SelectToken("homeworld").ToString()).SelectToken("name").ToString(),
                Language = result.SelectToken("language").ToString(),
                People = GetNames.Get("people", result),
                Films = GetNames.Get("films", result),
                Url = result.SelectToken("url").ToObject<Uri>()
            };
        }
    }
}