using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class FilmProfileModel : PageModel
    {
        public IMakeRequest MakeRequest;
        public JObject Result;
        public FilmModel Profile;


        public FilmProfileModel(IMakeRequest makeRequest)
        {
            MakeRequest = makeRequest;
        }
        public void OnGet(string url)
        {
            var decodedPath = HttpUtility.UrlDecode(url);
            Result = MakeRequest.GetSpecificData(decodedPath);
            BuildProfile(Result);
        }

        public FilmModel BuildProfile(JObject result)
        {
            Profile = new FilmModel
            {
                Title = result.SelectToken("title").ToString(),
                EpisodeId = result.SelectToken("episode_id").ToString(),
                OpeningCrawl = result.SelectToken("opening_crawl").ToString(),
                Director = result.SelectToken("director").ToString(),
                Producer = result.SelectToken("producer").ToString(),
                ReleaseDate = result.SelectToken("release_date").ToString(),
                Characters = GetNames("characters"),
                Planets = GetNames("planets"),
                StarShips = GetNames("starships"),
                Vehicles = GetNames("vehicles"),
                Species = GetNames("species"),
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

                if (item == "films") //Stupid change in convention in the API
                    items.Add(itemResult.SelectToken("title").ToString());
                else
                    items.Add(itemResult.SelectToken("name").ToString());
            }

            return items;
        }
    }
}