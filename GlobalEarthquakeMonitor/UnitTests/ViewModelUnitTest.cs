using System.Linq;
using Plethora.GlobalEarthquakeMonitor.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plethora.GlobalEarthquakeMonitor.UnitTests
{
    /// <summary>
    /// ViewModel Unit Tests
    /// </summary>
    [TestClass]
    public class ViewModelUnitTest
    {
        [TestMethod]
        public void TestEarthquakeInstancesCount()
        {
            var earthquakeMonitorViewModel = new EarthquakeMonitorViewModel();
            var earthquakeInstances = earthquakeMonitorViewModel.EarthquakeInstances;

            Assert.IsTrue(earthquakeInstances.Count() > 0);
        }

        [TestMethod]
        public void TestEarthquakeInstancesLatestSyncTime()
        {
            var earthquakeMonitorViewModel = new EarthquakeMonitorViewModel();
            var latestSyncTime = earthquakeMonitorViewModel.EarthquakeInstancesSyncTime;

            Assert.IsNotNull(latestSyncTime);
        }
    }
}
