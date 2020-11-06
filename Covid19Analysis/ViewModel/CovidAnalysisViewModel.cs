using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Covid19Analysis.Annotations;
using Covid19Analysis.Model;
using Covid19Analysis.Utility;

namespace Covid19Analysis.ViewModel
{
    /// <summary>
    ///     <para>The view model class for the covid list view</para>
    /// </summary>
    public class CovidAnalysisViewModel : INotifyPropertyChanged
    {
        #region Data members

        private ObservableCollection<CovidRecord> covidDataRecordsRecords;

        private CovidRecord selectedCovidRecord;

        #endregion

        #region Properties

        /// <summary>Gets the remove command.</summary>
        /// <value>The remove command.</value>
        public RelayCommand RemoveCommand { get; private set; }

        /// <summary>Gets or sets the covid data records saved in the application.</summary>
        /// <value>The covid data records.</value>
        public ObservableCollection<CovidRecord> CovidDataRecords
        {
            get => this.covidDataRecordsRecords;
            set
            {
                this.covidDataRecordsRecords = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the selected covid record.</summary>
        /// <value>The selected covid record.</value>
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

        /// <summary>Initializes a new instance of the <see cref="CovidAnalysisViewModel" /> class.</summary>
        public CovidAnalysisViewModel()
        {
            this.loadCommands();
        }

        #endregion

        #region Methods

        /// <summary>Occurs when a property value changes.</summary>
        /// <returns>The PropertyChangedEventHandler</returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Called when [property changed].</summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Private Helpers

        private void loadCommands()
        {
            this.RemoveCommand = new RelayCommand(this.deleteCovidRecord, this.canDeleteCovidRecord);
        }

        private void deleteCovidRecord(object obj)
        {
            this.CovidDataRecords.Remove(this.selectedCovidRecord);
        }

        private bool canDeleteCovidRecord(object obj)
        {
            return this.SelectedCovidRecord != null;
        }

        #endregion
    }
}