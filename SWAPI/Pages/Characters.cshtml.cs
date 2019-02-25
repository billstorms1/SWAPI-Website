using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SWAPI.Pages
{
    public class CharactersModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<JObject> Results;
        public List<CharacterModel> Characters;
        
        public CharactersModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }

        public void OnGet()
        {
            Topic = "people";
            Results = MakeRequest.GetGeneralData(Topic);
            Characters = ProcessRequest.CreateCharacterList(Results);
        }
    }
}