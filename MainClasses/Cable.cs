using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.JSONClasses;

namespace TCC.Classes
{
    public class Cable
    {
        public string Name { get; set; }
        public List<Section> Sections { get; set; }
        public List<Layer> Layers { get; set; }
        public List<LayerConnection> LayerConnections { get; set; }
        public List<LayerMaterial> LayerMaterials { get; set; }

        private void SaveFile()
        {
            JSONCable cable = new JSONCable();
        }
    }
}
