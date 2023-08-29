using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class Area
    {
        public string Surface { get; set; }
        public double[] Pressure { get; set; }
        public Line Frontier { get; set; }
        public string[] Status { get; set; }
        public double[] ImposedDisplacements { get; set; }
    }
}
