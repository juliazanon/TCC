using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONLine
    {
        public JSONLine() { }

        [DataMember(Name = "fourier_order")]
        public int fourier_order { get; set; }

        [DataMember(Name = "design_only")]
        public bool design_only { get; set; }

        [DataMember(Name = "start")]
        public JSONBoundaries start { get; set; }
        
        [DataMember(Name = "end")]
        public JSONBoundaries end { get; set; }
        
        [DataMember(Name ="distributed_loads")]
        public double[] distributed_loads { get; set; }

        [DataMember(Name = "status")]
        public string[] status { get; set; }
        
        [DataMember(Name = "imposed_displacements")]
        public double[] imposed_displacements { get; set; }
    }
}
