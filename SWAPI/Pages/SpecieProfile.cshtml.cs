using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class SpecieProfileModel : PageModel
    {
        public IMakeRequest MakeRequest;
        public JObject Result;
        public SpecieModel Profile;

        public SpecieProfileModel(IMakeRequest makeRequest)
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
            Profile = new SpecieModel()
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
                People = GetNames("people"),
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