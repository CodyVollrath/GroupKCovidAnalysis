using System;
using System.IO;
using System.Xml.Serialization;
using Windows.Storage;
using Covid19Analysis.Model;

namespace Covid19Analysis.DataTier
{
    /// <summary>
    ///     This class writes the contents of a CovidDataCollection to a xml file
    /// </summary>
    /// <seealso cref="Covid19Analysis.DataTier.CovidDataSaver" />
    public class XmlCovidDataSaver : CovidDataSaver
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlCovidDataSaver" /> class.
        ///     <code>Precondition: file != null AND covidData != null</code>
        ///     <code>Postcondition: File == file AND CovidData = covidData</code>
        /// </summary>
        /// <param name="file">The storage file.</param>
        /// <param name="covidData">The covid data collection.</param>
        /// <exception cref="ArgumentNullException">
        ///     file
        ///     or
        ///     covidData
        /// </exception>
        public XmlCovidDataSaver(StorageFile file, CovidDataCollection covidData) : base(file, covidData)
        {
        }

        #endregion

        #region Methods

        public override async void WriteCovidDataToFile()
        {
            var serializer = new XmlSerializer(typeof(CovidDataCollection));

            using (var outStream = await File.OpenStreamForWriteAsync())
            {
                serializer.Serialize(outStream, CovidData);
            }
        }

        #endregion
    }
}