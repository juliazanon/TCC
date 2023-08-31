using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    public class JSONOrthotropic : JSONMaterial
    {
        public double[] young { get; set; }
        public double[] poisson { get; set; }
        public double[] shear { get; set; }
    }
}
