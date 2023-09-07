using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONBoundaries
    {
        public JSONBoundaries() { }
        
        [DataMember(Name = "id")]
        public int id { get; set; }
        
        [DataMember(Name = "design_only")]
        public bool design_only { get; set; }

        [DataMember(Name = "coordinate_system")]
        public string coordinate_system { get; set; }
        
        [DataMember(Name = "coordinates")]
        public double[] coordinates { get; set; }
        
        [DataMember(Name = "loads")]
        public double[] loads { get; set; }

        [DataMember(Name = "status")]
        public string[] status { get; set; }

        [DataMember(Name = "imposed_displacements")]
        public double[] imposed_displacements { get; set; }
    }
}
