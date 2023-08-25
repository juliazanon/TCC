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
        public int Wires { get; set; }
        public Line Line { get; set; }
        public double Length { get; set; }
        public int SectionID { get; set; }
        public double Radius { get; set; }
        public double LayAngle { get; set; }
        public double InitialAngle { get; set; }
        public int Divisions { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public int MaterialID { get; set; }
        public float[] BodyLoad { get; set; }
        public HelicalLayerWindow(Dictionary<int, Section> sections, Dictionary<int, LayerMaterial> materials)
        {
            InitializeComponent();
            TextBlockCilyndricalCoord1.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2.Visibility = Visibility.Collapsed;
            coordinateComboBox.SelectionChanged += SectionComboBox_SelectionChanged_Coordinate;
        }

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

        }
    }
}
