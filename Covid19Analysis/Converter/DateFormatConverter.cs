using System;
using Windows.UI.Xaml.Data;
using Covid19Analysis.Resources;

namespace Covid19Analysis.Converter
{
    /// <summary>
    ///     Author: Cody Vollrath
    ///     The DateFormatConverter converts a DateTime into a formatted date string and vice versa
    /// </summary>
    public class DateFormatConverter : IValueConverter
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
            var date = (DateTime) value;
            return $"Date:{date.ToString(Assets.DateStringFormatted)}";
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
            var dateString = (string) value;
            var index = dateString.IndexOf(':');
            var parsedDate = index > 0 ? dateString.Remove(index) : dateString;

            var date = Format.FormatAsDateTime(parsedDate, Assets.DateStringFormatted);
            return date;
        }

        #endregion
    }
}