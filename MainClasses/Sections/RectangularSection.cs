using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MainClasses
{
    [DataContract]
    public class RectangularSection : Section
    {
        [Newtonsoft.Json.JsonConstructor]
        public RectangularSection()
        {
            this.Type = "rectangular";
        }

        [DataMember(Name = "height")]
        public double Width { get; set; }
        
        [DataMember(Name = "width")]
        public double Height { get; set; }
    }
}
