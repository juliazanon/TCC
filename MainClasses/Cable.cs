using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Microsoft.Win32;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

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
            
            string lastDirectory = Properties.Settings.Default.LastSaveDirectory;

            if (!string.IsNullOrEmpty(lastDirectory) && System.IO.Directory.Exists(lastDirectory))
            {
                dialog.InitialDirectory = lastDirectory;
            }
            else
            {
                // Set a default initial directory if the last one doesn't exist
                dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // User selected a file, update the last used directory
                Properties.Settings.Default.LastSaveDirectory = System.IO.Path.GetDirectoryName(dialog.FileName);
                Properties.Settings.Default.Save();

                File.WriteAllText(dialog.FileName, json);
                lastSavedFile = json;
            }
        }
    }
}
