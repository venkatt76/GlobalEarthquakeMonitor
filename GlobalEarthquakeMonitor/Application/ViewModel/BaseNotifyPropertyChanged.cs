using System.ComponentModel;

namespace Plethora.GlobalEarthquakeMonitor.ViewModel
{
    /// <summary>
    /// Base class that implements INotifyPropertyChanged which can be inherited
    /// in view model (and model) classes.
    /// </summary>
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed event which accepts the updated property name as the input,
        /// for which the change notification is being fired.
        /// </summary>
        /// <param name="propertyName"></param>
        public void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
