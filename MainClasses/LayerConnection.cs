using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class LayerConnection
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string FirstLayer { get; set; }
        public string SecondLayer { get; set; }
        public double FrictionCoefficient { get; set; }
        public double[] NormalDirection { get; set; }
        public double[] FirstTangentDirection { get; set; }
        public double[] SecondTangentDirection { get; set; }
        public double NormalPenalty { get; set; }
        public double TangentialPenalty { get; set; }
        public double PinballSearchRadius { get; set; }
    }
}
