using System;
using Covid19Analysis.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19AnalysisTests.CovidDataStatisticsTests
{
    /// <summary>
    ///     <para>
    ///         Testing The functionality of the TestFindDayOfFirstPositiveTest Method in the CovidDataStatistics class
    ///     </para>
    ///     <para>TestCase: TestEmptyCovidDataCollection</para>
    ///     <para>Input: {}  ExpectedOutput:InvalidOperationException</para>
    ///     <para>TestCase: TestOneItemCovidDataCollectionNoPositiveTest</para>
    ///     <para>Input:{record1.PositiveTests = 0}  ExpectedOutput: InvalidOperationException</para>
    ///     <para>TestCase: TestOneItemCovidDataCollectionOnePositiveTest</para>
    ///     <para>Input: {record1.PositiveTests = 1} ExpectedOutput: record1.Date </para>
    ///     <para>TestCase: TestMultipleItemCovidDataCollectionLastPlace</para>
    ///     <para>
    ///         Input:{record1.PositiveTests = 0, record2.PositiveTests = 0, record3.PositiveTests = 1}  ExpectedOutput:
    ///         record3.Date
    ///     </para>
    ///     <para>TestCase: TestMultipleItemCovidDataCollectionMiddlePlace</para>
    ///     <para>
    ///         Input:{record1.PositiveTests = 0, record2.PositiveTests = 1, record3.PositiveTests = 0} ExpectedOutput:
    ///         record2.Date
    ///     </para>
    ///     <para>TestCase: TestMultipleItemCovidDataCollectionFirstPlace</para>
    ///     <para>
    ///         Input:{record1.PositiveTests = 1, record2.PositiveTests = 0, record3.PositiveTests = 0} ExpectedOutput:
    ///         record1.Date
    ///     </para>
    /// </summary>
    [TestClass]
    public class TestFindDayOfFirstPositiveTest
    {
        #region Methods

        #region Setup

        [TestInitialize]
        public void Setup()
        {
            this.inputDate1 = new DateTime(2020, 05, 01);
            this.inputDate2 = new DateTime(2020, 05, 02);
            this.inputDate3 = new DateTime(2020, 05, 03);
        }

        #endregion

        [TestMethod]
        public void TestEmptyCovidDataCollection()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var covidCollection = new CovidDataCollection();
            var statistics = new CovidDataStatistics(covidCollection);
            Assert.ThrowsException<InvalidOperationException>(() => statistics.FindDayOfFirstPositiveTest());
        }

        [TestMethod]
        public void TestOneItemCovidDataCollectionNoPositiveTest()
        {
            var record = new CovidRecord(DateTime.Now, "GA");

            var covidCollection = new CovidDataCollection {
                record
            };
            var statistics = new CovidDataStatistics(covidCollection);

            Assert.ThrowsException<InvalidOperationException>(() => statistics.FindDayOfFirstPositiveTest());
        }

        [TestMethod]
        public void TestOneItemCovidDataCollectionOnePositiveTest()
        {
            var record = new CovidRecord(this.inputDate1, "GA") {
                PositiveTests = 1
            };
            var covidCollection = new CovidDataCollection {
                record
            };
            var statistics = new CovidDataStatistics(covidCollection);

            var result = statistics.FindDayOfFirstPositiveTest();

            Assert.AreEqual(this.inputDate1, result.Date);
        }

        [TestMethod]
        public void TestMultipleItemCovidDataCollectionLastPlace()
        {
            var record1 = new CovidRecord(this.inputDate1, "GA");
            var record2 = new CovidRecord(this.inputDate2, "GA");
            var record3 = new CovidRecord(this.inputDate3, "GA") {
                PositiveTests = 1
            };

            var covidCollection = new CovidDataCollection {
                record1,
                record2,
                record3
            };

            var statistics = new CovidDataStatistics(covidCollection);

            var result = statistics.FindDayOfFirstPositiveTest();

            Assert.AreEqual(this.inputDate3, result.Date);
        }

        [TestMethod]
        public void TestMultipleItemCovidDataCollectionMiddlePlace()
        {
            var record1 = new CovidRecord(this.inputDate1, "GA");
            var record2 = new CovidRecord(this.inputDate2, "GA") {
                PositiveTests = 1
            };
            var record3 = new CovidRecord(this.inputDate3, "GA");

            var covidCollection = new CovidDataCollection {
                record1,
                record2,
                record3
            };

            var statistics = new CovidDataStatistics(covidCollection);

            var result = statistics.FindDayOfFirstPositiveTest();

            Assert.AreEqual(this.inputDate2, result.Date);
        }

        [TestMethod]
        public void TestMultipleItemCovidDataCollectionFirstPlace()
        {
            var record1 = new CovidRecord(this.inputDate1, "GA") {
                PositiveTests = 1
            };
            var record2 = new CovidRecord(this.inputDate2, "GA");
            var record3 = new CovidRecord(this.inputDate3, "GA");

            var covidCollection = new CovidDataCollection {
                record1,
                record2,
                record3
            };

            var statistics = new CovidDataStatistics(covidCollection);

            var result = statistics.FindDayOfFirstPositiveTest();

            Assert.AreEqual(this.inputDate1, result.Date);
        }

        #endregion

        #region Private Members

        private DateTime inputDate1;
        private DateTime inputDate2;
        private DateTime inputDate3;

        #endregion
    }
}