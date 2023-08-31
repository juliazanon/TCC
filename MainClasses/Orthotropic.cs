using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class Orthotropic : LayerMaterial
    {
        public double[] Young { get; set; }
        public double[] Poisson { get; set; }
        public double[] Shear { get; set; }
    }
}
