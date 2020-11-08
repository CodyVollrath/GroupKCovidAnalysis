using System;
using Windows.UI.Xaml.Data;

namespace Covid19Analysis.Converter
{
    /// <summary>
    ///     Author: Cody Vollrath
    ///     This class converts PositiveCase data from int to formatted string and vice versa
    /// </summary>
    public class PositiveCasesFormatConverter : IValueConverter
    {
        #region Methods

        /// <summary>Converts the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>
        ///     <br />
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var positiveCases = (int) value;

            var positiveCaseLabel = $"Positive Cases:{positiveCases}";

            return positiveCaseLabel;
        }

        /// <summary>Converts the back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>
        ///     <br />
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var positiveCasesString = (string) value;

            var index = positiveCasesString.IndexOf(':');
            var parsedPositives = index > 0 ? positiveCasesString.Remove(index) : positiveCasesString;

            var positives = int.Parse(parsedPositives);
            return positives;
        }

        #endregion
    }
}