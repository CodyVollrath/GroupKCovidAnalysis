using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Covid19Analysis.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CovidListViewPage : Page
    {
        #region Constructors

        public CovidListViewPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void deleteCovidRecord_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void editCovidRecord_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}