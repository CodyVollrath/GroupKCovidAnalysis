using System;
using Covid19Analysis.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19AnalysisTests.CovidDataStatisticsTests
{
    /// <summary>
    ///     <para>
    ///         Testing The functionality of the FindRecordWithHighestPositiveCases Method in the CovidDataStatistics class
    ///     </para>
    ///     <para>TestCase: TestEmptyCovidDataCollection</para>
    ///     <para>Input: {} ExpectedOutput:InvalidOperationException</para>
    ///     <para>TestCase: TestOneItemCovidDataCollection</para>
    ///     <para>Input: {record1.PositiveTests = 1} ExpectedOutput: record1</para>
    ///     <para>TestCase: TestMultipleItemCovidDataCollectionLastPlace</para>
    ///     <para>
    ///         Input: {record1.PositiveTests = 1,record2.PositiveTests = 2, record3.PositiveTests = 3} ExpectedOutput:
    ///         record3
    ///     </para>
    ///     <para>TestCase: TestMultipleItemCovidDataCollectionMiddlePlace</para>
    ///     <para>
    ///         Input: {record1.PositiveTests = 1,record3.PositiveTests = 3, record2.PositiveTests = 2} ExpectedOutput:
    ///         record3
    ///     </para>
    ///     <para>TestCase: TestMultipleItemCovidDataCollectionFirstPlace</para>
    ///     <para>
    ///         Input: {record3.PositiveTests = 3,record1.PositiveTests = 1, record2.PositiveTests = 2} ExpectedOutput:
    ///         record3
    ///     </para>
    /// </summary>
    [TestClass]
    public class TestFindRecordWithHighestPositiveCases
    {
        #region Methods

        #region Setup

        [TestInitialize]
        public void SetUp()
        {
            var date1 = new DateTime(2020, 10, 1);
            var date2 = new DateTime(2020, 11, 1);
            var date3 = new DateTime(2020, 12, 1);
            this.record1 = new CovidRecord(date1, "GA") {
                PositiveTests = 1,
                NegativeTests = 1,
                Hospitalizations = 0,
                Deaths = 0
            };
            this.record2 = new CovidRecord(date2, "GA") {
                PositiveTests = 2,
                NegativeTests = 1,
                Hospitalizations = 0,
                Deaths = 0
            };
            this.record3 = new CovidRecord(date3, "GA") {
                PositiveTests = 3,
                NegativeTests = 1,
                Hospitalizations = 0,
                Deaths = 0
            };
        }

        #endregion

        [TestMethod]
        public void TestEmptyCovidDataCollection()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var covidCollection = new CovidDataCollection();
            var covidStatistics = new CovidDataStatistics(covidCollection);
            Assert.ThrowsException<InvalidOperationException>(
                () => covidStatistics.FindRecordWithHighestPositiveCases());
        }

        [TestMethod]
        public void TestOneItemCovidDataCollection()
        {
            var covidCollection = new CovidDataCollection {
                this.record1
            };
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var record = covidStatistics.FindRecordWithHighestPositiveCases();
            Assert.AreEqual(this.record1, record);
        }

        [TestMethod]
        public void TestMultipleItemCovidDataCollectionLastPlace()
        {
            var covidCollection = new CovidDataCollection {
                this.record1,
                this.record2,
                this.record3
            };
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var record = covidStatistics.FindRecordWithHighestPositiveCases();
            Assert.AreEqual(this.record3, record);
        }

        [TestMethod]
        public void TestMultipleItemCovidDataCollectionMiddlePlace()
        {
            var covidCollection = new CovidDataCollection {
                this.record1,
                this.record3,
                this.record2
            };
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var record = covidStatistics.FindRecordWithHighestPositiveCases();
            Assert.AreEqual(this.record3, record);
        }

        [TestMethod]
        public void TestMultipleItemCovidDataCollectionFirstPlace()
        {
            var covidCollection = new CovidDataCollection {
                this.record3,
                this.record1,
                this.record2
            };
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var record = covidStatistics.FindRecordWithHighestPositiveCases();
            Assert.AreEqual(this.record3, record);
        }

        #endregion

        #region Private Members

        private CovidRecord record1;
        private CovidRecord record2;
        private CovidRecord record3;

        #endregion
    }
}