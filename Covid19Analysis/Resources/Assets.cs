
using System;

namespace Covid19Analysis.Resources
{
    /// <Summary>This class contains static constants that are used frequently in the Covid19 Analysis application.</Summary>
    public class Assets
    {
        #region Misc
        public const string CsvDelimiter = ",";


        /// <summary>The date string unformatted - yyyyMMdd</summary>
        public const string DateStringUnformatted = "yyyyMMdd";

        /// <Summary>
        /// The date string formatted - MM/dd/yyyy
        /// </Summary>
        public const string DateStringFormatted = "MM/dd/yyyy";

        public const string TimeStamp = "MMddyyyyhhmmss";
        public const string MonthNameIdentifier = "MMMM";

        public const string NoCovidDataText = "No CovidData in the dataset";

        public static readonly string NoPositiveData = $"No Positive Tests are present in the data set {Environment.NewLine}";

        #endregion

        #region Prompt Labels
        public const string StateCovidDataHeadingLabel = "Statistics for Covid-19" +
                                                           "\n-------------------------------------------------------------\n";

        public const string HistogramLabel = "Positive Case Histogram: ";
        public const string FirstDayOfPositiveTestLabel = "First positive case " + DateOfOccurrenceLabel;
        
        public const string AveragePositiveTestsLabel = "Average # positive tests:";
        public const string AverageTotalTestsLabel = "Average # total tests:";
        
        public const string OverallPositivityRateLabel = "The Overall Positivity Rate:";

        public const string DaysGreaterThanValueLabel = "Days Since First Positive Test for Cases That are Greater Than";
        public const string DaysLessThanValueLabel = "Days Since First Positive Test for Cases That are Less Than";
        public const string DateOfOccurrenceLabel = "occurred on";
        public const string DayOfOccurrenceMonthlyLabel = "occurred on the";
        public const string DaysOfDataLabel = "days of data";

        public const string HighestNegativeTestsLabel = "Highest # negative tests";
        public const string HighestDeathsLabel = "Highest # deaths:";
        public const string HighestHospitalizationsLabel = "Highest # Hospitalizations:";
        public const string HighestPercentageOfPositiveCasesLabel = "Highest percentage of positive cases:";
        public const string HighestPositiveTestsLabel = "Highest # positive tests:";
        public const string HighestTotalTestsLabel = "Highest # total tests:";
        public const string LowestPositiveTestsLabel = "Lowest  # positive tests:";
        public const string LowestTotalTestsLabel = "Lowest  # total tests:";
        public static readonly string[] HeadersForData = new string[] {"date", "state", "positiveIncrease", "negativeIncrease", "deathIncrease", "hospitalizedIncrease" };

        #endregion

        #region Filter Specifiers
        public const string GeorgiaFilterValue = "GA";
        #endregion

        #region Column Numbers
        public const int ColumnNumberForDate = 0;
        public const int ColumnNumberForState = 1;
        public const int ColumnNumberForPositives = 2;
        public const int ColumnNumberForNegatives = 3;
        public const int ColumnNumberForDeaths = 4;
        public const int ColumnNumberForHospitalizations = 5;
        #endregion

        #region Numeric Constants
        public const int DefaultGreaterThanThreshHold = 2500;
        public const int DefaultLessThanThreshold = 1000;
        public const int NumberOfFields = 6;
        public const int BeginningRowNumber = 2;
        public const int ColumnStartOfNumericData = 2;
        public const int RangeWidth = 500;
        public const int DefaultYear = 2020;
        public const int DefaultDay = 1;
        public const int NumberOfMonthsInOneYear = 12;
        #endregion

        #region Dialog Box Prompts
        public const string MergeFilesContent = "Would you like to merge files or replace the current loaded content?";
        public const string MergeFilesTitle = "Merge Files?";
        public const string MergeFilesPrimaryButtonText = "Merge";
        public const string MergeFilesSecondaryButtonText = "Replace";

        public const string SaveFailedTitle = "File Not Saved!";
        public const string SaveFailedContent = "The file that was attempted to be saved to could not be opened or failed to be created.";

        public const string SaveSuccessfulTitle = "Success!";
        public const string SaveSuccessfulContent = "File has been saved";
        public const string OkPrompt = "Ok";

        #endregion

    }
}
