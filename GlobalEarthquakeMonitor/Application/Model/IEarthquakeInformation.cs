using System;
using System.Collections.Generic;

namespace Plethora.GlobalEarthquakeMonitor.Model
{
    /// <summary>
    /// IEarthquakeInformation interface defines the main model interface.
    /// The collection of earthquake instances are returned by this interface.
    /// The request to monitor for earthquake information is also provided by 
    /// this interface.
    /// </summary>
    public interface IEarthquakeInformation
    {
        IEnumerable<IEarthquakeInstance> EarthquakeInstances { get; }

        DateTime LatestSyncTime { get; }

        void Monitor(Action UpdateEarthquakeInformation);
    }
}
