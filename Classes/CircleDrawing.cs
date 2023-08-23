using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TCC
{
    class CircleDrawing
    {
        private OpenGL gl;

        public CircleDrawing(OpenGL gl, int n, float r, float rw, vec3 rgb, bool triangles)
        {
            this.gl = gl;

            CreateCircle(n, r, rw, rgb, triangles);
        }

        /// <summary>
        /// Draw circle with lines (max width = 10) if triangles is false, or with triangles if true (poor antialias)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="r"></param>
        /// <param name="rw"></param>
        /// <param name="rgb"></param>
        /// <param name="triangles"></param>
        private void CreateCircle(int n, float r, float rw, vec3 rgb, bool triangles)
        {
            if (!triangles)
            {
                // Antialiasing
                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Hint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);

                gl.LineWidth(rw);
                gl.Begin(BeginMode.LineLoop);
                gl.Color(rgb.x, rgb.y, rgb.z, 1);

                double theta;

                for (int i = 0; i < n; i++)
                {
                    theta = i * 2 * Math.PI / n;
                    gl.Vertex(r * Math.Cos(theta), r * Math.Sin(theta));
                }
                gl.End();
                gl.Flush();
            }

            else
            {
                gl.Disable(OpenGL.GL_BLEND);
                gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
                gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);

                gl.Begin(BeginMode.Triangles);
                gl.Color(rgb.x, rgb.y, rgb.z, 1);

                double theta;

                for (int i = 0; i < n; i++)
                {
                    // First triangle
                    theta = i * 2 * Math.PI / n;
                    gl.Vertex(r * Math.Cos(theta), r * Math.Sin(theta));
                    gl.Vertex(rw * Math.Cos(theta), rw * Math.Sin(theta));
                    theta = (i + 1) * 2 * Math.PI / n;
                    gl.Vertex(r * Math.Cos(theta), r * Math.Sin(theta));

                    // Second triangle
                    gl.Vertex(r * Math.Cos(theta), r * Math.Sin(theta));
                    gl.Vertex(rw * Math.Cos(theta), rw * Math.Sin(theta));
                    theta = i * 2 * Math.PI / n;
                    gl.Vertex(rw * Math.Cos(theta), rw * Math.Sin(theta));
                }
                gl.End();
                gl.Flush();
            }
        }
    }
}
