using System;
using Windows.Storage;
using Covid19Analysis.Model;
using Covid19Analysis.Resources;

namespace Covid19Analysis.DataTier
{

    /// <summary>This class writes the contents of a CovidDataCollection to a csv file</summary>
    public class CovidDataSaver
    {
        #region Properties

        /// <summary>Gets the file being saved.</summary>
        /// <value>The file that is being saved.</value>
        public StorageFile File { get; }

        /// <summary>Gets the covid data that is being saved to the file.</summary>
        /// <value>The covid data being saved to a file.</value>
        public CovidDataCollection CovidData { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CovidDataSaver" /> class.
        /// <code>Precondition: file != null AND covidData != null</code>
        /// <code>Postcondition: File == file AND CovidData = covidData</code>
        /// </summary>
        /// <param name="file">The storage file.</param>
        /// <param name="covidData">The covid data collection.</param>
        /// <exception cref="ArgumentNullException">file
        /// or
        /// covidData</exception>
        public CovidDataSaver(StorageFile file, CovidDataCollection covidData)
        {
            this.File = file ?? throw new ArgumentNullException(nameof(file));
            this.CovidData = covidData ?? throw new ArgumentNullException(nameof(covidData));
        }

        #endregion

        #region Public Methods
        /// <summary>Writes the covid data to the storage file.</summary>
        public async void WriteCovidDataToFile()
        {
            var contents = string.Empty;
            contents += this.getCovidDataHeaders();
            contents += this.getCovidDataRecords();
            await FileIO.WriteTextAsync(this.File, contents);
        }
        #endregion

        #region Private Helpers
        private string getCovidDataHeaders()
        {
            var headers = string.Empty;
            foreach (var header in Assets.HeadersForData)
            {
                headers += $"{header},";
            }
            headers = headers.Remove(headers.Length - 1, 1) + Environment.NewLine;
            return headers;
        }

        private string getCovidDataRecords()
        {
            var covidDataRecords = string.Empty;
            foreach (var record in this.CovidData)
            {
                var date = record.Date.ToString(Assets.DateStringUnformatted);
                covidDataRecords += $"{date},{record.State},{record.PositiveTests},{record.NegativeTests},{record.HospitalizedCurrently},{record.Hospitalizations},{record.Deaths}{Environment.NewLine}";
            }
            return covidDataRecords;
        }
        #endregion
    }
}
