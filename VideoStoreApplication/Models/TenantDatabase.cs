using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VideoStoreApplication.Models
{
    /// <summary>
    /// Database that contains a collection of known tenants.
    /// </summary>
    [Serializable]
    [XmlRoot("Database")]
    public sealed class TenantDatabase
    {
        #region Properties
        [XmlArray("Tenants")]
        [XmlArrayItem("Tenant", typeof(Tenant))]
        public List<Tenant> Tenants
        {
            get;
            set;
        }
        #endregion

        public TenantDatabase()
        {
            Tenants = new List<Tenant>();
        }
    }
}
