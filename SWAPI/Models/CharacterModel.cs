using System;
using System.Collections.Generic;

namespace SWAPI.Models
{
    public class CharacterModel
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string HairColor { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string BirthYear { get; set; }
        public string Gender { get; set; }
        public string HomeWorld { get; set; }
        public List<string> Films { get; set; }
        public string Species { get; set; }
        public List<string> Vehicles { get; set; }
        public List<string> StarShips { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Edited { get; set; }
        public Uri Url { get; set; }
    }
}
