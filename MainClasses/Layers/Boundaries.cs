using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    [DataContract]
    public class Boundaries
    {
        public Boundaries() { }

        [DataMember(Name = "id")]
        public int ID { get; set; }
        
        [DataMember(Name = "design_only")]
        public bool DesignOnly { get; set; }
        
        [DataMember(Name = "coordinate_system")]
        public string CoordinateSystem { get; set; }
        
        [DataMember(Name = "coordinates")]
        public double[] Coordinates { get; set; }
        
        [DataMember(Name = "loads")]
        public double[] Loads { get; set; }
        
        [DataMember(Name = "status")]
        public string[] Status { get; set; }
        
        [DataMember(Name = "imposed_displacements")]
        public double[] ImposedDisplacements { get; set; }
    }
}
