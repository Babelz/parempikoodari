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
using System.Windows.Shapes;
using VideoStoreApplication.Database;
using VideoStoreApplication.Models;
using VideoStoreApplication.ViewModels;

namespace VideoStoreApplication
{
    /// <summary>
    /// Interaction logic for RentManagerWindow.xaml
    /// </summary>
    public partial class RentManagerWindow : Window
    {
        #region Fields
        private readonly TenantsViewModel tenantsViewModel;

        private readonly MovieDatabase movieDatabase;
        #endregion

        public RentManagerWindow(TenantDatabase tenantDatabase)
        {
            DatabaseManager<MovieDatabase> databaseManager = new DatabaseManager<MovieDatabase>(StringConsts.MovieDatabaseLocation);
            movieDatabase = databaseManager.Locate();

            tenantsViewModel = new TenantsViewModel(tenantDatabase);

            DataContext = tenantsViewModel;

            InitializeComponent();
        }

        string AskUserForInformation(string whatToAsk)
        {
            DialogWindow dialog = new DialogWindow()
            {
                Header          = "Uuden käyttäjän lisäys",
                WhatToEnter     = whatToAsk,
                ResponseText    = string.Empty
            };

            dialog.ShowDialog();

            return dialog.ResponseText;
        }

        #region Properties
        private void removeUserButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("haluatko varmasti poistaa valitun käyttäjän?", "HOX!", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                tenantsViewModel.Tenants.Remove(tenantsViewModel.Selected);
                tenantsViewModel.Selected = null;
            }
        }
        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AskUserForInformation("Nimi: ");
            string contactInformation = AskUserForInformation("Yhteystiedot: ");

            Tenant newTenant = new Tenant()
            {
                Name = name,
                ContactInformation = contactInformation
            };

            tenantsViewModel.Tenants.Add(newTenant);
        }

        private void removeRentedMovieButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("elokuva palautettu?", "HOX!", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // Sync to database.
                Movie databaseEntry = movieDatabase.Movies.FirstOrDefault(m => m == tenantsViewModel.SelectedMovie);
                databaseEntry.Available = true;
                databaseEntry.RentDate = string.Empty;
                
                // Update view model.
                tenantsViewModel.RemoveMovie(tenantsViewModel.SelectedMovie);
                
                removeRentedMovieButton.IsEnabled = false;
            }
        }
        private void addMovieButton_Click(object sender, RoutedEventArgs e)
        {
            SelectMovieDialog dialog = new SelectMovieDialog(movieDatabase);

            dialog.ShowDialog();

            if (dialog.SelectedMovie != null)
            {
                tenantsViewModel.AddMovie(dialog.SelectedMovie);

                dialog.SelectedMovie.RentDate = DateTime.Now.Date.ToShortDateString();
                dialog.SelectedMovie.Available = false;

                MessageBox.Show("elokuva vuokrattu", "HOX!", MessageBoxButton.OK);
            }
        }

        private void rentedMoviesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            removeRentedMovieButton.IsEnabled = e.AddedItems.Count != 0;
        }
        private void usersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            removeUserButton.IsEnabled = e.AddedItems.Count != 0;
            addMovieButton.IsEnabled = e.AddedItems.Count != 0;

            tenantsViewModel.SelectedMovie = null;
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TenantDatabase tenantDatabase = new TenantDatabase();
            tenantDatabase.Tenants = tenantsViewModel.Tenants.ToList();

            DatabaseManager<TenantDatabase> tenantDatabaseManager = new DatabaseManager<TenantDatabase>(StringConsts.TenantDatabaseLocation);
            tenantDatabaseManager.Save(tenantDatabase);

            DatabaseManager<MovieDatabase> movieDatabaseManager = new DatabaseManager<MovieDatabase>(StringConsts.MovieDatabaseLocation);
            movieDatabaseManager.Save(movieDatabase);
        }
    }
}
