using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VideoStoreApplication.Models
{
    /// <summary>
    /// Database that contains a collection of movies.
    /// </summary>
    [Serializable]
    [XmlRoot("Database")]
    public sealed class MovieDatabase
    {
        #region Properties
        [XmlArray("Movies")]
        [XmlArrayItem("Movie", typeof(Movie))]
        public List<Movie> Movies
        {
            get;
            set;
        }
        #endregion

        public MovieDatabase()
        {
            Movies = new List<Movie>();
        }
    }
}
