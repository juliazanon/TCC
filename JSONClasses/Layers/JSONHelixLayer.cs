using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONHelixLayer : JSONLayer
    {
        public JSONHelixLayer() { }

        [DataMember(Name = "wires")]
        public int wires { get; set; }
        
        [DataMember(Name = "line")]
        public JSONLine line { get; set;}
        
        [DataMember(Name = "section")]
        public int section { get; set;}
        
        [DataMember(Name = "lay_angle")]
        public double lay_angle { get; set;}
        
        [DataMember(Name = "initial_angle")]
        public double initial_angle { get; set;}
        
        [DataMember(Name = "divisions")]
        public int divisions { get; set;}
        
        [DataMember(Name = "radius")]
        public double radius { get; set;}
    }
}
