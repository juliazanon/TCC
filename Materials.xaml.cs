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

namespace TCC
{
    /// <summary>
    /// Interaction logic for Materials.xaml
    /// </summary>
    public partial class Materials : Window
    {
        public float Teste { get; set; }
        public Materials()
        {
            InitializeComponent();
            IsotropicRadioButton.IsChecked = true;
        }
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
    }
}
