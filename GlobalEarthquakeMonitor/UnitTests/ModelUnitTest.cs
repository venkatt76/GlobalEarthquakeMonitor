using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plethora.GlobalEarthquakeMonitor.Model;
using Plethora.GlobalEarthquakeMonitor.Model.DataAccess;

namespace Plethora.GlobalEarthquakeMonitor.UnitTests
{
    /// <summary>
    /// Model Unit Tests
    /// </summary>
    [TestClass]
    public class ModelUnitTest
    {
        [TestMethod]
        public void TestEarthquakeInformationCount()
        {
            var earthquakeInformation = new EarthquakeInformation();
            var earthquakeInstances = earthquakeInformation.EarthquakeInstances;
            Assert.IsTrue(earthquakeInstances.Count() > 0);
        }

        [TestMethod]
        public void TestEarthquakeInformationLatestSyncTime()
        {
            var earthquakeInformation = new EarthquakeInformation();
            var latestSyncTime = earthquakeInformation.LatestSyncTime;
            Assert.IsNotNull(latestSyncTime);
        }

        [TestMethod]
        public void TestEarthquakeDataInLastTimePeriodCount()
        {
            var earthquakeInstances = EarthquakeDataFeedReader.GetEarthquakeInstances();
            Assert.IsTrue(earthquakeInstances.Count() > 0);
        }

        [TestMethod]
        public void TestEarthquakeDataFromStartTimeToEndTimeCount()
        {
            DateTime fromTime = DateTime.Parse("2016-09-04T04:49:04");
            DateTime endTime = DateTime.Parse("2016-09-04T04:49:14");
            var earthquakeInstances = EarthquakeDataFeedReader.GetEarthquakeInstances(fromTime, endTime);
            Assert.IsTrue(earthquakeInstances.Count() > 0);
        }

        [TestMethod]
        public void TestWorldCitiesReader()
        {
            IEnumerable<IWorldLocation> worldLocations = WorldCitiesReader.GetWorldCitiesData();
            Assert.IsTrue(worldLocations.Count() > 0);
        }
    }
}
