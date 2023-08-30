using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class Isotropic : LayerMaterial
    {
        public double Young { get; set; }
        public double Poisson { get; set; }
        public string Type { get; set; }
    }
}
