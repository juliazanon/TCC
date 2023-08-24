using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.Shaders;
using GlmNet;
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
        public MainWindow()
        {
            InitializeComponent();

            // Sample array of strings
            string[] dataArray = { "Element 1", "Element 2", "Element 3", "Element 4", "Element 5" };
            itemsControl.ItemsSource = dataArray;

            cable = new Cable
            {
                Name = "New Cable",
                Sections = new Dictionary<int, Section>(),
                Layers = new Layer[0],
                LayerConnections = new LayerConnections[0],
                LayerMaterials = new Dictionary<int, LayerMaterial>()
            };
        }
        //  Menu

        //  Layers
        private void ButtonNewCylinder(object sender, RoutedEventArgs e)
        {
            CylindricalLayerWindow windowCylinder = new CylindricalLayerWindow(cable.LayerMaterials);
            windowCylinder.Show();

            CylinderLayer layer = new CylinderLayer
            {
                Length = windowCylinder.Length,
                Radius = windowCylinder.Radius,
                Thickness = windowCylinder.Thickness,
                FourierOrder = windowCylinder.FourierOrder,
                RadialDivisions = windowCylinder.RadialDivisions,
                AxialDivisions = windowCylinder.AxialDivisions,
                Areas = windowCylinder.Areas,
                Name = windowCylinder.Label,
                Type = windowCylinder.Type,
                MaterialID = windowCylinder.MaterialID,
                BodyLoad = windowCylinder.BodyLoad
            };

            cable.Layers.Append(layer).ToArray();
        }
        private void ButtonNewHelix(object sender, RoutedEventArgs e)
        {
            HelicalLayerWindow windowHelix = new HelicalLayerWindow(cable.Sections, cable.LayerMaterials);
            windowHelix.Show();

            HelixLayer layer = new HelixLayer
            {
                Line = windowHelix.Line,
                Length = windowHelix.Length,
                SectionID = windowHelix.SectionID,
                LayAngle = windowHelix.LayAngle,
                InitialAngle = windowHelix.InitialAngle,
                Divisions = windowHelix.Divisions,
                Name = windowHelix.Label,
                Type = windowHelix.Type,
                MaterialID = windowHelix.MaterialID,
                BodyLoad = windowHelix.BodyLoad
            };

            cable.Layers.Append(layer).ToArray();
        }

        //  Materials
        private void ButtonNewMaterial(object sender, RoutedEventArgs e)
        {
            MaterialsWindow windowMaterial = new MaterialsWindow(cable.LayerMaterials);

            windowMaterial.SubmitButtonClick += SubmitMaterialButtonClick;
            windowMaterial.Show();
        }

        private void SubmitMaterialButtonClick(object sender, EventArgs e)
        {
            MaterialsWindow windowMaterial = sender as MaterialsWindow;
            if (windowMaterial.LayerIsotropic != null)
            {
                Isotropic materialIsotropic = new Isotropic
                {
                    ID = windowMaterial.LayerIsotropic.ID,
                    Name = windowMaterial.LayerIsotropic.Name,
                    Density = windowMaterial.LayerIsotropic.Density,
                    Poisson = windowMaterial.LayerIsotropic.Poisson,
                    Young = windowMaterial.LayerIsotropic.Young
                };

                cable.LayerMaterials.Add(materialIsotropic.ID, materialIsotropic);
            }

            if (windowMaterial.LayerOrthotropic != null)
            {
                Orthotropic materialOrthotropic = new Orthotropic
                {
                    ID = windowMaterial.LayerOrthotropic.ID,
                    Name = windowMaterial.LayerOrthotropic.Name,
                    Density = windowMaterial.LayerOrthotropic.Density,
                    Ex = windowMaterial.LayerOrthotropic.Ex,
                    Ey = windowMaterial.LayerOrthotropic.Ey,
                    Ez = windowMaterial.LayerOrthotropic.Ez,
                    Nxy = windowMaterial.LayerOrthotropic.Nxy,
                    Nxz = windowMaterial.LayerOrthotropic.Nxz,
                    Nyz = windowMaterial.LayerOrthotropic.Nyz,
                    Gxy = windowMaterial.LayerOrthotropic.Gxy,
                    Gxz = windowMaterial.LayerOrthotropic.Gxz,
                    Gyz = windowMaterial.LayerOrthotropic.Gyz,
                };

                cable.LayerMaterials.Add(materialOrthotropic.ID, materialOrthotropic);
                teste.Text = materialOrthotropic.Name.ToString();
            }
        }

        //  Sections
        private void ButtonNewSection(object sender, RoutedEventArgs e)
        {
            SectionWindow windowSection = new SectionWindow();
            windowSection.Show();
        }

        //  Camera parameters
        float[] _viewPoint = new float[] { 0.0f, 0.0f, 0.0f };
        float[] _position = new float[] { 0.0f, 0.0f, 10.0f };
        float[] _upVector = new float[] { 0.0f, 1.0f, 0.0f };
        float scale = 0.1f;

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
            //gl.Ortho(0, w, 0, h, -1, 1);
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

            float prop = w * h / 1000000;
            vec3 rgb = new vec3(111, 112, 112) / 255;
            CircleDrawing c1 = new CircleDrawing(gl, 10000, 40, 40f * prop, rgb, false);

            rgb = new vec3(150, 150, 150) / 255;
            CircleDrawing c2 = new CircleDrawing(gl, 5000, 30, 30f * prop, rgb, false);

            rgb = new vec3(80, 80, 80) / 255;
            HelicoidalDrawing c3 = new HelicoidalDrawing(gl, 33, 27, 25, rgb);
            HelicoidalDrawing c4 = new HelicoidalDrawing(gl, 30, 24.5f, 22.5f, rgb);

            Orthotropic o = new Orthotropic
            {
                Density = 1.0,
                ID = 1
            };

            // circles with triangles
            //rgb = new vec3(80, 80, 80) / 255;
            //Circle c3 = new Circle(gl, 1000, 20, 17, rgb, true);

            //rgb = new vec3(120, 120, 120) / 255;
            //Circle c4 = new Circle(gl, 1000, 10, 7, rgb, true);
        }
    }
}
