using GlmNet;
using SharpGL.Enumerations;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Runtime.Serialization;

namespace TCC.MainClasses
{
    [DataContract]
    public class HelixLayer : Layer
    {
        private int wires;
        private Line line;
        private Section section;
        private double layAngle;
        private double initialAngle;
        private int divisions;
        private double radius;

        public HelixLayer() { }

        [DataMember(Name = "wires")]
        public int Wires {
            get { return wires; }
            set { wires = value; }
        }
        
        [DataMember(Name = "line")]
        public Line Line {
            get { return line; }
            set { line = value; }
        }
        
        [DataMember(Name = "section")]
        public int SectionID {
            get { return section.ID; }
        }
        public Section Section {
            get { return section; }
            set { section = value; }
        }
        
        [DataMember(Name = "radius")]
        public double Radius {
            get { return radius; }
            set { radius = value; }
        }
        
        [DataMember(Name = "lay_angle")]
        public double LayAngle {
            get { return layAngle; }
            set { layAngle = value; }
        }
        
        [DataMember(Name = "initial_angle")]
        public double InitialAngle {
            get { return initialAngle; }
            set { initialAngle = value; }
        }
        
        [DataMember(Name = "divisions")]
        public int Divisions {
            get { return divisions; }
            set { divisions = value; }
        }

        public void Draw(OpenGL gl, vec3 rgb)
        {
            if (section.Type == "rectangular")
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
            else if (section.Type == "tubular")
            {
                TubularSection ts = section as TubularSection;

                int n;
                if (ts.InternalRadius == 0) n = 100;
                else n = 1000;

                // Calculate dots coordinates
                double theta, phi, xcenter, ycenter, x, y;

                List<List<Tuple<double, double>>> coordinates = new List<List<Tuple<double, double>>>();

                for (int i = 0; i < wires; i++)
                {
                    theta = i * 2 * Math.PI / wires;
                    xcenter = radius * Math.Cos(theta);
                    ycenter = radius * Math.Sin(theta);
                    List<Tuple<double, double>> dots = new List<Tuple<double, double>>();

                    for (int j = 0; j < n; j++)
                    {
                        phi = j * 2 * Math.PI / n;
                        x = ts.ExternalRadius * Math.Cos(phi);
                        y = ts.ExternalRadius * Math.Sin(phi);
                        dots.Add(new Tuple<double, double>(x + xcenter, y + ycenter));
                    }
                    coordinates.Add(dots);
                }

                // Draw
                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Hint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);

                gl.LineWidth((float)(ts.ExternalRadius - ts.InternalRadius));
                gl.Color(rgb.x, rgb.y, rgb.z, 1);
                foreach (var circle in coordinates)
                {
                    if (ts.InternalRadius == 0) { gl.Begin(BeginMode.Polygon); }
                    else { gl.Begin(BeginMode.LineLoop); }
                    foreach (var point in circle)
                    {
                        gl.Vertex(point.Item1, point.Item2);
                    }
                    gl.End();
                }
                gl.Flush();
            }
        }
    }
}
