using GlmNet;
using SharpGL.Enumerations;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class HelixLayer : Layer
    {
        private int wires;
        private Line line;
        private double length;
        private Section section;
        private double radius;
        private double layAngle;
        private double initialAngle;
        private int divisions;
        private string name;
        private string type;
        private LayerMaterial material;
        private double[] bodyLoad;

        public int Wires {
            get { return wires; }
            set { wires = value; }
        }
        public Line Line {
            get { return line; }
            set { line = value; }
        }
        public double Length {
            get { return length; }
            set { length = value; }
        }
        public Section Section {
            get { return section; }
            set { section = value; }
        }
        public double Radius {
            get { return radius; }
            set { radius = value; }
        }
        public double LayAngle {
            get { return layAngle; }
            set { layAngle = value; }
        }
        public double InitialAngle {
            get { return initialAngle; }
            set { initialAngle = value; }
        }
        public int Divisions {
            get { return divisions; }
            set { divisions = value; }
        }
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string Type {
            get { return type; }
            set { type = value; }
        }
        public LayerMaterial Material {
            get { return material; }
            set { material = value; }
        }
        public double[] BodyLoad {
            get { return bodyLoad; }
            set { bodyLoad = value; }
        }

        public void Draw(OpenGL gl, vec3 rgb)
        {
            if (section.Type == "Rectangular")
            {
                RectangularSection rs = section as RectangularSection;

                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Hint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);

                gl.LineWidth(2);
                gl.Begin(BeginMode.Lines);
                gl.Color(rgb.x, rgb.y, rgb.z, 1);

                double r1 = radius + rs.Height / 2;
                double r2 = radius - rs.Height / 2;
                double theta;

                for (int i = 0; i < wires; i++)
                {
                    // Left line
                    theta = i * 2 * Math.PI / wires;
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));

                    // Top line
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    theta = (i + 1) * 2 * Math.PI / wires - (2 * Math.PI / wires - rs.Width / r1);
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));

                    // Right line
                    gl.Vertex(r1 * Math.Cos(theta), r1 * Math.Sin(theta));
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));

                    // Bottom line
                    theta = i * 2 * Math.PI / wires;
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
                    theta = (i + 1) * 2 * Math.PI / wires - (2 * Math.PI / wires - rs.Width / r2);
                    gl.Vertex(r2 * Math.Cos(theta), r2 * Math.Sin(theta));
                }

                gl.End();
                gl.Flush();
            }
        }
    }
}
