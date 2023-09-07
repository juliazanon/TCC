using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONTubularSection : JSONSection
    {
        [Newtonsoft.Json.JsonConstructor]
        public JSONTubularSection()
        {
            this.type = "tubular";
        }

        [DataMember(Name = "internal_radius")]
        public double internal_radius { get; set; }

        [DataMember(Name = "external_radius")]
        public double external_radius { get; set; }
    }
}
