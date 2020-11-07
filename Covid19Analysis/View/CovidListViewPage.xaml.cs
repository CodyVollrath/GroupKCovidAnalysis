using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Covid19Analysis.ViewModel;

namespace Covid19Analysis.View
{
    /// <summary>
    ///     A page that displays the list and detail view
    /// </summary>
    public sealed partial class CovidListViewPage
    {
        #region Data members

        #region Private Members

        private readonly CovidAnalysisViewModel covidAnalysisViewModel;

        #endregion

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CovidListViewPage" /> class.</summary>
        public CovidListViewPage()
        {
            this.InitializeComponent();
            this.covidAnalysisViewModel = new CovidAnalysisViewModel();
            DataContext = this.covidAnalysisViewModel;
            this.disableTextBoxes();
        }

        #endregion

        #region Methods

        #region Appbar Button Events

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), this.covidAnalysisViewModel);
        }

        #endregion

        /// <summary>Invoked when the Page is loaded and becomes the current source of a parent Frame.</summary>
        /// <param name="e">
        ///     Event data that can be examined by overriding code. The event data is representative of the pending navigation that
        ///     will load the current Page. Usually the most relevant property to examine is Parameter.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = e.Parameter;
            if (parameter == null || parameter.ToString().Equals(string.Empty))
            {
                return;
            }

            var covidViewModel = (CovidAnalysisViewModel) parameter;
            this.covidAnalysisViewModel.CovidDataRecords = covidViewModel.CovidDataRecords;
        }

        #endregion

        #region UI Events

        private void positiveCasesTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void negativeCasesTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void deathsTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void hospitalizationsTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void hospitalizedCurrentlyTextBox_BeforeTextChanging(TextBox sender,
            TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void positiveCasesTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.positiveCasesTextBox.Text.Equals(string.Empty))
            {
                this.positiveCasesTextBox.Text = "0";
            }
        }

        private void negativeCasesTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.negativeCasesTextBox.Text.Equals(string.Empty))
            {
                this.negativeCasesTextBox.Text = "0";
            }
        }

        private void deathsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.deathsTextBox.Text.Equals(string.Empty))
            {
                this.deathsTextBox.Text = "0";
            }
        }

        private void hospitalizationsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.hospitalizationsTextBox.Text.Equals(string.Empty))
            {
                this.hospitalizationsTextBox.Text = "0";
            }
        }

        private void hospitalizedCurrentlyTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.hospitalizedCurrentlyTextBox.Text.Equals(string.Empty))
            {
                this.hospitalizedCurrentlyTextBox.Text = "0";
            }
        }

        private void covidRecordsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.enableTextBoxes();
        }

        #endregion

        #region Private Helpers

        private void disableTextBoxes()
        {
            this.positiveCasesTextBox.IsEnabled = false;
            this.negativeCasesTextBox.IsEnabled = false;
            this.deathsTextBox.IsEnabled = false;
            this.hospitalizationsTextBox.IsEnabled = false;
            this.hospitalizedCurrentlyTextBox.IsEnabled = false;
        }

        private void enableTextBoxes()
        {
            this.positiveCasesTextBox.IsEnabled = true;
            this.negativeCasesTextBox.IsEnabled = true;
            this.deathsTextBox.IsEnabled = true;
            this.hospitalizationsTextBox.IsEnabled = true;
            this.hospitalizedCurrentlyTextBox.IsEnabled = true;
        }

        #endregion
    }
}