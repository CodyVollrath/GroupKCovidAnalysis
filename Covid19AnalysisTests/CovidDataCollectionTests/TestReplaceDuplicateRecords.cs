using System;
using Covid19Analysis.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19AnalysisTests.CovidDataCollectionTests
{
    /// <summary>
    ///     <para>
    ///         Testing The functionality of the TestReplaceDuplicateRecords Method in the CovidDataCollection class
    ///     </para>
    ///     <para>TestCase: TestReplaceEmptyCollection</para>
    ///     <para>Input: record1,{}  ExpectedOutput:False</para>
    ///     <para>TestCase: TestReplaceSingleItemCollectionNoMatch</para>
    ///     <para>Input: record3, {record1.PositiveTests = 50}  ExpectedOutput: False</para>
    ///     <para>TestCase: TestReplaceSingleItemCollectionWithMatch</para>
    ///     <para>Input: record2, {record1.PositiveTests = 50} ExpectedOutput: True, {record2.PositiveTests = 30} </para>
    ///     <para>TestCase: TestReplaceMultipleItemCollection</para>
    ///     <para>
    ///         Input: record2, {record1.PositiveTests = 50, record3.PositiveTests = 10}  ExpectedOutput: True,
    ///         {record3.PositiveTests = 10, record1.PositiveTests = 30}
    ///     </para>
    /// </summary>
    [TestClass]
    public class TestReplaceDuplicateRecords
    {
        #region Methods

        #region Setup

        [TestInitialize]
        public void Setup()
        {
            var date = new DateTime(2020, 05, 01);
            var date2 = new DateTime(2020, 05, 02);
            this.record1 = new CovidRecord(date, "GA") {
                PositiveTests = 50
            };
            this.record2 = new CovidRecord(date, "GA") {
                PositiveTests = 30
            };
            this.record3 = new CovidRecord(date2, "GA") {
                PositiveTests = 10
            };
        }

        #endregion

        [TestMethod]
        public void TestReplaceEmptyCollection()
        {
            var covidCollection = new CovidDataCollection();
            Assert.IsFalse(covidCollection.ReplaceDuplicateRecords(this.record1));
        }

        [TestMethod]
        public void TestReplaceSingleItemCollectionNoMatch()
        {
            var covidCollection = new CovidDataCollection {
                this.record1
            };
            var recordBeforeChange = covidCollection[0];
            var isReplaced = covidCollection.ReplaceDuplicateRecords(this.record3);
            var recordAfterChange = covidCollection[0];

            Assert.AreEqual(this.record1.PositiveTests, recordBeforeChange.PositiveTests);
            Assert.IsFalse(isReplaced);
            Assert.AreEqual(this.record1.PositiveTests, recordAfterChange.PositiveTests);
        }

        [TestMethod]
        public void TestReplaceSingleItemCollectionWithMatch()
        {
            var covidCollection = new CovidDataCollection {
                this.record1
            };
            var recordBeforeChange = covidCollection[0];
            var isReplaced = covidCollection.ReplaceDuplicateRecords(this.record2);
            var recordAfterChange = covidCollection[0];

            Assert.AreNotEqual(this.record2.PositiveTests, recordBeforeChange.PositiveTests);
            Assert.IsTrue(isReplaced);
            Assert.AreEqual(this.record2.PositiveTests, recordAfterChange.PositiveTests);
        }

        [TestMethod]
        public void TestReplaceMultipleItemCollection()
        {
            var covidCollection = new CovidDataCollection {
                this.record1,
                this.record3
            };
            var recordBeforeChange = covidCollection[0];
            var isReplaced = covidCollection.ReplaceDuplicateRecords(this.record2);
            var recordAfterChange = covidCollection[1];
            var unalteredRecord = covidCollection[0];

            Assert.AreNotEqual(this.record2.PositiveTests, recordBeforeChange.PositiveTests);
            Assert.IsTrue(isReplaced);
            Assert.AreEqual(this.record2.PositiveTests, recordAfterChange.PositiveTests);
            Assert.AreEqual(this.record3.PositiveTests, unalteredRecord.PositiveTests);
        }

        #endregion

        #region Private Members

        private CovidRecord record1;
        private CovidRecord record2;
        private CovidRecord record3;

        #endregion
    }
}