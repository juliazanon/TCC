using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class Cable
    {
        public string Name { get; set; }
        public Dictionary<int, Section> Sections { get; set; }
        public Layer[] Layers { get; set; }
        public LayerConnections[] LayerConnections { get; set; }
        public Dictionary<int, LayerMaterial> LayerMaterials { get; set; }

        private void SaveFile()
        {

        }
    }
}
