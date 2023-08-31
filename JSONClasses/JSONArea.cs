using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    internal class JSONArea
    {
        public string surface { get; set; }
        public double[] pressure { get; set; }
        public JSONLine frontier { get; set; }
        public string[] status { get; set; }
        public double[] imposed_displacements { get; set; }
    }
}
