using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class SpeciesModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<JObject> Results;
        public List<NameModel> Species;

        public SpeciesModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }

        public void OnGet()
        {
            Topic = "species";
            Results = MakeRequest.GetGeneralData(Topic);
            Species = new List<NameModel>(ProcessRequest.CreateNameList(Results).OrderBy(n => n.Name));
        }
    }
}