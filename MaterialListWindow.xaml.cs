using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TCC.Classes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for MaterialListWindow.xaml
    /// </summary>
    public partial class MaterialListWindow : Window
    {
        ObservableCollection<Layer> observableLayer = new ObservableCollection<Layer>();
        public MaterialListWindow(List<LayerMaterial> materials)
        {
            InitializeComponent();
            for (int i=0; i < materials.Count(); i++){
                observableLayer.Add(materials[i]);
            }
            itemsControl.ItemsSource = observableLayer;
        }
    }
}
