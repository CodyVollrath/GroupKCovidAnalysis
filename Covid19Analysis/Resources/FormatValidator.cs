using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Covid19Analysis.Resources
{
    /// <Summary>
    /// This class serves as a set of static methods to validate formats for strings
    /// </Summary>
    public class FormatValidator
    {
        #region Date Formats

        /// <Summary>
        /// Determines if a date is formatted in MM/dd/yyyy
        /// </Summary>
        /// <param name="dateString">The date string.</param>
        /// <returns>
        /// <c>true</c> if dateString is of MM/dd/yyyy format; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDateStringValid(string dateString)
        {
            if (dateString == null )
            {
                return false;
            }
            var isValidDateTime = DateTime.TryParseExact(dateString, Assets.DateStringUnformatted,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            return isValidDateTime;
        }

        #endregion

        #region Numeric Formats
        /// <Summary>
        /// Determines whether numericString is a number
        /// <code>Precondition: numericString != null</code>
        /// </Summary>
        /// <param name="numericString">The numeric string.</param>
        /// <returns>
        ///   <c>true</c> if numericString is a number or is empty or null; otherwise, <c>false</c>.</returns>
        public static bool IsNumericStringValid(string numericString)
        {
            if (numericString == null)
            {
                return true;
            }

            if (numericString.Equals(string.Empty))
            {
                return true;
            }

            var numberValidator = new Regex("[-]?\\d+");
            return numberValidator.IsMatch(numericString);
        }
        #endregion

        #region State Label Formats

        /// <Summary>
        /// Determines whether the state label is valid.
        /// <example>Valid State Labels: GA, AZ, AL</example>
        /// </Summary>
        /// <param name="stateLabel">The state label.</param>
        /// <returns>
        ///   <c>true</c> if state label is valid; otherwise, <c>false</c>.</returns>
        public static bool IsStateLabelValid(string stateLabel)
        {
            if (stateLabel == null)
            {
                return false;
            }
            var stateLabelValidator = new Regex("^([A-Z]{2})|([a-z]{2})$");
            return stateLabelValidator.IsMatch(stateLabel);
        }
        #endregion
    }
}
