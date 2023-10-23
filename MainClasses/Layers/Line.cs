using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    [DataContract]
    public class Line
    {
        public Line() { }
        
        [DataMember(Name = "fourier_order")]
        public int FourierOrder { get; set; }
        
        [DataMember(Name = "design_only")]
        public bool DesignOnly { get; set; }

        [DataMember(Name = "start")]
        public Boundaries Start { get; set; }
        
        [DataMember(Name = "end")]
        public Boundaries End { get; set; }
        
        [DataMember(Name ="distributed_loads")]
        public double[] DistributedLoads { get; set; }
        
        [DataMember(Name = "status")]
        public string[] Status { get; set; }
        
        [DataMember(Name = "imposed_displacements")]
        public double[] ImposedDisplacements { get; set; }
    }
}
