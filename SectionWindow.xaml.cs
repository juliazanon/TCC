using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TCC.Classes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for SectionWindow.xaml
    /// </summary>
    public partial class SectionWindow : Window
    {
        private List<Section> sections;
        private RectangularSection rectangularSection;
        private TubularSection tubularSection;
        private string section = "rectangular";

        public event EventHandler SubmitButtonClick;

        public SectionWindow(List<Section> sections)
        {
            InitializeComponent();
            this.sections = sections;

            Cylindrical.Visibility = Visibility.Collapsed;
            sectionComboBox.SelectionChanged += SectionComboBox_SelectionChanged;
        }

        public RectangularSection RectangularSection { get { return rectangularSection; } }
        public TubularSection TubularSection { get { return tubularSection; } }

        private void SectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedParameter = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (selectedParameter == "Rectangular")
            {
                section = "rectangular";
                // Show Rectangular
                Rectangular.Visibility = Visibility.Visible;

                // Hide Cylindrical
                Cylindrical.Visibility = Visibility.Collapsed;
            }
            else if (selectedParameter == "Tubular")
            {
                section = "tubular";
                // Show Cylindrical
                Cylindrical.Visibility = Visibility.Visible;

                // Hide Rectangular
                Rectangular.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitNewSection(sender, e);
            }
        }
        private void SubmitNewSection(object sender, RoutedEventArgs e)
        {
            if (section == "rectangular")
            {
                rectangularSection = new RectangularSection
                {
                    Name = "New Section",
                    ID = 0,
                    Type = "rectangular",
                    Width = 1.0,
                    Height = 1.0
                };

                rectangularSection.Name = SectionName.Text;
                rectangularSection.ID = sections.Count + 1;
                double.TryParse(WidthTextBox.Text, out double result);
                rectangularSection.Width = result;
                double.TryParse(HeightTextBox.Text, out result);
                rectangularSection.Height = result;
            }
            else if (section == "tubular")
            {
                tubularSection = new TubularSection
                {
                    Name = "New Section",
                    ID = 0,
                    Type = "tubular",
                    InternalRadius = 0.0,
                    ExternalRadius = 1.0
                };

                tubularSection.Name = SectionName.Text;
                tubularSection.ID = sections.Count + 1;
                double.TryParse(InternalRadiusTextBox.Text, out double result);
                tubularSection.InternalRadius = result;
                double.TryParse(ExternalRadiusTextBox.Text, out result);
                tubularSection.ExternalRadius = result;
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
