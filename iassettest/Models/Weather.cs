using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iassettest.Models
{
    public class Weather
    {
        public string location;
        public string time;
        public string wind;
        public string visibility;
        public string skyConditions;
        public string temperature;
        public string dewPoint;
        public string relativeHumidity;
        public string pressure;

        public Weather()
        {

        }

        public Weather(string webServiceResponse)
        {
            JObject o = JObject.Parse(webServiceResponse);

            location = (string)o.SelectToken("name");
            wind = (string)o.SelectToken("wind").SelectToken("speed");
            visibility = (string)o.SelectToken("visibility");
            time = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((int)o.SelectToken("dt")).ToShortTimeString();
            temperature = (string)o.SelectToken("main").SelectToken("temp");
            dewPoint = ""; // not available in api
            relativeHumidity = (string)o.SelectToken("main").SelectToken("humidity");
            pressure = (string)o.SelectToken("main").SelectToken("pressure");
            skyConditions = (string)o.SelectToken("weather").First.SelectToken("description");
        }
    }
}