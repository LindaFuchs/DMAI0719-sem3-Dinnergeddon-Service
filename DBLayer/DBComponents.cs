using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DBLayer
{
    public class DbComponents : IDbComponents
    {
        // Using a singleton
        private static DbComponents instance;

        private DbComponents()
        {
            string provider = ConfigurationManager.ConnectionStrings["auto"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["auto"].ConnectionString;

            var factory = DbProviderFactories.GetFactory(provider);

            Connection = factory.CreateConnection();
            Connection.ConnectionString = connectionString;
        }

        public static DbComponents GetInstance()
        {
            if (instance == null)
                instance = new DbComponents();

            return instance;
        }

        public IDbConnection Connection{ get; private set; }
    }
}
