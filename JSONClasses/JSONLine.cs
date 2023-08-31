using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    public class JSONLine
    {
        public int fourier_order { get; set; }
        public bool design_only { get; set; }
        public JSONBoundaries start { get; set; }
        public JSONBoundaries end { get; set; }
        public double[] distributed_loads { get; set; }
        public string[] status { get; set; }
        public double[] imposed_displacements { get; set; }
    }
}
