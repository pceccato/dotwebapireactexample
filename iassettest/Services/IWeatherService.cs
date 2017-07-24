using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iassettest.Models;

namespace iassettest.Services
{
    public interface IWeatherService
    {
        IEnumerable<string> GetCitiesForCountry(string country);

        Weather GetWeatherForCity(string city, string country);
    }
}
