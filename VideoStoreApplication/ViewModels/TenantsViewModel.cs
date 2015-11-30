using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreApplication.Models;

namespace VideoStoreApplication.ViewModels
{
    /// <summary>
    /// View model that contains tenant information.
    /// </summary>
    public sealed class TenantsViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly ObservableCollection<Tenant> tenants;

        private Tenant selected;
        #endregion

        #region Properties
        /// <summary>
        /// Selected tenant.
        /// </summary>
        public Tenant Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;

                OnPropertyChanged("Selected");
            }
        }
        /// <summary>
        /// List of all known tenants.
        /// </summary>
        public ObservableCollection<Tenant> Tenants
        {
            get
            {
                return tenants;
            }
        }
        /// <summary>
        /// Selected movie owned by the selected tenant.
        /// </summary>
        public Movie SelectedMovie
        {
            get;
            set;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public TenantsViewModel(TenantDatabase tenantDatabase)
        {
            tenants = new ObservableCollection<Tenant>(tenantDatabase.Tenants);
        }

        public void RemoveMovie(Movie movie)
        {
            Debug.Assert(selected != null);

            selected.RentedMovies.Remove(movie);

            OnPropertyChanged("Selected");
        }
        public void AddMovie(Movie movie)
        {
            Debug.Assert(selected != null);

            selected.RentedMovies.Add(movie);

            OnPropertyChanged("Selected");
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
