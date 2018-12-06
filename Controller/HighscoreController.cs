using DBLayer;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class HighscoreController : IHighscoreController
    {
        private readonly IHighscoreRepo highscoreRepo = new HighscoreRepo(DbComponents.GetInstance());

        public int GetHighscore(Guid accountId)
        {
            return highscoreRepo.GetHighscore(accountId);
        }

        public IDictionary<Guid, int> GetHighscores()
        {
            return highscoreRepo.GetHighscores();
        }

        public IDictionary<Guid, int> GetHighscores(int n)
        {
            return highscoreRepo.GetHighscores(n);
        }
    }
}
