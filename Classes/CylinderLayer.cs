using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class CylinderLayer : Layer
    {
        public double Length { get; set; }
        public double Radius { get; set; }
        public double Thickness { get; set; }
        public int FourierOrder { get; set; }
        public int RadialDivisions { get; set; }
        public int AxialDivisions { get; set; }
        public Area[] Areas { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int MaterialID { get; set; }
        public double[] BodyLoad { get; set; }
    }
}
