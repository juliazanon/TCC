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
using System.Text.RegularExpressions;
using System.Globalization;

namespace TCC
{
    /// <summary>
    /// Interaction logic for MaterialsWindow.xaml
    /// </summary>
    public partial class MaterialsWindow : Window
    {
        private Dictionary<int, LayerMaterial> materials;
        private Isotropic layerIsotropic;
        private Orthotropic layerOrthotropic;

        public event EventHandler SubmitButtonClick;
        public MaterialsWindow(Dictionary<int, LayerMaterial> materials)
        {
            InitializeComponent();
            this.materials = materials;
            IsotropicRadioButton.IsChecked = true;
        }
        public Isotropic LayerIsotropic { get { return layerIsotropic; } }
        public Orthotropic LayerOrthotropic { get { return layerOrthotropic; } }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsotropicRadioButton.IsChecked == true)
            {
                // Show Isotropic material fields
                MaterialIsotropic.Visibility = Visibility.Visible;
                YoungLabel.Visibility = Visibility.Visible;
                PoissonLabel.Visibility = Visibility.Visible;
                YoungTextBox.Visibility = Visibility.Visible;
                PoissonTextBox.Visibility = Visibility.Visible;

                // Hide Orthotropic material fields
                MaterialOrthotropic.Visibility = Visibility.Collapsed;
                ETitle.Visibility = Visibility.Collapsed;
                EInputs.Visibility = Visibility.Collapsed;
                NuTitle.Visibility = Visibility.Collapsed;
                NuInputs.Visibility = Visibility.Collapsed;
                GTitle.Visibility = Visibility.Collapsed;
                GInputs.Visibility = Visibility.Collapsed;
            }
            else if (OrthotropicRadioButton.IsChecked == true)
            {
                // Show Orthotropic material fields
                MaterialOrthotropic.Visibility = Visibility.Visible;
                ETitle.Visibility = Visibility.Visible;
                EInputs.Visibility = Visibility.Visible;
                NuTitle.Visibility = Visibility.Visible;
                NuInputs.Visibility = Visibility.Visible;
                GTitle.Visibility = Visibility.Visible;
                GInputs.Visibility = Visibility.Visible;

                // Hide Isotropic material fields
                MaterialIsotropic.Visibility = Visibility.Collapsed;
                YoungLabel.Visibility = Visibility.Collapsed;
                PoissonLabel.Visibility = Visibility.Collapsed;
                YoungTextBox.Visibility = Visibility.Collapsed;
                PoissonTextBox.Visibility = Visibility.Collapsed;
            }
        }
        private void SubmitNewMaterial(object sender, RoutedEventArgs e)
        {
            if (IsotropicRadioButton.IsChecked == true)
            {
                layerIsotropic = new Isotropic { Density = 1.0, ID = 1, Name = "New Material" };
                layerIsotropic.ID = materials.Count + 1;
                layerIsotropic.Name = NameTextBox.Text;
                double.TryParse(DensityTextBox.Text, out double result);
                layerIsotropic.Density = result;
                double.TryParse(PoissonTextBox.Text, out result);
                layerIsotropic.Poisson = result;
                double.TryParse(YoungTextBox.Text, out result);
                layerIsotropic.Young = result;
            }
            else if (OrthotropicRadioButton.IsChecked == true)
            {
                layerOrthotropic = new Orthotropic { Density = 1.0, ID = 1, Name = "New Material" };
                layerOrthotropic.ID = materials.Count + 1;
                layerOrthotropic.Name = NameTextBox.Text;
                //double.TryParse(DensityTextBox.Text, out double result);
                layerOrthotropic.Density = double.Parse(DensityTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                double.TryParse(EXTextBox.Text, out double result);
                layerOrthotropic.Ex = result;
                double.TryParse(EYTextBox.Text, out result);
                layerOrthotropic.Ey = result;
                double.TryParse(EZTextBox.Text, out result);
                layerOrthotropic.Ez = result;
                double.TryParse(NuXYTextBox.Text, out result);
                layerOrthotropic.Nxy = result;
                double.TryParse(NuXZTextBox.Text, out result);
                layerOrthotropic.Nxz = result;
                double.TryParse(NuYZTextBox.Text, out result);
                layerOrthotropic.Nyz = result;
                double.TryParse(GXYTextBox.Text, out result);
                layerOrthotropic.Gxy = result;
                double.TryParse(GXZTextBox.Text, out result);
                layerOrthotropic.Gxz = result;
                double.TryParse(GYZTextBox.Text, out result);
                layerOrthotropic.Gyz = result;
            }

            SubmitButtonClick?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
        private double ParseDouble(string input)
        {
            // Create a custom culture with a dot as the decimal separator
            var customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            // Use the custom culture for parsing
            return double.Parse(input, NumberStyles.AllowDecimalPoint, customCulture);
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
