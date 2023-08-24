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
using System.Text.RegularExpressions;

namespace TCC
{
    /// <summary>
    /// Interaction logic for MaterialsWindow.xaml
    /// </summary>
    public partial class MaterialsWindow : Window
    {
        private Dictionary<int, LayerMaterial> materials;
        private Isotropic layerIsotropic = new Isotropic { Density = 1.0, ID = 1, Name = "New Material", Poisson = 1.0, Young = 1.0 };
        private Orthotropic layerOrthotropic;

        public event EventHandler SubmitButtonClick;
        public MaterialsWindow(Dictionary<int, LayerMaterial> materials)
        {
            InitializeComponent();
            this.materials = materials;
            IsotropicRadioButton.IsChecked = true;
        }
        public Isotropic LayerIsotropic { get { return layerIsotropic; } }
        public LayerMaterial LayerOrthotropic { get { return layerOrthotropic; } }

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

            }

            SubmitButtonClick?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool isNumeric = Regex.IsMatch(e.Text, @"^\d+$");
            // Check if the entered text is not a digit
            if (!isNumeric)
            {
                e.Handled = true; // Block non-numeric input
            }
        }
    }
}
