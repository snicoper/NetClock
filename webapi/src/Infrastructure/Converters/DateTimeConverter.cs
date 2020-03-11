using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetClock.Infrastructure.Converters
{
    // Microsoft.Text.Json.
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToLocalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.FFFFFF'Z'"));
        }
    }
}
