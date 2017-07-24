using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using iassettest.Models;
using iassettest.Services;


namespace iassettest.Controllers
{
    [RoutePrefix("api")]
    public class WeatherAPIController : ApiController
    {
        readonly private IWeatherService _ws;

        public WeatherAPIController(IWeatherService weatherApi)
        {
            _ws = weatherApi;
        }

        [Route("cities")]
        public IHttpActionResult GetCitiesForCountry(string country)
        {
            var cities = _ws.GetCitiesForCountry(country);
            if (cities == null || !cities.GetEnumerator().MoveNext()) // test for empty
            {
                return NotFound();
            }
            return Ok(cities);
        }

        [Route("weather")]
        public IHttpActionResult GetWeatherForCity(string city, string country)
        {
            var weather = _ws.GetWeatherForCity(city, country);
            if(weather == null)
            {
                return NotFound();
            }
            return Ok(weather);
        }
    }
}
