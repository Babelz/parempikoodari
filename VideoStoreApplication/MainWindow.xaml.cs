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
            /*
                    Initialize new movie management window and 
                    present known movies to the user.
             */

            // RAII.
            DatabaseManager<TenantDatabase> databaseManager = new DatabaseManager<TenantDatabase>(StringConsts.TenantDatabaseLocation);

            TenantDatabase tenantDatabase = databaseManager.Locate();

            RentManagerWindow rentManagerWindow = new RentManagerWindow(tenantDatabase) 
            {
                Topmost = true
            };

            rentManagerWindow.ShowDialog();
        }

        private void manageMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            // RAII.
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
