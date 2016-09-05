using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plethora.GlobalEarthquakeMonitor.Model
{
    class LocationCoordinates : ILocationCoordinates
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public LocationCoordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return string.Format("{0:0.000}°{1}, {2:0.000}°{3}", 
                Math.Abs(Latitude), (Latitude > 0 ? "N" : "S"),
                Math.Abs(Longitude), (Longitude > 0 ? "E" : "W"));
        }
    }
}
