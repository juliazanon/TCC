using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SharpGL.SceneGraph.Quadrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC.MainClasses.Layers
{
    public class LayerConverter : JsonConverter
    {
        static readonly JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new SpecifiedConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Layer));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch (jo["type"].Value<String>())
            {
                case "helix":
                    return JsonConvert.DeserializeObject<HelixLayer>(jo.ToString(), SpecifiedSubclassConversion);
                case "cylinder":
                    return JsonConvert.DeserializeObject<CylinderLayer>(jo.ToString(), SpecifiedSubclassConversion);
                case "armor":
                    return JsonConvert.DeserializeObject<HelixLayer>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new Exception();
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}
