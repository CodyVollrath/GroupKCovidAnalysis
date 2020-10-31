using System;

namespace Covid19Analysis.DataTier
{

    /// <summary>This class is responsible logging lines with errors or invalid data</summary>
    public class CovidDataErrorLogger
    {
        #region Properties
        /// <Summary>Gets the errorLine string accumulated from the errors found.</Summary>
        /// <value>The errorLine string.</value>
        public string ErrorString { get; private set; }
        #endregion

        #region Constructors
        /// <Summary>Initializes a new instance of the <a onclick="return false;" href="CovidDataErrorLogger" originaltag="see">CovidDataErrorLogger</a> class.</Summary>
        public CovidDataErrorLogger()
        {
            this.ErrorString = string.Empty;
        }
        #endregion

        #region Public Methods
        /// <Summary>Adds the errorLine to an errorLine line logger string.</Summary>
        /// <param name="lineNumber">The line number where the errorLine was found.</param>
        /// <param name="errorLine">The errorLine.</param>
        public void AddErrorLineToErrorLogger(int lineNumber, string errorLine)
        {
            errorLine = errorLine ?? throw new ArgumentNullException(nameof(errorLine));
            this.ErrorString += $"Error on Line {lineNumber}: {errorLine}{Environment.NewLine}";
        }
        #endregion
    }
}
