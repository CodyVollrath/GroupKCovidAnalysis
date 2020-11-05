using System;
using System.Collections.Generic;
using System.Linq;
using Covid19Analysis.Resources;

namespace Covid19Analysis.OutputFormatter
{
    /// <Summary>This class creates a histogram Summary of a passed in collection of numeric Covid data</Summary>
    public class CovidDataHistogramGenerator
    {
        #region Properties

        /// <summary>Gets the size of the bin for the histogram.</summary>
        /// <value>The size of the bin.</value>
        public int BinSize { get; }

        /// <summary>Gets the numeric covid data as a list of numeric values related to covid data.</summary>
        /// <value>The numeric covid data as a list of numeric values.</value>
        public List<int> CollectionOfNumericValues { get; }

        /// <summary>Gets the highest value in the frequency histogram table.</summary>
        /// <value>The highest value in the table.</value>
        public int HighestValueInHistogram { get; }

        #endregion

        #region Constructors

        /// <Summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="CovidDataHistogram" originaltag="see">CovidDataHistogram</a> class.
        ///     <code>Precondition: collection != null</code>
        ///     <code>Postcondition: CollectionOfCovidRecords == collection AND BinSize == binSize</code>
        /// </Summary>
        /// <param name="collection">The collection.</param>
        /// <param name="binSize">The size of the bin</param>
        public CovidDataHistogramGenerator(List<int> collection, int binSize)
        {
            this.CollectionOfNumericValues = collection ?? throw new ArgumentNullException(nameof(collection));
            this.BinSize = binSize;
            this.HighestValueInHistogram = this.convertHighestValueToMultipleOfRangeWidth();
        }

        #endregion

        #region Methods

        /// <Summary>Generates the histogram.</Summary>
        /// <returns>
        ///     <para>the histogram</para>
        /// </returns>
        public string GenerateHistogram()
        {
            var collectionOfRanges = this.getCollectionOfRanges();
            var histogram = this.createAlignedHistogram(collectionOfRanges);
            return $"{Environment.NewLine}{histogram}";
        }

        #region Inner Classes

        /// <summary>This Inner class keeps track of the range values for individual ranges in the histogram</summary>
        private class Range : IComparable<Range>
        {
            #region Properties

            public int Min { get; }
            public int Max { get; }

            #endregion

            #region Constructors

            public Range(int min, int max)
            {
                this.Min = min;
                this.Max = max;
            }

            #endregion

            #region Methods

            public int CompareTo(Range other)
            {
                return other == null ? 1 : Comparer<int>.Default.Compare(this.Min, other.Min);
            }

            #endregion
        }

        #endregion

        #endregion

        #region Private Helpers

        private int convertHighestValueToMultipleOfRangeWidth()
        {
            this.CollectionOfNumericValues.Sort();
            var highestCombinedTest = this.CollectionOfNumericValues[this.CollectionOfNumericValues.Count - 1];
            while (highestCombinedTest % this.BinSize != 0)
            {
                highestCombinedTest++;
            }

            return highestCombinedTest;
        }

        private IEnumerable<Range> getCollectionOfRanges()
        {
            var maxRange = this.HighestValueInHistogram;
            var minRange = maxRange - (this.BinSize - 1);
            var collectionOfRanges = new List<Range> {new Range(minRange, maxRange)};
            while (minRange > 0 && maxRange > this.BinSize)
            {
                if (minRange == this.BinSize + 1)
                {
                    minRange = 0;
                }
                else
                {
                    minRange -= this.BinSize;
                }

                maxRange -= this.BinSize;
                var range = new Range(minRange, maxRange);
                collectionOfRanges.Add(range);
            }

            collectionOfRanges.Sort();
            return collectionOfRanges;
        }

        private string createAlignedHistogram(IEnumerable<Range> collectionOfRanges)
        {
            var histogram = string.Empty;
            foreach (var range in collectionOfRanges)
            {
                histogram += this.generateHistogramLine(range);
            }

            return histogram;
        }

        private string generateHistogramLine(Range range)
        {
            var countOfValuesInRange =
                Format.FormatIntegerAsFormattedString(this.getCountOfValuesWithinRange(range));

            var min = Format.FormatIntegerAsFormattedString(range.Min);
            var max = Format.FormatIntegerAsFormattedString(range.Max);

            var histogramLine = $"{min,5} - {max,5}: {countOfValuesInRange,2}{Environment.NewLine}";

            return histogramLine;
        }

        private int getCountOfValuesWithinRange(Range range)
        {
            return this.CollectionOfNumericValues.Count(testCase => testCase >= range.Min && testCase <= range.Max);
        }

        #endregion
    }
}