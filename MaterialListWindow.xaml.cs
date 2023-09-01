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
using System.Windows.Threading;
using TCC.Classes;
using static System.Collections.Specialized.BitVector32;

namespace TCC
{
    /// <summary>
    /// Interaction logic for MaterialListWindow.xaml
    /// </summary>
    public partial class MaterialListWindow : Window
    {
        private List<LayerMaterial> materials;
        private string materialName;
        private Cable cable;
        ObservableCollection<LayerMaterial> observableMaterials = new ObservableCollection<LayerMaterial>();
        bool isChildWindowOpen = false;

        public MaterialListWindow(Cable cable)
        {
            InitializeComponent();
            this.materials = cable.LayerMaterials;
            this.cable = cable;

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

        private void EditMaterialButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            materialName = button.Tag.ToString();
            LayerMaterial material = new LayerMaterial();
            foreach (LayerMaterial m in materials) if (m.Name == materialName) material = m;
            if (material != null)
            {
                MaterialsWindow windowMaterial = new MaterialsWindow(materials, material);
                windowMaterial.SubmitButtonClick += SubmitMaterialButtonClick;
                windowMaterial.Closed += MaterialWindow_Closed;
                windowMaterial.Show();
            }

            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void MaterialWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }
        private void SubmitMaterialButtonClick(object sender, EventArgs e)
        {
            MaterialsWindow windowMaterial = sender as MaterialsWindow;
            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i].Name == materialName)
                {
                    if (materials[i].Type == "isotropic")
                    {
                        materials[i] = windowMaterial.LayerIsotropic;
                        observableMaterials[i] = windowMaterial.LayerIsotropic;
                        PopUpTextBlock.Text = windowMaterial.LayerIsotropic.Name + " Edited Successfully";
                        popup.IsOpen = true;
                    }
                    else if (materials[i].Type == "orthotropic")
                    {
                        materials[i] = windowMaterial.LayerOrthotropic;
                        observableMaterials[i] = windowMaterial.LayerOrthotropic;
                        PopUpTextBlock.Text = windowMaterial.LayerOrthotropic.Name + " Edited Successfully";
                        popup.IsOpen = true;
                    }
                }
            }
        }

        Layer materialLayer;
        private void DeleteMaterialButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            materialName = button.Tag.ToString();
            bool foundLayer = false;
            foreach (Layer l in cable.Layers)
            {
                Layer layer = l as Layer;
                if (layer.Material.Name == materialName)
                {
                    foundLayer = true;
                    materialLayer = layer;
                }
            }

            if (!foundLayer)
            {
                // Delete Material if no layer is found with it
                for (int i = 0; i < materials.Count; i++)
                {
                    if (materials[i].Name == materialName)
                    {
                        observableMaterials.Remove(materials[i]);
                        PopUpTextBlock.Text = materialName + " Deleted Successfully";
                        materials.Remove(materials[i]);
                        popup.IsOpen = true;
                    }
                }
            }
            else
            {
                WarningWindow windowWarning = new WarningWindow(
                    "This Material is part of a layer. Deleting it will modify the layer to no material. Are you sure you want to continue?"
                    );
                windowWarning.ConfirmButtonClick += ConfirmButtonClick;
                windowWarning.CancelButtonClick += CancelButtonClick;
                windowWarning.Show();
            }
        }
        private void ConfirmButtonClick(object sender, EventArgs e)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                // First modify layer
                materialLayer.Material = new LayerMaterial();
                // Then delete section after confirmation
                if (materials[i].Name == materialName)
                {
                    observableMaterials.Remove(materials[i]);
                    PopUpTextBlock.Text = materialName + " Deleted Successfully";
                    materials.Remove(materials[i]);
                    popup.IsOpen = true;
                }
            }
        }
        private void CancelButtonClick(object sender, EventArgs e)
        {
            return;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isChildWindowOpen) e.Cancel = true;
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            StartCloseTimer();
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3d);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            popup.IsOpen = false;
        }
    }
}
