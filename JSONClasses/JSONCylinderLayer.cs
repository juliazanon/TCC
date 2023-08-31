using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Classes;

namespace TCC.JSONClasses
{
    internal class JSONCylinderLayer : JSONLayer
    {
        public double radius { get; set;  }
        public double thickness { get; set; }
        public int fourier_series_order { get; set; }
        public int divisions { get; set; }
        public int radial_divisions { get; set; }
        public int axial_divisions { get; set; }
        public List<JSONArea> areas { get; set; }
    }
}
