using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    internal class JSONLayer
    {
        public string name { get; set; }
        public string type { get; set; }
        public int material { get; set; }
        public double length { get; set; }
        public double[] body_load { get; set; }
    }
}
