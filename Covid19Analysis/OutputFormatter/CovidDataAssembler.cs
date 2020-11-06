using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Covid19Analysis.DataTier;
using Covid19Analysis.Model;
using Covid19Analysis.Resources;
using Covid19Analysis.ViewModel;

namespace Covid19Analysis.OutputFormatter
{
    /// <summary>This class assembles COVID data output from all summaries and accumulates them together.</summary>
    public class CovidDataAssembler
    {
        #region Properties

        /// <summary>Gets a value indicating whether this instance is covid data loaded.</summary>
        /// <value>
        ///     <c>true</c> if this instance is covid data loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsCovidDataLoaded { get; private set; }

        /// <summary>Gets or sets the state filter.</summary>
        /// <value>The state filter.</value>
        public string StateFilter { get; set; }

        /// <summary>Gets or sets the upper positive threshold.</summary>
        /// <value>The upper positive threshold.</value>
        public string UpperPositiveThreshold { get; set; }

        /// <summary>Gets or sets the lower positive threshold.</summary>
        /// <value>The lower positive threshold.</value>
        public string LowerPositiveThreshold { get; set; }

        /// <summary>Gets or sets the size of the bin for the histogram.</summary>
        /// <value>The size of the bin.</value>
        public string BinSize { get; set; }

        /// <summary>Gets the covid data summary.</summary>
        /// <value>The summary.</value>
        public string Summary { get; private set; }

        /// <summary>
        ///     Gets all covid data.
        /// </summary>
        /// <value>
        ///     All covid data.
        /// </value>
        public CovidDataCollection AllCovidData { get; private set; }

        /// <summary>Gets the filtered covid data collection.</summary>
        /// <value>The filtered covid data collection.</value>
        public CovidDataCollection FilteredCovidDataCollection { get; private set; }

        #endregion

        #region Private Members

        private CovidDataErrorLogger covidErrorLogger;

        private CovidDataMergeController mergeController;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="CovidDataAssembler" originaltag="see">CovidDataAssembler</a> class.
        ///     <para>By default the filter is set to GA</para>
        ///     <para>If stateFilter is empty then all states represented in the table will be represented</para>
        ///     <code>Postcondition: StateFilter == stateFilter AND Summary == null AND IsCovidDataLoaded == false</code>
        /// </summary>
        /// <param name="stateFilter">The state filter.</param>
        public CovidDataAssembler(StateAbbreviations stateFilter)
        {
            this.StateFilter = Enum.GetName(typeof(StateAbbreviations), stateFilter);
            this.Reset();
        }

        #endregion

        #region Methods

        /// <summary>Resets this instance to the start value.</summary>
        public void Reset()
        {
            this.Summary = string.Empty;
            this.IsCovidDataLoaded = false;
            this.covidErrorLogger = null;
            this.FilteredCovidDataCollection = null;
            this.AllCovidData = new CovidDataCollection();
            this.mergeController = null;
        }

        /// <Summary>Gets the covid data errors.</Summary>
        /// <returns>
        ///     <para>the string that represents the covid data errors</para>
        /// </returns>
        public string GetCovidDataErrors()
        {
            return this.covidErrorLogger == null ? string.Empty : this.covidErrorLogger.ErrorString;
        }

        /// <summary>Loads the covid data from text.</summary>
        /// <param name="textContent">Content of the text.</param>
        /// <exception cref="ArgumentNullException">textContent</exception>
        public void LoadCovidData(string textContent)
        {
            textContent = textContent ?? throw new ArgumentNullException(nameof(textContent));
            var parser = new CovidCsvParser(textContent);
            this.covidErrorLogger = parser.CovidErrorLogger;
            this.FilteredCovidDataCollection = parser.GenerateCovidDataCollection();
            this.AllCovidData = this.FilteredCovidDataCollection.Clone();
            this.IsCovidDataLoaded = true;
            this.buildCovidSummary();
        }






        /// <summary>Loads the covid data from xml.</summary>
        /// <param name="xmlContent">Content of the xml.</param>
        /// <exception cref="ArgumentNullException">xmlContent</exception>
        public void LoadCovidData(CovidDataCollection xmlContent)
        {
            xmlContent = xmlContent ?? throw new ArgumentNullException(nameof(xmlContent));

            this.FilteredCovidDataCollection = xmlContent;
            this.AllCovidData = this.FilteredCovidDataCollection.Clone();
            this.IsCovidDataLoaded = true;
            this.buildCovidSummary();
        }

        /// <summary>Updates the collection from view model.</summary>
        /// <param name="viewModel">The view model.</param>
        /// <exception cref="System.ArgumentNullException">viewModel</exception>
        public void UpdateCollectionFromViewModel(CovidAnalysisViewModel viewModel)
        {
            viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            if (viewModel.CovidDataRecords == null)
            {
                this.setFilteredCovidDataAndNoDataMessage();
                return;
            }

            if (!viewModel.CovidDataRecords.Any())
            {
                this.AllCovidData.RemoveAllCovidRecordsInAList(this.FilteredCovidDataCollection);
                if (!this.AllCovidData.Any())
                {
                    this.Reset();
                    return;
                }

                this.setFilteredCovidDataAndNoDataMessage();
                return;
            }

            this.FilteredCovidDataCollection.ReplaceAllWithNewCovidCollection(viewModel.CovidDataRecords.ToList());
            this.buildCovidSummary();
        }


        /// <summary>
        ///     Merges the and loads the covid data using csv content.
        /// </summary>
        /// <param name="textContent">Content of the text.</param>
        /// <param name="mergeAllStates">if set to <c>true</c> [merge all states].</param>
        /// <exception cref="ArgumentNullException">textContent</exception>
        public void MergeAndLoadCovidData(string textContent, bool mergeAllStates)
        {
            textContent = textContent ?? throw new ArgumentNullException(nameof(textContent));
            var parser = new CovidCsvParser(textContent);
            var newCovidDataCollection = parser.GenerateCovidDataCollection();
            this.covidErrorLogger = parser.CovidErrorLogger;
            if (mergeAllStates)
            {
                this.FilteredCovidDataCollection = this.AllCovidData.Clone();
            }

            this.mergeController = new CovidDataMergeController(this.FilteredCovidDataCollection,
                newCovidDataCollection);
            this.mergeController.AddAllNonDuplicates();
            this.mergeAndRebuildAllCovidData();
        }

        /// <summary>
        ///     Merges the and loads the covid data using xml content.
        /// </summary>
        /// <param name="xmlContent">Content of the XML.</param>
        /// <param name="mergeAllStates">if set to <c>true</c> [merge all states].</param>
        /// <exception cref="ArgumentNullException">xmlContent</exception>
        public void MergeAndLoadCovidData(CovidDataCollection xmlContent, bool mergeAllStates)
        {
            xmlContent = xmlContent ?? throw new ArgumentNullException(nameof(xmlContent));
            var newCovidDataCollection = xmlContent;

            if (mergeAllStates)
            {
                this.FilteredCovidDataCollection = this.AllCovidData.Clone();
            }

            this.mergeController = new CovidDataMergeController(this.FilteredCovidDataCollection,
                newCovidDataCollection);
            this.mergeController.AddAllNonDuplicates();
            this.mergeAndRebuildAllCovidData();
        }

        /// <summary>Replaces the Covid covidRecord with the covidRecord passed in to it.</summary>
        /// <param name="covidRecord">The covidRecord.</param>
        public void ReplaceRecord(CovidRecord covidRecord)
        {
            if (this.mergeController == null)
            {
                this.FilteredCovidDataCollection.ReplaceDuplicateRecords(covidRecord);
                this.AllCovidData.ReplaceDuplicateRecords(covidRecord);
                this.buildCovidSummary();
                return;
            }

            this.mergeController.ReplaceDuplicate(covidRecord);
            this.FilteredCovidDataCollection = this.mergeController.MergedCovidDataCollection;
            this.IsCovidDataLoaded = true;
            this.mergeAndRebuildAllCovidData();
        }

        /// <summary>Does the covid record exist.</summary>
        /// <param name="record">The record.</param>
        /// <returns>True if record is present in the CovidDataCollection, false otherwise.</returns>
        public bool DoesCovidRecordExist(CovidRecord record)
        {
            var doesCovidRecordExist = false;
            if (this.IsCovidDataLoaded)
            {
                doesCovidRecordExist = this.AllCovidData.Contains(record);
            }

            return doesCovidRecordExist;
        }

        /// <summary>Adds the covid record to collection or creates a new collection if there is not a collection present.</summary>
        /// <param name="record">The record.</param>
        public void AddCovidRecordToCollection(CovidRecord record)
        {
            if (this.IsCovidDataLoaded)
            {
                this.addCovidRecordToExistingCollection(record);
            }
            else
            {
                this.createAndAddToTheCovidDataCollection(record);
            }

            if (this.FilteredCovidDataCollection != null)
            {
                this.buildCovidSummary();
            }
        }

        /// <summary>Gets the duplicates from merged data.</summary>
        /// <returns>The duplicates from the merged data</returns>
        public IEnumerable<CovidRecord> GetDuplicatesFromMergedData()
        {
            var duplicates = this.mergeController.GetDuplicates();
            return duplicates;
        }

        /// <summary>Writes the covid data to a csv or txt file.</summary>
        /// <param name="file">The file.</param>
        /// <returns>True if the file was saved properly, otherwise false</returns>
        public bool WriteCovidDataToCsvFile(StorageFile file)
        {
            var isSaved = true;
            try
            {
                CovidDataSaver covidDataWriter = new CsvCovidDataSaver(file, this.AllCovidData);
                covidDataWriter.WriteCovidDataToFile();
            }
            catch (Exception)
            {
                isSaved = false;
            }

            return isSaved;
        }

        /// <summary>Writes the covid data to a xml file.</summary>
        /// <param name="file">The file.</param>
        /// <returns>True if the file was saved properly, otherwise false</returns>
        public bool WriteCovidDataToXmlFile(StorageFile file)
        {
            var isSaved = true;
            try
            {
                CovidDataSaver covidDataWriter = new XmlCovidDataSaver(file, this.AllCovidData);
                covidDataWriter.WriteCovidDataToFile();
            }
            catch (Exception)
            {
                isSaved = false;
            }

            return isSaved;
        }

        /// <summary>Updates the covid summary with new data that is loaded.</summary>
        public void UpdateSummary()
        {
            this.buildCovidSummary();
        }

        #endregion

        #region Private Methods
        private void buildCovidSummary()
        {
            try
            {
                const string genericHeader = Assets.StateCovidDataHeadingLabel;
                this.FilteredCovidDataCollection = this.AllCovidData.Clone();
                var stateSummary = new CovidDataSummary(this.FilteredCovidDataCollection, this.StateFilter);
                var isStateNotNull = this.StateFilter != null;
                var stateSpecificHeader = $"{this.StateFilter} {Assets.StateCovidDataHeadingLabel}";

                this.Summary = string.Empty;
                this.Summary += isStateNotNull ? stateSpecificHeader : genericHeader;

                this.Summary += stateSummary.GetFirstDayOfPositiveTest();
                this.Summary += stateSummary.GetHighestPositiveWithDate();
                this.Summary += stateSummary.GetHighestNegativeWithDate();

                this.Summary += stateSummary.GetHighestTotalTestsWithDate();
                this.Summary += stateSummary.GetHighestDeathsWithDate();
                this.Summary += stateSummary.GetHighestHospitalizationsWithDate();
                this.Summary += stateSummary.GetHighestCurrentHospitalizationsWithDate();

                this.Summary += stateSummary.GetHighestPercentageOfTestsPerDayWithDate();
                this.Summary += stateSummary.GetAveragePositiveTestsSinceFirstPositiveTest();
                this.Summary += stateSummary.GetOverallPositivityRateSinceFirstPositiveTest();

                this.Summary += this.getPositiveThresholds(stateSummary);
                this.Summary += this.getHistogram(stateSummary);
                this.Summary += stateSummary.GetMonthlySummary();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.Reset();
            }
        }

        private string getPositiveThresholds(CovidDataSummary stateSummary)
        {
            var upperPositiveCaseThreshold = Format.FormatStringToInteger(this.UpperPositiveThreshold);
            var lowerPositiveCaseThreshold = Format.FormatStringToInteger(this.LowerPositiveThreshold);
            var summary = string.Empty;
            summary += stateSummary.GetTheDaysFromTheFirstPositiveTestGreaterThanThreshold(upperPositiveCaseThreshold);
            summary += stateSummary.GetTheDaysFromTheFirstPositiveTestLessThanThreshold(lowerPositiveCaseThreshold);
            return summary;
        }

        private string getHistogram(CovidDataSummary stateSummary)
        {
            var binSize = Format.FormatStringToInteger(this.BinSize);
            var summary = string.Empty;
            summary += stateSummary.GetTheFrequencyTableHistogramOfPositiveTests(binSize);
            return summary;
        }

        private void mergeAndRebuildAllCovidData()
        {
            this.AllCovidData.AddAll(this.mergeController.MergedCovidDataCollection);
            this.buildCovidSummary();
        }

        private void addCovidRecordToExistingCollection(CovidRecord record)
        {
            if (record.State.Equals(this.StateFilter))
            {
                this.FilteredCovidDataCollection.Add(record);
                this.AllCovidData.Add(record);
            }
            else
            {
                this.AllCovidData.Add(record);
            }
        }

        private void createAndAddToTheCovidDataCollection(CovidRecord record)
        {
            if (record.State.Equals(this.StateFilter))
            {
                this.FilteredCovidDataCollection = new CovidDataCollection {record};
                this.AllCovidData = this.FilteredCovidDataCollection.Clone();
                this.IsCovidDataLoaded = true;
            }
            else
            {
                this.AllCovidData = new CovidDataCollection {record};
            }
        }

        private void setFilteredCovidDataAndNoDataMessage()
        {
            this.FilteredCovidDataCollection = null;
            this.Summary = Assets.NoCovidDataText;
        }

        #endregion
    }
}