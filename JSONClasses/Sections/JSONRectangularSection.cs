using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONRectangularSection : JSONSection
    {
        [Newtonsoft.Json.JsonConstructor]
        public JSONRectangularSection()
        {
            this.type = "rectangular";
        }

        [DataMember(Name = "height")]
        public double height { get; set; }
        
        [DataMember(Name = "width")]
        public double width { get; set; }
    }
}
