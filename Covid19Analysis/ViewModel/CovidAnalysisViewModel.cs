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

        public RelayCommand RemoveCommand { get; set; }

        private ObservableCollection<CovidRecord> covidData;

        public ObservableCollection<CovidRecord> CovidData
        {
            get => this.covidData;
            set
            {
                this.covidData = value;
                this.OnPropertyChanged();
            }
        }

        private CovidRecord selectedCovidRecord;

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

        public CovidAnalysisViewModel()
        {
            this.loadCommands();
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
