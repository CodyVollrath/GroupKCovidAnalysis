using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Covid19Analysis.Resources;

namespace Covid19Analysis.Model
{

    /// <Summary>This class keeps a collection of covid records</Summary>
    public class CovidDataCollection : ICollection<CovidRecord>
    {
        #region Properties

        /// <Summary>Gets the covid records.</Summary>
        /// <value>The covid records.</value>
        public ICollection<CovidRecord> CovidRecords { get; private set; }

        #endregion

        #region Constructors
        /// <Summary>
        /// Initializes a new instance of the <a onclick="return false;" href="CovidDataCollection" originaltag="see">CovidDataCollection</a> class.
        /// <code>Postcondition: CovidRecords = new List(Type=CovidRecord)</code>
        /// </Summary>
        public CovidDataCollection()
        {
            this.CovidRecords = new List<CovidRecord>();
        }
        #endregion

        #region Public Methods


        /// <summary>Gets or sets the <see cref="CovidRecord" /> at the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <value>The <see cref="CovidRecord" />.</value>
        /// <returns>The element at the specified index</returns>
        public CovidRecord this[int index]
        {
            get => ((List<CovidRecord>)this.CovidRecords)[index];
            set => ((List<CovidRecord>)this.CovidRecords)[index] = value;
        }

        /// <Summary>
        /// Replaces the record with the matching date and state and then swaps it for the record that is passed in.
        /// <code>Precondition: record != null</code>
        /// </Summary>
        /// <param name="record">The record.</param>
        /// <returns>True if the record was replaced, otherwise False.</returns>
        /// <exception cref="ArgumentNullException">record</exception>
        public bool ReplaceDuplicateRecords(CovidRecord record)
        {
            record = record ?? throw new ArgumentNullException(nameof(record));
            var isCovidRecordPresentInCollection = this.CovidRecords.Remove(record);
            if (isCovidRecordPresentInCollection)
            {
                this.CovidRecords.Add(record);
            }

            return isCovidRecordPresentInCollection;
        }

        /// <summary>Replaces all current records with a new covid collection.
        /// <code>Postcondition: CovidRecord == covidRecords</code>
        /// </summary>
        /// <param name="covidRecords">The covid records.</param>
        public void ReplaceAllWithNewCovidCollection(ICollection<CovidRecord> covidRecords)
        {
            this.CovidRecords = covidRecords.ToList();
        }

        /// <summary>Clones this instance.</summary>
        /// <returns>The Cloned instance of the covid data collection</returns>
        public CovidDataCollection Clone()
        {
            var clonedCollection = new CovidDataCollection();
            foreach (var record in this.CovidRecords)
            {
                clonedCollection.Add(record);
            }

            return clonedCollection;
        }


        /// <summary>Adds all.
        /// <code>Precondition: covidRecords != null</code>
        /// <code>Postcondition: CovidRecords.Count() += covidRecords.Count()</code>
        /// </summary>
        /// <param name="covidRecords">The covid records.</param>
        /// <exception cref="ArgumentNullException">covidRecords</exception>
        public void AddAll(IEnumerable<CovidRecord> covidRecords)
        {
            covidRecords = covidRecords ?? throw new ArgumentNullException(nameof(covidRecords));
            foreach (var record in covidRecords)
            {
                if (!this.ReplaceDuplicateRecords(record))
                {
                    this.Add(record);
                }
            }
        }


        /// <summary>Creates a filtered collection by state.</summary>
        /// <param name="stateFilter">The state filter.</param>
        /// <returns>the filtered collection by state if state is valid, and the original CovidData if state is invalid</returns>
        public List<CovidRecord> CreateAFilteredCollection(string stateFilter)
        {
            stateFilter = stateFilter ?? throw new ArgumentNullException(nameof(stateFilter));
            var collectionOfStates = this.CovidRecords.Select(record => record.State);
            var isStateNotPresent = !collectionOfStates.Contains(stateFilter.ToUpper());
            var isStateNotValid = !FormatValidator.IsStateLabelValid(stateFilter) || isStateNotPresent;

            if (isStateNotValid)
            {
                return this.CovidRecords.ToList();
            }

            var filteredList = this.CovidRecords.Where(record => record.State.Equals(stateFilter, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return filteredList;
        }

        #endregion

        #region Inherited Methods

        /// <Summary>
        ///   <para>
        /// Gets the number of elements contained in the CovidDataCollection.
        /// </para>
        /// </Summary>
        public int Count => this.CovidRecords.Count;

        /// <Summary>Gets a value indicating whether the CovidDataCollection is read-only.</Summary>
        /// <exception cref="NotImplementedException">
        ///   <br />
        /// </exception>
        public bool IsReadOnly => this.CovidRecords.IsReadOnly;

        /// <Summary>Adds an item to the CovidDataCollection .</Summary>
        /// <param name="item">The object to add to the CovidDataCollection .</param>
        public void Add(CovidRecord item)
        {
            this.CovidRecords.Add(item);
        }

        /// <Summary>Removes all items from the CovidDataCollection  .</Summary>
        public void Clear()
        {
            this.CovidRecords.Clear();
        }

        /// <Summary>Determines whether this instance contains the object.</Summary>
        /// <param name="item">The object to locate in the CovidDataCollection  .</param>
        /// <returns>true if item is found in the CovidDataCollection ; otherwise, false.</returns>
        public bool Contains(CovidRecord item)
        {
            return this.CovidRecords.Contains(item);
        }

        /// <Summary>
        /// Copies the elements of the CovidDataCollection to an <a onclick="return false;" href="T:System.Array" originaltag="see">Array</a>, starting at a particular <a onclick="return false;" href="T:System.Array" originaltag="see">Array</a> index.
        /// </Summary>
        /// <param name="array">
        /// The one-dimensional <a onclick="return false;" href="T:System.Array" originaltag="see">Array</a> that is the destination of the elements copied from CovidDataCollection  . The <a onclick="return false;" href="T:System.Array" originaltag="see">Array</a> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(CovidRecord[] array, int arrayIndex)
        {
            this.CovidRecords.CopyTo(array, arrayIndex);
        }

        /// <Summary>Returns an enumerator that iterates through the collection.</Summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<CovidRecord> GetEnumerator()
        {
            return this.CovidRecords.GetEnumerator();
        }

        /// <Summary>Removes the first occurrence of a specific object from the CovidDataCollection.</Summary>
        /// <param name="item">The object to remove from the CovidDataCollection.</param>
        /// <returns>true if item was successfully removed from the CovidDataCollection; otherwise, false. This method also returns false if item is not found in the original CovidDataCollection.</returns>
        public bool Remove(CovidRecord item)
        {
            return this.CovidRecords.Remove(item);
        }

        /// <Summary>
        /// Returns an enumerator that iterates through a collection.
        /// </Summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.CovidRecords.AsEnumerable().GetEnumerator();
        }
        #endregion
    }
}
