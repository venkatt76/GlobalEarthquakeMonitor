
namespace Plethora.GlobalEarthquakeMonitor.Model
{
    /// <summary>
    /// IWorldLocation interface defines a world location (city).
    /// The world city name and coordinates are provided by this interface.
    /// </summary>
    public interface IWorldLocation
    {
        string Name { get; }

        ILocationCoordinates Coordinates { get; }
    }
}
