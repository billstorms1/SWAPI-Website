using System.Collections.Generic;
using SWAPI.Helpers;
using SWAPI.Models;

namespace SWAPI.Pages
{
    public class StarShipsModel : CommonModel
    {
        public IMakeRequest MakeRequest;
        public IProcessRequest ProcessRequest;
        public List<StarShipModel> StarShips;

        public StarShipsModel(IMakeRequest makeRequest, IProcessRequest processRequest)
        {
            MakeRequest = makeRequest;
            ProcessRequest = processRequest;
        }


        public void OnGet()
        {
            Topic = "starships";
            var results = MakeRequest.GetGeneralData(Topic);
            StarShips = ProcessRequest.CreateStarShipList(results);
        }
    }
}