using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC.MainClasses.Materials
{
    public class MaterialConverter : JsonConverter
    {
        static readonly JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new SpecifiedConcreteClassConverter() };
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(LayerMaterial));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                JObject jo = JObject.Load(reader);
                switch (jo["type"].Value<string>())
                {
                    case "isotropic":
                        return JsonConvert.DeserializeObject<Isotropic>(jo.ToString(), SpecifiedSubclassConversion);
                    case "orthotropic":
                        return JsonConvert.DeserializeObject<Orthotropic>(jo.ToString(), SpecifiedSubclassConversion);
                    default:
                        throw new JsonReaderException();
                }
            }
            catch (JsonReaderException)
            {
                throw new JsonReaderException();
            }
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
