using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes
{
    public class CylinderLayer : Layer
    {
        private string name;
        private double length;
        private double radius;
        private double thickness;
        private int fourierOrder;
        private int divisions;
        private int radialDivisions;
        private int axialDivisions;
        private List<Area> areas;
        private string label;
        private string type;
        private LayerMaterial material;
        private double[] bodyLoad;

        public string Name { get { return name; } set { name = value; } }
        public double Length { get { return length; } set { length = value; } }
        public double Radius { get { return radius; } set { radius = value; } }
        public double Thickness { get { return thickness; } set { thickness = value; } }
        public int FourierOrder { get { return fourierOrder; } set { fourierOrder = value; } }
        public int Divisions { get { return divisions; } set { divisions = value; } }
        public int RadialDivisions { get { return radialDivisions; } set { radialDivisions = value; } }
        public int AxialDivisions { get { return axialDivisions; } set { axialDivisions = value; } }
        public List<Area> Areas { get { return areas; } set { areas = value; } }
        public string Label { get { return label; } set { label = value; } }
        public string Type { get { return type; } set { type = value; } }
        public LayerMaterial Material { get { return material; } set { material = value; } }
        public double[] BodyLoad { get { return bodyLoad; } set { bodyLoad = value; } }
    }
}
