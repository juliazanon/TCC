using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TCC.MainClasses.Materials;

namespace TCC.JSONClasses
{
    [DataContract]
    [JsonConverter(typeof(MaterialConverter))]
    public abstract class JSONMaterial
    {
        public JSONMaterial() { }

        [DataMember(Name = "density")]
        public double density { get; set; }

        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }
    }
}
