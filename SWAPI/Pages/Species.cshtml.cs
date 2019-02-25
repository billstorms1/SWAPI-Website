using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class SpeciesModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<SpecieModel> Species;
        public List<JObject> Results;

        public SpeciesModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }

        public void OnGet()
        {
            Topic = "species";
            Results = MakeRequest.GetGeneralData(Topic);
            Species = ProcessRequest.CreateSpeciesList(Results);
        }
    }
}