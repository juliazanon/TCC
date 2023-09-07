using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TCC.JSONClasses
{
    [DataContract]
    public class JSONOrthotropic : JSONMaterial
    {
        [Newtonsoft.Json.JsonConstructor]
        public JSONOrthotropic()
        {
            this.type = "orthopropic";
        }
        public double Ex { get; set; }
        public double Ey { get; set; }
        public double Ez { get; set; }
        public double nuxy{ get; set; }
        public double nuxz { get; set; }
        public double nuyz { get; set; }
        public double Gxy { get; set; }
        public double Gxz { get; set; }
        public double Gyz { get; set; }
    }
}
