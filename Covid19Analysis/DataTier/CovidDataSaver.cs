﻿using System;
using Windows.Storage;
using Covid19Analysis.Model;

namespace Covid19Analysis.DataTier
{
    /// <summary>
    ///     Authors: Eboni Walker
    ///     This class writes the contents of a CovidDataCollection to a file.
    /// </summary>
    public abstract class CovidDataSaver
    {
        #region Properties

        /// <summary>Gets the file being saved.</summary>
        /// <value>The file that is being saved.</value>
        public StorageFile File { get; }

        /// <summary>Gets the covid data that is being saved to the file.</summary>
        /// <value>The covid data being saved to a file.</value>
        public CovidDataCollection CovidData { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CovidDataSaver" /> class.
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
        protected CovidDataSaver(StorageFile file, CovidDataCollection covidData)
        {
            this.File = file ?? throw new ArgumentNullException(nameof(file));
            this.CovidData = covidData ?? throw new ArgumentNullException(nameof(covidData));
        }

        #endregion

        #region Methods

        /// <summary>Writes the covid data to the storage file.</summary>
        public abstract void WriteCovidDataToFile();

        #endregion
    }
}