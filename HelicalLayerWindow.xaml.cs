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
using TCC.Classes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for HelicalLayerWindow.xaml
    /// </summary>
    public partial class HelicalLayerWindow : Window
    {
        private HelixLayer helixLayer;
        private Dictionary<int, Section> sections;
        private OpenGL gl;

        public event EventHandler SubmitButtonClick;

        public HelixLayer HelixLayer { get { return helixLayer; } }

        public HelicalLayerWindow(Dictionary<int, Section> sections, Dictionary<int, LayerMaterial> materials, OpenGL gl)
        {
            InitializeComponent();
            this.sections = sections;
            this.gl = gl;

            //  Section comboBox
            if (sections.Count == 0)
            {
                List<Section> sectionList = new List<Section>
                {
                    new Section { ID = 0, Name = "No Section Created" },
                };
                SectionComboBox.ItemsSource = sectionList;
                SectionComboBox.SelectedIndex = 0;
            }
            else
            {
                SectionComboBox.ItemsSource = sections.Values;
                SectionComboBox.SelectedIndex = 0;
            }

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
            double.TryParse(RadiusTextBox.Text, out result);
            helixLayer.Radius = result;
            double.TryParse(LayAngleTextBox.Text, out result);
            helixLayer.LayAngle = result;
            double.TryParse(InitialAngleTextBox.Text, out result);
            helixLayer.InitialAngle = result;
            int.TryParse(DivisionsTextBox.Text, out intresult);
            helixLayer.Divisions = intresult;
            helixLayer.Name = NameTextBox.Text;
            helixLayer.Type = "helix";

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

            Draw(gl);
            SubmitButtonClick?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void Draw(OpenGL gl)
        {
            Section s = new RectangularSection
            {
                Name = "Default Section",
                ID = 0,
                Type = "Rectangular",
                Width = 10.0,
                Height = 5.0
            };

            if (s.Type == "Rectangular")
            {
                RectangularSection rs = helixLayer.Section as RectangularSection;
                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Hint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);

                gl.LineWidth(2);
                gl.Begin(BeginMode.Lines);
                vec3 rgb = new vec3(80, 80, 80) / 255;
                gl.Color(rgb.x, rgb.y, rgb.z, 1);

                double r1 = helixLayer.Radius + rs.Height / 2;
                double r2 = helixLayer.Radius - rs.Height / 2;
                double theta;

                for (int i = 0; i < helixLayer.Wires; i++)
                {
                    // Left line
                    theta = i * 2 * Math.PI / helixLayer.Wires;
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));

                    // Top line
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    theta = (i + 1) * 2 * Math.PI / helixLayer.Wires - (2 * Math.PI / helixLayer.Wires - rs.Width / r1);
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));

                    // Right line
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));

                    // Bottom line
                    theta = i * 2 * Math.PI / helixLayer.Wires;
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
                    theta = (i + 1) * 2 * Math.PI / helixLayer.Wires - (2 * Math.PI / helixLayer.Wires - rs.Width / r2);
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
                }

                gl.End();
                gl.Flush();
            }

            else if (s.Type == "Cylindrical")
            {
                CylindricalSection cs = s as CylindricalSection;
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
