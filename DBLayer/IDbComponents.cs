using System.Data;
using System.Data.Common;

namespace DBLayer
{
    public interface IDbComponents
    {

        IDbConnection Connection { get; }
        DbProviderFactory Factory { get; }
    }
}
