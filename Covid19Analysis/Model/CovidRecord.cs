using System;
using Covid19Analysis.Resources;

namespace Covid19Analysis.Model
{

    /// <Summary>This class keeps track of an individual covid record</Summary>
    public class CovidRecord
    {

        #region Properties
        /// <Summary>
        ///   <para>
        /// Gets the date as a DateTime object.
        /// </para>
        /// </Summary>
        /// <value>The date.</value>
        public DateTime Date { get; }


        /// <Summary>Gets the state.</Summary>
        /// <value>The state.</value>
        public string State { get; }


        /// <Summary>Gets the positive tests.</Summary>
        /// <value>The positive tests.</value>
        public int PositiveTests { get; set; }


        /// <Summary>Gets the negative tests.</Summary>
        /// <value>The negative tests.</value>
        public int NegativeTests { get; set; }


        /// <summary>Gets the total tests.</summary>
        /// <value>The total tests.</value>
        public int TotalTests => this.PositiveTests + this.NegativeTests;

        /// <Summary>Gets the deaths.</Summary>
        /// <value>The deaths.</value>
        public int Deaths { get; set; }


        /// <Summary>Gets the hospitalizations.</Summary>
        /// <value>The hospitalizations.</value>
        public int Hospitalizations { get; set; }


        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <a onclick="return false;" href="CovidRecord" originaltag="see">CovidRecord</a> class.</summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="state">The state.</param>
        /// <exception cref="ArgumentNullException">state</exception>
        public CovidRecord(DateTime dateTime, string state)
        {
            this.Date = dateTime;
            state = state ?? throw new ArgumentNullException(nameof(state));
            this.State = state.ToUpper();
        }
        #endregion

        #region Public Methods

        /// <summary>Converts to a string.</summary>
        /// <returns>A <a onclick="return false;" href="System.String" originaltag="see">System.String</a> that represents this instance.</returns>
        public override string ToString()
        {
            return $"Date: {this.Date.ToString(Assets.DateStringFormatted)}, State: {this.State}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Tuple.Create(this.Date, this.State).GetHashCode();
        }

        /// <summary>Determines whether the specified <a onclick="return false;" href="System.Object" originaltag="see">System.Object</a>, is equal to this instance.</summary>
        /// <param name="obj">The <a onclick="return false;" href="System.Object" originaltag="see">System.Object</a> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <a onclick="return false;" href="System.Object" originaltag="see">System.Object</a> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var otherRecord = (CovidRecord) obj;
            return this.Date.Equals(otherRecord.Date) && this.State.Equals(otherRecord.State);
        }

        #endregion
    }
}
