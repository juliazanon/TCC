using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONIsotropic : JSONMaterial
    {
        [Newtonsoft.Json.JsonConstructor]
        public JSONIsotropic()
        {
            this.type = "isotropic";
        }

        [DataMember(Name = "young_modulus")]
        public double young_modulus { get; set; }
        
        [DataMember(Name = "poisson_ratio")]
        public double poisson_ratio { get; set; }
    }
}
