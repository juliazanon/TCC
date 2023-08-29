﻿using System;
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

namespace TCC
{
    /// <summary>
    /// Interaction logic for CylindricalLayerWindow.xaml
    /// </summary>
    public partial class CylindricalLayerWindow : Window
    {
        private CylinderLayer cylinderLayer;
        private LayerMaterial material;
        private List<Area> areas;

        public event EventHandler SubmitButtonClick;

        public CylinderLayer CylinderLayer { get { return cylinderLayer; } }
        public CylindricalLayerWindow(Dictionary<int, LayerMaterial> materials)
        {
            InitializeComponent();
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
            string areaType = clickedButton.Name;

            CylindricalAreasWindow windowArea = new CylindricalAreasWindow(areaType);

            windowArea.SubmitButtonClick += SubmitAreaButtonClick;
            windowArea.Show();
        }
        private void SubmitAreaButtonClick(object sender, EventArgs e)
        {
            CylindricalAreasWindow windowArea = sender as CylindricalAreasWindow;
            Area area = windowArea.Area;

            areas.Add(area);
        }

        private void SubmitNewLayer(object sender, RoutedEventArgs e)
        {
            cylinderLayer = new CylinderLayer();
            cylinderLayer.Name = NameTextBox.Text;
            cylinderLayer.Material = material;
            // FALTA THICKNESS

            double.TryParse(RadiusTextBox.Text, out double result);
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
    }
}
