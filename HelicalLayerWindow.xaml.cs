using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
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
using TCC.MainClasses;

namespace TCC
{
    /// <summary>
    /// Interaction logic for HelicalLayerWindow.xaml
    /// </summary>
    public partial class HelicalLayerWindow : Window
    {
        private HelixLayer helixLayer;
        private List<Layer> layers;
        private string editName;
        
        public event EventHandler SubmitButtonClick;

        public HelixLayer HelixLayer { get { return helixLayer; } }

        // New Helix constructor
        public HelicalLayerWindow(List<Layer> layers,List<Section> sections, List<LayerMaterial> materials)
        {
            InitializeComponent();
            TitleTextBlock.Text = "Create New Helical Layer";
            NameTextBox.Focus();
            NameTextBox.CaretIndex = NameTextBox.Text.Length;
            ButtonToggleStart.Content = "Hide";
            ButtonToggleEnd.Content = "Hide";
            this.layers = layers;

            //  Section comboBox
            if (sections.Count == 0)
            {
                List<Section> sectionList = new List<Section>
                {
                    new RectangularSection { ID = 0, Name = "No Section Created" },
                };
                SectionComboBox.ItemsSource = sectionList;
                SectionComboBox.SelectedIndex = 0;
                SubmitButton.IsEnabled = false;
                SectionWarning.Visibility = Visibility.Visible;
            }
            else
            {
                SectionComboBox.ItemsSource = sections;
                SectionComboBox.SelectedIndex = 0;
                SubmitButton.IsEnabled = true;
                SectionWarning.Visibility = Visibility.Collapsed;
            }

            //  Materials comboBox
            if (materials.Count == 0)
            {
                List<LayerMaterial> materialList = new List<LayerMaterial>
                {
                    new Isotropic { ID = 0, Name = "No Material Created" },
                };
                MaterialComboBox.ItemsSource = materialList;
                MaterialComboBox.SelectedIndex = 0;
            }
            else
            {
                MaterialComboBox.ItemsSource = materials;
                MaterialComboBox.SelectedIndex = 0;
            }
            
            // Hide Cylindrical Coordinates elements
            TextBlockCilyndricalCoord1Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord1End.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2End.Visibility = Visibility.Collapsed;
            
            //  Handle the ComboBox SelectionChange
            CoordinateComboBoxStart.SelectionChanged += ComboBox_SelectionChanged_CoordinateStart;
            CoordinateComboBoxEnd.SelectionChanged += ComboBox_SelectionChanged_CoordinateEnd;
        }

        // Edit helix constructor
        public HelicalLayerWindow(List<Layer> layers, List<Section> sections, List<LayerMaterial> materials, HelixLayer layer)
        {
            InitializeComponent();
            TitleTextBlock.Text = "Edit Helical Layer";
            NameTextBox.Focus();
            NameTextBox.CaretIndex = NameTextBox.Text.Length;
            ButtonToggleStart.Content = "Hide";
            ButtonToggleEnd.Content = "Hide";
            this.layers = layers;
            editName = layer.Name;

            //  Section comboBox
            if (sections.Count == 0)
            {
                List<Section> sectionList = new List<Section>
                {
                    new RectangularSection { ID = 0, Name = "No Section Created" },
                };
                SectionComboBox.ItemsSource = sectionList;
                SectionComboBox.SelectedIndex = 0;
                SubmitButton.IsEnabled = false;
                SectionWarning.Visibility = Visibility.Visible;
            }
            else
            {
                SectionComboBox.ItemsSource = sections;
                SectionComboBox.SelectedIndex = 0;
                SubmitButton.IsEnabled = true;
                SectionWarning.Visibility = Visibility.Collapsed;
            }

            //  Materials comboBox
            if (materials.Count == 0)
            {
                List<LayerMaterial> materialList = new List<LayerMaterial>
                {
                    new Isotropic { ID = 0, Name = "No Material Created" },
                };
                MaterialComboBox.ItemsSource = materialList;
                MaterialComboBox.SelectedIndex = 0;
            }
            else
            {
                MaterialComboBox.ItemsSource = materials;
                MaterialComboBox.SelectedIndex = 0;
            }

            // Hide Cylindrical Coordinates elements
            TextBlockCilyndricalCoord1Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2Start.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord1End.Visibility = Visibility.Collapsed;
            TextBlockCilyndricalCoord2End.Visibility = Visibility.Collapsed;

            //  Handle the ComboBox SelectionChange
            CoordinateComboBoxStart.SelectionChanged += ComboBox_SelectionChanged_CoordinateStart;
            CoordinateComboBoxEnd.SelectionChanged += ComboBox_SelectionChanged_CoordinateEnd;

            // Include original parameters
            NameTextBox.Text = layer.Name;
            WiresTextBox.Text = layer.Wires.ToString();
            RadiusTextBox.Text = layer.Radius.ToString();
            LengthTextBox.Text = layer.Length.ToString();
            LayAngleTextBox.Text = layer.LayAngle.ToString();
            SectionComboBox.SelectedItem = layer.Section;
            MaterialComboBox.SelectedItem = layer.Material;
            DivisionsTextBox.Text = layer.Divisions.ToString();
            BodyLoadXTextBox.Text = layer.BodyLoad[0].ToString();
            BodyLoadYTextBox.Text = layer.BodyLoad[1].ToString();
            BodyLoadYTextBox.Text = layer.BodyLoad[2].ToString();
            // Line
            DesignCheckBoxLine.IsChecked = layer.Line.DesignOnly;
            FxTextBox.Text = layer.Line.DistributedLoads[0].ToString();
            FyTextBox.Text = layer.Line.DistributedLoads[1].ToString();
            FzTextBox.Text = layer.Line.DistributedLoads[2].ToString();
            TxTextBox.Text = layer.Line.DistributedLoads[3].ToString();
            TyTextBox.Text = layer.Line.DistributedLoads[4].ToString();
            TzTextBox.Text = layer.Line.DistributedLoads[5].ToString();
            FxComboBox.SelectedItem = layer.Line.Status[0];
            FyComboBox.SelectedItem = layer.Line.Status[1];
            FzComboBox.SelectedItem = layer.Line.Status[2];
            TxComboBox.SelectedItem = layer.Line.Status[3];
            TyComboBox.SelectedItem = layer.Line.Status[4];
            TzComboBox.SelectedItem = layer.Line.Status[5];
            FxDispTextBox.Text = layer.Line.ImposedDisplacements[0].ToString();
            FyDispTextBox.Text = layer.Line.ImposedDisplacements[1].ToString();
            FzDispTextBox.Text = layer.Line.ImposedDisplacements[2].ToString();
            TxDispTextBox.Text = layer.Line.ImposedDisplacements[3].ToString();
            TyDispTextBox.Text = layer.Line.ImposedDisplacements[4].ToString();
            TzDispTextBox.Text = layer.Line.ImposedDisplacements[5].ToString();
            // Start
            DesignCheckBoxStart.IsChecked = layer.Line.Start.DesignOnly;
            CoordinateComboBoxStart.SelectedItem = layer.Line.Start.CoordinateSystem;
            XCoordTextBoxStart.Text = layer.Line.Start.Coordinates[0].ToString();
            YCoordTextBoxStart.Text = layer.Line.Start.Coordinates[1].ToString();
            ZCoordTextBoxStart.Text = layer.Line.Start.Coordinates[2].ToString();
            RXCoordTextBoxStart.Text = layer.Line.Start.Coordinates[3].ToString();
            RYCoordTextBoxStart.Text = layer.Line.Start.Coordinates[4].ToString();
            RZCoordTextBoxStart.Text = layer.Line.Start.Coordinates[5].ToString();
            FxTextBoxStart.Text = layer.Line.Start.Loads[0].ToString();
            FyTextBoxStart.Text = layer.Line.Start.Loads[1].ToString();
            FzTextBoxStart.Text = layer.Line.Start.Loads[2].ToString();
            TxTextBoxStart.Text = layer.Line.Start.Loads[3].ToString();
            TyTextBoxStart.Text = layer.Line.Start.Loads[4].ToString();
            TzTextBoxStart.Text = layer.Line.Start.Loads[5].ToString();
            FxComboBoxStart.SelectedItem = layer.Line.Start.Status[0];
            FyComboBoxStart.SelectedItem = layer.Line.Start.Status[1];
            FzComboBoxStart.SelectedItem = layer.Line.Start.Status[2];
            TxComboBoxStart.SelectedItem = layer.Line.Start.Status[3];
            TyComboBoxStart.SelectedItem = layer.Line.Start.Status[4];
            TzComboBoxStart.SelectedItem = layer.Line.Start.Status[5];
            FxDispTextBoxStart.Text = layer.Line.Start.ImposedDisplacements[0].ToString();
            FyDispTextBoxStart.Text = layer.Line.Start.ImposedDisplacements[1].ToString();
            FzDispTextBoxStart.Text = layer.Line.Start.ImposedDisplacements[2].ToString();
            TxDispTextBoxStart.Text = layer.Line.Start.ImposedDisplacements[3].ToString();
            TyDispTextBoxStart.Text = layer.Line.Start.ImposedDisplacements[4].ToString();
            TzDispTextBoxStart.Text = layer.Line.Start.ImposedDisplacements[5].ToString();
            // End
            DesignCheckBoxEnd.IsChecked = layer.Line.End.DesignOnly;
            CoordinateComboBoxEnd.SelectedItem = layer.Line.Start.CoordinateSystem;
            XCoordTextBoxEnd.Text = layer.Line.End.Coordinates[0].ToString();
            YCoordTextBoxEnd.Text = layer.Line.End.Coordinates[1].ToString();
            ZCoordTextBoxEnd.Text = layer.Line.End.Coordinates[2].ToString();
            RXCoordTextBoxEnd.Text = layer.Line.End.Coordinates[3].ToString();
            RYCoordTextBoxEnd.Text = layer.Line.End.Coordinates[4].ToString();
            RZCoordTextBoxEnd.Text = layer.Line.End.Coordinates[5].ToString();
            FxTextBoxEnd.Text = layer.Line.End.Loads[0].ToString();
            FyTextBoxEnd.Text = layer.Line.End.Loads[1].ToString();
            FzTextBoxEnd.Text = layer.Line.End.Loads[2].ToString();
            TxTextBoxEnd.Text = layer.Line.End.Loads[3].ToString();
            TyTextBoxEnd.Text = layer.Line.End.Loads[4].ToString();
            TzTextBoxEnd.Text = layer.Line.End.Loads[5].ToString();
            FxComboBoxEnd.SelectedItem = layer.Line.End.Status[0];
            FyComboBoxEnd.SelectedItem = layer.Line.End.Status[1];
            FzComboBoxEnd.SelectedItem = layer.Line.End.Status[2];
            TxComboBoxEnd.SelectedItem = layer.Line.End.Status[3];
            TyComboBoxEnd.SelectedItem = layer.Line.End.Status[4];
            TzComboBoxEnd.SelectedItem = layer.Line.End.Status[5];
            FxDispTextBoxEnd.Text = layer.Line.End.ImposedDisplacements[0].ToString();
            FyDispTextBoxEnd.Text = layer.Line.End.ImposedDisplacements[1].ToString();
            FzDispTextBoxEnd.Text = layer.Line.End.ImposedDisplacements[2].ToString();
            TxDispTextBoxEnd.Text = layer.Line.End.ImposedDisplacements[3].ToString();
            TyDispTextBoxEnd.Text = layer.Line.End.ImposedDisplacements[4].ToString();
            TzDispTextBoxEnd.Text = layer.Line.End.ImposedDisplacements[5].ToString();
        }

        //  Show and Hide Start and End of line
        private void StackPanel_ShowHide_OptionStart(object sender, EventArgs e)
        {   
            if ((sender as Button).Content == "Show")
            {
                (sender as Button).Content = "Hide";
                StackPanelStart.Visibility = Visibility.Visible;
            } 
            else if ((sender as Button).Content == "Hide")
            {
                (sender as Button).Content = "Show";
                StackPanelStart.Visibility = Visibility.Collapsed;
            }
        }
        private void StackPanel_ShowHide_OptionEnd(object sender, EventArgs e)
        {
            if ((sender as Button).Content == "Show")
            {
                (sender as Button).Content = "Hide";
                StackPanelEnd.Visibility = Visibility.Visible;
            }
            else if ((sender as Button).Content == "Hide")
            {
                (sender as Button).Content = "Show";
                StackPanelEnd.Visibility = Visibility.Collapsed;
            }
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

        private void SubmitNewLayer(object sender, RoutedEventArgs e)
        {
            if (layers.Count != 0)  // Conditions
            {
                if (layers.Any(obj => obj.Name == NameTextBox.Text) && NameTextBox.Text != editName)  // Name already used
                {
                    InputWarning("Name");
                    return;
                }
            }

            helixLayer = new HelixLayer
            {
                Name = NameTextBox.Text,
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
            double.TryParse(RadiusTextBox.Text, out result);
            helixLayer.Radius = result;
            double.TryParse(LayAngleTextBox.Text, out result);
            helixLayer.LayAngle = result;
            double.TryParse(InitialAngleTextBox.Text, out result);
            helixLayer.InitialAngle = result;
            int.TryParse(DivisionsTextBox.Text, out intresult);
            helixLayer.Divisions = intresult;

            if (helixLayer.Wires == 1) helixLayer.Type = "helix";
            else if (helixLayer.Wires > 1) helixLayer.Type = "armor";

            LayerMaterial selectedMaterial = (LayerMaterial)MaterialComboBox.SelectedItem;
            helixLayer.Material = selectedMaterial;

            Section selectedSection = (Section)SectionComboBox.SelectedItem;
            helixLayer.Section = selectedSection;

            double.TryParse(BodyLoadXTextBox.Text, out double xresult);
            double.TryParse(BodyLoadYTextBox.Text, out double yresult);
            double.TryParse(BodyLoadZTextBox.Text, out double zresult);
            helixLayer.BodyLoad = new double[] { xresult, yresult, zresult };

            // Start
            helixLayer.Line.Start.ID = 1;
            helixLayer.Line.Start.DesignOnly = (bool)DesignCheckBoxStart.IsChecked;
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
            helixLayer.Line.End.DesignOnly = (bool)DesignCheckBoxEnd.IsChecked;
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
            helixLayer.Line.DesignOnly = (bool)DesignCheckBoxLine.IsChecked;

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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (SectionWarning.Visibility == Visibility.Collapsed) SubmitNewLayer(sender, e);
            }
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

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Check if the entered character is a digit or a dot
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true; // Prevent the character from being entered
            }
        }
    }
}
