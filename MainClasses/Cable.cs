﻿using System;
using System.Collections.Generic;
using TCC.JSONClasses;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace TCC.MainClasses
{
    public class Cable
    {
        private JSONCable jsoncable;
        public string Name { get; set; }
        public List<Section> Sections { get; set; }
        public List<Layer> Layers { get; set; }
        public List<LayerConnection> LayerConnections { get; set; }
        public List<LayerMaterial> LayerMaterials { get; set; }

        public JSONCable OpenNewFile(string filepath)
        {
            string json = File.ReadAllText(filepath);
            JSONCable cable = JsonConvert.DeserializeObject<JSONCable>(json);

            ReverseCopy(cable);

            return cable;
        }

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

        private void ReverseCopy(JSONCable cable)
        {
            // Cable
            Name = cable.name;

            // Sections
            Sections = new List<Section>();
            int nsections = 1;
            foreach (JSONSection s in cable.sections)
            {
                if (s.type == "rectangular")
                {
                    JSONRectangularSection rs = s as JSONRectangularSection;
                    RectangularSection section = new RectangularSection
                    {
                        Name = "Section" + nsections.ToString(),
                        Height = rs.height,
                        Width = rs.width,
                        ID = rs.id,
                        Type = rs.type
                    };
                    Sections.Add(section);
                    nsections++;
                }
                else if (s.type == "tubular")
                {
                    JSONTubularSection ts = s as JSONTubularSection;
                    TubularSection section = new TubularSection
                    {
                        Name = "Section" + nsections.ToString(),
                        InternalRadius = ts.internal_radius,
                        ExternalRadius = ts.external_radius,
                        ID = ts.id,
                        Type = ts.type
                    };
                    Sections.Add(section);
                    nsections++;
                }
            }

            // Materials
            LayerMaterials = new List<LayerMaterial>();
            int nmaterials = 1;
            foreach (JSONMaterial m in cable.materials)
            {
                if (m.type == "isotropic")
                {
                    JSONIsotropic iso = m as JSONIsotropic;
                    Isotropic material = new Isotropic
                    {
                        Name = "Material" + nmaterials.ToString(),
                        Young = iso.young_modulus,
                        Poisson = iso.poisson_ratio,
                        Type = m.type,
                        ID = iso.id,
                        Density = iso.density
                    };
                    LayerMaterials.Add(material);
                    nmaterials++;
                }
                else if (m.type == "orthotropic")
                {
                    JSONOrthotropic ortho = m as JSONOrthotropic;
                    Orthotropic material = new Orthotropic
                    {
                        Name = "Material" + nmaterials.ToString(),
                        EX = ortho.Ex,
                        EY = ortho.Ey,
                        EZ = ortho.Ez,
                        NuXY = ortho.nuxy,
                        NuXZ = ortho.nuxz,
                        NuYZ = ortho.nuyz,
                        GXY = ortho.Gxy,
                        GXZ = ortho.Gxz,
                        GYZ = ortho.Gyz,
                        Type = m.type,
                        ID = ortho.id,
                        Density = ortho.density
                    };
                    LayerMaterials.Add(material);
                    nmaterials++;
                }
            }

            // Layers
            Layers = new List<Layer>();
            foreach (JSONLayer l in cable.layers)
            {
                // Helix
                if (l.type == "helix" || l.type == "armor")
                {
                    JSONHelixLayer hl = l as JSONHelixLayer;
                    Boundaries start = new Boundaries
                    {
                        ID = hl.line.start.id,
                        DesignOnly = hl.line.start.design_only,
                        CoordinateSystem = hl.line.start.coordinate_system,
                        Coordinates = hl.line.start.coordinates,
                        Loads = hl.line.start.loads,
                        Status = hl.line.start.status,
                        ImposedDisplacements = hl.line.start.imposed_displacements
                    };
                    Boundaries end = new Boundaries
                    {
                        ID = hl.line.end.id,
                        DesignOnly = hl.line.end.design_only,
                        CoordinateSystem = hl.line.end.coordinate_system,
                        Coordinates = hl.line.end.coordinates,
                        Loads = hl.line.end.loads,
                        Status = hl.line.end.status,
                        ImposedDisplacements = hl.line.end.imposed_displacements
                    };
                    Line line = new Line
                    {
                        FourierOrder = hl.line.fourier_order,
                        DesignOnly = hl.line.design_only,
                        Start = start,
                        End = end,
                        DistributedLoads = hl.line.distributed_loads,
                        Status = hl.line.status,
                        ImposedDisplacements = hl.line.imposed_displacements
                    };
                    LayerMaterial material = null;
                    Section section = new Section();
                    foreach (LayerMaterial m in LayerMaterials)
                    {
                        if (m.ID == hl.material) material = m;
                    }
                    foreach (Section s in Sections)
                    {
                        if (s.ID == hl.section) section = s;
                    }
                    HelixLayer layer = new HelixLayer
                    {
                        Line = line,
                        Wires = hl.wires,
                        Length = hl.length,
                        Section = section,
                        Radius = hl.radius,
                        LayAngle = hl.lay_angle,
                        InitialAngle = hl.initial_angle,
                        Divisions = hl.divisions,
                        Name = hl.name,
                        Type = hl.type,
                        Material = material,
                        BodyLoad = hl.body_load
                    };
                    Layers.Add(layer);
                }
                // Cylinder
                else if (l.type == "cylinder")
                {
                    JSONCylinderLayer cl = l as JSONCylinderLayer;
                    List<Area> areas = new List<Area>();
                    foreach (JSONArea a in cl.areas)
                    {
                        double[] StartLoads = UnpopulateLoads(a.frontier.start.loads);
                        string[] StartStatus = UnpopulateStatus(a.frontier.start.status);
                        double[] StartImposedDisplacements = UnpopulateLoads(a.frontier.start.imposed_displacements);
                        Boundaries start = new Boundaries
                        {
                            ID = a.frontier.start.id,
                            DesignOnly = a.frontier.start.design_only,
                            CoordinateSystem = a.frontier.start.coordinate_system,
                            Coordinates = a.frontier.start.coordinates,
                            Loads = StartLoads,
                            Status = StartStatus,
                            ImposedDisplacements = StartImposedDisplacements
                        };

                        double[] EndLoads = UnpopulateLoads(a.frontier.end.loads);
                        string[] EndStatus = UnpopulateStatus(a.frontier.end.status);
                        double[] EndImposedDisplacements = UnpopulateLoads(a.frontier.end.imposed_displacements);
                        Boundaries end = new Boundaries
                        {
                            ID = a.frontier.end.id,
                            DesignOnly = a.frontier.end.design_only,
                            CoordinateSystem = a.frontier.end.coordinate_system,
                            Coordinates = a.frontier.end.coordinates,
                            Loads = EndLoads,
                            Status = EndStatus,
                            ImposedDisplacements = EndImposedDisplacements
                        };

                        double[] FDistributedLoads = UnpopulateLoads(a.frontier.distributed_loads);
                        string[] FStatus = UnpopulateStatus(a.frontier.status);
                        double[] FImposedDisplacementes = UnpopulateLoads(a.frontier.imposed_displacements);
                        Line frontier = new Line
                        {
                            FourierOrder = a.frontier.fourier_order,
                            DesignOnly = a.frontier.design_only,
                            Start = start,
                            End = end,
                            DistributedLoads = FDistributedLoads,
                            Status = FStatus,
                            ImposedDisplacements = FImposedDisplacementes
                        };

                        double[] AImposedDisplacements = UnpopulateLoads(a.imposed_displacements);
                        string[] AStatus = UnpopulateStatus(a.status);
                        Area area = new Area
                        {
                            Surface = a.surface,
                            Pressure = a.pressure,
                            Frontier = frontier,
                            Status = AStatus,
                            ImposedDisplacements = AImposedDisplacements
                        };
                        areas.Add(area);
                    }
                    LayerMaterial material = null;
                    foreach (LayerMaterial m in LayerMaterials)
                    {
                        if (m.ID == cl.material) material = m;
                    }
                    CylinderLayer layer = new CylinderLayer
                    {
                        Length = cl.length,
                        Radius = cl.radius,
                        Thickness = cl.thickness,
                        FourierOrder = cl.fourier_series_order,
                        RadialDivisions = cl.radial_divisions,
                        AxialDivisions = cl.axial_divisions,
                        Areas = areas,
                        Name = cl.name,
                        Type = cl.type,
                        Material = material,
                        BodyLoad = cl.body_load
                    };
                    Layers.Add(layer);
                }
            }

            // Layer connections
            LayerConnections = new List<LayerConnection>();
            int nconnections = 1;
            foreach (JSONLayerConnection lc in cable.layer_connections)
            {
                LayerConnection connection = new LayerConnection
                {
                    Name = "Connection" + nconnections.ToString(),
                    Type = lc.type,
                    FirstLayer = lc.first_layer,
                    SecondLayer = lc.second_layer,
                    FrictionCoefficient = lc.friction_coefficient,
                    NormalDirection = lc.normal_direction,
                    FirstTangentDirection = lc.first_tangent_direction,
                    SecondTangentDirection = lc.second_tangent_direction,
                    NormalPenalty = lc.normal_penalty,
                    TangentialPenalty = lc.tangential_penalty,
                    PinballSearchRadius = lc.pinball_search_radius,
                };
                LayerConnections.Add(connection);
                nconnections++;
            }
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
                        Ex = ortho.EX,
                        Ey = ortho.EY,
                        Ez = ortho.EZ,
                        nuxy = ortho.NuXY,
                        nuxz = ortho.NuXZ,
                        nuyz = ortho.NuYZ,
                        Gxy = ortho.GXY,
                        Gxz = ortho.GXZ,
                        Gyz = ortho.GYZ,
                        type = m.Type,
                        id = ortho.ID,
                        density = ortho.Density
                    };
                    jsoncable.materials.Add(jmaterial);
                }
            }
        }

        private double[] UnpopulateLoads(double[] loads)
        {
            double[] newloads = new double[6];
            for (int i = 0; i < 6; i++)
            {
                newloads[i] = loads[i];
            }
            return newloads;
        }
        private string[] UnpopulateStatus(string[] status)
        {
            string[] newstatus = new string[6];
            for (int i = 0; i < 6; i++)
            {
                if (status[i] == "Unused") newstatus[i] = "Free";
                else newstatus[i] = status[i];
            }
            return newstatus;
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
