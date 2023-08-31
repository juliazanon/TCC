using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    public class JSONBoundaries
    {
        public int id { get; set; }
        public bool design_only { get; set; }
        public string coordinate_system { get; set; }
        public double[] coordinates { get; set; }
        public double[] loads { get; set; }
        public string[] status { get; set; }
        public double[] imposed_displacements { get; set; }
    }
}
