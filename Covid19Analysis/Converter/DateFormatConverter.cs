using System;
using Windows.UI.Xaml.Data;
using Covid19Analysis.Resources;
namespace Covid19Analysis.Converter
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var date = (DateTime) value;
            return $"Date:{date.ToString(Assets.DateStringFormatted)}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var dateString = (string) value;
            var index = dateString.IndexOf(':');
            var parsedDate = index > 0 ? dateString.Remove(index) : dateString;

            var date = Format.FormatAsDateTime(parsedDate, Assets.DateStringFormatted);
            return date;
        }
    }
}
