using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iassettest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iassettest.Tests.Models
{
    [TestClass]
    public class ModelsTest
    {
        [TestMethod]
        public void TestWeatherParse()
        {
            string payload = @"{'coord':{'lon':153.03,'lat':-27.58},'weather':[{'id':800,'main':'Clear','description':'clear sky','icon':'01n'}],'base':'stations','main':{'temp':283.1,'pressure':1020,'humidity':71,'temp_min':280.15,'temp_max':286.15},'visibility':10000,'wind':{'speed':2.31,'deg':255.503},'clouds':{'all':0},'dt':1500811200,'sys':{'type':1,'id':8164,'message':0.0188,'country':'AU','sunrise':1500755648,'sunset':1500794091},'id':2178321,'name':'Acacia Ridge','cod':200}";

            var w = new Weather(payload);
            Assert.AreEqual(w.dewPoint, "");
            Assert.AreEqual(w.location, "Acacia Ridge");
            Assert.AreEqual(w.pressure, "1020");
            Assert.AreEqual(w.relativeHumidity, "71");
            Assert.AreEqual(w.skyConditions, "clear sky");
            Assert.AreEqual(w.temperature, "283.1");
            Assert.AreEqual(w.visibility, "10000");
            Assert.AreEqual(w.wind, "2.31");

        }
    }
}
