using System;
using System.Collections.ObjectModel;
using System.Linq;
using Plethora.GlobalEarthquakeMonitor.Model;

namespace Plethora.GlobalEarthquakeMonitor.ViewModel
{
    /// <summary>
    /// EarthquakeMonitorViewModel class represents the view model in the MVVM pattern.
    /// The model object is accessible via the EarthquakeInformation property.
    /// Earthquake Instances is an observable collection that is bound to the view. Since this is an
    /// observable collection, updates to this collection will automatically update the view.
    /// </summary>
    public class EarthquakeMonitorViewModel : BaseNotifyPropertyChanged
    {
        public IEarthquakeInformation EarthquakeInformation { get; private set; }

        public ObservableCollection<IEarthquakeInstance> EarthquakeInstances { get; private set; }

        private DateTime earthquakeInstancesSyncTime;
        public DateTime EarthquakeInstancesSyncTime
        {
            get { return earthquakeInstancesSyncTime; }
            private set
            {
                earthquakeInstancesSyncTime = value;
                RaisePropertyChangedEvent("EarthquakeInstancesSyncTime");
            }
        }

        private string earthquakeInformationTitle;
        public string EarthquakeInformationTitle
        {
            get { return earthquakeInformationTitle; }
            private set
            {
                earthquakeInformationTitle = value;
                RaisePropertyChangedEvent("EarthquakeInformationTitle");
            }
        }

        /// <summary>
        /// ViewModel constructor initializes the model to obtain earthquake information.
        /// This also requests the model to monitor for realtime earthquake information. The model does
        /// a call back periodically when the information is obtained so that the view can update.
        /// </summary>
        public EarthquakeMonitorViewModel()
        {
            try
            {
                EarthquakeInformationTitle = Properties.Settings.Default.EarthquakeInformationTitle;

                // Instantiate the model class.
                EarthquakeInformation = new EarthquakeInformation();

                if (EarthquakeInformation != null)
                {
                    // Obtain the information about earthquakes in the last time span (e.g 1 hour).
                    // Initialize the observable collection which notifies the view.
                    EarthquakeInstances = new ObservableCollection<IEarthquakeInstance>(EarthquakeInformation.EarthquakeInstances);

                    // Get the time at which the earthquake information was requested.
                    EarthquakeInstancesSyncTime = EarthquakeInformation.LatestSyncTime;
                }
                else
                    EarthquakeInstances = new ObservableCollection<IEarthquakeInstance>();
            }
            catch (Exception)
            {
                EarthquakeInformationTitle = "Unable to obtain earthquake information.";
            }

            try
            {
                // Request model to periodically monitor for updated earthquake information.
                // The method call back is passed in as the parameter.
                if (Properties.Settings.Default.EarthquakeInformationContinousMonitoring)
                    EarthquakeInformation.Monitor(UpdateEarthquakeInformation);
            }
            catch (Exception)
            {
                EarthquakeInformationTitle = "Unable to obtain realtime earthquake information.";
            }
        }
             
        /// <summary>
        /// Call back from the model notifying the availability of updated earthquake information.
        /// The updated information can be requested from the model and used to the update the
        /// observable collection which automatically updates the view.
        /// </summary>                   
        private void UpdateEarthquakeInformation()
        {
            try
            {
                // Update the observable collection with the latest earthquake information.
                // Clear the current collection and popuplate with latest data.

                if (EarthquakeInstances.Count() > 0)
                    EarthquakeInstances.Clear();

                var updatedEarthquakeInstances = EarthquakeInformation.EarthquakeInstances;
                foreach (var updatedEarthquakeInstance in updatedEarthquakeInstances)
                    EarthquakeInstances.Add(updatedEarthquakeInstance);

                EarthquakeInstancesSyncTime = EarthquakeInformation.LatestSyncTime;
            }
            catch (Exception)
            {
                EarthquakeInformationTitle = "Unable to obtain realtime earthquake information.";
            }
        }
    }
}
