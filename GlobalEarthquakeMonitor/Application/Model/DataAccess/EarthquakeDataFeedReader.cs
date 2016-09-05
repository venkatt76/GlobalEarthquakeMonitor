using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using Plethora.GlobalEarthquakeMonitor.Model.Utilities;

namespace Plethora.GlobalEarthquakeMonitor.Model.DataAccess
{
    /// <summary>
    /// EarthquakeDataFeedReader class handles the USGS data feed request and parsing
    /// The data is stored in model classes that provide easy access to earthquake information.
    /// The update datetime returned in the GeoJSON is also saved to indicate when the information
    /// was generated.
    /// </summary>
    public static class EarthquakeDataFeedReader
    {
        public static DateTime LatestSyncTime { get; private set; }

        /// <summary>
        /// Earthquake data get instances method that returns all earthquake instances within the last time period.
        /// The time period is implicitly defined in the URI which can be updated in the applicaiton configuration file.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IEarthquakeInstance> GetEarthquakeInstances()
        {
            try
            {
                // Newtonsoft JObject to parse Json data returned by USGS earthquake API.    
                JObject geoJsonObject;

                // GEOJson FeatureCollection object stores feature collection, commented since using Newtonsoft libraries.
                //FeatureCollection featureCollection;

                // Using a WebClient without incurring WCF overhead, but using WCF is preferred for most cases.
                using (WebClient webclient = new WebClient())
                {
                    // Web API call to obtain GEO Json data about earthquakes.
                    // The URI for the web service request is defined in the application configuration file, hence
                    // it can be updated without affecting application code.
                    var geoJsonText = webclient.DownloadString(new Uri(Properties.Settings.Default.EarthquakeInformationFeedURI));

                    geoJsonObject = JObject.Parse(geoJsonText);

                    // GEOJson API to deserialize GEOJson text, commented out since using Newtonsoft libraries.
                    //featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(geoJsonText, 
                    //    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                    return GetEarthquakeInstancesFromGeoJsonObject(geoJsonObject);

                }
            }
            catch(Exception ex)
            {
                throw new Exception("GEM: Unable to obtain earthquake data from USGS feed." 
                    + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Earthquake data get instances method that returns all earthquake instances between specified
        /// Start time and end time.
        /// The time period is passed in as parameters to this method.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IEarthquakeInstance> GetEarthquakeInstances(DateTime fromTime, DateTime endTime)
        {
            try
            { 
                // Newtonsoft JObject to parse Json data returned by USGS earthquake API.    
                JObject geoJsonObject;

                // GEOJson FeatureCollection object stores feature collection, commented since using Newtonsoft libraries.
                //FeatureCollection featureCollection;

                // Using a WebClient without incurring WCF overhead, but this is preferred for most cases.
                using (WebClient webclient = new WebClient())
                {
                    // Web API call to obtain GEO Json data about earthquakes.
                    var geoJsonText = webclient.DownloadString(
                        new Uri(String.Format("{0}&starttime={1}&endtime={2}",
                            Properties.Settings.Default.EarthquakeInformationFDSNWSFeedURI, 
                                fromTime.ToString("yyyy-MM-ddTHH:mm:ssZ"), endTime.ToString("yyyy-MM-ddTHH:mm:ssZ"))));

                    geoJsonObject = JObject.Parse(geoJsonText);

                    // GEOJson API to deserialize GEOJson text, commented out since using Newtonsoft libraries.
                    //featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(geoJsonText, 
                    //    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                    return GetEarthquakeInstancesFromGeoJsonObject(geoJsonObject);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GEM: Unable to obtain earthquake data from USGS feed."
                    + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Helper function to parse returned GEOJson data
        /// </summary>
        /// <param name="geoJsonObject"></param>
        /// <returns></returns>
        private static IEnumerable<IEarthquakeInstance> GetEarthquakeInstancesFromGeoJsonObject(JObject geoJsonObject)
        {
            try
            {
                // Obtain the time at which the earthquake data was generated.
                LatestSyncTime = DateTimeConverter.ConvertToDateTime(geoJsonObject.SelectToken("metadata.generated").ToString());

                var earthquakeInstances = new List<IEarthquakeInstance>();

                // Construct a collection of EarthquakeInstance objects from raw data.
                foreach (var feature in geoJsonObject.SelectToken("features"))
                {
                    earthquakeInstances.Add(new EarthquakeInstance(
                       DateTimeConverter.ConvertToDateTime(feature.SelectToken("properties.time").ToString()),
                       Convert.ToDouble(feature.SelectToken("properties.mag")),
                       new LocationCoordinates(Convert.ToDouble(feature.SelectToken("geometry.coordinates[1]")),
                           Convert.ToDouble(feature.SelectToken("geometry.coordinates[0]")))));
                }

                return earthquakeInstances;
            }
            catch(Exception ex)
            {
                throw new Exception("GEM: Unable to parse earthquake data from USGS feed."
                    + Environment.NewLine + ex.Message);
            }
        }
    }
}
