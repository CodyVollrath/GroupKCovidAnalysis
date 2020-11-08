using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Covid19Analysis.Resources;

namespace Covid19Analysis.Model
{
    /// <Summary>
    ///     Author: Cody Vollrath, Eboni Walker
    ///     This class keeps a collection of covid records
    /// </Summary>
    public class CovidDataCollection : ICollection<CovidRecord>, IXmlSerializable
    {
        #region Data members

        #region Private Members

        private ICollection<CovidRecord> covidRecords;

        #endregion

        #endregion

        #region Properties

        /// <summary>Gets or sets the <see cref="CovidRecord" /> at the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <value>The <see cref="CovidRecord" />.</value>
        /// <returns>The element at the specified index</returns>
        public CovidRecord this[int index]
        {
            get => ((List<CovidRecord>) this.covidRecords)[index];
            set => ((List<CovidRecord>) this.covidRecords)[index] = value;
        }

        /// <Summary>
        ///     <para>
        ///         Gets the number of elements contained in the CovidDataCollection.
        ///     </para>
        /// </Summary>
        public int Count => this.covidRecords.Count;

        /// <Summary>Gets a value indicating whether the CovidDataCollection is read-only.</Summary>
        /// <exception cref="NotImplementedException">
        ///     <br />
        /// </exception>
        public bool IsReadOnly => this.covidRecords.IsReadOnly;

        #endregion

        #region Constructors

        /// <Summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="CovidDataCollection" originaltag="see">CovidDataCollection</a> class.
        ///     <code>Postcondition: covidRecords = new List(Type=CovidRecord)</code>
        /// </Summary>
        public CovidDataCollection()
        {
            this.covidRecords = new List<CovidRecord>();
        }

        #endregion

        #region Methods

        /// <Summary>Adds an item to the CovidDataCollection .</Summary>
        /// <param name="item">The object to add to the CovidDataCollection .</param>
        public void Add(CovidRecord item)
        {
            this.covidRecords.Add(item);
        }

        /// <Summary>Removes all items from the CovidDataCollection  .</Summary>
        public void Clear()
        {
            this.covidRecords.Clear();
        }

        /// <Summary>Determines whether this instance contains the object.</Summary>
        /// <param name="item">The object to locate in the CovidDataCollection  .</param>
        /// <returns>true if item is found in the CovidDataCollection ; otherwise, false.</returns>
        public bool Contains(CovidRecord item)
        {
            return this.covidRecords.Contains(item);
        }

        /// <Summary>
        ///     Copies the elements of the CovidDataCollection to an
        ///     <a onclick="return false;" href="T:System.Array" originaltag="see">Array</a>, starting at a particular
        ///     <a onclick="return false;" href="T:System.Array" originaltag="see">Array</a> index.
        /// </Summary>
        /// <param name="array">
        ///     The one-dimensional <a onclick="return false;" href="T:System.Array" originaltag="see">Array</a> that is the
        ///     destination of the elements copied from CovidDataCollection  . The
        ///     <a onclick="return false;" href="T:System.Array" originaltag="see">Array</a> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(CovidRecord[] array, int arrayIndex)
        {
            this.covidRecords.CopyTo(array, arrayIndex);
        }

        /// <Summary>Returns an enumerator that iterates through the collection.</Summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<CovidRecord> GetEnumerator()
        {
            return this.covidRecords.GetEnumerator();
        }

        /// <Summary>Removes the first occurrence of a specific object from the CovidDataCollection.</Summary>
        /// <param name="item">The object to remove from the CovidDataCollection.</param>
        /// <returns>
        ///     true if item was successfully removed from the CovidDataCollection; otherwise, false. This method also returns
        ///     false if item is not found in the original CovidDataCollection.
        /// </returns>
        public bool Remove(CovidRecord item)
        {
            return this.covidRecords.Remove(item);
        }

        /// <Summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </Summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.covidRecords.AsEnumerable().GetEnumerator();
        }

        /// <summary>
        ///     This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return
        ///     null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the
        ///     <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"></see> to the class.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.Schema.XmlSchema"></see> that describes the XML representation of the object that is
        ///     produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"></see>
        ///     method and consumed by the
        ///     <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"></see> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        ///     Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"></see> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            var deserializer = new XmlSerializer(typeof(CovidRecord));
            reader.ReadStartElement("CovidDataCollection");
            reader.ReadStartElement("Count");
            var count = reader.ReadContentAsInt();
            reader.ReadEndElement();

            for (var i = 0; i < count; i++)
            {
                var record = (CovidRecord) deserializer.Deserialize(reader);
                this.covidRecords.Add(record);
            }

            reader.ReadEndElement();
        }

        /// <summary>
        ///     Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"></see> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            var serializer = new XmlSerializer(typeof(CovidRecord));

            writer.WriteStartElement("Count");
            writer.WriteString(this.Count.ToString());
            writer.WriteEndElement();

            foreach (var record in this.covidRecords)
            {
                serializer.Serialize(writer, record);
            }
        }

        /// <Summary>
        ///     Replaces the record with the matching date and state and then swaps it for the record that is passed in.
        ///     <code>Precondition: record != null</code>
        /// </Summary>
        /// <param name="record">The record.</param>
        /// <returns>True if the record was replaced, otherwise False.</returns>
        /// <exception cref="ArgumentNullException">record</exception>
        public bool ReplaceDuplicateRecords(CovidRecord record)
        {
            record = record ?? throw new ArgumentNullException(nameof(record));
            var isCovidRecordPresentInCollection = this.covidRecords.Remove(record);
            if (isCovidRecordPresentInCollection)
            {
                this.covidRecords.Add(record);
            }

            return isCovidRecordPresentInCollection;
        }

        /// <summary>
        ///     Replaces all current records with a new covid collection.
        ///     <code>Postcondition: covidRecords == newCovidRecords</code>
        /// </summary>
        /// <param name="newCovidRecords">The covid records.</param>
        public void ReplaceAllWithNewCovidCollection(ICollection<CovidRecord> newCovidRecords)
        {
            this.covidRecords = newCovidRecords.ToList();
        }

        /// <summary>Clones this instance.</summary>
        /// <returns>The Cloned instance of the covid data collection</returns>
        public CovidDataCollection Clone()
        {
            var clonedCollection = new CovidDataCollection();
            foreach (var record in this.covidRecords)
            {
                clonedCollection.Add(record);
            }

            return clonedCollection;
        }

        /// <summary>
        ///     Adds all records if they do not exist, otherwise it replaces the current record.
        ///     <code>Precondition: newCovidRecords != null</code>
        ///     <code>Postcondition: covidRecords.Count() += newCovidRecords.Count()</code>
        /// </summary>
        /// <param name="newCovidRecords">The covid records.</param>
        /// <exception cref="ArgumentNullException">newCovidRecords</exception>
        public void AddAll(IEnumerable<CovidRecord> newCovidRecords)
        {
            newCovidRecords = newCovidRecords ?? throw new ArgumentNullException(nameof(newCovidRecords));
            foreach (var record in newCovidRecords)
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
            var collectionOfStates = this.covidRecords.Select(record => record.State);
            var isStateNotPresent = !collectionOfStates.Contains(stateFilter.ToUpper());
            var isStateNotValid = !FormatValidator.IsStateLabelValid(stateFilter) || isStateNotPresent;

            if (isStateNotValid)
            {
                return this.covidRecords.ToList();
            }

            var filteredList = this.covidRecords.Where(record =>
                record.State.Equals(stateFilter, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return filteredList;
        }

        /// <summary>Removes all covid records in a list.</summary>
        /// <param name="otherCovidRecords">The covid records.</param>
        public void RemoveAllCovidRecordsInAList(ICollection<CovidRecord> otherCovidRecords)
        {
            foreach (var record in otherCovidRecords)
            {
                this.Remove(record);
            }
        }

        /// <summary>
        ///     Removes the state of the covid records by missing records with the same state.
        ///     The Collection passed in must have only one state represented
        ///     Will go by the first record state value.
        ///     <code>Precondition: filteredCovidCollection != null</code>
        ///     <code>Postcondition: CovidRecords removes all missing records from filteredCovidCollection</code>
        /// </summary>
        /// <param name="filteredCovidCollection">The filtered covid collection</param>
        public void RemoveCovidRecordsByMissingRecordsWithTheSameState(ICollection<CovidRecord> filteredCovidCollection)
        {
            filteredCovidCollection = filteredCovidCollection ??
                                      throw new ArgumentNullException(nameof(filteredCovidCollection));
            if (!filteredCovidCollection.Any())
            {
                return;
            }

            var stateFilter = filteredCovidCollection.First().State;
            var recordsByState = this.covidRecords.Where(record => record.State.Equals(stateFilter)).ToList();

            var missingRecordsFromFilteredCollection =
                recordsByState.Where(record => !filteredCovidCollection.Contains(record)).ToList();

            foreach (var record in missingRecordsFromFilteredCollection)
            {
                this.Remove(record);
            }
        }

        #endregion
    }
}