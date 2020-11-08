using System;
using System.Collections.Generic;
using System.Linq;
using Covid19Analysis.Model;

namespace Covid19Analysis.DataTier
{
    /// <summary>
    ///     Author: Cody Vollrath
    ///     This class is responsible for merging CovidDataCollections
    /// </summary>
    public class CovidDataMergeController
    {
        #region Properties

        /// <Summary>Gets the original covid data collection.</Summary>
        /// <value>The original covid data collection.</value>
        public CovidDataCollection MergedCovidDataCollection { get; }

        /// <Summary>Gets the recently created collection.</Summary>
        /// <value>The new covid data collection.</value>
        public CovidDataCollection NewCovidDataCollection { get; }

        #endregion

        #region Constructors

        #region Construtors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="CovidDataMergeController" originaltag="see">CovidDataMergeController</a> class.
        ///     <code>Precondition: originalCollection != null AND newCollection != null</code>
        ///     <code>Postcondition: MergedCovidDataCollection == originalCollection NewCovidDataCollection == newCollection</code>
        /// </summary>
        /// <param name="originalCollection">The original collection.</param>
        /// <param name="newCollection">The new collection.</param>
        /// <exception cref="ArgumentNullException">
        ///     originalCollection
        ///     or
        ///     newCollection
        /// </exception>
        public CovidDataMergeController(CovidDataCollection originalCollection, CovidDataCollection newCollection)
        {
            this.MergedCovidDataCollection =
                originalCollection ?? throw new ArgumentNullException(nameof(originalCollection));
            this.NewCovidDataCollection = newCollection ?? throw new ArgumentNullException(nameof(newCollection));
        }

        #endregion

        #endregion

        #region Methods

        /// <Summary>
        ///     Adds all non duplicates to the merged collection.
        ///     <code>Postcondition: MergedCovidDataCollection will have all records that are not duplicates</code>
        /// </Summary>
        public void AddAllNonDuplicates()
        {
            var nonDuplicates =
                (from record in this.NewCovidDataCollection
                 where !this.isDuplicate(record)
                 select record).ToList();
            foreach (var record in nonDuplicates)
            {
                this.MergedCovidDataCollection.Add(record);
                this.NewCovidDataCollection.Remove(record);
            }
        }

        /// <Summary>
        ///     Gets the duplicate records that are found.
        ///     <para>Duplicates are qualified as having the same date and state value.</para>
        /// </Summary>
        /// <value>The duplicate records.</value>
        public List<CovidRecord> GetDuplicates()
        {
            return this.NewCovidDataCollection.ToList();
        }

        /// <Summary>
        ///     Replaces the duplicate.
        ///     <code>Postcondition: MergedCovidDataCollection replaces </code>
        /// </Summary>
        /// <param name="record">The record.</param>
        public void ReplaceDuplicate(CovidRecord record)
        {
            this.MergedCovidDataCollection.ReplaceDuplicateRecords(record);
        }

        #region Private Helpers

        private bool isDuplicate(CovidRecord record)
        {
            return this.MergedCovidDataCollection.Contains(record);
        }

        #endregion

        #endregion
    }
}