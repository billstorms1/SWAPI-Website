using System;
using System.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class FilmProfileModel : PageModel
    {
        public IMakeRequest MakeRequest;
        public IGetNames GetNames;
        public JObject Result;
        public FilmModel Profile;


        public FilmProfileModel(IMakeRequest makeRequest, IGetNames getNames)
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
                Characters = GetNames.Get("characters", result),
                Planets = GetNames.Get("planets", result),
                StarShips = GetNames.Get("starships", result),
                Vehicles = GetNames.Get("vehicles", result),
                Species = GetNames.Get("species", result),
                Url = result.SelectToken("url").ToObject<Uri>()
            };

            return Profile;
        }
    }
}