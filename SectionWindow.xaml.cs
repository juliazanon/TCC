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
using TCC.MainClasses;

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
        private string editName = "";

        public event EventHandler SubmitButtonClick;

        // New Section constructor
        public SectionWindow(List<Section> sections)
        {
            InitializeComponent();
            NameTextBox.Focus();
            NameTextBox.CaretIndex = NameTextBox.Text.Length;
            SectionTitle.Text = "Create New Section";
            this.sections = sections;

            Cylindrical.Visibility = Visibility.Collapsed;
            TypeComboBox.SelectionChanged += SectionComboBox_SelectionChanged;
        }
        // Edit Section constructor
        public SectionWindow(List<Section> sections, Section section)
        {
            InitializeComponent();
            NameTextBox.Focus();
            NameTextBox.CaretIndex = NameTextBox.Text.Length;
            SectionTitle.Text = "Edit Section";
            this.sections = sections;
            editName = section.Name;
            TypeComboBox.IsEnabled = false;

            NameTextBox.Text = section.Name;
            if (section.Type == "rectangular")
            {
                Cylindrical.Visibility = Visibility.Collapsed;
                TypeComboBox.SelectedIndex = 0;

                RectangularSection rs = section as RectangularSection;
                WidthTextBox.Text = rs.Width.ToString();
                HeightTextBox.Text = rs.Height.ToString();
            }
            else if (section.Type == "tubular")
            {
                Rectangular.Visibility = Visibility.Collapsed;
                TypeComboBox.SelectedIndex = 1;

                TubularSection ts = section as TubularSection;
                InternalRadiusTextBox.Text = ts.InternalRadius.ToString();
                ExternalRadiusTextBox.Text = ts.ExternalRadius.ToString();
            }
        }

        public RectangularSection RectangularSection { get { return rectangularSection; } }
        public TubularSection TubularSection { get { return tubularSection; } }

        private void SectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedParameter = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (selectedParameter == "rectangular")
            {
                section = "rectangular";
                // Show Rectangular
                Rectangular.Visibility = Visibility.Visible;

                // Hide Cylindrical
                Cylindrical.Visibility = Visibility.Collapsed;
            }
            else if (selectedParameter == "tubular")
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
            if (sections.Count != 0)  // Conditions
            {
                if (sections.Any(obj => obj.Name == NameTextBox.Text) && NameTextBox.Text != editName)  // Name already used
                {
                    InputWarning("Name");
                    return;
                }
            }

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

                rectangularSection.Name = NameTextBox.Text;
                if (editName == "") rectangularSection.ID = sections.Count + 1;
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

                tubularSection.Name = NameTextBox.Text;
                if (editName == "") tubularSection.ID = sections.Count + 1;
                double.TryParse(InternalRadiusTextBox.Text, out double result);
                tubularSection.InternalRadius = result;
                double.TryParse(ExternalRadiusTextBox.Text, out result);
                tubularSection.ExternalRadius = result;
            }

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
    }
}
