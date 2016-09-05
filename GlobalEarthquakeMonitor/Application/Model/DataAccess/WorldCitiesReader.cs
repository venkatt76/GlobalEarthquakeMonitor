using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Plethora.GlobalEarthquakeMonitor.Model.DataAccess
{
    /// <summary>
    /// WorldCitiesReader class reads the world cities information from the data file.
    /// The data is stored in WorldLocation objects and returned to the caller.
    /// The data file to read is specified in the configuration file. The column numbers that contain the
    /// relevant data, i.e. City Name, Latitude, Longitude are are also specified in the configuration file.
    /// </summary>
    public class WorldCitiesReader
    {
        /// <summary>
        /// GetWorldCities method reads, parses and returns world cities information.
        /// </summary>
        /// <returns></returns>
        public static ICollection<IWorldLocation> GetWorldCitiesData()
        {
            try
            {
                ICollection<IWorldLocation> worldLocations = new List<IWorldLocation>();

                // Read world cities data file. 
                var worldCitiesInformation = File.ReadAllLines(Properties.Settings.Default.WorldCitiesDataFile, Encoding.UTF7);

                var worldCitiesDataColumnDelimiter = Properties.Settings.Default.WorldCitiesDataColumnDelimiter;
                var worldCitiesDataCityNameColumnNumber = Properties.Settings.Default.WorldCitiesDataCityNameColumnNumber;
                var worldCitiesDataCityLatitudeColumnNumber = Properties.Settings.Default.WorldCitiesDataCityLatitudeColumnNumber;
                var worldCitiesDataCityLongitudeColumnNumber = Properties.Settings.Default.WorldCitiesDataCityLongitudeColumnNumber;

                // Parse the read information to construct WorldLocation objects.
                // If a particular line cannot be read, it is ommitted and next line is read.
                foreach (var worldCityInformation in worldCitiesInformation)
                {
                    var worldCityRecord = worldCityInformation.Split(worldCitiesDataColumnDelimiter);

                    try
                    {
                        worldLocations.Add(new WorldLocation(
                            new LocationCoordinates(
                                Convert.ToDouble(worldCityRecord[worldCitiesDataCityLatitudeColumnNumber]),
                                Convert.ToDouble(worldCityRecord[worldCitiesDataCityLongitudeColumnNumber])),
                            worldCityRecord[worldCitiesDataCityNameColumnNumber]));
                    }
                    catch (Exception)
                    { }
                }

                return worldLocations;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("GEM: Unable to read world cities information from {0}.", 
                        Properties.Settings.Default.WorldCitiesDataFile) + Environment.NewLine + ex.Message);
            }
        }
    }
}
