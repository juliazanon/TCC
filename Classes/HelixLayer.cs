using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class HelixLayer : Layer
    {
        public int Wires { get; set; }
        public Line Line { get; set; }
        public double Length { get; set; }
        public int SectionID { get; set; }
        public double Radius { get; set; }
        public double LayAngle { get; set; }
        public double InitialAngle { get; set; }
        public int Divisions { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int MaterialID { get; set; }
        public double[] BodyLoad { get; set; }
    }
}
