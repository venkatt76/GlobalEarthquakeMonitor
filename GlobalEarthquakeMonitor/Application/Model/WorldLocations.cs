using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Location;
using Plethora.GlobalEarthquakeMonitor.Model.DataAccess;

namespace Plethora.GlobalEarthquakeMonitor.Model
{
    /// <summary>
    /// WorldLocations static class that reads and stores word locations information.
    /// This class contains a static data member that stores the world city information.
    /// The static constructor gets the world city information and saves the information
    /// in the collection. This class also contains the methods to find the closest
    /// locations for a specified geo coordinate location.
    /// </summary>
    static class WorldLocations
    {
        // Static collection of world city location objects.
        private static readonly ICollection<IWorldLocation> worldLocations;

        /// <summary>
        /// GetClosestLocationNames returns the names of the world cities that are closes to
        /// the specified input geo coordinate location. This method returns the number of
        /// locations requested.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="numberOfLocations"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetClosestLocationNames(double latitude, double longitude, int numberOfLocations)
        {
            try
            {
                // Create dictionary to save world location information.
                // Key = world location name, Value = distance between world location and specified input point.
                var worldLocationsNearestToLocation = new Dictionary<string, double>();

                // Populate dictionary from world location collection object
                // For each location, the distance between the location and specified input
                // point is calculated and stored as the value.
                foreach (var worldLocation in worldLocations)
                {
                    if (!worldLocationsNearestToLocation.Keys.Contains(worldLocation.Name))
                        worldLocationsNearestToLocation.Add(
                            worldLocation.Name,
                            GetDistanceBetweenLocations(worldLocation.Coordinates.Latitude, worldLocation.Coordinates.Longitude,
                                latitude, longitude));
                }

                // Order the dictionary by distances and return a collection of Key, Value pairs.
                var worldLocationsNearestToLocationOrdered =
                    from worldLocationNearestToLocation in worldLocationsNearestToLocation
                    orderby worldLocationNearestToLocation.Value ascending
                    select worldLocationNearestToLocation;

                // Retrieve the first n locations.
                var worldLocationNamesNearestToLocation =
                    worldLocationsNearestToLocationOrdered.Take(numberOfLocations).Select(w => w.Key).ToList();

                return worldLocationNamesNearestToLocation;
            }
            catch (Exception ex)
            {
                throw new Exception("GEM: Unable to obtain world cities closest to specified coordinates."
                    + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// WorldLocations static constructor obtains world city information as a collection
        /// The reading of this information is delegated to the WorldCitiesReader class.
        /// </summary>
        static WorldLocations()
        {
            try
            {
                worldLocations = WorldCitiesReader.GetWorldCitiesData();
            }
            catch (Exception ex)
            {
                throw new Exception("GEM: Unable to obtain world cities information."
                    + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// GetDistanceBetweenLocations utility location calculats the distance between two geo locations
        /// specified their latitude and longitude.
        /// </summary>
        /// <param name="firstLocationLatitude"></param>
        /// <param name="firstLocationLongitude"></param>
        /// <param name="secondLocationLatitude"></param>
        /// <param name="secondLocationLongitude"></param>
        /// <returns></returns>
        public static double GetDistanceBetweenLocations(double firstLocationLatitude, double firstLocationLongitude, 
            double secondLocationLatitude, double secondLocationLongitude)
        {
            try
            {
                var firstLocationGeoCoordinate = new GeoCoordinate(firstLocationLatitude, firstLocationLongitude);
                var secondLocationGeoCoordinate = new GeoCoordinate(secondLocationLatitude, secondLocationLongitude);

                return firstLocationGeoCoordinate.GetDistanceTo(secondLocationGeoCoordinate);
            }
            catch (Exception ex)
            {
                throw new Exception("GEM: Unable to calculate distance between geo locations."
                    + Environment.NewLine + ex.Message);
            }

        }
    }
}
