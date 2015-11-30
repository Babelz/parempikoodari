using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VideoStoreApplication.Models
{
    /// <summary>
    /// Represents a movie tenant.
    /// </summary>
    [Serializable]
    [XmlRoot("Tenant")]
    public sealed class Tenant
    {
        #region Properties
        /// <summary>
        /// Name of the tenant.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Contact information of the tenant.
        /// </summary>
        public string ContactInformation
        {
            get;
            set;
        }
        /// <summary>
        /// List of movies the tenant currently has.
        /// </summary>
        [XmlArray("Rented movies")]
        [XmlArrayItem("Rented movie", typeof(Movie))]
        public ObservableCollection<Movie> RentedMovies
        {
            get;
            set;
        }
        #endregion

        public Tenant()
        {
            RentedMovies = new ObservableCollection<Movie>();
        }
    }
}
