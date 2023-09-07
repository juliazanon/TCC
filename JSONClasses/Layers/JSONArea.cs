using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONArea
    {
        public JSONArea() { }

        [DataMember(Name = "surface")]
        public string surface { get; set; }
        
        [DataMember(Name = "pressure")]
        public double[] pressure { get; set; }
        
        [DataMember(Name = "frontier")]
        public JSONLine frontier { get; set; }

        [DataMember(Name = "status")]
        public string[] status { get; set; }
        
        [DataMember(Name = "imposed_displacements")]
        public double[] imposed_displacements { get; set; }
    }
}
