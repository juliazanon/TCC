using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    [DataContract]
    public class Isotropic : LayerMaterial
    {
        [Newtonsoft.Json.JsonConstructor]
        public Isotropic()
        {
            this.Type = "isotropic";
        }

        [DataMember(Name = "young_modulus")]
        public double Young { get; set; }
        
        [DataMember(Name = "poisson_ratio")]
        public double Poisson { get; set; }
    }
}
