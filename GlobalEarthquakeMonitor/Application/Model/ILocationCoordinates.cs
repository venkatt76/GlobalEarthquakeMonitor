
namespace Plethora.GlobalEarthquakeMonitor.Model
{
    /// <summary>
    /// ILocationCoordinates interface defines location coordinates.
    /// The latitude and longitude information is provided by this interface.
    /// </summary>
    public interface ILocationCoordinates
    {
        double Latitude { get; }

        double Longitude { get; }

        string ToString();
    }
}
