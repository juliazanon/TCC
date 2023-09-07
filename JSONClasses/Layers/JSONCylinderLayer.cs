using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TCC.MainClasses;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONCylinderLayer : JSONLayer
    {
        [Newtonsoft.Json.JsonConstructor]
        public JSONCylinderLayer()
        {
            this.type = "cylinder";
        }
        
        [DataMember(Name = "radius")]
        public double radius { get; set;  }
        
        [DataMember(Name = "thickness")]
        public double thickness { get; set; }
        
        [DataMember(Name = "fourier_series_order")]
        public int fourier_series_order { get; set; }
        
        [DataMember(Name = "divisions")]
        public int divisions { get; set; }
        
        [DataMember(Name = "radial_divisions")]
        public int radial_divisions { get; set; }
        
        [DataMember(Name = "axial_divisions")]
        public int axial_divisions { get; set; }
        
        [DataMember(Name = "areas")]
        public List<JSONArea> areas { get; set; }
    }
}
