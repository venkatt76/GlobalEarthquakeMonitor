using System;

namespace Plethora.GlobalEarthquakeMonitor.Model.Utilities
{
    /// <summary>
    /// DateTimeConverter class is a utility class that convertes time in milliseconds after epoch
    /// to UTC. The time in milliseconds is returned in the GEOJson data.
    /// </summary>
    static class DateTimeConverter
    {
        public static DateTime ConvertToDateTime(string timeInMilliseconds)
        {
            try
            {
                DateTime beginDate = Convert.ToDateTime("01/01/1970");

                double time = Convert.ToDouble(timeInMilliseconds);
                beginDate += TimeSpan.FromMilliseconds(time);

                return beginDate;
            }
            catch (Exception ex)
            {
                throw new Exception("GEM: Unable to convert time in milliseconds after epoch." 
                    + Environment.NewLine + ex.Message);
            }
        }
    }
}
