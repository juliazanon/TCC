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
using TCC.Classes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for CylindricalAreasWindow.xaml
    /// </summary>
    public partial class CylindricalAreasWindow : Window
    {
        private Area area;
        private string areaType;
        public event EventHandler SubmitButtonClick;

        public Area Area { get { return area; } }

        public CylindricalAreasWindow(string areaType)
        {
            InitializeComponent();

            WindowTitle.Text = areaType;
            this.areaType = areaType;

            // Hide Cylindrical Coordinates elements
            TextBlockCilyndricalCoord1Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord1End.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2End.Visibility = Visibility.Collapsed;

            //  Handle the ComboBox SelectionChange
            CoordinateComboBoxStart.SelectionChanged += ComboBox_SelectionChanged_CoordinateStart;
            CoordinateComboBoxEnd.SelectionChanged += ComboBox_SelectionChanged_CoordinateEnd;
        }

        //  Combobox coordinate
        private void ComboBox_SelectionChanged_CoordinateStart(object sender, SelectionChangedEventArgs e)
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

        private void ComboBox_SelectionChanged_CoordinateEnd(object sender, SelectionChangedEventArgs e)
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

        private void SubmitNewArea(object sender, RoutedEventArgs e)
        {
            area = new Area
            {
                Frontier = new Line
                {
                    Start = new Boundaries(),
                    End = new Boundaries(),
                }
            };

            area.Surface = areaType;

            double.TryParse(XPressureTextBox.Text, out double xresult);
            double.TryParse(YPressureTextBox.Text, out double yresult);
            double.TryParse(ZPressureTextBox.Text, out double zresult);
            double.TryParse(RxPressureTextBox.Text, out double rxresult);
            double.TryParse(RyPressureTextBox.Text, out double ryresult);
            double.TryParse(RzPressureTextBox.Text, out double rzresult);
            area.Pressure = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            string fxstatus = FxStatusComboBox.Text;
            string fystatus = FyStatusComboBox.Text;
            string fzstatus = FzStatusComboBox.Text;
            string txstatus = TxStatusComboBox.Text;
            string tystatus = TyStatusComboBox.Text;
            string tzstatus = TzStatusComboBox.Text;
            area.Status = new string[] { fxstatus, fystatus, fzstatus, txstatus, tystatus, tzstatus };

            double.TryParse(FxDispTextBox.Text, out xresult);
            double.TryParse(FyDispTextBox.Text, out yresult);
            double.TryParse(FzDispTextBox.Text, out zresult);
            double.TryParse(TxDispTextBox.Text, out rxresult);
            double.TryParse(TyDispTextBox.Text, out ryresult);
            double.TryParse(TzDispTextBox.Text, out rzresult);
            area.ImposedDisplacements = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            // FALTA DISTRIBUTED LOADS, STATUS E IMPOSED DISPLACEMENT DO FRONTIER

            // Start
            area.Frontier.Start.ID = 1;
            area.Frontier.Start.DesignOnly = false;
            area.Frontier.Start.CoordinateSystem = CoordinateComboBoxStart.Text;

            double.TryParse(XCoordTextBoxStart.Text, out xresult);
            double.TryParse(YCoordTextBoxStart.Text, out yresult);
            double.TryParse(ZCoordTextBoxStart.Text, out zresult);
            double.TryParse(RxCoordTextBoxStart.Text, out rxresult);
            double.TryParse(RyCoordTextBoxStart.Text, out ryresult);
            double.TryParse(RzCoordTextBoxStart.Text, out rzresult);
            area.Frontier.Start.Coordinates = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            double.TryParse(FxTextBoxStart.Text, out xresult);
            double.TryParse(FyTextBoxStart.Text, out yresult);
            double.TryParse(FzTextBoxStart.Text, out zresult);
            double.TryParse(TxTextBoxStart.Text, out rxresult);
            double.TryParse(TyTextBoxStart.Text, out ryresult);
            double.TryParse(TzTextBoxStart.Text, out rzresult);
            area.Frontier.Start.Loads = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            fxstatus = FxComboBoxStart.Text;
            fystatus = FyComboBoxStart.Text;
            fzstatus = FzComboBoxStart.Text;
            txstatus = TxComboBoxStart.Text;
            tystatus = TyComboBoxStart.Text;
            tzstatus = TzComboBoxStart.Text;
            area.Frontier.Start.Status = new string[] { fxstatus, fystatus, fzstatus, txstatus, tystatus, tzstatus };

            double.TryParse(FxDispTextBoxStart.Text, out xresult);
            double.TryParse(FyDispTextBoxStart.Text, out yresult);
            double.TryParse(FzDispTextBoxStart.Text, out zresult);
            double.TryParse(TxDispTextBoxStart.Text, out rxresult);
            double.TryParse(TyDispTextBoxStart.Text, out ryresult);
            double.TryParse(TzDispTextBoxStart.Text, out rzresult);
            area.Frontier.Start.ImposedDisplacements = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            // End
            area.Frontier.End.ID = 1;
            area.Frontier.End.DesignOnly = false;
            area.Frontier.End.CoordinateSystem = CoordinateComboBoxStart.Text;

            double.TryParse(XCoordTextBoxEnd.Text, out xresult);
            double.TryParse(YCoordTextBoxEnd.Text, out yresult);
            double.TryParse(ZCoordTextBoxEnd.Text, out zresult);
            double.TryParse(RxCoordTextBoxEnd.Text, out rxresult);
            double.TryParse(RyCoordTextBoxEnd.Text, out ryresult);
            double.TryParse(RzCoordTextBoxEnd.Text, out rzresult);
            area.Frontier.End.Coordinates = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            double.TryParse(FxTextBoxEnd.Text, out xresult);
            double.TryParse(FyTextBoxEnd.Text, out yresult);
            double.TryParse(FzTextBoxEnd.Text, out zresult);
            double.TryParse(TxTextBoxEnd.Text, out rxresult);
            double.TryParse(TyTextBoxEnd.Text, out ryresult);
            double.TryParse(TzTextBoxEnd.Text, out rzresult);
            area.Frontier.End.Loads = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            fxstatus = FxComboBoxEnd.Text;
            fystatus = FyComboBoxEnd.Text;
            fzstatus = FzComboBoxEnd.Text;
            txstatus = TxComboBoxEnd.Text;
            tystatus = TyComboBoxEnd.Text;
            tzstatus = TzComboBoxEnd.Text;
            area.Frontier.End.Status = new string[] { fxstatus, fystatus, fzstatus, txstatus, tystatus, tzstatus };

            double.TryParse(FxDispTextBoxEnd.Text, out xresult);
            double.TryParse(FyDispTextBoxEnd.Text, out yresult);
            double.TryParse(FzDispTextBoxEnd.Text, out zresult);
            double.TryParse(TxDispTextBoxEnd.Text, out rxresult);
            double.TryParse(TyDispTextBoxEnd.Text, out ryresult);
            double.TryParse(TzDispTextBoxEnd.Text, out rzresult);
            area.Frontier.End.ImposedDisplacements = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            SubmitButtonClick?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitNewArea(sender, e);
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
