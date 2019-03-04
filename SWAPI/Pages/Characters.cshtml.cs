using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<string> names = new List<string>();
            Topic = "people";
            Results = MakeRequest.GetGeneralData(Topic);
            Characters = new List<NameModel>(ProcessRequest.CreateNameList(Results).OrderBy(n=>n.Name));
        }
    }
}