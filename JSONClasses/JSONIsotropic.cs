using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    public class JSONIsotropic : JSONMaterial
    {
        public double young_modulus { get; set; }
        public double poisson_ratio { get; set; }
    }
}
