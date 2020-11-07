using System;
using Covid19Analysis.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19AnalysisTests.CovidDataStatisticsTests
{
    /// <summary>
    ///     <para>
    ///         Testing The functionality of the FindNumberOfDaysForPositiveTestsLessThanThreshold Method in the
    ///         CovidDataStatistics class
    ///     </para>
    ///     <para>TestCase: TestFindThresholdLessThanZero</para>
    ///     <para>Input: {} ExpectedOutput:ArgumentOutOfRangeException</para>
    ///     <para>TestCase: TestFindEmptyCovidDataCollection</para>
    ///     <para>Input: {} ExpectedOutput:InvalidOperationException</para>
    ///     <para>TestCase: TestFindNoRecordsLessThanThreshold</para>
    ///     <para>Input: {record1.PositiveTests = 50} ExpectedOutput: 0</para>
    ///     <para>TestCase: TestFindARecordOnThreshold</para>
    ///     <para>Input: {record2.PositiveTests = 40, record3.PositiveTests = 50} ExpectedOutput: 0</para>
    ///     <para>TestCase: TestFindARecordLessThanThreshold</para>
    ///     <para>Input: {record1.PositiveTests = 30,record2.PositiveTests = 40, record3.PositiveTests = 50} ExpectedOutput: 1</para>
    /// </summary>
    [TestClass]
    public class TestFindNumberOfDaysForPositiveTestsLessThanThreshold
    {
        #region Methods

        #region Setup

        [TestInitialize]
        public void Setup()
        {
            this.record1 = new CovidRecord(DateTime.Parse("06/04/2020"), "GA") {
                PositiveTests = 30
            };
            this.record2 = new CovidRecord(DateTime.Parse("07/24/2020"), "GA") {
                PositiveTests = 40
            };
            this.record3 = new CovidRecord(DateTime.Parse("08/15/2020"), "GA") {
                PositiveTests = 50
            };

            this.threshold = 40;
        }

        #endregion

        [TestMethod]
        public void TestFindThresholdLessThanZero()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var covidCollection = new CovidDataCollection();
            var covidStatistics = new CovidDataStatistics(covidCollection);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                covidStatistics.FindNumberOfDaysForPositiveTestsLessThanThreshold(-1));
        }

        [TestMethod]
        public void TestFindEmptyCovidDataCollection()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var covidCollection = new CovidDataCollection();
            var covidStatistics = new CovidDataStatistics(covidCollection);
            Assert.ThrowsException<InvalidOperationException>(() =>
                covidStatistics.FindNumberOfDaysForPositiveTestsLessThanThreshold(this.threshold));
        }

        [TestMethod]
        public void TestFindNoRecordsLessThanThreshold()
        {
            var covidCollection = new CovidDataCollection
                {this.record3};
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var result = covidStatistics.FindNumberOfDaysForPositiveTestsLessThanThreshold(this.threshold);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestFindARecordOnThreshold()
        {
            var covidCollection = new CovidDataCollection {
                this.record2,
                this.record3
            };
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var result = covidStatistics.FindNumberOfDaysForPositiveTestsLessThanThreshold(this.threshold);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestFindARecordLessThanThreshold()
        {
            var covidCollection = new CovidDataCollection {
                this.record1,
                this.record2,
                this.record3
            };
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var result = covidStatistics.FindNumberOfDaysForPositiveTestsLessThanThreshold(this.threshold);
            Assert.AreEqual(1, result);
        }

        #endregion

        #region Private Members

        private CovidRecord record1;
        private CovidRecord record2;
        private CovidRecord record3;
        private int threshold;

        #endregion
    }
}