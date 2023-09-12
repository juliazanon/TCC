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
using TCC.MainClasses;

namespace TCC
{
    /// <summary>
    /// Interaction logic for CylindricalLayerWindow.xaml
    /// </summary>
    public partial class CylindricalLayerWindow : Window
    {
        private CylinderLayer cylinderLayer;
        private List<Layer> layers;
        private LayerMaterial material;
        private List<Area> areas = new List<Area>();
        private bool isChildWindowOpen = false;
        private string editName;

        public event EventHandler SubmitButtonClick;

        public CylinderLayer CylinderLayer { get { return cylinderLayer; } }

        // New Cylinder constructor
        public CylindricalLayerWindow(List<Layer> layers,List<LayerMaterial> materials)
        {
            InitializeComponent();
            TitleTextBlock.Text = "Create New Cylindrical Layer";
            NameTextBox.Focus();
            NameTextBox.CaretIndex = NameTextBox.Text.Length;
            this.layers = layers;

            //  Materials comboBox
            if (materials.Count == 0)
            {
                List<LayerMaterial> materialList = new List<LayerMaterial>
                {
                    new Isotropic { ID = 0, Name = "No Material Created" },
                };
                MaterialComboBox.ItemsSource = materialList;
                MaterialComboBox.SelectedIndex = 0;
            }
            else
            {
                MaterialComboBox.ItemsSource = materials;
                MaterialComboBox.SelectedIndex = 0;
            }
        }
        // Edit Cylinder constructor
        public CylindricalLayerWindow(List<Layer> layers, List<LayerMaterial> materials, CylinderLayer layer)
        {
            InitializeComponent();
            TitleTextBlock.Text = "Edit Cylindrical Layer";
            NameTextBox.Focus();
            NameTextBox.CaretIndex = NameTextBox.Text.Length;
            this.layers = layers;
            editName = layer.Name;

            //  Materials comboBox
            if (materials.Count == 0)
            {
                List<LayerMaterial> materialList = new List<LayerMaterial>
                {
                    new Isotropic { ID = 0, Name = "No Material Created" },
                };
                MaterialComboBox.ItemsSource = materialList;
                MaterialComboBox.SelectedIndex = 0;
            }
            else
            {
                MaterialComboBox.ItemsSource = materials;
                MaterialComboBox.SelectedIndex = 0;
            }

            NameTextBox.Text = layer.Name;
            RadiusTextBox.Text = layer.Radius.ToString();
            ThicknessTextBox.Text = layer.Thickness.ToString();
            LengthTextBox.Text = layer.Length.ToString();
            MaterialComboBox.SelectedItem = layer.Material;
            DivisionsTextBox.Text = layer.Divisions.ToString();
            FourierTextBox.Text = layer.FourierOrder.ToString();
            RadialTextBox.Text = layer.RadialDivisions.ToString();
            AxialTextBox.Text = layer.AxialDivisions.ToString();
            XTextBox.Text = layer.BodyLoad[0].ToString();
            YTextBox.Text = layer.BodyLoad[1].ToString();
            ZTextBox.Text = layer.BodyLoad[2].ToString();
            areas = layer.Areas;
        }

        //  Material
        private void MaterialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaterialComboBox.SelectedItem != null)
            {
                // Get the selected Material instance.
                this.material = (LayerMaterial)MaterialComboBox.SelectedItem;
            }
        }

        //  Areas
        private void ButtonNewArea(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender; // Cast the sender as a Button
            string areaType = "";

            switch (clickedButton.Name)
            {
                case "InternalButton":
                    areaType = "Internal";
                    break;
                case "ExternalButton":
                    areaType = "External";
                    break;
                case "BottomButton":
                    areaType = "Bottom";
                    break;
                case "TopButton":
                    areaType = "Top";
                    break;
            }

            if (areaType != "")
            {
                bool exists = false;
                for (int i = 0; i < areas.Count; i++)
                {
                    if (areas[i].Surface == areaType)
                    {
                        exists = true;
                        CylindricalAreasWindow windowArea = new CylindricalAreasWindow(areaType, areas[i]);
                        windowArea.SubmitButtonClick += SubmitAreaButtonClick;
                        windowArea.Closed += AreaWindow_Closed;
                        windowArea.Show();
                        this.IsEnabled = false;
                        isChildWindowOpen = true;
                        break;
                    }
                }

                if (!exists)
                {
                    CylindricalAreasWindow windowArea = new CylindricalAreasWindow(areaType);
                    windowArea.SubmitButtonClick += SubmitAreaButtonClick;
                    windowArea.Closed += AreaWindow_Closed;
                    windowArea.Show();
                    this.IsEnabled = false;
                    isChildWindowOpen = true;
                }
            }
        }
        private void SubmitAreaButtonClick(object sender, EventArgs e)
        {
            CylindricalAreasWindow windowArea = sender as CylindricalAreasWindow;
            Area area = windowArea.Area;

            bool exists = false;
            for (int i = 0; i < areas.Count; i++)
            {
                if (areas[i].Surface == area.Surface)
                {
                    exists = true;
                    areas[i] = area;
                    break;
                }
            }
            if (!exists) areas.Add(area);
        }
        private void AreaWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }

        private void SubmitNewLayer(object sender, RoutedEventArgs e)
        {
            if (layers.Count != 0)
            {
                if (layers.Any(obj => obj.Name == NameTextBox.Text) && NameTextBox.Text != editName)
                {
                    InputWarning("Name");
                    return;
                }
            }

            cylinderLayer = new CylinderLayer
            {
                Name = NameTextBox.Text,
                Type = "cylinder",
                Material = material,
                Areas = areas,
            };
            
            double.TryParse(ThicknessTextBox.Text, out double result);
            cylinderLayer.Thickness = result;
            double.TryParse(RadiusTextBox.Text, out result);
            cylinderLayer.Radius = result;
            double.TryParse(LengthTextBox.Text, out result);
            cylinderLayer.Length = result;
            int.TryParse(DivisionsTextBox.Text, out int intresult);
            cylinderLayer.Divisions = intresult;
            int.TryParse(FourierTextBox.Text, out intresult);
            cylinderLayer.FourierOrder = intresult;
            int.TryParse(RadialTextBox.Text, out intresult);
            cylinderLayer.RadialDivisions = intresult;
            int.TryParse(AxialTextBox.Text, out intresult);
            cylinderLayer.AxialDivisions = intresult;

            double.TryParse(XTextBox.Text, out double xresult);
            double.TryParse(YTextBox.Text, out double yresult);
            double.TryParse(ZTextBox.Text, out double zresult);
            cylinderLayer.BodyLoad = new double[] { xresult, yresult, zresult };

            SubmitButtonClick?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
        private void InputWarning(string inputfild)
        {
            if (inputfild == "Name")
            {
                NameWarningTextBlock.Text = "Name already used";
                NameWarningTextBlock.Height = 18;
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

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the entered character is a digit or a dot
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitNewLayer(sender, e);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isChildWindowOpen) e.Cancel = true;
        }
    }
}
