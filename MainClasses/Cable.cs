using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.JSONClasses;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace TCC.Classes
{
    public class Cable
    {
        private JSONCable jsoncable;
        public string Name { get; set; }
        public List<Section> Sections { get; set; }
        public List<Layer> Layers { get; set; }
        public List<LayerConnection> LayerConnections { get; set; }
        public List<LayerMaterial> LayerMaterials { get; set; }

        public void SaveFile()
        {
            jsoncable = new JSONCable();
            Copy();
            string json = JsonConvert.SerializeObject(jsoncable, Formatting.Indented);

            SaveFileDialog dialog = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Save JSON",
                CheckPathExists = true,
                DefaultExt = "json",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK) File.WriteAllText(dialog.FileName, json);
        }

        private void Copy()
        {
            // Cable
            jsoncable.name = Name;

            // Sections
            jsoncable.sections = new List<JSONSection>();
            foreach (Section s in Sections)
            {
                if (s.Type == "rectangular")
                {
                    RectangularSection rs = s as RectangularSection;
                    JSONRectangularSection jsection = new JSONRectangularSection
                    {
                        height = rs.Height,
                        width = rs.Width,
                        id = rs.ID,
                        type = rs.Type
                    };
                    jsoncable.sections.Add(jsection);
                }
                else if (s.Type == "tubular")
                {
                    TubularSection ts  = s as TubularSection;
                    JSONTubularSection jsection = new JSONTubularSection
                    {
                        internal_radius = ts.InternalRadius,
                        external_radius = ts.ExternalRadius,
                        id = ts.ID,
                        type = ts.Type
                    };
                    jsoncable.sections.Add(jsection);
                }
            }

            // Layers
            jsoncable.layers = new List<JSONLayer>();
            foreach (Layer l in Layers)
            {
                // Helix
                if (l.Type == "helix" || l.Type == "armor")
                {
                    HelixLayer hl = l as HelixLayer;
                    JSONBoundaries jstart = new JSONBoundaries
                    {
                        id = hl.Line.Start.ID,
                        design_only = hl.Line.Start.DesignOnly,
                        coordinate_system = hl.Line.Start.CoordinateSystem,
                        coordinates = hl.Line.Start.Coordinates,
                        loads = hl.Line.Start.Loads,
                        status = hl.Line.Start.Status,
                        imposed_displacements = hl.Line.Start.ImposedDisplacements
                    };
                    JSONBoundaries jend = new JSONBoundaries
                    {
                        id = hl.Line.End.ID,
                        design_only = hl.Line.End.DesignOnly,
                        coordinate_system = hl.Line.End.CoordinateSystem,
                        coordinates = hl.Line.End.Coordinates,
                        loads = hl.Line.End.Loads,
                        status = hl.Line.End.Status,
                        imposed_displacements = hl.Line.End.ImposedDisplacements
                    };
                    JSONLine jline = new JSONLine
                    {
                        fourier_order = hl.Line.FourierOrder,
                        design_only = hl.Line.DesignOnly,
                        start = jstart,
                        end = jend,
                        distributed_loads = hl.Line.DistributedLoads,
                        status = hl.Line.Status,
                        imposed_displacements = hl.Line.ImposedDisplacements
                    };
                    JSONHelixLayer jlayer = new JSONHelixLayer
                    {
                        line = jline,
                        wires = hl.Wires,
                        length = hl.Length,
                        section = hl.Section.ID,
                        radius = hl.Radius,
                        lay_angle = hl.LayAngle,
                        initial_angle = hl.InitialAngle,
                        divisions = hl.Divisions,
                        name = hl.Name,
                        type = hl.Type,
                        material = hl.Material.ID,
                        body_load = hl.BodyLoad
                    };
                    jsoncable.layers.Add(jlayer);
                }
                // Cylinder
                else if (l.Type == "cylinder")
                {
                    CylinderLayer cl = l as CylinderLayer;
                    List<JSONArea> jareas = new List<JSONArea>();
                    foreach (Area a in cl.Areas)
                    {
                        double[] startloads = PopulateLoads(a.Frontier.Start.Loads, a.Frontier.FourierOrder);
                        string[] startstatus = PopulateStatus(a.Frontier.Start.Status, a.Frontier.FourierOrder);
                        double[] startimposed_displacements = PopulateLoads(a.Frontier.Start.ImposedDisplacements, a.Frontier.FourierOrder);
                        JSONBoundaries jstart = new JSONBoundaries
                        {
                            id = a.Frontier.Start.ID,
                            design_only = a.Frontier.Start.DesignOnly,
                            coordinate_system = a.Frontier.Start.CoordinateSystem,
                            coordinates = a.Frontier.Start.Coordinates,
                            loads = startloads,
                            status = startstatus,
                            imposed_displacements = startimposed_displacements
                        };

                        double[] endloads = PopulateLoads(a.Frontier.End.Loads, a.Frontier.FourierOrder);
                        string[] endstatus = PopulateStatus(a.Frontier.End.Status, a.Frontier.FourierOrder);
                        double[] endimposed_displacements = PopulateLoads(a.Frontier.End.ImposedDisplacements, a.Frontier.FourierOrder);
                        JSONBoundaries jend = new JSONBoundaries
                        {
                            id = a.Frontier.End.ID,
                            design_only = a.Frontier.End.DesignOnly,
                            coordinate_system = a.Frontier.End.CoordinateSystem,
                            coordinates = a.Frontier.End.Coordinates,
                            loads = endloads,
                            status = endstatus,
                            imposed_displacements = endimposed_displacements
                        };

                        double[] fdistributed_loads = PopulateLoads(a.Frontier.DistributedLoads, a.Frontier.FourierOrder);
                        string[] fstatus = PopulateStatus(a.Frontier.Status, a.Frontier.FourierOrder);
                        double[] fimposed_displacementes = PopulateLoads(a.Frontier.ImposedDisplacements, a.Frontier.FourierOrder);
                        JSONLine jfrontier = new JSONLine
                        {
                            fourier_order = a.Frontier.FourierOrder,
                            design_only = a.Frontier.DesignOnly,
                            start = jstart,
                            end = jend,
                            distributed_loads = fdistributed_loads,
                            status = fstatus,
                            imposed_displacements = fimposed_displacementes
                        };

                        double[] aimposed_displacements = PopulateLoads(a.ImposedDisplacements, cl.FourierOrder);
                        string[] astatus = PopulateStatus(a.Status, cl.FourierOrder);
                        JSONArea jarea = new JSONArea
                        {
                            surface = a.Surface,
                            pressure = a.Pressure,
                            frontier = jfrontier,
                            status = astatus,
                            imposed_displacements = aimposed_displacements
                        };
                        jareas.Add(jarea);
                    }
                    JSONCylinderLayer jlayer = new JSONCylinderLayer
                    {
                        length = cl.Length,
                        radius = cl.Radius,
                        thickness = cl.Thickness,
                        fourier_series_order = cl.FourierOrder,
                        radial_divisions = cl.RadialDivisions,
                        axial_divisions = cl.AxialDivisions,
                        areas = jareas,
                        name = cl.Name,
                        type = cl.Type,
                        material = cl.Material.ID,
                        body_load = cl.BodyLoad
                    };
                    jsoncable.layers.Add(jlayer);
                }
            }

            // Layer connections
            jsoncable.layer_connections = new List<JSONLayerConnection>();
            foreach (LayerConnection lc in LayerConnections)
            {
                JSONLayerConnection jconnection = new JSONLayerConnection
                {
                    type = lc.Type,
                    first_layer = lc.FirstLayer,
                    second_layer = lc.SecondLayer,
                    friction_coefficient = lc.FrictionCoefficient,
                    normal_direction = lc.NormalDirection,
                    first_tangent_direction = lc.FirstTangentDirection,
                    second_tangent_direction = lc.SecondTangentDirection,
                    normal_penalty = lc.NormalPenalty,
                    tangential_penalty = lc.TangentialPenalty,
                    pinball_search_radius = lc.PinballSearchRadius,
                };
                jsoncable.layer_connections.Add(jconnection);
            }

            // Materials
            jsoncable.materials = new List<JSONMaterial>();
            foreach (LayerMaterial m in LayerMaterials)
            {
                if (m.Type == "isotropic")
                {
                    Isotropic iso = m as Isotropic;
                    JSONIsotropic jmaterial = new JSONIsotropic
                    {
                        young_modulus = iso.Young,
                        poisson_ratio = iso.Poisson,
                        type = m.Type,
                        id = iso.ID,
                        density = iso.Density
                    };
                    jsoncable.materials.Add(jmaterial);
                }
                else if (m.Type == "orthotropic")
                {
                    Orthotropic ortho = m as Orthotropic;
                    JSONOrthotropic jmaterial = new JSONOrthotropic
                    {
                        young = ortho.Young,
                        poisson = ortho.Poisson,
                        shear = ortho.Shear,
                        type = m.Type,
                        id = ortho.ID,
                        density = ortho.Density
                    };
                    jsoncable.materials.Add(jmaterial);
                }
            }
        }

        private double[] PopulateLoads(double[] loads, int order)
        {
            double[] newloads = new double[6 * (order + 1)];
            for (int i = 0; i < (6 * (order + 1)); i++)
            {
                if (i < 6) newloads[i] = loads[i];
                else newloads[i] = 0.0;
            }
            return newloads;
        }
        private string[] PopulateStatus(string[] status, int order)
        {
            string[] newstatus = new string[6 * (order + 1)];

            for (int i = 0; i < (6 * (order + 1)); i++)
            {
                if (i < 3) newstatus[i] = status[i];
                else if (i < 6) newstatus[i] = "Unused";
                else newstatus[i] = status[i % 3];
            }
            return newstatus;
        }
    }
}
