using System;
using System.Collections.Generic;

namespace Plethora.GlobalEarthquakeMonitor.Model
{
    /// <summary>
    /// EarthquakeInstance class stores earthquake instance information.
    /// The occurrence time, magnitude, coordinates and closest cities 
    /// are stored in this lightweight wrapper class for easy access.
    /// </summary>
    class EarthquakeInstance : IEarthquakeInstance
    {
        public IEnumerable<string> ClosestCities { get; }

        public ILocationCoordinates Coordinates { get; }

        public double Magnitude { get; }

        public DateTime OccurrenceTime { get; }

        public EarthquakeInstance(DateTime occurrenceTime, double magnitude, ILocationCoordinates coordinates)
        {
            OccurrenceTime = occurrenceTime;
            Magnitude = magnitude;
            Coordinates = coordinates;

            ClosestCities = WorldLocations.GetClosestLocationNames(coordinates.Latitude, coordinates.Longitude, 3);
        }
    }
}
