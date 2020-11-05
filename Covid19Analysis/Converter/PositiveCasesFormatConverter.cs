using System;
using Windows.UI.Xaml.Data;

namespace Covid19Analysis.Converter
{
    public class PositiveCasesFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var positiveCases = (int) value;

            var positiveCaseLabel = $"Positive Cases:{positiveCases}";

            return positiveCaseLabel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var positiveCasesString = (string) value;

            var index = positiveCasesString.IndexOf(':');
            var parsedPositives = index > 0 ? positiveCasesString.Remove(index) : positiveCasesString;

            var positives = int.Parse(parsedPositives);
            return positives;
        }
    }
}
