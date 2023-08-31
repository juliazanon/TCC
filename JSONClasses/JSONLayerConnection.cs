using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    public class JSONLayerConnection
    {
        public string type { get; set; }
        public string first_layer { get; set; }
        public string second_layer { get; set; }
        public double friction_coefficient { get; set; }
        public double[] normal_direction { get; set; }
        public double[] first_tangent_direction { get; set; }
        public double[] second_tangent_direction { get; set; }
        public double normal_penalty { get; set; }
        public double tangential_penalty { get; set; }
        public double pinball_search_radius { get; set; }
    }
}
