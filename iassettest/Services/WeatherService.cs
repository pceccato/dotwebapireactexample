using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iassettest;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using iassettest.Models;
using System.Net;

namespace iassettest.Services
{

    public class WeatherService : IWeatherService
    {
        GlobalWeather.GlobalWeatherSoap _weatherApi = null;

        public WeatherService()
        {
            _weatherApi = new GlobalWeather.GlobalWeatherSoapClient("GlobalWeatherSoap");
        }

        public IEnumerable<string> GetCitiesForCountry(string country)
        {
            try
            { 
                var response = _weatherApi.GetCitiesByCountry(country);
                var xml = XElement.Parse(response);
                IEnumerable<string> cities = xml.Descendants("City").Select(x => x.Value);
                return cities;
            }
            catch( Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error calling GetCitiesByCountry {0}", e);
            }
            return null;
        }

        public Weather GetWeatherForCity(string city, string country)
        {
            try
            {
                var response = _weatherApi.GetWeather(city, country);
                if (response == "Data Not Found")
                {

                    var endpoint = String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID=f496d1d662df5325d84eb929e5fb8229", Uri.EscapeUriString(city));

                    using (var client = new WebClient())
                    {
                        return new Weather(client.DownloadString(endpoint));
                    }
                }
                //
                // I never actually got a valid response from this web service so don't know what schema to expect....
                //
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error calling GetWeatherForCity {0}", e);
            }
            return null;
        }
    }
}