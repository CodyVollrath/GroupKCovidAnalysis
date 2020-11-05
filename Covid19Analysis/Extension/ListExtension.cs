using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Covid19Analysis.Extension
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
    }
}
