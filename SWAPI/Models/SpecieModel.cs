using System;
using System.Collections.Generic;

namespace SWAPI.Models
{
    public class SpecieModel
    {
        public string Name { get; set; }
        public string Classification { get; set; }
        public string Designation { get; set; }
        public string AverageHeight { get; set; }
        public string SkinColors { get; set; }
        public string HairColors { get; set; }
        public string EyeColors { get; set; }
        public string AverageLifeSpan { get; set; }
        public string HomeWorld { get; set; }
        public string Language { get; set; }
        public List<string> Films { get; set; }
        public List<string> People { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Edited { get; set; }
        public Uri Url { get; set; }
    }
}
