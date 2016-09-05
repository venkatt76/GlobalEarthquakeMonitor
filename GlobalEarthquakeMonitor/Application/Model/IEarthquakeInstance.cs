using System;
using System.Collections.Generic;

namespace Plethora.GlobalEarthquakeMonitor.Model
{
    /// <summary>
    /// IEarthquakeInstance interface defines an earthquake instance.
    /// The earthquake occurrence time, magnitude, coordinates and closest cities 
    /// are provided by this interface.
    /// </summary>
    public interface IEarthquakeInstance
    {
        DateTime OccurrenceTime { get; }

        double Magnitude { get; }

        ILocationCoordinates Coordinates { get; }

        IEnumerable<string> ClosestCities { get; }
    }
}
