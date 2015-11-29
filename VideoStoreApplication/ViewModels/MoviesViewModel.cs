using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreApplication.Models;

namespace VideoStoreApplication.ViewModels
{
    /// <summary>
    /// View model used with controls that require movie database information.
    /// </summary>
    public sealed class MoviesViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly ObservableCollection<Movie> movies;

        private Movie selected;
        #endregion

        #region Properties
        /// <summary>
        /// Movie that is selected.
        /// </summary>
        public Movie Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;

                // Notify observers that properties give new values.
                OnPropertyChanged("Selected");
                OnPropertyChanged("SelectedAvailableDisplayString");
                OnPropertyChanged("RentPricePerDayDisplayString");
                OnPropertyChanged("SelectedFormatDisplayString");
            }
        }
        /// <summary>
        /// Returns the display string of the selected movies
        /// availability.
        /// </summary>
        public string SelectedAvailableDisplayString
        {
            get
            {
                if (selected == null)
                {
                    return string.Empty;
                }

                return selected.Available ? "vuokrattavissa" : "vuokrattu";
            }
        }
        /// <summary>
        /// Returns the display string of the selected movies
        /// format.
        /// </summary>
        public string SelectedFormatDisplayString
        {
            get
            {
                if (selected == null)
                {
                    return string.Empty;
                }

                return Enum.GetName(typeof(MovieFormat), selected.Format);
            }
        }
        public string RentPricePerDayDisplayString
        {
            get
            {
                if (selected == null)
                {
                    return string.Empty;
                }

                return selected.RentPricePerDay + "€";
            }
        }
        /// <summary>
        /// List of the movies.
        /// </summary>
        public ObservableCollection<Movie> Movies
        {
            get
            {
                return movies;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public MoviesViewModel(MovieDatabase movieDatabase)
        {
            movies = new ObservableCollection<Movie>(movieDatabase.Movies);
        }


        /// <summary>
        /// Wrap null checks and calls the property changed event.
        /// </summary>
        /// <param name="propertyName">name of the property that was changed</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
