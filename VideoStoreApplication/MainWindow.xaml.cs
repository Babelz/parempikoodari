using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoStoreApplication.Database;
using VideoStoreApplication.Models;

namespace VideoStoreApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event handlers
        private void manageRentsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void manageMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManager<MovieDatabase> databaseManager = new DatabaseManager<MovieDatabase>(StringConsts.MovieDatabaseLocation);

            MovieDatabase movieDatabase = databaseManager.Locate();

            MovieManagerWindow manageMoviesWindow = new MovieManagerWindow(movieDatabase)
            {
                Topmost = true
            };

            manageMoviesWindow.ShowDialog();
        }
        #endregion
    }
}
