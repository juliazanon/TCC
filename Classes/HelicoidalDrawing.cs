using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TCC.Classes;

namespace TCC
{
    internal class HelicoidalDrawing
    {
        private OpenGL gl;

        public HelicoidalDrawing(OpenGL gl, int n, double r, Section s, vec3 rgb)
        {
            this.gl = gl;

            CreateLayer(n, r, s, rgb);
        }

        private void CreateLayer(int n, double r, Section s, vec3 rgb)
        {
            if (s.Type == "Rectangular")
            {
                RectangularSection rs = s as RectangularSection;

                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Hint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);

                gl.LineWidth(2);
                gl.Begin(BeginMode.Lines);
                gl.Color(rgb.x, rgb.y, rgb.z, 1);

                double r1 = r + rs.Height / 2;
                double r2 = r - rs.Height / 2;
                double theta;

                for (int i = 0; i < n; i++)
                {
                    // Left line
                    theta = i * 2 * Math.PI / n;
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));

                    // Top line
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    theta = (i + 1) * 2 * Math.PI / n - (2 * Math.PI / n - rs.Width / r1);
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));

                    // Right line
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));

                    // Bottom line
                    theta = i * 2 * Math.PI / n;
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
                    theta = (i + 1) * 2 * Math.PI / n - (2 * Math.PI / n - rs.Width / r2);
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
                }

                gl.End();
                gl.Flush();
            }
        }
    }
}
