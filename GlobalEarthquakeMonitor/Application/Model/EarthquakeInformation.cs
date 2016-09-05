using System;
using System.Windows.Threading;
using System.Collections.Generic;
using Plethora.GlobalEarthquakeMonitor.Model.DataAccess;

namespace Plethora.GlobalEarthquakeMonitor.Model
{
    /// <summary>
    /// EarthquakeInformation is the main Model class in the MVVM pattern.
    /// The view model holds a reference to this object and calls methods
    /// and properties on the IEarthquakeInformation to request for
    /// earthquake information.
    /// </summary>
    public class EarthquakeInformation : IEarthquakeInformation 
    {
        private Action earthquakeInformationUpdateAction;

        public IEnumerable<IEarthquakeInstance> EarthquakeInstances { get; private set; }

        public DateTime LatestSyncTime { get; private set; }

        public EarthquakeInformation()
        {
            GetLatestEarthquakeInformation();
        }

        public void Monitor(Action earthquakeInformationUpdateAction)
        {
            try
            {
                // Initialize the action delegate that should be called to inform
                // the subscriber.
                this.earthquakeInformationUpdateAction = earthquakeInformationUpdateAction;

                // Backgroundworker thread cannot update ObservableCollection.
                // Instead using a dispatch timer that runs on a separate thread.
                // The call back function is specified, this is called when the timer tick ends.
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(Properties.Settings.Default.EarthquakeInformationUpdateFrequency);
                timer.Tick += UpdateEarthquakeInformation;
                timer.Start();
            }
            catch (Exception ex)
            {
                throw new Exception("GEM: Unable to initiate monitoring for realtime earthquake information."
                    + Environment.NewLine + ex.Message);
            }
        }

        void UpdateEarthquakeInformation(object sender, EventArgs e)
        {
            try
            {
                // Get latest earthquake information.
                GetLatestEarthquakeInformation();

                // Call action to inform subscriber that updated earthquake information
                // is available.
                earthquakeInformationUpdateAction();

            }
            catch (Exception ex)
            {
                throw new Exception("GEM: Unable to obtain realtime earthquake information."
                    + Environment.NewLine + ex.Message);
            }
        }

        void GetLatestEarthquakeInformation()
        {
            try
            {
                // Request earthquake data from the feed reader.
                EarthquakeInstances = EarthquakeDataFeedReader.GetEarthquakeInstances();

                // Compare the current time with the latest time at which USGS updated their feed data.
                // Return the later time. Sometimes USGS feed update frequency is lesser than our request
                // frequency.
                DateTime currentTime = DateTime.Now.ToUniversalTime();

                LatestSyncTime = DateTime.Compare(EarthquakeDataFeedReader.LatestSyncTime, currentTime) > 0 ?
                    EarthquakeDataFeedReader.LatestSyncTime : currentTime;

            }
            catch (Exception ex)
            {
                throw new Exception("GEM: Unable to obtain latest earthquake information."
                    + Environment.NewLine + ex.Message);
            }
        }
    }
}
