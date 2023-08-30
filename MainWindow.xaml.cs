using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.Shaders;
using GlmNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using SharpGL.WPF;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Drawing.Drawing2D;
using SharpGL.SceneGraph.Assets;
using TCC.Classes;
using System.Windows.Documents.DocumentStructures;

namespace TCC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cable cable;
        ObservableCollection<Layer> observableLayer = new ObservableCollection<Layer>();
        bool isChildWindowOpen = false;

        public MainWindow()
        {
            InitializeComponent();

            itemsControl.ItemsSource = observableLayer;

            cable = new Cable
            {
                Name = "New Cable",
                Sections = new List<Section>(),
                Layers = new List<Layer>(),
                LayerConnections = new List<LayerConnection>(),
                LayerMaterials = new List<LayerMaterial>()
            };
        }

        //  Menu
        //  Layers
        private void ButtonNewCylinder(object sender, RoutedEventArgs e)
        {
            CylindricalLayerWindow windowCylinder = new CylindricalLayerWindow(cable.LayerMaterials);
            windowCylinder.SubmitButtonClick += SubmitCylinderButtonClick;
            windowCylinder.Closed += CylinderWindow_Closed;
            windowCylinder.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SubmitCylinderButtonClick(object sender, EventArgs e)
        {
            CylindricalLayerWindow windowCylinder = sender as CylindricalLayerWindow;
            CylinderLayer layer = windowCylinder.CylinderLayer;

            cable.Layers.Add(layer);
            observableLayer.Add(layer);
            itemsControl.ItemsSource = observableLayer;

            //CylinderLayer aux = cable.Layers[0] as CylinderLayer;
            //teste.Text = aux.Areas[0].Frontier.DesignOnly.ToString();
        }
        private void CylinderWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }
        private void ButtonNewHelix(object sender, RoutedEventArgs e)
        {
            HelicalLayerWindow windowHelix = new HelicalLayerWindow(cable.Sections, cable.LayerMaterials);
            windowHelix.SubmitButtonClick += SubmitHelixButtonClick;
            windowHelix.Closed += HelixWindow_Closed;
            windowHelix.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SubmitHelixButtonClick(object sender, EventArgs e)
        {
            HelicalLayerWindow windowHelix = sender as HelicalLayerWindow;
            HelixLayer layer = windowHelix.HelixLayer;

            cable.Layers.Add(layer);
            observableLayer.Add(layer);
            itemsControl.ItemsSource = observableLayer;

            //HelixLayer aux = cable.Layers[0] as HelixLayer;
            //teste.Text = aux.Section.Type;
        }
        private void HelixWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }
        private void ButtonNewConnection(object sender, RoutedEventArgs e)
        {
            LayerConnectionsWindow windowConnection = new LayerConnectionsWindow(cable.Layers);
            windowConnection.SubmitButtonClick += SubmitConnectionButtonClick;
            windowConnection.Closed += ConnectionsWindow_Closed;
            windowConnection.Show();
        }
        private void SubmitConnectionButtonClick(object sender, EventArgs e)
        {
            LayerConnectionsWindow windowConnection = sender as LayerConnectionsWindow;
            LayerConnection layerConnection = windowConnection.LayerConnection;

            cable.LayerConnections.Add(layerConnection);
        }
        private void ConnectionsWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }

        //  Materials
        private void ButtonNewMaterial(object sender, RoutedEventArgs e)
        {
            MaterialsWindow windowMaterial = new MaterialsWindow(cable.LayerMaterials);
            windowMaterial.SubmitButtonClick += SubmitMaterialButtonClick;
            windowMaterial.Closed += MaterialWindow_Closed;
            windowMaterial.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SubmitMaterialButtonClick(object sender, EventArgs e)
        {
            MaterialsWindow windowMaterial = sender as MaterialsWindow;
            if (windowMaterial.LayerIsotropic != null)
            {
                Isotropic materialIsotropic = windowMaterial.LayerIsotropic;

                cable.LayerMaterials.Add(materialIsotropic);
            }

            else if (windowMaterial.LayerOrthotropic != null)
            {
                Orthotropic materialOrthotropic = windowMaterial.LayerOrthotropic;

                cable.LayerMaterials.Add(materialOrthotropic);
            }
        }
        private void MaterialWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }

        //  Sections
        private void ButtonNewSection(object sender, RoutedEventArgs e)
        {
            SectionWindow windowSection = new SectionWindow(cable.Sections);
            windowSection.SubmitButtonClick += SubmitSectionButtonClick;
            windowSection.Closed += SectionWindow_Closed;
            windowSection.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SubmitSectionButtonClick(object sender, EventArgs e)
        {
            SectionWindow windowSection = sender as SectionWindow;
            if (windowSection.RectangularSection != null)
            {
                RectangularSection rectangularSection = windowSection.RectangularSection;
                cable.Sections.Add(rectangularSection);
            }
            else if (windowSection.TubularSection != null)
            {
                TubularSection tubularSection = windowSection.TubularSection;
                cable.Sections.Add(tubularSection);
            }
        }
        private void SectionWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPlus || e.Key == Key.Add) { scale += 0.01f; }
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract) { scale -= 0.01f; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isChildWindowOpen) e.Cancel = true;
        }

        //  Camera parameters
        float[] _viewPoint = new float[] { 0.0f, 0.0f, 0.0f };
        float[] _position = new float[] { 0.0f, 0.0f, 10.0f };
        float[] _upVector = new float[] { 0.0f, 1.0f, 0.0f };
        float scale = 0.04f;

        private void ZoomButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (clickedButton.Name == "ButtonZoomIn") { scale += 0.01f; }
            else if (clickedButton.Name == "ButtonZoomOut") { scale -= 0.01f; }
        }

        // Graphics
        private void OpenGLDraw(object sender, SharpGL.WPF.OpenGLRoutedEventArgs args)
        {
            OpenGL gl = GLControl.OpenGL;
            float w = (float)GLControl.ActualWidth;
            float h = (float)GLControl.ActualHeight;

            //  Perspective projection
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Perspective(60, (int)w / h, 0.01, 1000);
            gl.Scale(scale, scale, 1);

            //  Camera Position
            gl.LookAt(_position[0], _position[1], _position[2],
                _viewPoint[0], _viewPoint[1], _viewPoint[2],
                _upVector[0], _upVector[1], _upVector[2]);

            gl.MatrixMode(MatrixMode.Modelview);
            gl.LoadIdentity();

            //  Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Background color
            vec3 backrgb = new vec3(225, 227, 226) / 255;
            gl.ClearColor(backrgb.x, backrgb.y, backrgb.z, 0);
            vec3 rgb;

            float prop = w * h / 1000000;

            foreach (Layer l in cable.Layers)
            {
                if (l is HelixLayer)
                {
                    HelixLayer layer = l as HelixLayer;
                    rgb = new vec3(80, 80, 80) / 255;
                    layer.Draw(gl, rgb);
                }
                else if (l is CylinderLayer)
                {
                    CylinderLayer layer = l as CylinderLayer;
                    rgb = new vec3(150, 150, 150) / 255;
                    layer.Draw(gl, rgb, 10000, prop, false);
                }
            }
        }
    }
}
