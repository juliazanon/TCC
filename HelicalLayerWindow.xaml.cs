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
using TCC.Classes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for HelicalLayerWindow.xaml
    /// </summary>
    public partial class HelicalLayerWindow : Window
    {
        //private int wires;
        //private Line line;
        //private double length;
        //private int sectionID;
        //private double radius;
        //private double layAngle;
        //private double initialAngle;
        //private int divisions;
        //private string label;
        //private string type;
        //private int materialID;
        //private float[] bodyLoad;

        private HelixLayer helixLayer;

        public event EventHandler SubmitButtonClick;

        public HelicalLayerWindow(Dictionary<int, Section> sections, Dictionary<int, LayerMaterial> materials)
        {
            InitializeComponent();
            TextBlockCilyndricalCoord1.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2.Visibility = Visibility.Collapsed;
            CoordinateComboBox.SelectionChanged += SectionComboBox_SelectionChanged_Coordinate;
        }

        public HelixLayer HelixLayer { get { return helixLayer; } }

        private void SectionComboBox_SelectionChanged_Coordinate(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedParameter = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (selectedParameter == "Cartesian")
            {
                // Show Cartesian
                TextBlockCartesianCoord1.Visibility = Visibility.Visible;
                TextBlockCartesianCoord2.Visibility = Visibility.Visible;

                // Hide Cylindrical
                TextBlockCilyndricalCoord1.Visibility = Visibility.Collapsed;
                TextBlockCilyndricalCoord2.Visibility = Visibility.Collapsed;
            }
            else if (selectedParameter == "Cylindrical")
            {
                // Show Cylindrical
                TextBlockCilyndricalCoord1.Visibility = Visibility.Visible;
                TextBlockCilyndricalCoord2.Visibility = Visibility.Visible;

                // Hide Cartesian
                TextBlockCartesianCoord1.Visibility = Visibility.Collapsed;
                TextBlockCartesianCoord2.Visibility = Visibility.Collapsed;
            }
        }

        private void SubmitNewLayer(object sender, RoutedEventArgs e)
        {
            helixLayer = new HelixLayer
            {
                Line = new Line
                {
                    Start = new Boundaries(),
                    End = new Boundaries()
                }
            };

            int.TryParse(WiresTextBox.Text, out int intresult);
            helixLayer.Wires = intresult;
            helixLayer.Line.FourierOrder = 0;
            helixLayer.Line.DesignOnly = false;
            helixLayer.Line.Start.ID = 1;
            helixLayer.Line.Start.DesignOnly = false;
            helixLayer.Line.Start.CoordinateSystem = CoordinateComboBox.Text;

            double.TryParse(LengthTextBox.Text, out double result);
            helixLayer.Length = result;
            double.TryParse(LengthTextBox.Text, out result);
            helixLayer.Radius = result;
            double.TryParse(LayAngleTextBox.Text, out result);
            helixLayer.LayAngle = result;
            double.TryParse(InitialAngleTextBox.Text, out result);
            helixLayer.InitialAngle = result;
            int.TryParse(DivisionsTextBox.Text, out intresult);
            helixLayer.Divisions = intresult;
            helixLayer.Name = NameTextBox.Text;
            helixLayer.Type = "helix";
            double.TryParse(BodyLoadXTextBox.Text, out double xresult);
            double.TryParse(BodyLoadYTextBox.Text, out double yresult);
            double.TryParse(BodyLoadZTextBox.Text, out double zresult);
            helixLayer.BodyLoad = new double[] { xresult, yresult, zresult };


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

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the entered character is a digit or a dot
            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }
    }
}
