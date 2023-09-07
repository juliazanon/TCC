using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    public abstract class LayerMaterial
    {
        public double Density { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
