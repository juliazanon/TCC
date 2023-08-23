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
using System.Windows.Documents;
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

namespace TCC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Sample array of strings
            string[] dataArray = { "Element 1", "Element 2", "Element 3", "Element 4", "Element 5" };
            itemsControl.ItemsSource = dataArray;
        }
        //  Menu

        //  Layers
        private void ButtonNewCylinder(object sender, RoutedEventArgs e)
        {
            CylindricalLayer windowCylinder = new CylindricalLayer();
            windowCylinder.Show();
        }
        private void ButtonNewHelix(object sender, RoutedEventArgs e)
        {
            HelicalLayer windowHelix = new HelicalLayer();
            windowHelix.Show();
        }

        //  Materials
        private void ButtonNewMaterial(object sender, RoutedEventArgs e)
        {
            Materials windowMaterial = new Materials();
            windowMaterial.Show();
        }

        //  Camera parameters

        float[] _viewPoint = new float[] { 0.0f, 0.0f, 0.0f };
        float[] _position = new float[] { 0.0f, 0.0f, 10.0f };
        float[] _upVector = new float[] { 0.0f, 1.0f, 0.0f };
        float _moveDistance = 1.0f;
        float scale = 0.1f;

        int keyCode = 0;

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
            Circle c1 = new Circle(gl, 10000, 40, 40f * prop, rgb, false);

            rgb = new vec3(150, 150, 150) / 255;
            Circle c2 = new Circle(gl, 5000, 30, 30f * prop, rgb, false);

            rgb = new vec3(80, 80, 80) / 255;
            Helicoidal c3 = new Helicoidal(gl, 33, 27, 25, rgb);
            Helicoidal c4 = new Helicoidal(gl, 30, 24.5f, 22.5f, rgb);

            //teste.Text = "alo";

            // circles with triangles
            //rgb = new vec3(80, 80, 80) / 255;
            //Circle c3 = new Circle(gl, 1000, 20, 17, rgb, true);

            //rgb = new vec3(120, 120, 120) / 255;
            //Circle c4 = new Circle(gl, 1000, 10, 7, rgb, true);
        }

        private new void KeyDown(object sender, KeyEventArgs e)
        {
            // Key definitions
            if (e.Key == Key.W || e.Key == Key.Up) { keyCode = 1; }
            else if (e.Key == Key.S || e.Key == Key.Down) { keyCode = 2; }
            else if (e.Key == Key.A || e.Key == Key.Left) { keyCode = 3; }
            else if (e.Key == Key.D || e.Key == Key.Right) { keyCode = 4; }
            else if (e.Key == Key.Add || e.Key == Key.OemPlus) { keyCode = 5; }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus) { keyCode = 6; }
            else { keyCode = 0; }

            //  pan
            //  y axis
            //  Up
            if (keyCode == 1)
            {
                _viewPoint[1] += _moveDistance;
                _position[1] += _moveDistance;
            }
            //  Down
            else if (keyCode == 2)
            {
                _viewPoint[1] += -_moveDistance;
                _position[1] += -_moveDistance;
            }

            //  x axis
            //  Left
            else if (keyCode == 3)
            {
                _viewPoint[2] += _moveDistance;
                _position[2] += _moveDistance;
            }
            //  Right
            else if (keyCode == 4)
            {
                _viewPoint[2] += -_moveDistance;
                _position[2] += -_moveDistance;
            }
            //  zoom
            else if (keyCode == 5)
            {
                scale += 0.01f;
            }
            else if (keyCode == 6)
            {
                if (scale > 0.04) { scale -= 0.03f; }
            }
        }
    }
}
