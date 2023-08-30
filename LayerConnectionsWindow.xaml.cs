using System;
using System.Collections.Generic;
using System.Linq;
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
using static System.Collections.Specialized.BitVector32;

namespace TCC
{
    /// <summary>
    /// Interaction logic for LayerConnectionsWindow.xaml
    /// </summary>
    public partial class LayerConnectionsWindow : Window
    {
        public LayerConnectionsWindow(List<Layer> layers)
        {
            InitializeComponent();

            if (layers.Count == 0)
            {
                List<Layer> layerList = new List<Layer>
                {
                    new Layer { Name = "No Layer Created" },
                };
                FirstLayerComboBox.ItemsSource = layerList;
                FirstLayerComboBox.SelectedIndex = 0;
                SecondLayerComboBox.ItemsSource = layerList;
                SecondLayerComboBox.SelectedIndex = 0;
            }
            else if (layers.Count == 1)
            {
                FirstLayerComboBox.ItemsSource = layers;
                FirstLayerComboBox.SelectedIndex = 0;
                SecondLayerComboBox.ItemsSource = layers;
                SecondLayerComboBox.SelectedIndex = 0;
            } else
            {
                FirstLayerComboBox.ItemsSource = layers;
                FirstLayerComboBox.SelectedIndex = 0;
                SecondLayerComboBox.ItemsSource = layers;
                SecondLayerComboBox.SelectedIndex = 1;
            }
        }
        private void FirstLayerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstLayerComboBox.SelectedItem != null)
            {
                // Get the selected Material instance.
                //this.firstLayer = (Layer)FirstLayerComboBox.SelectedItem;
            }
        }
        private void SecondLayerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondLayerComboBox.SelectedItem != null)
            {
                // Get the selected Material instance.
                //this.firstLayer = (Layer)FirstLayerComboBox.SelectedItem;
            }
        }
    }
}
