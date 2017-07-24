using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iassettest.Services;
using iassettest.Models;
using iassettest.Controllers;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace iassettest.Tests.Controllers
{ 


    class TestWeatherService : IWeatherService
    {
        public string[] cities = { "sydney", "melboune" };

        public IEnumerable<string> GetCitiesForCountry(string country)
        {
            return cities;
        }

        public Weather GetWeatherForCity(string city, string country)
        {
            var w = new Weather();
            w.dewPoint = "30";
            w.location = "somewhere";
            w.pressure = "1000";

            return w;
        }

    }



    [TestClass]
    public class WeatherAPIControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var ws = new TestWeatherService();

            // Arrange
            var controller = new WeatherAPIController(ws);

            // Act
            var actionResult = controller.GetCitiesForCountry("blah");


            // if your action was returning data in the body like: Ok<string>("data: 12")

            var conNegResult = actionResult as OkNegotiatedContentResult<IEnumerable<string>>;

            // Assert
            Assert.IsNotNull(conNegResult);

            int i = 0;
            foreach( string s in conNegResult.Content)
            {
                Assert.AreEqual(ws.cities[i++], s);

            }


        }
    }
}


