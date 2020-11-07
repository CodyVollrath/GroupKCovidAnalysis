using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Covid19Analysis.Extension
{

    /// <summary>
    /// Author: Cody Vollrath
    /// This class allows for conversion of an enumerable inherited object to be converted to an observable collection
    /// </summary>
    public static class ListExtensions
    {
        #region Methods

        /// <summary>Converts to ObservableCollection.</summary>
        /// <typeparam name="T">Any Object Type</typeparam>
        /// <param name="collection">The collection of any object type</param>
        /// <returns>a new ObservableCollection of the object type</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }

        #endregion
    }
}