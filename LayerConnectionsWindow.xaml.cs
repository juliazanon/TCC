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
        private Layer firstLayer;
        private Layer secondLayer;
        private LayerConnection layerConnection;

        public event EventHandler SubmitButtonClick;

        public LayerConnection LayerConnection { get { return layerConnection; } }
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
                // Get the selected Layer instance.
                firstLayer = (Layer)FirstLayerComboBox.SelectedItem;
            }
        }
        private void SecondLayerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondLayerComboBox.SelectedItem != null)
            {
                // Get the selected Material instance.
                secondLayer = (Layer)SecondLayerComboBox.SelectedItem;
            }
        }

        private void SubmitNewLayerConnection(object sender, RoutedEventArgs e)
        {
            layerConnection = new LayerConnection();

            if (FrictionalRadioButton.IsChecked == true) { layerConnection.Type = "frictional"; }
            else { layerConnection.Type = "bonded"; }

            layerConnection.FirstLayer = firstLayer.Name;
            layerConnection.SecondLayer = secondLayer.Name;

            double.TryParse(FrictionTextBox.Text, out double result);
            layerConnection.FrictionCoefficient = result;

            double.TryParse(XNormalTextBox.Text, out double xresult);
            double.TryParse(YNormalTextBox.Text, out double yresult);
            double.TryParse(ZNormalTextBox.Text, out double zresult);
            layerConnection.NormalDirection = new double[] { xresult, yresult, zresult };

            double.TryParse(XFTangentTextBox.Text, out xresult);
            double.TryParse(YFTangentTextBox.Text, out yresult);
            double.TryParse(ZFTangentTextBox.Text, out zresult);
            layerConnection.FirstTangentDirection = new double[] { xresult, yresult, zresult };

            double.TryParse(XSTangentTextBox.Text, out xresult);
            double.TryParse(YSTangentTextBox.Text, out yresult);
            double.TryParse(ZSTangentTextBox.Text, out zresult);
            layerConnection.SecondTangentDirection = new double[] { xresult, yresult, zresult };

            double.TryParse(NormalTextBox.Text, out result);
            layerConnection.NormalPenalty = result;
            double.TryParse(TangentialTextBox.Text, out result);
            layerConnection.TangentialPenalty = result;
            double.TryParse(PinballTextBox.Text, out result);
            layerConnection.PinballSearchRadius = result;

            SubmitButtonClick?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the entered character is a digit or a dot
            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true; // Prevent the character from being entered
            }

            // Check if the text already contains a dot, and if so, prevent entering another dot
            if (e.Text == "." && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}
