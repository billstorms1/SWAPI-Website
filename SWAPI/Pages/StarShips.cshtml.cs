using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class StarShipsModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<JObject> Results;
        public List<NameModel> StarShips;

        public StarShipsModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }

        public void OnGet()
        {
            Topic = "starships";
            Results = MakeRequest.GetGeneralData(Topic);
            StarShips = new List<NameModel>(ProcessRequest.CreateNameList(Results).OrderBy(n => n.Name));
        }
    }
}