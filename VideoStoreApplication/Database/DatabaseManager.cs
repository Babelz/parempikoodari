using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using VideoStoreApplication.Models;

namespace VideoStoreApplication.Database
{
    /// <summary>
    /// Class responsible of locating and syncing the databases.
    /// </summary>
    /// <typeparam name="T">runtime type of the database, type must have SerializableAttribute</typeparam>
    public sealed class DatabaseManager<T>
    {
        #region Fields
        private readonly string databaseLocation;
        #endregion

        public DatabaseManager(string databaseLocation)
        {
            this.databaseLocation = databaseLocation;

            if (typeof(T).GetCustomAttributes(typeof(SerializableAttribute), true) == null)
            {
                throw new InvalidDataException("T must have a serializable attribute");
            }
        }

        private bool DatabaseExists()
        {
            return File.Exists(databaseLocation);
        }

        /// <summary>
        /// Creates empty database.
        /// </summary>
        private void CreateEmptyDatabase()
        {
            // The database.
            XmlDocument database = new XmlDocument();

            // Create declaration.
            XmlDeclaration databaseDeclaration = database.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement databaseRoot = database.DocumentElement;

            database.InsertBefore(databaseDeclaration, databaseRoot);

            // Create root for the database.
            XmlElement rootElement = database.CreateElement(string.Empty, "Database", string.Empty);

            database.AppendChild(rootElement);

            // "Create" (save) the database.
            database.Save(databaseLocation);
        }

        /// <summary>
        /// Loads the database from the database file.
        /// </summary>
        /// <returns>the database</returns>
        private MovieDatabase LoadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            MovieDatabase database = null;

            using (StreamReader reader = new StreamReader(databaseLocation))
            {
                database = serializer.Deserialize(reader) as MovieDatabase;
            }

            return database;
        }

        /// <summary>
        /// Locates the database for the user.
        /// </summary>
        /// <returns>the located database</returns>
        public MovieDatabase Locate()
        {
            if (!DatabaseExists())
            {
                CreateEmptyDatabase();
            }

            return LoadFromFile();
        }

        /// <summary>
        /// Saves the given database to the disk.
        /// </summary>
        /// <param name="database"></param>
        public void Save(MovieDatabase database)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter writer = new StreamWriter(databaseLocation))
            {
                serializer.Serialize(writer, database);
            }
        }
    }
}
