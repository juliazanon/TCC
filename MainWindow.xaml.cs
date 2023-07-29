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

            // Antialiasing
            //gl.Enable(OpenGL.GL_POLYGON_SMOOTH);

            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.ClearDepth(1.0f);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);

            //gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
            gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);

            gl.PixelStore(OpenGL.GL_UNPACK_ALIGNMENT, 4);

            gl.Disable(OpenGL.GL_CULL_FACE);
            gl.Enable(OpenGL.GL_LINE_SMOOTH);
            gl.Hint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);

            //  Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Background color
            vec3 backrgb = new vec3(225, 227, 226) / 255;
            gl.ClearColor(backrgb.x, backrgb.y, backrgb.z, 0);


            vec3 rgb = new vec3(111, 112, 112) / 255;
            CreateCircle(100, 50, 47, rgb);

            rgb = new vec3(150, 150, 150) / 255;
            CreateCircle(1000, 44, 41, rgb);

            rgb = new vec3(120, 120, 120) / 255;
            CreateCircle(1000, 10, 7, rgb);
        }

        private void CreateCircle(int n, float r, float r2, vec3 rgb)
        {
            OpenGL gl = GLControl.OpenGL;
            gl.Begin(BeginMode.TriangleString);
            gl.Color(rgb.x, rgb.y, rgb.z, 0);

            double theta;

            for (int i = 0; i < n; i++)
            {
                // First triangle
                theta = i * 2 * Math.PI / n;
                gl.Vertex(r * Math.Cos(theta), r * Math.Sin(theta));
                gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
                theta = (i + 1) * 2 * Math.PI / n;
                gl.Vertex(r * Math.Cos(theta), r * Math.Sin(theta));

                // Second triangle
                gl.Vertex(r * Math.Cos(theta), r * Math.Sin(theta));
                gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
                theta = i * 2 * Math.PI / n;
                gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
            }
            gl.End();
            gl.Flush();
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
