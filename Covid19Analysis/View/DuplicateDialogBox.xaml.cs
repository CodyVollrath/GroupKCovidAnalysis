using System;
using Windows.UI.Xaml.Controls;
using Covid19Analysis.Model;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Covid19Analysis.View
{
    /// <summary>This content dialog box is responsible for displaying a pop up for duplicate records.</summary>
    public sealed partial class DuplicateDialogBox
    {
        #region Properties

        /// <Summary>Gets a value indicating whether the chosen user action will continue for all items in the data table</Summary>
        /// <value>
        ///     <c>true</c> if checkbox is checked;otherwise, <c>false</c>.
        /// </value>
        public bool DoAll { get; private set; }

        /// <Summary>Signifies that the action being performed is replace or keep</Summary>
        /// <value>
        ///     <c>true</c> if the user selects replace; otherwise, <c>false</c>.
        /// </value>
        public bool Replace { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <a onclick="return false;" href="DuplicateDialogBox" originaltag="see">DuplicateDialogBox</a> class.
        /// </summary>
        public DuplicateDialogBox()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>Sets the duplicate record string representation to the current value of the covidRecordTextBlock.</summary>
        /// <param name="covidRecord">The covid record.</param>
        /// <exception cref="ArgumentNullException">covidRecord</exception>
        public void SetDuplicateRecord(CovidRecord covidRecord)
        {
            covidRecord = covidRecord ?? throw new ArgumentNullException(nameof(covidRecord));
            this.covidRecordTextBlock.Text = covidRecord.ToString();
        }

        #endregion

        #region Private Helpers

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.determineIfCheckBoxIsChecked();
            this.Replace = true;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.determineIfCheckBoxIsChecked();
            this.Replace = false;
        }

        private void determineIfCheckBoxIsChecked()
        {
            var isChecked = this.performActionForAll.IsChecked;
            if (isChecked != null)
            {
                this.DoAll = isChecked.Value;
            }
        }

        #endregion
    }
}