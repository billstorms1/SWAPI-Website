using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWAPI.Helpers;

namespace SWAPI.Models
{
    public class CommonModel : PageModel
    {
        public List<string> Names;
        public string Topic;
        public string Node;
    }
}
