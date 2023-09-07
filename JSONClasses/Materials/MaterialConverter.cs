using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.JSONClasses;

namespace TCC.MainClasses.Materials
{
    public class MaterialConverter : JsonConverter
    {
        static readonly JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new SpecifiedConcreteClassConverter() };
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(JSONMaterial));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch (jo["type"].Value<string>())
            {
                case "isotropic":
                    return JsonConvert.DeserializeObject<JSONIsotropic>(jo.ToString(), SpecifiedSubclassConversion);
                case "orthotropic":
                    return JsonConvert.DeserializeObject<JSONOrthotropic>(jo.ToString(), SpecifiedSubclassConversion);
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
