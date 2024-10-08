using System;
using System.ComponentModel;
using System.Windows;

namespace PrestaconnectWebService.Contexts
{
    public abstract class Context : INotifyPropertyChanged
    {
        #region Properties

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("NotBusy");
            }
        }

        public bool NotBusy
        {
            get
            {
                return !isBusy;
            }
        }

        private string loadingStep;
        public string LoadingStep
        {
            get { return loadingStep; }
            set
            {
                loadingStep = value;
                OnPropertyChanged("LoadingStep");
            }
        }

        private string loadingPercentage;
        public string LoadingPercentage
        {
            get { return loadingPercentage; }
            set
            {
                loadingPercentage = value;
                OnPropertyChanged("LoadingPercentage");
            }
        }

        public Visibility ExtranetOnly
        {
            get
            {
                return /*(Core.UpdateVersion.License.ExtranetOnly) ? Visibility.Collapsed : */ Visibility.Visible;
            }
        }

        #endregion
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal event EventHandler Loaded;
        protected virtual void OnLoaded()
        {
            Loaded?.Invoke(this, new EventArgs());
        }

        #endregion
        #region Methods

        public void Load()
        {
            OnLoaded();
        }

        #endregion
    }
}
