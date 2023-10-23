using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    [DataContract]
    public class Area
    {
        public Area() { }

        [DataMember(Name = "surface")]
        public string Surface { get; set; }
        
        [DataMember(Name = "pressure")]
        public double[] Pressure { get; set; }
        
        [DataMember(Name = "frontier")]
        public Line Frontier { get; set; }
        
        [DataMember(Name = "status")]
        public string[] Status { get; set; }
        
        [DataMember(Name = "imposed_displacements")]
        public double[] ImposedDisplacements { get; set; }
    }
}
