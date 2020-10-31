﻿using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Covid19Analysis.Model;
using Covid19Analysis.Resources;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Covid19Analysis.View
{

    /// <summary>This class is responsible for the logic behind the addition of a CovidRecord to the data set within the application</summary>
    public sealed partial class CovidRecordAdder
    {
        #region Properties

        /// <summary>Gets the created record.</summary>
        /// <value>The created record.</value>
        public CovidRecord CreatedRecord { get; private set; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CovidRecordAdder" /> class.</summary>
        public CovidRecordAdder()
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
        }

        #endregion

        #region Events

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var date = this.covidRecordDate.Date.Date;
            var state = this.stateTextBox.Text;
            var positiveCases = Format.FormatStringToInteger(this.positiveCasesTextBox.Text);
            var negativeCases = Format.FormatStringToInteger(this.negativeCasesTextBox.Text);
            var deaths = Format.FormatStringToInteger(this.deathsTextBox.Text);
            var hospitalizations = Format.FormatStringToInteger(this.hospitalizationsTextBox.Text);

            this.CreatedRecord = new CovidRecord(date, state)
            {
                PositiveTests = positiveCases,
                NegativeTests = negativeCases,
                Deaths = deaths,
                Hospitalizations = hospitalizations
            };
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void stateTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(char.IsDigit);
            sender.Text = args.NewText.ToUpper();
            sender.Select(sender.Text.Length, 0);
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
        private void stateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.stateTextBox.Text.Equals(string.Empty) || this.stateTextBox.Text.Length != 2)
            {
                this.stateTextBox.Text = Assets.GeorgiaFilterValue;
            }
        }

        private void covidRecordDate_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if (this.covidRecordDate.SelectedDate != null)
            {
                this.IsPrimaryButtonEnabled = true;
            }
        }


        #endregion


    }
}
