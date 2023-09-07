using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    public class Line
    {
        public int FourierOrder { get; set; }
        public bool DesignOnly { get; set; }
        public Boundaries Start { get; set; }
        public Boundaries End { get; set; }
        public double[] DistributedLoads { get; set; }
        public string[] Status { get; set; }
        public double[] ImposedDisplacements { get; set; }
    }
}
