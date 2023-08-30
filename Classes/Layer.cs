using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class Layer
    {
        private string name;
        private string type;
        private LayerMaterial material;
        private double length;
        private double[] bodyLoad;

        public string Name { get { return name; } set { name = value; } }
        public double Length { get { return length; } set { length = value; } }
        public string Type { get { return type; } set { type = value; } }
        public LayerMaterial Material { get { return material; } set { material = value; } }
        public double[] BodyLoad { get { return bodyLoad; } set { bodyLoad = value; } }

        public virtual void Draw(OpenGL gl)
        {
            return;
        }
    }
}
