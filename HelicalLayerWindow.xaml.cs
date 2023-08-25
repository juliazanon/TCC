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

            //  Materials comboBox
            if (materials.Count == 0)
            {
                List<LayerMaterial> materialList = new List<LayerMaterial>
                {
                    new LayerMaterial { ID = 0, Name = "No Material Created" },
                };
                MaterialComboBox.ItemsSource = materialList;
                MaterialComboBox.SelectedIndex = 0;
            }
            else
            {
                MaterialComboBox.ItemsSource = materials.Values;
                MaterialComboBox.SelectedIndex = 0;
            }
            
            // Hide Cylindrical Coordinates elements
            TextBlockCilyndricalCoord1Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord1End.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2End.Visibility = Visibility.Collapsed;
            
            //  Handle the ComboBox SelectionChange
            coordinateComboBoxStart.SelectionChanged += SectionComboBox_SelectionChanged_CoordinateStart;
            coordinateComboBoxEnd.SelectionChanged += SectionComboBox_SelectionChanged_CoordinateEnd;
        }

        //  Material
        private void MaterialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaterialComboBox.SelectedItem != null)
            {
                // Get the selected Material instance.
                LayerMaterial selectedMaterial = (LayerMaterial)MaterialComboBox.SelectedItem;

                // Access the selected ID and Name.
                int selectedID = selectedMaterial.ID;
                
                // USE THIS VALUE AS MATERIAL ID

                // teste.Text = selectedID.ToString();
            }
        }

        //  Combobox coordinate
        private void SectionComboBox_SelectionChanged_CoordinateStart(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedParameter = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (selectedParameter == "Cartesian")
            {
                // Show Cartesian
                TextBlockCartesianCoord1Start.Visibility = Visibility.Visible;
                TextBlockCartesianCoord2Start.Visibility = Visibility.Visible;

                // Hide Cylindrical
                TextBlockCilyndricalCoord1Start.Visibility = Visibility.Collapsed;
                TextBlockCilyndricalCoord2Start.Visibility = Visibility.Collapsed;
            }
            else if (selectedParameter == "Cylindrical")
            {
                // Show Cylindrical
                TextBlockCilyndricalCoord1Start.Visibility = Visibility.Visible;
                TextBlockCilyndricalCoord2Start.Visibility = Visibility.Visible;

                // Hide Cartesian
                TextBlockCartesianCoord1Start.Visibility = Visibility.Collapsed;
                TextBlockCartesianCoord2Start.Visibility = Visibility.Collapsed;
            }
        }

        private void SectionComboBox_SelectionChanged_CoordinateEnd(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedParameter = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (selectedParameter == "Cartesian")
            {
                // Show Cartesian
                TextBlockCartesianCoord1End.Visibility = Visibility.Visible;
                TextBlockCartesianCoord2End.Visibility = Visibility.Visible;

                // Hide Cylindrical
                TextBlockCilyndricalCoord1End.Visibility = Visibility.Collapsed;
                TextBlockCilyndricalCoord2End.Visibility = Visibility.Collapsed;
            }
            else if (selectedParameter == "Cylindrical")
            {
                // Show Cylindrical
                TextBlockCilyndricalCoord1End.Visibility = Visibility.Visible;
                TextBlockCilyndricalCoord2End.Visibility = Visibility.Visible;

                // Hide Cartesian
                TextBlockCartesianCoord1End.Visibility = Visibility.Collapsed;
                TextBlockCartesianCoord2End.Visibility = Visibility.Collapsed;
            }
        }

        private void SubmitNewLayer(object sender, RoutedEventArgs e)
        {

        }
    }
}
