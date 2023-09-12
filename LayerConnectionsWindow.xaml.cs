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
        private string editName;

        public event EventHandler SubmitButtonClick;
        public List<LayerConnection> connections;
        public LayerConnection LayerConnection { get { return layerConnection; } }

        // New Connection constructor
        public LayerConnectionsWindow(List<LayerConnection> connections,List<Layer> layers)
        {
            InitializeComponent();
            TitleTextBlock.Text = "Create New Connection";
            NameConnectionTextBox.Focus();
            NameConnectionTextBox.CaretIndex = NameConnectionTextBox.Text.Length;
            this.connections = connections;

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
        // Edit Connection constructor
        public LayerConnectionsWindow(List<LayerConnection> connections, List<Layer> layers, LayerConnection connection)
        {
            InitializeComponent();
            TitleTextBlock.Text = "Edit Connection";
            NameConnectionTextBox.Focus();
            NameConnectionTextBox.CaretIndex = NameConnectionTextBox.Text.Length;
            this.connections = connections;
            editName = connection.Name;

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
            }
            else
            {
                FirstLayerComboBox.ItemsSource = layers;
                FirstLayerComboBox.SelectedIndex = 0;
                SecondLayerComboBox.ItemsSource = layers;
                SecondLayerComboBox.SelectedIndex = 1;
            }

            NameConnectionTextBox.Text = connection.Name.ToString();
            FrictionalRadioButton.IsChecked = (connection.Type == "frictional");
            BondedRadioButton.IsChecked = (connection.Type == "bonded");
            FirstLayerComboBox.SelectedItem = connection.FirstLayer;
            SecondLayerComboBox.SelectedItem = connection.SecondLayer;
            FrictionTextBox.Text = connection.FrictionCoefficient.ToString();
            XNormalTextBox.Text = connection.NormalDirection[0].ToString();
            YNormalTextBox.Text = connection.NormalDirection[1].ToString();
            ZNormalTextBox.Text = connection.NormalDirection[2].ToString();
            XFTangentTextBox.Text = connection.FirstTangentDirection[0].ToString();
            YFTangentTextBox.Text = connection.FirstTangentDirection[1].ToString();
            ZFTangentTextBox.Text = connection.FirstTangentDirection[2].ToString();
            XSTangentTextBox.Text = connection.SecondTangentDirection[0].ToString();
            YSTangentTextBox.Text = connection.SecondTangentDirection[1].ToString();
            ZSTangentTextBox.Text = connection.SecondTangentDirection[2].ToString();
            NormalTextBox.Text = connection.NormalPenalty.ToString();
            TangentialTextBox.Text = connection.TangentialPenalty.ToString();
            PinballTextBox.Text = connection.PinballSearchRadius.ToString();
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
            if (connections.Count != 0)  // Conditions
            {
                if (connections.Any(obj => obj.Name == NameConnectionTextBox.Text) && NameConnectionTextBox.Text != editName)  // Name already used
                {
                    InputWarning("Name");
                    return;
                }
            }
            if (firstLayer.Name == secondLayer.Name)  // Layers
            {
                InputWarning("Layer");
                return;
            }

            layerConnection = new LayerConnection();

            if (FrictionalRadioButton.IsChecked == true) { layerConnection.Type = "frictional"; }
            else { layerConnection.Type = "bonded"; }

            layerConnection.Name = NameConnectionTextBox.Text;
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
        private void InputWarning(string inputfild)
        {
            switch (inputfild)
            {
                case "Name":
                    NameWarningTextBlock.Text = "Name already used";
                    NameWarningTextBlock.Height = 18;
                    LayerWarningTextBlock.Height = 0;
                    //GeralMenuRow.Height = new GridLength(108);
                    break;
                case "Layer":
                    LayerWarningTextBlock.Text = "Can't create a connection on same layer";
                    LayerWarningTextBlock.Height = 18;
                    NameWarningTextBlock.Height = 0;
                    break;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitNewLayerConnection(sender, e);
            }
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
