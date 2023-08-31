using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Classes;

namespace TCC.JSONClasses
{
    internal class JSONCable
    {
        public string name { get; set; }
        public List<JSONSection> sections { get; set; }
        public List<JSONLayer> layers { get; set; }
        public List<JSONLayerConnection> layer_connections { get; set; }
        public List<JSONMaterial> materials { get; set; }
    }
}
