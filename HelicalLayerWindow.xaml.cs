﻿using System;
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

        public HelixLayer HelixLayer { get { return helixLayer; } }

        public HelicalLayerWindow(Dictionary<int, Section> sections, Dictionary<int, LayerMaterial> materials)
        {
            InitializeComponent();

            List<string> materialNames = materials.Values.Select(material => material.Name).ToList();
            if (materials.Count == 0)
            {
                MaterialComboBox.ItemsSource = new List<string> { "No Material" };
                MaterialComboBox.SelectedIndex = 0;
            }
            else
            {
                List<LayerMaterial> materialList = materials.Values.ToList();
                MaterialComboBox.ItemsSource = materialList;
            }
            TextBlockCilyndricalCoord1Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord1End.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2End.Visibility = Visibility.Collapsed;
            CoordinateComboBoxStart.SelectionChanged += SectionComboBox_SelectionChanged_CoordinateStart;
            CoordinateComboBoxEnd.SelectionChanged += SectionComboBox_SelectionChanged_CoordinateEnd;
        }

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

            // Start
            helixLayer.Line.Start.ID = 1;
            helixLayer.Line.Start.DesignOnly = false;
            helixLayer.Line.Start.CoordinateSystem = CoordinateComboBoxStart.Text;

            double.TryParse(XCoordTextBoxStart.Text, out xresult);
            double.TryParse(YCoordTextBoxStart.Text, out yresult);
            double.TryParse(ZCoordTextBoxStart.Text, out zresult);
            double.TryParse(RXCoordTextBoxStart.Text, out double rxresult);
            double.TryParse(RYCoordTextBoxStart.Text, out double ryresult);
            double.TryParse(RZCoordTextBoxStart.Text, out double rzresult);
            helixLayer.Line.Start.Coordinates = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            double.TryParse(FxTextBoxStart.Text, out xresult);
            double.TryParse(FyTextBoxStart.Text, out yresult);
            double.TryParse(FzTextBoxStart.Text, out zresult);
            double.TryParse(TxTextBoxStart.Text, out rxresult);
            double.TryParse(TyTextBoxStart.Text, out ryresult);
            double.TryParse(TzTextBoxStart.Text, out rzresult);
            helixLayer.Line.Start.Loads = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            string fxstatus = FxComboBoxStart.Text;
            string fystatus = FyComboBoxStart.Text;
            string fzstatus = FzComboBoxStart.Text;
            string txstatus = TxComboBoxStart.Text;
            string tystatus = TyComboBoxStart.Text;
            string tzstatus = TzComboBoxStart.Text;
            helixLayer.Line.Start.Status = new string[] { fxstatus, fystatus, fzstatus, txstatus, tystatus, tzstatus };

            double.TryParse(FxDispTextBoxStart.Text, out xresult);
            double.TryParse(FyDispTextBoxStart.Text, out yresult);
            double.TryParse(FzDispTextBoxStart.Text, out zresult);
            double.TryParse(TxDispTextBoxStart.Text, out rxresult);
            double.TryParse(TyDispTextBoxStart.Text, out ryresult);
            double.TryParse(TzDispTextBoxStart.Text, out rzresult);
            helixLayer.Line.Start.ImposedDisplacements = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            // End
            helixLayer.Line.End.ID = 1;
            helixLayer.Line.End.DesignOnly = false;
            helixLayer.Line.End.CoordinateSystem = CoordinateComboBoxEnd.Text;

            double.TryParse(XCoordTextBoxEnd.Text, out xresult);
            double.TryParse(YCoordTextBoxEnd.Text, out yresult);
            double.TryParse(ZCoordTextBoxEnd.Text, out zresult);
            double.TryParse(RXCoordTextBoxEnd.Text, out rxresult);
            double.TryParse(RYCoordTextBoxEnd.Text, out ryresult);
            double.TryParse(RZCoordTextBoxEnd.Text, out rzresult);
            helixLayer.Line.End.Coordinates = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            double.TryParse(FxTextBoxEnd.Text, out xresult);
            double.TryParse(FyTextBoxEnd.Text, out yresult);
            double.TryParse(FzTextBoxEnd.Text, out zresult);
            double.TryParse(TxTextBoxEnd.Text, out rxresult);
            double.TryParse(TyTextBoxEnd.Text, out ryresult);
            double.TryParse(TzTextBoxEnd.Text, out rzresult);
            helixLayer.Line.End.Loads = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            fxstatus = FxComboBoxEnd.Text;
            fystatus = FyComboBoxEnd.Text;
            fzstatus = FzComboBoxEnd.Text;
            txstatus = TxComboBoxEnd.Text;
            tystatus = TyComboBoxEnd.Text;
            tzstatus = TzComboBoxEnd.Text;
            helixLayer.Line.End.Status = new string[] { fxstatus, fystatus, fzstatus, txstatus, tystatus, tzstatus };

            double.TryParse(FxDispTextBoxEnd.Text, out xresult);
            double.TryParse(FyDispTextBoxEnd.Text, out yresult);
            double.TryParse(FzDispTextBoxEnd.Text, out zresult);
            double.TryParse(TxDispTextBoxEnd.Text, out rxresult);
            double.TryParse(TyDispTextBoxEnd.Text, out ryresult);
            double.TryParse(TzDispTextBoxEnd.Text, out rzresult);
            helixLayer.Line.End.ImposedDisplacements = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            // Line
            helixLayer.Line.FourierOrder = 0;
            helixLayer.Line.DesignOnly = false;

            double.TryParse(FxTextBox.Text, out xresult);
            double.TryParse(FyTextBox.Text, out yresult);
            double.TryParse(FzTextBox.Text, out zresult);
            double.TryParse(TxTextBox.Text, out rxresult);
            double.TryParse(TyTextBox.Text, out ryresult);
            double.TryParse(TzTextBox.Text, out rzresult);
            helixLayer.Line.DistributedLoads = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            fxstatus = FxComboBox.Text;
            fystatus = FyComboBox.Text;
            fzstatus = FzComboBox.Text;
            txstatus = TxComboBox.Text;
            tystatus = TyComboBox.Text;
            tzstatus = TzComboBox.Text;
            helixLayer.Line.Status = new string[] { fxstatus, fystatus, fzstatus, txstatus, tystatus, tzstatus };

            double.TryParse(FxDispTextBox.Text, out xresult);
            double.TryParse(FyDispTextBox.Text, out yresult);
            double.TryParse(FzDispTextBox.Text, out zresult);
            double.TryParse(TxDispTextBox.Text, out rxresult);
            double.TryParse(TyDispTextBox.Text, out ryresult);
            double.TryParse(TzDispTextBox.Text, out rzresult);
            helixLayer.Line.ImposedDisplacements = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };


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
