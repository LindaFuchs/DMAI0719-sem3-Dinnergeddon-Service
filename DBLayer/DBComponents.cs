using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DBLayer
{
    public class DBComponents
    {
        private static DBComponents instance;
        
        private DBComponents()
        {
            string provider = ConfigurationManager.ConnectionStrings["auto"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["auto"].ConnectionString;

            Factory = DbProviderFactories.GetFactory(provider);
            Connection = Factory.CreateConnection();
            Connection.ConnectionString = connectionString;
        }

        public static DBComponents GetInstance()
        {
            if (instance == null)
                instance = new DBComponents();

            return instance;
        }
        
        public IDbConnection Connection { get; private set; }
        public DbProviderFactory Factory { get; private set; }
    }
}
