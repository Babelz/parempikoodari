using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VideoStoreApplication.Database;
using VideoStoreApplication.Models;
using VideoStoreApplication.ViewModels;

namespace VideoStoreApplication
{
    /// <summary>
    /// Window used to manage movies.
    /// </summary>
    public partial class MovieManagerWindow : Window
    {
        #region Fields
        private readonly MoviesViewModel moviesViewModel;
        #endregion

        public MovieManagerWindow(MovieDatabase movieDatabase)
        {
            // Initialize model.
            moviesViewModel = new MoviesViewModel(movieDatabase);
            
            DataContext = moviesViewModel;

            InitializeComponent();
        }

        private string AskUserForInformation(string whatToAsk)
        {
            DialogWindow dialog = new DialogWindow()
            {
                Header          = "Uuden elokuvan lisäys",
                WhatToEnter     = whatToAsk,
                ResponseText    = string.Empty
            };

            dialog.ShowDialog();

            return dialog.ResponseText;
        }

        private void DisplayError(string message)
        {
            MessageBox.Show(message + ".\nElokuvaa ei lisätty tietokantaan.", "Virhe!", MessageBoxButton.OK);
        }

        private bool IsValidReleaseDate(string dateTime)
        {
            DateTime parsed = DateTime.Now;

            return DateTime.TryParse(dateTime, out parsed);
        }
        private bool IsValidRentPrice(string rentPricePerDay)
        {
            float parsed = 0.0f;

            return float.TryParse(rentPricePerDay, out parsed);
        }
        private bool IsValidMovieFormat(string movieFormat)
        {
            return Enum.GetNames(typeof(MovieFormat)).FirstOrDefault(s => s.ToLower() == movieFormat.ToLower())
                                                      != string.Empty;
        }

        private MovieFormat ParseFormat(string format)
        {
            string formatInLower = format.ToLower();

            string[] values = Enum.GetNames(typeof(MovieFormat));

            for (int i = 0; i < values.Length; i++)
            {
                string valueInLower = values[i].ToLower();

                if (valueInLower == formatInLower)
                {
                    return (MovieFormat)Enum.GetValues(typeof(MovieFormat)).GetValue(i);
                }
            }

            throw new FormatException("invalid movie format");
        }

        #region Event handlers
        private void moviesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Disables/enables the remove button.
            // Movie must be selected before it can be removed.
            removeSelectedMovieButton.IsEnabled = e.AddedItems.Count != 0;
        }

        private void removeSelectedMovieButton_Click(object sender, RoutedEventArgs e)
        {
            // As the user for validation before removing the movie.
            MessageBoxResult result = MessageBox.Show("haluatko varmasti poistaa valitun elokuvan?", "HOX!", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // Remove the movie from the "database"
                moviesViewModel.Movies.Remove(moviesViewModel.Selected);
                moviesViewModel.Selected = null;
            }
        }

        private void addMovieButton_Click(object sender, RoutedEventArgs e)
        {
            // Name and desc do not require any special checking.

            string name = AskUserForInformation("Nimi :");

            string description = AskUserForInformation("Kuvaus :");
            
            // Get date from the user and validate it.
            string releaseDate = AskUserForInformation("Julkaisupäivä :");

            if (!IsValidReleaseDate(releaseDate))
            {
                DisplayError("Virheellinen päivämäärä!");
                return;
            }

            // Get rent price from the user and validate it.
            string rentPricePerDay = AskUserForInformation("Vuokra/päivä:").Replace('.', ',');

            if (!IsValidRentPrice(rentPricePerDay))
            {
                DisplayError("Virheellinen hinta!");
                return;
            }

            // Get format from the user and validate it.
            string format = AskUserForInformation("Formaatti :").ToLower();

            if (!IsValidMovieFormat(format))
            {
                DisplayError("Virheellinen formaatti!");
                return;
            }

            // Create new movie and add it to the "database"
            Movie newMovie = new Movie()
            {
                Name                = name,
                Description         = description,
                ReleaseDate         = releaseDate,
                RentPricePerDay     = float.Parse(rentPricePerDay),
                Format              = ParseFormat(format)
            };

            moviesViewModel.Movies.Add(newMovie);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Sync "database" before we close the window.
            // Just rewrite the whole file, skip checks.
            DatabaseManager<MovieDatabase> databaseManager = new DatabaseManager<MovieDatabase>(StringConsts.MovieDatabaseLocation);

            MovieDatabase movieDatabase = new MovieDatabase();
            movieDatabase.Movies = moviesViewModel.Movies.ToList();

            databaseManager.Save(movieDatabase);
        }
        #endregion
    }
}
