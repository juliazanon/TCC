using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class Orthotropic : LayerMaterial
    {
        public string Type { get; set; }
        public double Ex { get; set; }
        public double Ey { get; set; }
        public double Ez { get; set; }
        public double Nxy { get; set; }
        public double Nxz { get; set; }
        public double Nyz { get; set; }
        public double Gxy { get; set; }
        public double Gxz { get; set; }
        public double Gyz { get; set; }
    }
}
