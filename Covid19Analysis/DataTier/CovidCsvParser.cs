using System;
using System.Collections.Generic;
using System.Linq;
using Covid19Analysis.Model;
using Covid19Analysis.Resources;

namespace Covid19Analysis.DataTier
{
    /// <Summary>
    /// Author: Cody Vollrath
    /// This class is responsible for taking string data and formatting the values to be representative of a table in
    /// memory.
    /// <seealso cref="Covid19Analysis.DataTier.CovidDataSaver" />
    /// </Summary>
    public class CovidCsvParser
    {
        #region Private Fields

        private readonly CovidDataCollection covidDataCollection;

        #endregion

        #region Properties

        /// <Summary>Gets the content of the text.</Summary>
        /// <value>The content of the current text to be processed.</value>
        public string TextContent { get; }

        /// <Summary>Gets the covid error logger which contains the errors that may exist in the data set that is loaded.</Summary>
        /// <value>The covid error logger.</value>
        public CovidDataErrorLogger CovidErrorLogger { get; }

        #endregion

        #region Constructors

        /// <Summary>
        ///     Initializes a new instance of the <see cref="CovidCsvParser" /> class.
        ///     <code>Precondition: textContent != null</code>
        ///     <code>Postcondition: TextContent == textContent</code>
        /// </Summary>
        /// <param name="textContent">Content of the text.</param>
        /// <exception cref="ArgumentNullException">textContent</exception>
        public CovidCsvParser(string textContent)
        {
            this.TextContent = textContent ?? throw new ArgumentNullException(nameof(textContent));
            this.covidDataCollection = new CovidDataCollection();
            this.CovidErrorLogger = new CovidDataErrorLogger();
        }

        #endregion

        #region Methods

        /// <summary>Generates the covid data collection.</summary>
        /// <returns>The CovidDataCollection</returns>
        public CovidDataCollection GenerateCovidDataCollection()
        {
            var records = this.breakDownTextContent();
            var lineNumber = Assets.BeginningRowNumber;

            foreach (var record in records)
            {
                this.setCovidDataCollection(record, lineNumber);
                lineNumber++;
            }

            return this.covidDataCollection;
        }

        #endregion

        #region Private Helpers

        private void setCovidDataCollection(string record, int lineNumber)
        {
            var covidRecord = createCovidRecord(record);
            var isCovidRecordDuplicateOrNull = this.isRecordDuplicate(covidRecord) || covidRecord == null;
            if (isCovidRecordDuplicateOrNull)
            {
                this.CovidErrorLogger.AddErrorLineToErrorLogger(lineNumber, record);
            }
            else
            {
                this.covidDataCollection.Add(covidRecord);
            }
        }

        private static CovidRecord createCovidRecord(string record)
        {
            var fields = record.Split(Assets.CsvDelimiter);
            CovidRecord covidRecord;
            if (!doesRecordHaveNoErrors(fields))
            {
                return null;
            }

            try
            {
                covidRecord = new CovidRecord(Format.FormatAsDateTime(fields[Assets.ColumnNumberForDate]),
                    fields[Assets.ColumnNumberForState]) {
                    PositiveTests = Format.FormatStringToInteger(fields[Assets.ColumnNumberForPositives]),
                    NegativeTests = Format.FormatStringToInteger(fields[Assets.ColumnNumberForNegatives]),
                    HospitalizedCurrently =
                        Format.FormatStringToInteger(fields[Assets.ColumnNumberForHospitalizedCurrently]),
                    Hospitalizations = Format.FormatStringToInteger(fields[Assets.ColumnNumberForHospitalizations]),
                    Deaths = Format.FormatStringToInteger(fields[Assets.ColumnNumberForDeaths])
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                covidRecord = null;
            }

            return covidRecord;
        }

        private static bool doesRecordHaveNoErrors(IReadOnlyList<string> fields)
        {
            if (fields.Count != Assets.NumberOfFields)
            {
                return false;
            }

            var isDateValid = FormatValidator.IsDateStringValid(fields[Assets.ColumnNumberForDate]);
            var isStateValid = FormatValidator.IsStateLabelValid(fields[Assets.ColumnNumberForState]);
            var areNumericFieldsValid = true;
            for (var index = Assets.ColumnStartOfNumericData; index < fields.Count; index++)
            {
                areNumericFieldsValid = FormatValidator.IsNumericStringValid(fields[index]);
                if (!areNumericFieldsValid)
                {
                    break;
                }
            }

            return isDateValid && isStateValid && areNumericFieldsValid;
        }

        private IEnumerable<string> breakDownTextContent()
        {
            var covidData = this.TextContent.Replace("\r", "").Split("\n").ToList();
            var lastItemIndex = covidData.Count - 1;
            if (covidData[lastItemIndex] == string.Empty || covidData[lastItemIndex] == null)
            {
                covidData.RemoveAt(lastItemIndex);
            }

            covidData.RemoveAt(0);
            return covidData.ToArray();
        }

        private bool isRecordDuplicate(CovidRecord record)
        {
            return this.covidDataCollection.Contains(record);
        }

        #endregion
    }
}