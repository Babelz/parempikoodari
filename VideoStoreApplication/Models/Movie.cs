using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VideoStoreApplication.Models
{
    /// <summary>
    /// Contains information about a given movie.
    /// </summary>
    [Serializable]
    [XmlRoot("Movie")]
    public sealed class Movie
    {
        #region Properties
        /// <summary>
        /// Name of the movie.
        /// </summary>
        public string Name 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// Short description about the movie.
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// Release date of the movie.
        /// </summary>
        public string ReleaseDate
        {
            get;
            set;
        }
        /// <summary>
        /// Rent price per day.
        /// </summary>
        public float RentPricePerDay
        {
            get;
            set;
        }
        /// <summary>
        /// Is the movie available for renting.
        /// </summary>
        public bool Available
        {
            get;
            set;
        }
        /// <summary>
        /// Date the movie was given to a tenant.
        /// </summary>
        public string RentDate
        {
            get;
            set;
        }
        /// <summary>
        /// Format of the movie.
        /// </summary>
        public MovieFormat Format
        {
            get;
            set;
        }
        #endregion

        public Movie()
        {
            Available = true;
        }

        public static bool operator ==(Movie rhs, Movie lhs)
        {
            if (ReferenceEquals(rhs, null) && ReferenceEquals(lhs, null)) return true;
            if (ReferenceEquals(rhs, null)) return false;
            if (ReferenceEquals(lhs, null)) return false;
            
            return rhs.Name             == lhs.Name &&
                   rhs.Description      == lhs.Description &&
                   rhs.ReleaseDate      == lhs.ReleaseDate &&
                   rhs.RentPricePerDay  == lhs.RentPricePerDay &&
                   rhs.Available        == lhs.Available &&
                   rhs.RentDate         == lhs.RentDate &&
                   rhs.Format           == lhs.Format;
        }
        public static bool operator !=(Movie rhs, Movie lhs)
        {
            return !(rhs == lhs);
        }
    }
}
