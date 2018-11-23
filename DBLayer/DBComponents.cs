using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DBLayer
{
    public class DbComponents : IDbComponents
    {
        private static DbComponents instance;

        private DbComponents()
        {
            string provider = ConfigurationManager.ConnectionStrings["auto"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["auto"].ConnectionString;

            Factory = DbProviderFactories.GetFactory(provider);

            Connection = Factory.CreateConnection();
            Connection.ConnectionString = connectionString;
        }

        public static DbComponents GetInstance()
        {
            if (instance == null)
                instance = new DbComponents();

            return instance;
        }

        public IDbConnection Connection{ get; private set; }
        //public IDbConnection Connection { get; private set; }
        public DbProviderFactory Factory { get; private set; }
    }
}
