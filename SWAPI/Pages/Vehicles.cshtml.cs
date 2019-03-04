using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class VehiclesModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<JObject> Results;
        public List<NameModel> Vehicles;

        public VehiclesModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }

        public void OnGet()
        {
            Topic = "vehicles";
            Results = MakeRequest.GetGeneralData(Topic);
            Vehicles = new List<NameModel>(ProcessRequest.CreateNameList(Results).OrderBy(n => n.Name));
        }
    }
}