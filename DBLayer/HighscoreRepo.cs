using Model;
using System;
using System.Collections.Generic;

namespace DBLayer
{
    class HighscoreRepo : IHighscoreRepo
    {
        public int GetHighscore(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Account, int> TopHighscore(int n)
        {
            throw new NotImplementedException();
        }
    }
}
