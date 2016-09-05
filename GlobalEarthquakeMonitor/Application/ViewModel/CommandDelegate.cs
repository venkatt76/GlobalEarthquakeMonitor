using System;

namespace Plethora.GlobalEarthquakeMonitor.ViewModel
{
    /// <summary>
    /// CommandDelegate class supports command delegation in the MVVM pattern.
    /// This class enables delegation of commands called the view to be routed
    /// to corresponding handlers.
    /// </summary>
    class CommandDelegate
    {
        private readonly Action action;

        /// <summary>
        /// Constructor to initialize command call back method.
        /// </summary>
        /// <param name="action"></param>
        public CommandDelegate(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// Execute command - call specified delegate.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
           action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

    }
}
