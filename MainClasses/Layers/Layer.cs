﻿using Newtonsoft.Json;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TCC.MainClasses.Layers;

namespace TCC.MainClasses
{
    [DataContract]
    [JsonConverter(typeof(LayerConverter))]
    public abstract class Layer
    {
        public Layer() { }
        private int materialID;
        private LayerMaterial material;

        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "length")]
        public double Length { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
        
        [DataMember(Name = "material")]
        public int MaterialID { get { return materialID; } set { materialID = value; } }
        public LayerMaterial Material { get { return material; } set { material = value; materialID = material.ID; } }
        
        [DataMember(Name = "body_load")]
        public double[] BodyLoad { get; set; }

        public virtual void Draw(OpenGL gl)
        {
            return;
        }
    }
}
