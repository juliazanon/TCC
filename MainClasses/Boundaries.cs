using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class Boundaries
    {
        public int ID { get; set; }
        public bool DesignOnly { get; set; }
        public string CoordinateSystem { get; set; }
        public double[] Coordinates { get; set; }
        public double[] Loads { get; set; }
        public string[] Status { get; set; }
        public double[] ImposedDisplacements { get; set; }
    }
}
