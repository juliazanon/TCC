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
using TCC.MainClasses;

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

        // New Area constructor
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
        // Edit Area constructor
        public CylindricalAreasWindow(string areaType, Area area)
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

            XPressureTextBox.Text = area.Pressure[0].ToString();
            YPressureTextBox.Text = area.Pressure[1].ToString();
            ZPressureTextBox.Text = area.Pressure[2].ToString();
            RxPressureTextBox.Text = area.Pressure[3].ToString();
            RyPressureTextBox.Text = area.Pressure[4].ToString();
            RzPressureTextBox.Text = area.Pressure[5].ToString();
            FxStatusComboBox.Text = area.Status[0];
            FyStatusComboBox.Text = area.Status[1];
            FzStatusComboBox.Text = area.Status[2];
            TxStatusComboBox.Text = area.Status[3];
            TyStatusComboBox.Text = area.Status[4];
            TzStatusComboBox.Text = area.Status[5];
            FxDispTextBox.Text = area.ImposedDisplacements[0].ToString();
            FyDispTextBox.Text = area.ImposedDisplacements[1].ToString();
            FzDispTextBox.Text = area.ImposedDisplacements[2].ToString();
            TxDispTextBox.Text = area.ImposedDisplacements[3].ToString();
            TyDispTextBox.Text = area.ImposedDisplacements[4].ToString();
            TzDispTextBox.Text = area.ImposedDisplacements[5].ToString();
            // Frontier
            DesignCheckBoxFrontier.IsChecked = area.Frontier.DesignOnly;
            FxTextBoxFrontier.Text = area.Frontier.DistributedLoads[0].ToString();
            FyTextBoxFrontier.Text = area.Frontier.DistributedLoads[1].ToString();
            FzTextBoxFrontier.Text = area.Frontier.DistributedLoads[2].ToString();
            TxTextBoxFrontier.Text = area.Frontier.DistributedLoads[3].ToString();
            TyTextBoxFrontier.Text = area.Frontier.DistributedLoads[4].ToString();
            TzTextBoxFrontier.Text = area.Frontier.DistributedLoads[5].ToString();
            FxStatusComboBoxFrontier.Text = area.Frontier.Status[0];
            FyStatusComboBoxFrontier.Text = area.Frontier.Status[1];
            FzStatusComboBoxFrontier.Text = area.Frontier.Status[2];
            TxStatusComboBoxFrontier.Text = area.Frontier.Status[3];
            TyStatusComboBoxFrontier.Text = area.Frontier.Status[4];
            TzStatusComboBoxFrontier.Text = area.Frontier.Status[5];
            FxDispTextBoxFrontier.Text = area.Frontier.ImposedDisplacements[0].ToString();
            FyDispTextBoxFrontier.Text = area.Frontier.ImposedDisplacements[1].ToString();
            FzDispTextBoxFrontier.Text = area.Frontier.ImposedDisplacements[2].ToString();
            TxDispTextBoxFrontier.Text = area.Frontier.ImposedDisplacements[3].ToString();
            TyDispTextBoxFrontier.Text = area.Frontier.ImposedDisplacements[4].ToString();
            TzDispTextBoxFrontier.Text = area.Frontier.ImposedDisplacements[5].ToString();
            // Start
            DesignCheckBoxStart.IsChecked = area.Frontier.Start.DesignOnly;
            CoordinateComboBoxStart.SelectedItem = area.Frontier.Start.CoordinateSystem;
            XCoordTextBoxStart.Text = area.Frontier.Start.Coordinates[0].ToString();
            YCoordTextBoxStart.Text = area.Frontier.Start.Coordinates[1].ToString();
            ZCoordTextBoxStart.Text = area.Frontier.Start.Coordinates[2].ToString();
            RxCoordTextBoxStart.Text = area.Frontier.Start.Coordinates[3].ToString();
            RyCoordTextBoxStart.Text = area.Frontier.Start.Coordinates[4].ToString();
            RzCoordTextBoxStart.Text = area.Frontier.Start.Coordinates[5].ToString();
            FxTextBoxStart.Text = area.Frontier.Start.Loads[0].ToString();
            FyTextBoxStart.Text = area.Frontier.Start.Loads[1].ToString();
            FzTextBoxStart.Text = area.Frontier.Start.Loads[2].ToString();
            TxTextBoxStart.Text = area.Frontier.Start.Loads[3].ToString();
            TyTextBoxStart.Text = area.Frontier.Start.Loads[4].ToString();
            TzTextBoxStart.Text = area.Frontier.Start.Loads[5].ToString();
            FxComboBoxStart.Text = area.Frontier.Start.Status[0];
            FyComboBoxStart.Text = area.Frontier.Start.Status[1];
            FzComboBoxStart.Text = area.Frontier.Start.Status[2];
            TxComboBoxStart.Text = area.Frontier.Start.Status[3];
            TyComboBoxStart.Text = area.Frontier.Start.Status[4];
            TzComboBoxStart.Text = area.Frontier.Start.Status[5];
            FxDispTextBoxStart.Text = area.Frontier.Start.ImposedDisplacements[0].ToString();
            FyDispTextBoxStart.Text = area.Frontier.Start.ImposedDisplacements[1].ToString();
            FzDispTextBoxStart.Text = area.Frontier.Start.ImposedDisplacements[2].ToString();
            TxDispTextBoxStart.Text = area.Frontier.Start.ImposedDisplacements[3].ToString();
            TyDispTextBoxStart.Text = area.Frontier.Start.ImposedDisplacements[4].ToString();
            TzDispTextBoxStart.Text = area.Frontier.Start.ImposedDisplacements[5].ToString();
            // End
            DesignCheckBoxEnd.IsChecked = area.Frontier.End.DesignOnly;
            CoordinateComboBoxEnd.SelectedItem = area.Frontier.End.CoordinateSystem;
            XCoordTextBoxEnd.Text = area.Frontier.End.Coordinates[0].ToString();
            YCoordTextBoxEnd.Text = area.Frontier.End.Coordinates[1].ToString();
            ZCoordTextBoxEnd.Text = area.Frontier.End.Coordinates[2].ToString();
            RxCoordTextBoxEnd.Text = area.Frontier.End.Coordinates[3].ToString();
            RyCoordTextBoxEnd.Text = area.Frontier.End.Coordinates[4].ToString();
            RzCoordTextBoxEnd.Text = area.Frontier.End.Coordinates[5].ToString();
            FxTextBoxEnd.Text = area.Frontier.End.Loads[0].ToString();
            FyTextBoxEnd.Text = area.Frontier.End.Loads[1].ToString();
            FzTextBoxEnd.Text = area.Frontier.End.Loads[2].ToString();
            TxTextBoxEnd.Text = area.Frontier.End.Loads[3].ToString();
            TyTextBoxEnd.Text = area.Frontier.End.Loads[4].ToString();
            TzTextBoxEnd.Text = area.Frontier.End.Loads[5].ToString();
            FxComboBoxEnd.Text = area.Frontier.End.Status[0];
            FyComboBoxEnd.Text = area.Frontier.End.Status[1];
            FzComboBoxEnd.Text = area.Frontier.End.Status[2];
            TxComboBoxEnd.Text = area.Frontier.End.Status[3];
            TyComboBoxEnd.Text = area.Frontier.End.Status[4];
            TzComboBoxEnd.Text = area.Frontier.End.Status[5];
            FxDispTextBoxEnd.Text = area.Frontier.End.ImposedDisplacements[0].ToString();
            FyDispTextBoxEnd.Text = area.Frontier.End.ImposedDisplacements[1].ToString();
            FzDispTextBoxEnd.Text = area.Frontier.End.ImposedDisplacements[2].ToString();
            TxDispTextBoxEnd.Text = area.Frontier.End.ImposedDisplacements[3].ToString();
            TyDispTextBoxEnd.Text = area.Frontier.End.ImposedDisplacements[4].ToString();
            TzDispTextBoxEnd.Text = area.Frontier.End.ImposedDisplacements[5].ToString();
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

            // Frontier
            area.Frontier.DesignOnly = (bool)DesignCheckBoxFrontier.IsChecked;

            double.TryParse(FxTextBoxFrontier.Text, out xresult);
            double.TryParse(FyTextBoxFrontier.Text, out yresult);
            double.TryParse(FzTextBoxFrontier.Text, out zresult);
            double.TryParse(TxTextBoxFrontier.Text, out rxresult);
            double.TryParse(TyTextBoxFrontier.Text, out ryresult);
            double.TryParse(TzTextBoxFrontier.Text, out rzresult);
            area.Frontier.DistributedLoads = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            fxstatus = FxStatusComboBoxFrontier.Text;
            fystatus = FyStatusComboBoxFrontier.Text;
            fzstatus = FzStatusComboBoxFrontier.Text;
            txstatus = TxStatusComboBoxFrontier.Text;
            tystatus = TyStatusComboBoxFrontier.Text;
            tzstatus = TzStatusComboBoxFrontier.Text;
            area.Frontier.Status = new string[] { fxstatus, fystatus, fzstatus, txstatus, tystatus, tzstatus };

            double.TryParse(FxDispTextBoxFrontier.Text, out xresult);
            double.TryParse(FyDispTextBoxFrontier.Text, out yresult);
            double.TryParse(FzDispTextBoxFrontier.Text, out zresult);
            double.TryParse(TxDispTextBoxFrontier.Text, out rxresult);
            double.TryParse(TyDispTextBoxFrontier.Text, out ryresult);
            double.TryParse(TzDispTextBoxFrontier.Text, out rzresult);
            area.Frontier.ImposedDisplacements = new double[] { xresult, yresult, zresult, rxresult, ryresult, rzresult };

            // Start
            area.Frontier.Start.ID = 1;
            area.Frontier.Start.DesignOnly = (bool)DesignCheckBoxStart.IsChecked;
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
            area.Frontier.End.DesignOnly = (bool)DesignCheckBoxEnd.IsChecked;
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
            if (!char.IsDigit(e.Text, 0) && e.Text != "." && e.Text != "-")
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
