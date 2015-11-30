using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VideoStoreApplication.Models;
using VideoStoreApplication.ViewModels;

namespace VideoStoreApplication
{
    /// <summary>
    /// Interaction logic for SelectMovieDialog.xaml
    /// </summary>
    public partial class SelectMovieDialog : Window
    {
        #region Fields
        private readonly MoviesViewModel moviesViewModel;
        #endregion

        #region Properties
        public Movie SelectedMovie
        {
            get;
            private set;
        }
        #endregion

        public SelectMovieDialog(MovieDatabase movieDatabase)
        {
            Topmost = true;

            IEnumerable<Movie> rentableMovies = movieDatabase.Movies.Where(m => m.Available);

            moviesViewModel = new MoviesViewModel();

            foreach (Movie movie in rentableMovies)
            {
                moviesViewModel.Movies.Add(movie);
            }

            DataContext = moviesViewModel;

            InitializeComponent();
        }

        #region Event handlers
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void rentSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedMovie = moviesListView.SelectedItem as Movie;

            Close();
        }

        private void moviesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rentSelectedButton.IsEnabled = e.AddedItems.Count != 0;
        }
        #endregion
    }
}
