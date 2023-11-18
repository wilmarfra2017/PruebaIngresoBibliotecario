using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PruebaIngresoBibliotecario.Api
{
    public class GuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();

            if (Guid.TryParse(stringValue, out var guidValue))
            {
                return guidValue;
            }

            throw new InvalidGuidFormatException($"El valor '{stringValue}' no es un GUID valido.");
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
