using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public interface IHighscoreController
    {
        /// <summary>
        /// Gets the highscore of a single account given it's ID
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <returns>The highscore of that account</returns>
        int GetHighscore(Guid accountId);

        /// <summary>
        /// Gets the top N highscores
        /// </summary>
        /// <param name="n">The number of highscores to be returned</param>
        /// <returns>A list of highscores and their respective account IDs</returns>
        IDictionary<Guid, int> GetHighscores(int n);

        /// <summary>
        /// Gets all the highscore values
        /// </summary>
        /// <returns>A list of highscores and their respective account IDs</returns>
        IDictionary<Guid, int> GetHighscores();
    }
}
