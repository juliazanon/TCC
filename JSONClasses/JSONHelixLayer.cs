using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    public class JSONHelixLayer : JSONLayer
    {
        public int wires { get; set; }
        public JSONLine line { get; set;}
        public int section { get; set;}
        public double lay_angle { get; set;}
        public double initial_angle { get; set;}
        public int divisions { get; set;}
        public double radius { get; set;}
    }
}
