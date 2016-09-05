
namespace Plethora.GlobalEarthquakeMonitor.Model
{
    /// <summary>
    /// WorlLocation class stores world location (city) information.
    /// The name and coordinates are stored in this lightweight wrapper
    /// class for easy access.
    /// </summary>
    class WorldLocation : IWorldLocation
    {
        public ILocationCoordinates Coordinates { get; }

        public string Name { get; }

        public WorldLocation(ILocationCoordinates coordinates, string name)
        {
            Coordinates = coordinates;
            Name = name;
        }
    }
}
