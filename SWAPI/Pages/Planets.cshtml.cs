using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class PlanetsModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<JObject> Results;
        public List<NameModel> Planets;

        public PlanetsModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }

        public void OnGet()
        {
            Topic = "planets";
            Results = MakeRequest.GetGeneralData(Topic);
            Planets = new List<NameModel>(ProcessRequest.CreateNameList(Results).OrderBy(n => n.Name));
        }
    }
}
       