using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    public class Isotropic : LayerMaterial
    {
        public Isotropic()
        {
            this.Type = "isotropic";
        }

        public double Young { get; set; }
        public double Poisson { get; set; }
    }
}
