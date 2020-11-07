using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Covid19Analysis.Extension;
using Covid19Analysis.Model;
using Covid19Analysis.OutputFormatter;
using Covid19Analysis.Resources;
using Covid19Analysis.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Covid19Analysis.View
{
    /// <Summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </Summary>
    public sealed partial class MainPage
    {
        #region Data members

        /// <Summary>
        ///     The application height
        /// </Summary>
        public const int ApplicationHeight = 530;

        /// <Summary>
        ///     The application width
        /// </Summary>
        public const int ApplicationWidth = 620;

        #region Static Members

        /// <summary>
        ///     The state
        /// </summary>
        public static StateAbbreviations State;

        #endregion

        #endregion

        #region Constructors

        /// <Summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </Summary>
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
            this.covidDataAssembler = new CovidDataAssembler(StateAbbreviations.GA);
            this.covidViewModel = new CovidAnalysisViewModel();
            this.statesComboBox.ItemsSource = Enum.GetNames(typeof(StateAbbreviations));
            State = StateAbbreviations.GA;
            this.statesComboBox.SelectedValue = Enum.GetName(typeof(StateAbbreviations), StateAbbreviations.GA);
            this.mergeOrReplaceDialog = new MergeOrReplaceDialog();
        }

        #endregion

        #region Private Members

        #endregion

        #region Private Members

        private readonly CovidDataAssembler covidDataAssembler;

        private readonly ContentDialog mergeOrReplaceDialog;

        private readonly CovidAnalysisViewModel covidViewModel;

        #endregion

        #region Action Events

        #region AppButtonEvents

        private async void loadFile_ClickAsync(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
                {ViewMode = PickerViewMode.Thumbnail, SuggestedStartLocation = PickerLocationId.DocumentsLibrary};
            openPicker.FileTypeFilter.Add(".csv");
            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add(".xml");
            var file = await openPicker.PickSingleFileAsync();
            if (file == null)
            {
                return;
            }

            if (file.FileType == ".xml")
            {
                CovidDataCollection xmlContent;
                var deserializer = new XmlSerializer(typeof(CovidDataCollection));
                using (var inStream = await file.OpenStreamForReadAsync())
                {
                    xmlContent = (CovidDataCollection) deserializer.Deserialize(inStream);
                }

                this.displayCovidData(xmlContent);
                this.noRecordsForThatState();
            }
            else
            {
                string fileContent;
                var stream = await file.OpenAsync(FileAccessMode.Read);
                using (var fileReader = new StreamReader(stream.AsStream()))
                {
                    fileContent = await fileReader.ReadToEndAsync();
                }

                this.displayCovidData(fileContent);
                this.noRecordsForThatState();
            }

            this.applyFilteredCollectionToViewModel();
        }

        private void applyFilteredCollectionToViewModel()
        {
            try
            {

                this.covidViewModel.CovidDataRecords =
                    this.covidDataAssembler.FilteredCovidDataCollection.ToObservableCollection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.setViewModelCovidRecordsToNull();
            }
        }

        private void errorLog_Click(object sender, RoutedEventArgs e)
        {
            this.summaryTextBox.Text = this.covidDataAssembler.GetCovidDataErrors();
        }

        private async void saveFile_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!this.covidDataAssembler.IsCovidDataLoaded)
            {
                return;
            }

            var savePicker = new FileSavePicker {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = $"Covid19Analysis_{DateTime.Now.ToString(Assets.TimeStamp)}"
            };
            savePicker.FileTypeChoices.Add("Plain Text", new List<string> {".txt"});
            savePicker.FileTypeChoices.Add("Csv Comma Delimited", new List<string> {".csv"});
            savePicker.FileTypeChoices.Add("Xml Data", new List<string> {".xml"});

            var file = await savePicker.PickSaveFileAsync();
            bool isFileSaved;

            if (file == null)
            {
                return;
            }

            if (file.FileType == ".xml")
            {
                isFileSaved = this.covidDataAssembler.WriteCovidDataToXmlFile(file);
            }
            else
            {
                isFileSaved = this.covidDataAssembler.WriteCovidDataToCsvFile(file);
            }

            showSaveSuccessfulPrompt(isFileSaved);
        }

        private void displaySummary_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (this.covidDataAssembler.IsCovidDataLoaded)
            {
                this.summaryTextBox.Text = this.covidDataAssembler.Summary;
            }
        }

        #endregion

        #region Elements on UI Events

        private void clearData_Click(object sender, RoutedEventArgs e)
        {
            this.covidDataAssembler.Reset();
            this.setViewModelCovidRecordsToNull();
            this.summaryTextBox.Text = string.Empty;
        }

        private void addCovidRecordButton_Click(object sender, RoutedEventArgs e)
        {
            this.addCovidRecord();
        }

        private void upperPositiveCaseTextBox_BeforeTextChanging(TextBox sender,
            TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void lowerPositiveCaseTextBox_BeforeTextChanging(TextBox sender,
            TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void binSizeTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void upperPositiveCaseTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var isUpperPositiveCaseTextBoxEmpty = this.upperPositiveCaseTextBox.Text.Equals(string.Empty);
            if (isUpperPositiveCaseTextBoxEmpty)
            {
                this.upperPositiveCaseTextBox.Text = Assets.DefaultGreaterThanThreshHold.ToString();
            }

            this.updateCovidData();
        }

        private void lowerPositiveCaseTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var isLowerPositiveCaseTextBoxEmpty = this.lowerPositiveCaseTextBox.Text.Equals(string.Empty);
            if (isLowerPositiveCaseTextBoxEmpty)
            {
                this.lowerPositiveCaseTextBox.Text = Assets.DefaultLessThanThreshold.ToString();
            }

            this.updateCovidData();
        }

        private void binSizeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var isBinSizeEmpty = this.binSizeTextBox.Text.Equals(string.Empty);
            var binSize = Format.FormatStringToInteger(this.binSizeTextBox.Text);
            var isBinLessThanOrEqualZero = binSize <= 0;
            if (isBinSizeEmpty || isBinLessThanOrEqualZero)
            {
                this.binSizeTextBox.Text = Assets.RangeWidth.ToString();
            }

            this.updateCovidData();
        }

        private void statesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedState = this.statesComboBox.SelectedValue;

            if (selectedState == null || this.covidDataAssembler == null)
            {
                return;
            }

            var selectedStateConverted = Enum.Parse<StateAbbreviations>(selectedState.ToString());
            State = selectedStateConverted;
            this.covidDataAssembler.StateFilter = selectedState.ToString();
            this.updateCovidData();
            this.noRecordsForThatState();
        }

        private void listViewToggle_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CovidListViewPage), this.covidViewModel);
        }

        #endregion

        #endregion

        #region Private Helpers

        private void displayCovidData(string textContent)
        {
            try
            {
                this.applyThresholds();
                this.applyBinSize();
                if (this.covidDataAssembler.IsCovidDataLoaded)
                {
                    this.promptMergeOrReplaceDialog(textContent);
                }
                else
                {
                    this.loadCovidData(textContent);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                this.summaryTextBox.Text = Assets.NoCovidDataText;
            }
        }

        private void displayCovidData(CovidDataCollection xmlContent)
        {
            try
            {
                this.applyThresholds();
                this.applyBinSize();
                if (this.covidDataAssembler.IsCovidDataLoaded)
                {
                    this.promptMergeOrReplaceDialog(xmlContent);
                }
                else
                {
                    this.loadCovidData(xmlContent);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                this.summaryTextBox.Text = Assets.NoCovidDataText;
            }
        }

        private async void promptMergeOrReplaceDialog(string textContent)
        {
            var answer = await this.mergeOrReplaceDialog.ShowAsync();
            switch (answer)
            {
                case ContentDialogResult.Primary:
                    this.mergeAndLoadCovidData(textContent, false);
                    break;
                case ContentDialogResult.Secondary:
                    this.loadCovidData(textContent);
                    break;
                case ContentDialogResult.None:
                    break;
                default:
                    this.mergeAndLoadCovidData(textContent, true);
                    break;
            }

            this.applyFilteredCollectionToViewModel();
        }

        private async void promptMergeOrReplaceDialog(CovidDataCollection xmlContent)
        {
            var answer = await this.mergeOrReplaceDialog.ShowAsync();
            switch (answer)
            {
                case ContentDialogResult.Primary:
                    this.mergeAndLoadCovidData(xmlContent, false);
                    break;
                case ContentDialogResult.Secondary:
                    this.loadCovidData(xmlContent);
                    break;
                case ContentDialogResult.None:
                    break;
                default:
                    this.mergeAndLoadCovidData(xmlContent, true);
                    break;
            }

            this.applyFilteredCollectionToViewModel();
        }

        private void mergeAndLoadCovidData(string textContent, bool mergeAllStates)
        {
            this.covidDataAssembler.MergeAndLoadCovidData(textContent, mergeAllStates);
            var duplicates = this.covidDataAssembler.GetDuplicatesFromMergedData();
            var covidRecords = duplicates as CovidRecord[] ?? duplicates.ToArray();
            if (covidRecords.Any())
            {
                this.keepOrReplaceDialog(covidRecords);
            }

            this.applyFilteredCollectionToViewModel();
            this.summaryTextBox.Text = this.covidDataAssembler.Summary;
        }

        private void mergeAndLoadCovidData(CovidDataCollection xmlContent, bool mergeAllStates)
        {
            this.covidDataAssembler.MergeAndLoadCovidData(xmlContent, mergeAllStates);
            var duplicates = this.covidDataAssembler.GetDuplicatesFromMergedData();
            var covidRecords = duplicates as CovidRecord[] ?? duplicates.ToArray();
            if (covidRecords.Any())
            {
                this.keepOrReplaceDialog(covidRecords);
            }

            this.applyFilteredCollectionToViewModel();
            this.summaryTextBox.Text = this.covidDataAssembler.Summary;
        }

        private async void keepOrReplaceDialog(IEnumerable<CovidRecord> duplicates)
        {
            var duplicateDialogBox = new DuplicateDialogBox();
            foreach (var record in duplicates)
            {
                duplicateDialogBox.SetDuplicateRecord(record);
                if (duplicateDialogBox.DoAll)
                {
                    if (duplicateDialogBox.Replace)
                    {
                        this.covidDataAssembler.ReplaceRecord(record);
                    }
                }
                else
                {
                    var results = await duplicateDialogBox.ShowAsync();
                    if (results == ContentDialogResult.Primary)
                    {
                        this.covidDataAssembler.ReplaceRecord(record);
                    }
                }
            }

            this.applyFilteredCollectionToViewModel();
            this.summaryTextBox.Text = this.covidDataAssembler.Summary;
        }

        private void updateCovidData()
        {
            if (!this.covidDataAssembler.IsCovidDataLoaded)
            {
                return;
            }

            this.applyThresholds();
            this.applyBinSize();
            this.covidDataAssembler.UpdateSummary();
            this.applyFilteredCollectionToViewModel();
            this.summaryTextBox.Text = this.covidDataAssembler.Summary;
        }

        private void applyThresholds()
        {
            this.covidDataAssembler.UpperPositiveThreshold = this.upperPositiveCaseTextBox.Text;
            this.covidDataAssembler.LowerPositiveThreshold = this.lowerPositiveCaseTextBox.Text;
        }

        private void applyBinSize()
        {
            this.covidDataAssembler.BinSize = this.binSizeTextBox.Text;
        }

        private void loadCovidData(string textContent)
        {
            this.covidDataAssembler.LoadCovidData(textContent);
            this.summaryTextBox.Text = this.covidDataAssembler.Summary;
        }

        private void loadCovidData(CovidDataCollection xmlContent)
        {
            this.covidDataAssembler.LoadCovidData(xmlContent);
            this.summaryTextBox.Text = this.covidDataAssembler.Summary;
        }

        private async void addCovidRecord()
        {
            var covidRecordAdder = new CovidRecordAdder {statesComboBox = {ItemsSource = new[] {State}}};
            var result = await covidRecordAdder.ShowAsync();
            this.applyThresholds();
            this.applyBinSize();
            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            var newRecord = covidRecordAdder.CreatedRecord;
            var isRecordDuplicate = this.covidDataAssembler.DoesCovidRecordExist(newRecord);
            if (isRecordDuplicate)
            {
                var duplicates = new List<CovidRecord> {newRecord};
                this.keepOrReplaceDialog(duplicates);
            }
            else
            {
                this.covidDataAssembler.AddCovidRecordToCollection(newRecord);
                this.applyFilteredCollectionToViewModel();
            }

            this.summaryTextBox.Text = this.covidDataAssembler.Summary;
        }

        private static async void showSaveSuccessfulPrompt(bool didFileSaveProperly)
        {
            var title = Assets.SaveFailedTitle;
            var content = Assets.SaveFailedContent;
            if (didFileSaveProperly)
            {
                title = Assets.SaveSuccessfulTitle;
                content = Assets.SaveSuccessfulContent;
            }

            var isFileSavedDialog = new ContentDialog {
                Title = title,
                Content = content,
                CloseButtonText = Assets.OkPrompt
            };
            await isFileSavedDialog.ShowAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = e.Parameter;

            if (parameter != null && !parameter.ToString().Equals(string.Empty))
            {
                var covidViewModel = (CovidAnalysisViewModel) parameter;
                this.covidDataAssembler.UpdateCollectionFromViewModel(covidViewModel);
                this.summaryTextBox.Text = this.covidDataAssembler.Summary;
            }
            else
            {
                this.summaryTextBox.Text = string.Empty;
            }
        }

        private void noRecordsForThatState()
        {
            if (this.covidDataAssembler.AllCovidData == null)
            {
                return;
            }

            var stateCovidRecords = this.covidDataAssembler.AllCovidData.ToList()
                                        .FindAll(record => record.State == State.ToString());

            if (stateCovidRecords.Any())
            {
                return;
            }

            this.summaryTextBox.Text = Assets.NoCovidDataText;
            this.setViewModelCovidRecordsToNull();
        }

        private void setViewModelCovidRecordsToNull()
        {
            this.covidViewModel.CovidDataRecords = null;
        }

        #endregion
    }
}