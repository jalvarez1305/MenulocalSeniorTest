using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Models
{
    public class NearbyRoot
    {
        public List<object> html_attributions { get; set; }
        public string next_page_token { get; set; }
        public List<NearbyResult> results { get; set; }
        public string status { get; set; }
    }
}
