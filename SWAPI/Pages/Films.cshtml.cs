using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class FilmsModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<NameModel> Films;
        public List<JObject> Results;

        public FilmsModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }

        public void OnGet()
        {
            Topic = "films";
            Results = MakeRequest.GetGeneralData(Topic);
            Films = ProcessRequest.CreateTitleList(Results);
        }
    }
}