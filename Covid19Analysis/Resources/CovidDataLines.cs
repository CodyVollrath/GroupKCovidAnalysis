using System;

namespace Covid19Analysis.Resources
{

    /// <summary>
    /// This class arranges covid data into output values to be used by summary classes
    /// </summary>
    public class CovidDataLines
    {

        /// <summary>Gets the covid line for value and date.</summary>
        /// <param name="label">The label.</param>
        /// <param name="value">The value.</param>
        /// <param name="date">The date.</param>
        /// <returns>A formatted string with the value and date represented within it</returns>
        public static string GetCovidLineForValueAndDate(string label, string value, string date)
        {
            return $"{label} {value} {Assets.DateOfOccurrenceLabel} {date}{Environment.NewLine}";
        }

        /// <summary>Gets the output line for value and days of the month represented as a string.</summary>
        /// <param name="label">The label.</param>
        /// <param name="value">The value.</param>
        /// <param name="days">The days.</param>
        /// <returns>The formatted string with the value and the days that the value occurred in</returns>
        public static string GetCovidLineForValueAndDaysOfMonth(string label, string value, string days)
        {
            return $"{label} {value} {Assets.DayOfOccurrenceMonthlyLabel} {days} {Environment.NewLine}";
        }

        /// <summary>Gets the covid line for a single value.</summary>
        /// <param name="label">The label.</param>
        /// <param name="value">The value.</param>
        /// <returns>Gets the formatted string for a value</returns>
        public static string GetCovidLineForValue(string label, string value)
        {
            return $"{label} {value} {Environment.NewLine}";
        }

        /// <summary>Gets the covid line for value with threshold set.</summary>
        /// <param name="label">The label.</param>
        /// <param name="value">The value.</param>
        /// <param name="threshold">The threshold.</param>
        /// <returns>The formatted string with the threshold and the value that meets the threshold requirements</returns>
        public static string GetCovidLineForValueWithThreshold(string label, string value, string threshold)
        {
            return $"{label} {threshold}: {value}{Environment.NewLine}";
        }

    }
}
