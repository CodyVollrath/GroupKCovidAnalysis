using System;
using System.IO;
using System.Linq;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Covid19Analysis.Resources;
using Covid19Analysis.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Covid19Analysis.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CovidListViewPage : Page
    {
        private readonly CovidAnalysisViewModel covidAnalysisViewModel;
        public CovidListViewPage()
        {
            this.InitializeComponent();
            this.covidAnalysisViewModel = new CovidAnalysisViewModel();
            this.DataContext = this.covidAnalysisViewModel;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), this.covidAnalysisViewModel);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = e.Parameter;
            if (parameter != null && !parameter.ToString().Equals(string.Empty))
            {
                var covidViewModel = (CovidAnalysisViewModel) parameter;
                this.covidAnalysisViewModel.CovidData = covidViewModel.CovidData;
            }
        }

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

        private void hospitalizedCurrentlyTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
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
    }
}
