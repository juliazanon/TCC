using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    [DataContract]
    public class TubularSection : Section
    {
        [Newtonsoft.Json.JsonConstructor]
        public TubularSection()
        {
            this.Type = "tubular";
        }
        
        
        [DataMember(Name = "internal_radius")]
        public double InternalRadius { get; set; }
        
        [DataMember(Name = "external_radius")]
        public double ExternalRadius { get; set; }
    }
}
