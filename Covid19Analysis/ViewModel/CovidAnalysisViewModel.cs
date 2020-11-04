using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Covid19Analysis.Annotations;
using Covid19Analysis.DataTier;
using Covid19Analysis.Extension;
using Covid19Analysis.Model;

namespace Covid19Analysis.ViewModel
{
    public class CovidAnalysisViewModel : INotifyPropertyChanged
    {
        private CovidDataCollection covidDataCollection;

        private ObservableCollection<CovidRecord> covidData;

        public ObservableCollection<CovidRecord> CovidData
        {
            get => this.covidData;
            set
            {
                value = value ?? throw new ArgumentNullException(nameof(value));
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
                value = value ?? throw new ArgumentNullException(nameof(value));
                this.selectedCovidRecord = value;
                this.OnPropertyChanged();
            }
        }

        public CovidAnalysisViewModel()
        {
            this.covidDataCollection = new CovidDataCollection();
        }

        public void LoadCovidCsvListData(string textContent)
        {
            var covidCsvParser = new CovidCsvParser(textContent);
            this.covidDataCollection = covidCsvParser.GenerateCovidDataCollection();
            this.CovidData = this.covidDataCollection.ToObservableCollection();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
