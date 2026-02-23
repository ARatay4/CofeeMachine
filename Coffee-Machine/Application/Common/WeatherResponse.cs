using System.Text.Json.Serialization;

namespace Coffee_Machine.Application.Common
{
    public class WeatherResponse
    {
        [JsonPropertyName("main")]
        public MainInfo Main { get; set; }

        public class MainInfo
        {
            [JsonPropertyName("temp")]
            public double Temp { get; set; }
        }
    }

    public class IpGeoResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
