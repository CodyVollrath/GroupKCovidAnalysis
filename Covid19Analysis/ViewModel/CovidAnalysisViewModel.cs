using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Covid19Analysis.Annotations;
using Covid19Analysis.Model;
using Covid19Analysis.Utility;

namespace Covid19Analysis.ViewModel
{
    public class CovidAnalysisViewModel : INotifyPropertyChanged
    {
        #region Data members

        private ObservableCollection<CovidRecord> covidData;

        private CovidRecord selectedCovidRecord;

        #endregion

        #region Properties

        public RelayCommand RemoveCommand { get; set; }

        public ObservableCollection<CovidRecord> CovidData
        {
            get => this.covidData;
            set
            {
                this.covidData = value;
                this.OnPropertyChanged();
            }
        }

        public CovidRecord SelectedCovidRecord
        {
            get => this.selectedCovidRecord;
            set
            {
                this.selectedCovidRecord = value;
                this.OnPropertyChanged();
                this.RemoveCommand.OnCanExecuteChanged();
            }
        }

        #endregion

        #region Constructors

        public CovidAnalysisViewModel()
        {
            this.loadCommands();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        private void loadCommands()
        {
            this.RemoveCommand = new RelayCommand(this.deleteCovidRecord, this.canDeleteCovidRecord);
        }

        private void deleteCovidRecord(object obj)
        {
            this.CovidData.Remove(this.selectedCovidRecord);
        }

        private bool canDeleteCovidRecord(object obj)
        {
            return this.SelectedCovidRecord != null;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}