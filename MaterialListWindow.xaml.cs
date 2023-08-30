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
        ObservableCollection<LayerMaterial> observableMaterials = new ObservableCollection<LayerMaterial>();
        public MaterialListWindow(List<LayerMaterial> materials)
        {
            InitializeComponent();
            for (int i = 0; i < materials.Count(); i++) {
                if (materials[i] is Isotropic)
                {
                    Isotropic iso = materials[i] as Isotropic;
                    observableMaterials.Add(iso);
                }
                else if (materials[i] is Orthotropic)
                {
                    Orthotropic ortho = materials[i] as Orthotropic;
                    observableMaterials.Add(ortho);
                }
            }
            itemsControl.ItemsSource = observableMaterials;
        }
    }
}
