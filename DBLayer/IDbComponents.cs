using System.Data;

namespace DBLayer
{
    public interface IDbComponents
    {
        /// <summary>
        /// A connection with the database
        /// </summary>
        IDbConnection Connection { get; }
    }
}
