using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    [DataContract]
    public class Orthotropic : LayerMaterial
    {
        [Newtonsoft.Json.JsonConstructor]
        public Orthotropic()
        {
            this.Type = "orthotropic";
        }

        [DataMember(Name = "Ex")]
        public double EX { get; set; }
        
        [DataMember(Name = "Ey")]
        public double EY { get; set; }
        
        [DataMember(Name = "Ez")]
        public double EZ { get; set; }
        
        [DataMember(Name = "nuxy")]
        public double NuXY { get; set; }
        
        [DataMember(Name = "nuxz")]
        public double NuXZ { get; set; }
        
        [DataMember(Name = "nuyz")]
        public double NuYZ { get; set; }
        
        [DataMember(Name = "Gxy")]
        public double GXY { get; set; }
        
        [DataMember(Name = "Gxz")]
        public double GXZ { get; set; }
        
        [DataMember(Name = "Gyz")]
        public double GYZ { get; set; }
    }
}
