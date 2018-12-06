using System;
using System.Collections.Generic;
using System.Data;

namespace DBLayer
{
    class HighscoreRepo : IHighscoreRepo
    {
        private readonly IDbConnection connection;

        public HighscoreRepo(IDbComponents components)
        {
            connection = components.Connection;
        }

        /// <summary>
        /// Gets the highscore of an account
        /// </summary>
        /// <param name="accountID">The ID of the account to get</param>
        /// <returns>The highscore of that account</returns>
        public int GetHighscore(Guid accountID)
        {
            int score = 0;
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from highscores where accountId=@id";

                // Create parameters, escape SQL injection
                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = accountID;
                command.Parameters.Add(param);

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    command.Transaction = transaction;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            score = reader.GetInt32(1);
                    }
                }
            }

            connection.Close();
            return score;
        }

        /// <summary>
        /// Selects all the highscores in the database
        /// </summary>
        /// <returns>A list of highscores</returns>
        public IDictionary<Guid, int> GetHighscores()
        {
            IDictionary<Guid, int> scores = new Dictionary<Guid, int>();

            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from Highscores order by score desc";

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    command.Transaction = transaction;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scores.Add(reader.GetGuid(0), reader.GetInt32(1));
                        }
                    }
                }
            }

            connection.Close();
            return scores;
        }

        /// <summary>
        /// Gets a list of the top N highscores
        /// </summary>
        /// <param name="n">The number of highscores to select</param>
        /// <returns>A dictionary with the account and it's respective highscore</returns>
        public IDictionary<Guid, int> TopHighscore(int n)
        {
            IDictionary<Guid, int> scores = new Dictionary<Guid, int>();

            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select top @top * from Highscores order by score desc";

                // Create parameters, escape SQL injection
                IDataParameter param = command.CreateParameter();
                param.ParameterName = "@top";
                param.Value = n;
                command.Parameters.Add(param);

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    command.Transaction = transaction;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scores.Add(reader.GetGuid(0), reader.GetInt32(1));
                        }
                    }
                }
            }

            connection.Close();
            return scores;
        }
    }
}
