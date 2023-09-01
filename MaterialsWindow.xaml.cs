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
        private List<LayerMaterial> materials;
        private Isotropic layerIsotropic;
        private Orthotropic layerOrthotropic;
        private bool isEdit = false;

        public event EventHandler SubmitButtonClick;
        public MaterialsWindow(List<LayerMaterial> materials)
        {
            InitializeComponent();
            this.materials = materials;
            IsotropicRadioButton.IsChecked = true;
        }
        public MaterialsWindow(List<LayerMaterial> materials, LayerMaterial material)
        {
            InitializeComponent();
            this.materials = materials;
            IsotropicRadioButton.IsChecked = true;
            isEdit = true;

            NameTextBox.Text = material.Name.ToString();
            DensityTextBox.Text = material.Density.ToString();
            if (material.Type == "isotropic")
            {
                Isotropic iso = material as Isotropic;
                IsotropicRadioButton.IsChecked = true;
                YoungTextBox.Text = iso.Young.ToString();
                PoissonTextBox.Text = iso.Poisson.ToString();
            }
            else if (material.Type == "orthotropic")
            {
                Orthotropic ortho = material as Orthotropic;
                OrthotropicRadioButton.IsChecked = true;
                EXTextBox.Text = ortho.Young[0].ToString();
                EYTextBox.Text = ortho.Young[1].ToString();
                EZTextBox.Text = ortho.Young[2].ToString();
                NuXYTextBox.Text = ortho.Poisson[0].ToString();
                NuXZTextBox.Text = ortho.Poisson[1].ToString();
                NuYZTextBox.Text = ortho.Poisson[2].ToString();
                GXYTextBox.Text = ortho.Shear[0].ToString();
                GXZTextBox.Text = ortho.Shear[1].ToString();
                GYZTextBox.Text = ortho.Shear[2].ToString();
            }
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitNewMaterial(sender, e);
            }
        }
        private void SubmitNewMaterial(object sender, RoutedEventArgs e)
        {
            if (IsotropicRadioButton.IsChecked == true)
            {
                layerIsotropic = new Isotropic { Density = 1.0, ID = 1, Name = "New Material", Type = "isotropic" };
                if (!isEdit) layerIsotropic.ID = materials.Count + 1;
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
                layerOrthotropic = new Orthotropic { Density = 1.0, ID = 1, Name = "New Material", Type = "orthotropic" };
                if (!isEdit) layerOrthotropic.ID = materials.Count + 1;
                layerOrthotropic.Name = NameTextBox.Text;
                double.TryParse(DensityTextBox.Text, out double result);
                layerOrthotropic.Density = result;

                double.TryParse(EXTextBox.Text, out double xresult);
                double.TryParse(EYTextBox.Text, out double yresult);
                double.TryParse(EZTextBox.Text, out double zresult);
                layerOrthotropic.Young = new double[] { xresult, yresult, zresult };

                double.TryParse(NuXYTextBox.Text, out xresult);
                double.TryParse(NuXZTextBox.Text, out yresult);
                double.TryParse(NuYZTextBox.Text, out zresult);
                layerOrthotropic.Poisson = new double[] { xresult, yresult, zresult };

                double.TryParse(GXYTextBox.Text, out xresult);
                double.TryParse(GXZTextBox.Text, out yresult);
                double.TryParse(GYZTextBox.Text, out zresult);
                layerOrthotropic.Shear = new double[] { xresult, yresult, zresult };
            }

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
