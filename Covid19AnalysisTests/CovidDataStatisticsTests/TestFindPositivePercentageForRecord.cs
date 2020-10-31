using System;
using Covid19Analysis.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19AnalysisTests.CovidDataStatisticsTests
{
    /// <summary>
    ///   <para>
    ///    Testing The functionality of the FindPositivePercentageForRecord Method in the CovidDataStatistics class
    ///   </para>
    ///   <para>TestCase: TestZeroPositives</para>
    ///	  <para>Input:record1.TotalTests = 0  ExpectedOutput:Double.NaN</para>
    ///
    ///   <para>TestCase: TestZeroPercentPositive</para>
    ///   <para>Input: record2.PositiveTests = 0, record2.NegativeTests = 1 ExpectedOutput: 0.0</para>
    ///
    ///   <para>TestCase: TestFiftyPercentPositive</para>
    ///   <para>Input: record3.PositiveTests = 1, record3.NegativeTests = 1   ExpectedOutput: 0.5</para>
    ///
    ///   <para>TestCase: TestOneHundredPercentPositive</para>
    ///   <para>Input: record4.PositiveTests = 1, record4.NegativeTests = 0 ExpectedOutput: 1.0</para>
    /// </summary>
    [TestClass]
    public class TestFindPositivePercentageForRecord
    {
        #region Private Members
        private CovidRecord record1;
        private CovidRecord record2;
        private CovidRecord record3;
        private CovidRecord record4;

        private const double Delta = 0.001;
        #endregion

        #region Setup
        [TestInitialize]
        public void Setup()
        {
            var date1 = new DateTime(year: 2020, month: 10, day: 1);
            var date2 = new DateTime(year: 2020, month: 11, day: 1);
            var date3 = new DateTime(year: 2020, month: 12, day: 1);
            var date4 = new DateTime(year: 2021, month:1, day:01);
            this.record1 = new CovidRecord(dateTime: date1, state: "GA")
            {
                PositiveTests = 0,
                NegativeTests = 0,
                Hospitalizations = 0,
                Deaths = 0
            };
            this.record2 = new CovidRecord(dateTime: date2, state: "GA")
            {
                PositiveTests = 0,
                NegativeTests = 1,
                Hospitalizations = 0,
                Deaths = 0
            };
            this.record3 = new CovidRecord(dateTime: date3, state: "GA")
            {
                PositiveTests = 1,
                NegativeTests = 1,
                Hospitalizations = 0,
                Deaths = 0
            };
            this.record4 = new CovidRecord(dateTime: date4, state: "GA")
            {
                PositiveTests = 1,
                NegativeTests = 0,
                Hospitalizations = 0,
                Deaths = 0
            };
        }
        #endregion

        #region Test Methods
        [TestMethod]
        public void TestZeroPositives()
        {
            var result = CovidDataStatistics.FindPositivePercentageForRecord(this.record1);
            Assert.AreEqual(double.NaN, result);
        }

        [TestMethod]
        public void TestZeroPercentPositive()
        {
            var result = CovidDataStatistics.FindPositivePercentageForRecord(this.record2);
            Assert.AreEqual(0, result, Delta);
        }

        [TestMethod]
        public void TestFiftyPercentPositive()
        {
            var result = CovidDataStatistics.FindPositivePercentageForRecord(this.record3);
            Assert.AreEqual(0.5, result, Delta);
        }

        [TestMethod]
        public void TestOneHundredPercentPositive()
        {
            var result = CovidDataStatistics.FindPositivePercentageForRecord(this.record4);
            Assert.AreEqual(1.0, result, Delta);
        }

        #endregion
    }
}
