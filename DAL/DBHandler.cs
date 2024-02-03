using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DBHandler
    {
        private ConnectionStringSettings connectionStringSettings;

        public DBHandler(string connectionStringName)
        {
            connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        public IDBHandler CreateDatabase()
        {
            return new SqlDataAccess(connectionStringSettings.ConnectionString);
        }

        public string GetProviderName()
        {
            return connectionStringSettings.ProviderName;
        }
    }
}
