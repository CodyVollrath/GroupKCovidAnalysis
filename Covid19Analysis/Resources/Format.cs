using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Covid19Analysis.Resources
{
    /// <Summary>This class holds static formatters that are commonly used in the application</Summary>
    public class Format
    {
        #region Date Formatters

        /// <Summary>
        ///     Formats as date time.
        ///     <code>Precondition: dateString != null</code>
        /// </Summary>
        /// <param name="dateString">The date string.</param>
        /// <param name="format">The format to format the date time by</param>
        /// <returns>The date time object</returns>
        /// <exception cref="ArgumentNullException">dateString</exception>
        public static DateTime FormatAsDateTime(string dateString, string format = Assets.DateStringUnformatted)
        {
            dateString = dateString ?? throw new ArgumentNullException(nameof(dateString));
            return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
        }

        public static string GetMonthAndYearFromDateTime(DateTime date)
        {
            var monthName = GetMonthNameFromNumber(date.Month);
            var year = date.Year;

            return $"{monthName} {year}";
        }

        /// <Summary>Gets the day of month with the ordinal indicator.</Summary>
        /// <param name="date">The date.</param>
        /// <returns>the formatted string of the day of that month with its ordinal attached</returns>
        public static string GetDayOfMonthWithOrdinalIndicator(DateTime date)
        {
            var dayOfMonth = date.Day.ToString();
            return $"{dayOfMonth}{determineOrdinalIndicator(dayOfMonth)}";
        }

        /// <Summary>Gets the month name from month number.</Summary>
        /// <param name="monthNumber">The month number</param>
        /// <returns>The name of the month for the month number passed in</returns>
        public static string GetMonthNameFromNumber(int monthNumber)
        {
            var monthName =
                new DateTime(Assets.DefaultYear, monthNumber, Assets.DefaultDay).ToString(Assets.MonthNameIdentifier);
            return monthName;
        }

        /// <summary>Gets the list of days with ordinals.</summary>
        /// <param name="dates">The dates.</param>
        /// <returns>The formatted string of the numeric values with the ordinal suffix appended to each numeric instance.</returns>
        public static string GetListOfDaysWithOrdinals(IEnumerable<DateTime> dates)
        {
            var listOfDays = (from date in dates select GetDayOfMonthWithOrdinalIndicator(date)).ToList();
            return listOfDays.Aggregate(string.Empty,
                (current, day) => determineDayListOutput(day, listOfDays, current));
        }

        #endregion

        #region Number Formatters

        /// <Summary>
        ///     Formats the string to integer.
        ///     <code>Precondition: numericValue != null AND is a valid number</code>
        /// </Summary>
        /// <param name="numericValue">The numeric value.</param>
        /// <returns>The integer representation of that string</returns>
        public static int FormatStringToInteger(string numericValue)
        {
            if (!FormatValidator.IsNumericStringValid(numericValue))
            {
                throw new ArgumentException($"{nameof(numericValue)} is not a valid number");
            }

            if (numericValue == null)
            {
                return 0;
            }

            if (numericValue.Equals(string.Empty))
            {
                return 0;
            }

            var number = int.Parse(numericValue);
            if (number < 0)
            {
                number = 0;
            }

            return number;
        }

        /// <Summary>Formats the integer to a double.</Summary>
        /// <param name="numericValue">The numeric value.</param>
        /// <returns>The double representation of that integer</returns>
        public static double FormatIntegerToDouble(int numericValue)
        {
            return Convert.ToDouble(numericValue);
        }

        /// <Summary>Formats the string with numeric value as formatted string value with commas separating thousands.</Summary>
        /// <param name="numericValue">The numeric value.</param>
        /// <returns>The formatted integer value with commas separating thousands</returns>
        public static string FormatIntegerAsFormattedString(int numericValue)
        {
            return $"{numericValue:##,##0}";
        }

        /// <Summary>Formats the averages with two decimal places.</Summary>
        /// <param name="numericValue">The numeric value.</param>
        /// <returns>The formatted double as a number with two decimal places and thousands place separated by comma</returns>
        public static string FormatAveragesWithTwoDecimalPlaces(double numericValue)
        {
            return $"{numericValue:N}";
        }

        /// <Summary>Formats the numeric value as percentage.</Summary>
        /// <param name="numericValue">The numeric value.</param>
        /// <returns>The formatted double as a percentage</returns>
        public static string FormatNumericValueAsPercentage(double numericValue)
        {
            return $"{numericValue:P}";
        }

        #endregion

        #region Private Helpers

        private static string determineDayListOutput(string day, IReadOnlyList<string> listOfDays, string daysString)
        {
            var isListOfDaysGreaterThanOne = listOfDays.Count > 1;
            var isLastDay = day.Equals(listOfDays[listOfDays.Count - 1]);
            if (!isListOfDaysGreaterThanOne || isLastDay)
            {
                daysString += $"{day}";
            }
            else
            {
                var isNextToLastDay = day.Equals(listOfDays[listOfDays.Count - 2]);
                if (isNextToLastDay)
                {
                    daysString += $"{day}, and ";
                }
                else
                {
                    daysString += $"{day},";
                }
            }

            return daysString;
        }

        private static string determineOrdinalIndicator(string numericValue)
        {
            var ordinal = "th";
            var number = int.Parse(numericValue);
            var remainder = number % 100;
            switch (number % 10)
            {
                case 1:
                    ordinal = "st";
                    break;
                case 2:
                    ordinal = "nd";
                    break;
                case 3:
                    ordinal = "rd";
                    break;
            }

            if (remainder >= 11 && remainder <= 13)
            {
                ordinal = "th";
            }

            return ordinal;
        }

        #endregion
    }
}