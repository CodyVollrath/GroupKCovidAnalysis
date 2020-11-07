using System;
using Windows.Storage;
using Covid19Analysis.Model;
using Covid19Analysis.Resources;

namespace Covid19Analysis.DataTier
{
    /// <summary>
    ///     Authors: Cody Vollrath, Eboni Walker
    ///     This class writes the contents of a CovidDataCollection to a csv file
    /// </summary>
    /// <seealso cref="Covid19Analysis.DataTier.CovidDataSaver" />
    public class CsvCovidDataSaver : CovidDataSaver
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CsvCovidDataSaver" /> class.
        ///     <code>Precondition: file != null AND covidData != null</code>
        ///     <code>Postcondition: File == file AND CovidData = covidData</code>
        /// </summary>
        /// <param name="file">The storage file.</param>
        /// <param name="covidData">The covid data collection.</param>
        /// <exception cref="ArgumentNullException">
        ///     file
        ///     or
        ///     covidData
        /// </exception>
        public CsvCovidDataSaver(StorageFile file, CovidDataCollection covidData) : base(file, covidData)
        {
        }

        #endregion

        #region Methods

        /// <summary>Writes the covid data to the storage file as CSV.</summary>
        public override async void WriteCovidDataToFile()
        {
            var contents = string.Empty;
            contents += getCovidDataHeaders();
            contents += this.getCovidDataRecords();
            await FileIO.WriteTextAsync(File, contents);
        }

        #endregion

        #region Private Helpers

        private static string getCovidDataHeaders()
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
            foreach (var record in CovidData)
            {
                var date = record.Date.ToString(Assets.DateStringUnformatted);
                covidDataRecords +=
                    $"{date},{record.State},{record.PositiveTests},{record.NegativeTests},{record.HospitalizedCurrently},{record.Hospitalizations},{record.Deaths}{Environment.NewLine}";
            }

            return covidDataRecords;
        }

        #endregion
    }
}