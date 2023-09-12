using GlmNet;
using SharpGL.Enumerations;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TCC.MainClasses
{
    [DataContract]
    public class CylinderLayer : Layer
    {
        private double radius;
        private double thickness;
        private int fourierOrder;
        private int divisions;
        private int radialDivisions;
        private int axialDivisions;
        private List<Area> areas;

        [Newtonsoft.Json.JsonConstructor]
        public CylinderLayer() 
        {
            this.Type = "cylinder";
        }

        [DataMember(Name = "radius")]
        public double Radius { get { return radius; } set { radius = value; } }
        
        [DataMember(Name = "thickness")]
        public double Thickness { get { return thickness; } set { thickness = value; } }
        
        [DataMember(Name = "fourier_series_order")]
        public int FourierOrder { get { return fourierOrder; } set { fourierOrder = value; } }
        
        [DataMember(Name = "divisions")]
        public int Divisions { get { return divisions; } set { divisions = value; } }
        
        [DataMember(Name = "radial_divisions")]
        public int RadialDivisions { get { return radialDivisions; } set { radialDivisions = value; } }
        
        [DataMember(Name = "axial_divisions")]
        public int AxialDivisions { get { return axialDivisions; } set { axialDivisions = value; } }
        
        [DataMember(Name = "areas")]
        public List<Area> Areas { get { return areas; } set { areas = value; } }

        public void Draw(OpenGL gl, vec3 rgb, int n, float prop, bool triangles)
        {
            float w = (float)thickness * prop;
            if (!triangles)
            {
                // Antialiasing
                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
                gl.Enable(OpenGL.GL_LINE_SMOOTH);
                gl.Hint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);

                gl.LineWidth(w);
                gl.Begin(BeginMode.LineLoop);
                gl.Color(rgb.x, rgb.y, rgb.z, 1);

                double theta;

                for (int i = 0; i < n; i++)
                {
                    theta = i * 2 * Math.PI / n;
                    gl.Vertex(radius * Math.Cos(theta), radius * Math.Sin(theta));
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
                    gl.Vertex(radius * Math.Cos(theta), radius * Math.Sin(theta));
                    gl.Vertex(w * Math.Cos(theta), w * Math.Sin(theta));
                    theta = (i + 1) * 2 * Math.PI / n;
                    gl.Vertex(radius * Math.Cos(theta), radius * Math.Sin(theta));

                    // Second triangle
                    gl.Vertex(radius * Math.Cos(theta), radius * Math.Sin(theta));
                    gl.Vertex(w * Math.Cos(theta), w * Math.Sin(theta));
                    theta = i * 2 * Math.PI / n;
                    gl.Vertex(w * Math.Cos(theta), w * Math.Sin(theta));
                }
                gl.End();
                gl.Flush();
            }
        }
    }
}
