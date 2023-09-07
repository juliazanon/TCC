using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    public class Orthotropic : LayerMaterial
    {
        public Orthotropic()
        {
            this.Type = "orthotropic";
        }

        public double EX { get; set; }
        public double EY { get; set; }
        public double EZ { get; set; }
        public double NuXY { get; set; }
        public double NuXZ { get; set; }
        public double NuYZ { get; set; }
        public double GXY { get; set; }
        public double GXZ { get; set; }
        public double GYZ { get; set; }
    }
}
