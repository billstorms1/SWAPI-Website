using System.Collections.Generic;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class VehiclesModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<VehicleModel> Vehicles;
        public VehiclesModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }



        public void OnGet()
        {
            Topic = "vehicles";
            var results = MakeRequest.GetGeneralData(Topic);
            Vehicles = ProcessRequest.GetVehiclesList(results);
        }
    }
}