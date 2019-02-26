using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;
using System.Collections.Generic;

namespace SWAPI.Pages
{
    public class CharactersModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<JObject> Results;
        public List<NameModel> Characters;
        
        public CharactersModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }

        public void OnGet()
        {
            Topic = "people";
            Results = MakeRequest.GetGeneralData(Topic);
            Characters = ProcessRequest.CreateNameList(Results);
        }
    }
}