using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftwareKobo.CnblogsAPI.Converter
{
    internal class Int32StringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == null)
            {
                return false;
            }
            return objectType == typeof(int);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
