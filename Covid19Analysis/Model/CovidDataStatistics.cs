using System;
using System.Collections.Generic;
using System.Linq;
using Covid19Analysis.Resources;

namespace Covid19Analysis.Model
{
    /// <summary>This class finds particular data points out of a CovidDataCollection</summary>
    public class CovidDataStatistics
    {
        #region Properties

        /// <summary>Gets the covid records.</summary>
        /// <value>The covid records.</value>
        public IEnumerable<CovidRecord> CovidRecords { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CovidDataStatistics" /> class.
        ///     <code>Precondition: covidRecords != null</code>
        ///     <code>Postcondition: CovidRecords == covidRecords</code>
        /// </summary>
        /// <param name="covidRecords">The covid records.</param>
        /// <exception cref="ArgumentNullException">covidRecords</exception>
        public CovidDataStatistics(IEnumerable<CovidRecord> covidRecords)
        {
            this.CovidRecords = covidRecords ?? throw new ArgumentNullException(nameof(covidRecords));
        }

        #endregion

        #region Methods

        /// <summary>Finds the day of first positive test.</summary>
        /// <returns>
        ///     <para>the date of the first positive test</para>
        /// </returns>
        public DateTime FindDayOfFirstPositiveTest()
        {
            var dayOfFirstPositive = this.CovidRecords.OrderBy(record => record.Date)
                                         .First(record => record.PositiveTests > 0).Date;
            return dayOfFirstPositive;
        }

        /// <summary>Finds the record with highest positive cases.</summary>
        /// <returns>The CovidRecord with the highest positive test</returns>
        public CovidRecord FindRecordWithHighestPositiveCases()
        {
            var highestPositiveTestCaseRecord =
                this.CovidRecords.OrderByDescending(record => record.PositiveTests).First();
            return highestPositiveTestCaseRecord;
        }

        /// <summary>Finds the record with lowest positive cases.</summary>
        /// <param name="sinceDate">The date indicator to not count the lowest.</param>
        /// <returns>The record with the lowest positive cases</returns>
        public CovidRecord FindRecordWithLowestPositiveCases(DateTime sinceDate)
        {
            var lowestPositiveTestCaseRecord = this.CovidRecords.Where(record => record.Date >= sinceDate)
                                                   .OrderBy(record => record.PositiveTests).First();
            return lowestPositiveTestCaseRecord;
        }

        /// <summary>Finds the record with highest negative tests.</summary>
        /// <returns>
        ///     <para>The CovidRecord with the highest negative tests.</para>
        /// </returns>
        public CovidRecord FindRecordWithHighestNegativeTests()
        {
            var highestNegativeTestRecord = this.CovidRecords.OrderByDescending(record => record.NegativeTests).First();
            return highestNegativeTestRecord;
        }

        /// <summary>Finds the record with highest total tests.</summary>
        /// <returns>
        ///     <para>The CovidRecord with the highest total tests</para>
        /// </returns>
        public CovidRecord FindRecordWithHighestTotalTests()
        {
            var highestTotalTestRecord = this.CovidRecords.OrderByDescending(record => record.TotalTests).First();
            return highestTotalTestRecord;
        }

        /// <summary>Finds the record with lowest total tests.</summary>
        /// <param name="sinceDate">The date indicator to not count the lowest.</param>
        /// <returns>The record with the lowest total cases</returns>
        public CovidRecord FindRecordWithLowestTotalTests(DateTime sinceDate)
        {
            var lowestTotalTestRecord = this.CovidRecords.Where(record => record.Date >= sinceDate)
                                            .OrderBy(record => record.TotalTests).First();
            return lowestTotalTestRecord;
        }

        /// <summary>Finds the record with highest deaths.</summary>
        /// <returns>The CovidRecord with the highest deaths</returns>
        public CovidRecord FindRecordWithHighestDeaths()
        {
            var highestDeathRecord = this.CovidRecords.OrderByDescending(record => record.Deaths).First();
            return highestDeathRecord;
        }

        /// <summary>Finds the record with highest hospitalizations.</summary>
        /// <returns>The CovidRecord with the highest hospitalizations</returns>
        public CovidRecord FindRecordWithHighestHospitalizations()
        {
            var highestHospitalizationsRecord =
                this.CovidRecords.OrderByDescending(record => record.Hospitalizations).First();
            return highestHospitalizationsRecord;
        }

        /// <summary>Finds the record with highest current hospitalizations.</summary>
        /// <returns>The CovidRecord with the highest number of current hospitalizations</returns>
        public CovidRecord FindRecordWithHighestCurrentHospitalizations()
        {
            var highestCurrentHospitalizationsRecord =
                this.CovidRecords.OrderByDescending(record => record.HospitalizedCurrently).First();
            return highestCurrentHospitalizationsRecord;
        }

        /// <summary>Finds the record with lowest current hospitalizations since a date.</summary>
        /// <param name="sinceDate">The since date.</param>
        /// <returns>The CovidRecord with the current lowest hospitalizations since a date entered</returns>
        public CovidRecord FindRecordWithLowestCurrentHospitalizations(DateTime sinceDate)
        {
            var lowestHospitalizationsCurrently = this.CovidRecords.Where(record => record.Date >= sinceDate)
                                                      .OrderBy(record => record.HospitalizedCurrently).First();
            return lowestHospitalizationsCurrently;
        }

        /// <summary>Finds the record with highest percentage of positive tests.</summary>
        /// <returns>The CovidRecord with highest percentage of positive tests</returns>
        public CovidRecord FindRecordWithHighestPercentageOfPositiveTests()
        {
            var recordWithHighestPercentage = this.CovidRecords.OrderByDescending(FindPositivePercentageForRecord)
                                                  .ThenByDescending(record => record.PositiveTests)
                                                  .First(record => record.TotalTests != 0);
            return recordWithHighestPercentage;
        }

        /// <summary>Finds the average positive tests since first positive test.</summary>
        /// <returns>The average positive tests since first positive test date</returns>
        public double FindAveragePositiveTestsSinceFirstPositiveTest()
        {
            var firstDateWithPositive = this.FindDayOfFirstPositiveTest();
            var averagePositiveTests = this.CovidRecords
                                           .Where(record => record.Date >= firstDateWithPositive)
                                           .Select(record => record.PositiveTests).Average();
            return averagePositiveTests;
        }

        /// <summary>Finds the average positive tests since a specified date.</summary>
        /// <param name="sinceDate">The date indicator to not count the lowest.</param>
        /// <returns>The average positive tests since the date specified</returns>
        public double FindAveragePositiveTestsSinceSpecifiedDate(DateTime sinceDate)
        {
            var firstDateWithPositive = this.FindDayOfFirstPositiveTest();
            var averagePositiveTests = this.CovidRecords
                                           .Where(record => record.Date >= firstDateWithPositive)
                                           .Select(record => record.PositiveTests).Average();
            return averagePositiveTests;
        }

        /// <summary>Finds the average total tests since a specified date.</summary>
        /// <param name="sinceDate">The date indicator to not count the lowest.</param>
        /// <returns>The average total tests since the date specified</returns>
        public double FindAverageTotalTestsSinceSpecifiedDate(DateTime sinceDate)
        {
            var firstDateWithPositive = this.FindDayOfFirstPositiveTest();
            var averageTotalTests = this.CovidRecords
                                        .Where(record => record.Date >= firstDateWithPositive)
                                        .Select(record => record.TotalTests).Average();
            return averageTotalTests;
        }

        /// <summary>Finds the overall positivity rate since first positive test.</summary>
        /// <returns>The overall positivity rate since first positive test date</returns>
        public double FindOverallPositivityRateSinceFirstPositiveTest()
        {
            var firstDateWithPositive = this.FindDayOfFirstPositiveTest();
            var averagePositiveTests = this.FindAveragePositiveTestsSinceFirstPositiveTest();

            var averageOfTotalTests = this.CovidRecords
                                          .Where(record => record.Date.Date >= firstDateWithPositive)
                                          .Select(record => record.TotalTests).Average();

            var positivityRate = averagePositiveTests / averageOfTotalTests;
            return positivityRate;
        }

        /// <summary>
        ///     Finds the number of days since first positive test greater than threshold.
        ///     <code>Precondition: threshold >= 0</code>
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        /// <returns>The number of days greater than a threshold since first positive test date</returns>
        public int FindNumberOfDaysForPositiveTestsGreaterThanThreshold(int threshold)
        {
            if (threshold < 0)
            {
                throw new ArgumentException($"{nameof(threshold)} can not be less than 0");
            }

            var firstDateWithPositive = this.FindDayOfFirstPositiveTest();
            var daysGreaterThanThreshold = this.CovidRecords.Count(record =>
                record.Date.Date >= firstDateWithPositive && record.PositiveTests > threshold);
            return daysGreaterThanThreshold;
        }

        /// <summary>
        ///     Finds the number of days since first positive test less than threshold.
        ///     <code>Precondition: threshold >= 0</code>
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        /// <returns>The number of days less than a threshold since first positive test date</returns>
        public int FindNumberOfDaysForPositiveTestsLessThanThreshold(int threshold)
        {
            if (threshold < 0)
            {
                throw new ArgumentException($"{nameof(threshold)} can not be less than 0");
            }

            var firstDateWithPositive = this.FindDayOfFirstPositiveTest();
            var daysGreaterThanThreshold = this.CovidRecords.Count(record =>
                record.Date.Date >= firstDateWithPositive && record.PositiveTests < threshold);
            return daysGreaterThanThreshold;
        }

        /// <summary>
        ///     Finds the positive percentage for record.
        ///     <code>Precondition: record != null</code>
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>The positive percentage of a record</returns>
        public static double FindPositivePercentageForRecord(CovidRecord record)
        {
            record = record ?? throw new ArgumentNullException(nameof(record));
            var totalTests = record.TotalTests;
            if (totalTests != 0)
            {
                return record.PositiveTests / Format.FormatIntegerToDouble(totalTests);
            }

            return double.NaN;
        }

        #endregion
    }
}