using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SWAPI.Models;
using SWAPI.Pages;

namespace SWAPI.Helpers
{
    public interface IProcessRequest
    {
        List<CharacterModel> CreateCharacterList(List<JObject> resultBlobs);
        List<string> GetResults(List<JObject> resultBlobs, string node);
        List<PlanetModel> CreatePlanetList(List<JObject> resultBlobs);
        List<FilmModel> CreateFilmsList(List<JObject> results);
        List<SpecieModel> CreateSpeciesList(List<JObject> results);
        List<StarShipModel> CreateStarShipList(List<JObject> results);
        List<VehicleModel> GetVehiclesList(List<JObject> results);
    }

    public class ProcessRequest : IProcessRequest
    {
        public List<CharacterModel> CreateCharacterList(List<JObject> resultBlobs)
        {
            var characters = new List<CharacterModel>();

            foreach (var blob in resultBlobs)
                foreach (var result in blob.SelectToken("results"))
                    characters.Add(new CharacterModel
                    {
                        Name = result.SelectToken("name").ToString(),
                        Url = result.SelectToken("url").ToObject<Uri>()
                    });
            return characters;
        }


        public List<PlanetModel> CreatePlanetList(List<JObject> resultBlobs)
        {
            var planets = new List<PlanetModel>();
            foreach (var blob in resultBlobs)
            {
                foreach (var result in blob.SelectToken("results"))
                {
                    planets.Add(new PlanetModel
                    {
                        Name = result.SelectToken("name").ToString(),
                        Url = result.SelectToken("url").ToObject<Uri>()
                    });
                }
            }
            return planets;
        }

        public List<FilmModel> CreateFilmsList(List<JObject> resultBlobs)
        {
            var films = new List<FilmModel>();
            foreach (var blob in resultBlobs)
            {
                foreach (var result in blob.SelectToken("results"))
                {
                    films.Add(new FilmModel
                    {
                        Title = result.SelectToken("title").ToString(),
                        Url = result.SelectToken("url").ToObject<Uri>()
                    });
                }
            }
            return films;
        }

        public List<SpecieModel> CreateSpeciesList(List<JObject> resultBlobs)
        {
            var species = new List<SpecieModel>();
            foreach (var blob in resultBlobs)
            {
                foreach (var result in blob.SelectToken("results"))
                {
                    species.Add(new SpecieModel
                    {
                        Name = result.SelectToken("name").ToString(),
                        Url = result.SelectToken("url").ToObject<Uri>()
                    });
                }
            }
            return species;
        }

        public List<StarShipModel> CreateStarShipList(List<JObject> resultBlobs)
        {
            var starShip = new List<StarShipModel>();
            foreach (var blob in resultBlobs)
            {
                foreach (var result in blob.SelectToken("results"))
                {
                    starShip.Add(new StarShipModel
                    {
                        Name = result.SelectToken("name").ToString(),
                        Url = result.SelectToken("url").ToObject<Uri>()
                    });
                }
            }
            return starShip;
        }

        public List<VehicleModel> GetVehiclesList(List<JObject> resultBlobs)
        {
            var vehicle = new List<VehicleModel>();
            foreach (var blob in resultBlobs)
            {
                foreach (var result in blob.SelectToken("results"))
                {
                    vehicle.Add(new VehicleModel
                    {
                        Name = result.SelectToken("name").ToString(),
                        Url = result.SelectToken("url").ToObject<Uri>()
                    });
                }
            }
            return vehicle;
        }

        public List<string> GetResults(List<JObject> resultBlobs, string node)
        {
            var results = new List<string>();
            foreach (var blob in resultBlobs)
                if (blob.SelectToken("results").SelectToken(node).ToString() == node)
                {
                    var count = blob.SelectToken("results").Children();
                    results.Add(blob.SelectTokens("results").ToString());
                }

            return results;
        }

        private static List<string> GetNodeValues(JObject resultBlob, string node)
        {
            var nodeValue = new List<string>();
            var count = resultBlob.SelectToken("results").Count();
            for (var i = 0; i < count; i++)
                nodeValue.Add(resultBlob.SelectToken("results")[i].SelectToken(node).ToString());

            return nodeValue;
        }
    }
}