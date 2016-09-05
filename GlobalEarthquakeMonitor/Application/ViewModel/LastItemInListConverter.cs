using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Globalization;

namespace Plethora.GlobalEarthquakeMonitor.ViewModel
{
    /// <summary>
    /// List Item Value converter to create a proper comma delimited
    /// list of city names which is displayed in the view.
    /// </summary>
    class LastItemInListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (targetType != typeof(string))
                    throw new InvalidOperationException("The conversion target must be a string.");

                // Create and return comma delimited string names from the list source.
                return String.Join(", ", ((List<string>)value).ToArray());
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
