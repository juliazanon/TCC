using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace TCC.MainClasses
{
    [DataContract]
    public class Cable
    {
        // Default json result when object is empty, used for checking if saved
        private string lastSavedFile = "{\r\n  \"name\": \"New Cable\",\r\n  \"sections\": [],\r\n  \"layers\": [],\r\n  \"layer_connections\": [],\r\n  \"materials\": []\r\n}";
        public Cable() { }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "sections")]
        public List<Section> Sections { get; set; }

        [DataMember(Name = "layers")]
        public List<Layer> Layers { get; set; }

        [DataMember(Name = "layer_connections")]
        public List<LayerConnection> LayerConnections { get; set; }

        [DataMember(Name = "materials")]
        public List<LayerMaterial> LayerMaterials { get; set; }

        [JsonIgnore]
        public string LastSavedFile { get { return lastSavedFile; } }

        public void SaveFile()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            SaveFileDialog dialog = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Save JSON",
                CheckPathExists = true,
                DefaultExt = "json",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, json);
                lastSavedFile = json;
            }
        }
    }
}
