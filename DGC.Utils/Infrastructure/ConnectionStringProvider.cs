using System;
using System.Configuration;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {

        public string ConnectionString
        {
            get
            {
                var server = ConfigurationManager.ConnectionStrings["DCBankAdminContext"].ConnectionString;
                if(string.IsNullOrEmpty(server))
                    throw new Exception("A valid connection string needs to be set in the configuration file.");
                return server;
            }
        }
    }
}