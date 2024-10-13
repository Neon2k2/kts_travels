using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace kts_travels.WebAPI.Utilities
{

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string outputFormat = "dd/MM/yyyy";
        private readonly string[] inputFormats =
        [
            "dd/MM/yyyy",
            "d/M/yyyy",
            "MM/dd/yyyy",
            "M/d/yyyy",
            "yyyy-MM-dd",
            "yyyy/MM/dd",
            "dd-MM-yyyy",
            "M/d/yy"
         ];

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();

            if (string.IsNullOrWhiteSpace(dateString))
            {
                throw new JsonException("Date string is null or empty.");
            }

            foreach (var format in inputFormats)
            {
                if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate;
                }
            }
            throw new JsonException($"Invalid date format: {dateString}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var formattedValue = value.ToString(outputFormat);
            writer.WriteStringValue(formattedValue);
        }
    }


}
