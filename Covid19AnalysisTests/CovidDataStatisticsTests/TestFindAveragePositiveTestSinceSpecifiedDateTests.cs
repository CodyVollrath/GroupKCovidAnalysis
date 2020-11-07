using System;
using Covid19Analysis.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19AnalysisTests.CovidDataStatisticsTests
{
    /// <summary>
    ///     <para>
    ///         Testing The functionality of the FindAveragePositiveTestSinceSpecifiedDate Method in the CovidDataStatistics
    ///         class
    ///     </para>
    ///     <para>TestCase: TestAverageEmptyCovidDataCollection</para>
    ///     <para>Input: {} ExpectedOutput:InvalidOperationException</para>
    ///     <para>TestCase: TestAverageOneItemCovidDataCollection</para>
    ///     <para>Input: {record1.PositiveTests = 30} ExpectedOutput: 30</para>
    ///     <para>TestCase: TestAverageMultiItemCovidDataCollection</para>
    ///     <para>Input: {record1.PositiveTests = 30,record2.PositiveTests = 40, record3.PositiveTests = 50} ExpectedOutput: 40</para>
    /// </summary>
    [TestClass]
    public class TestFindAveragePositiveTestSinceSpecifiedDate
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

            this.defaultDate = DateTime.Parse("06/04/2020");
        }

        #endregion

        [TestMethod]
        public void TestAverageEmptyCovidDataCollection()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var covidCollection = new CovidDataCollection();
            var covidStatistics = new CovidDataStatistics(covidCollection);
            Assert.ThrowsException<InvalidOperationException>(() =>
                covidStatistics.FindAveragePositiveTestsSinceSpecifiedDate(this.defaultDate));
        }

        [TestMethod]
        public void TestAverageOneItemCovidDataCollection()
        {
            var covidCollection = new CovidDataCollection
                {this.record1};
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var result = covidStatistics.FindAveragePositiveTestsSinceSpecifiedDate(this.defaultDate);
            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void TestAverageMultiItemCovidDataCollection()
        {
            var covidCollection = new CovidDataCollection {
                this.record1,
                this.record2,
                this.record3
            };
            var covidStatistics = new CovidDataStatistics(covidCollection);
            var result = covidStatistics.FindAveragePositiveTestsSinceSpecifiedDate(this.defaultDate);
            Assert.AreEqual(40, result);
        }

        #endregion

        #region Private Members

        private CovidRecord record1;
        private CovidRecord record2;
        private CovidRecord record3;
        private DateTime defaultDate;

        #endregion
    }
}