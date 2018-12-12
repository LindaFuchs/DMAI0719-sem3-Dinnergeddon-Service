using System.Collections.Generic;
using DinnergeddonUI.DinnergeddonServiceReference;

namespace DinnergeddonUI.ViewModels
{
    class HighscoresViewModel : BaseViewModel, IPageViewModel
    {
        private IDictionary<string, int> _highscores;
        private readonly HighscoreServiceClient _highscoreProxy;
        private readonly AccountServiceClient _accountProxy;

        public HighscoresViewModel()
        {
            _highscoreProxy = new HighscoreServiceClient();
            _accountProxy = new AccountServiceClient();
            _highscores = new Dictionary<string, int>();

            foreach (var kvp in _highscoreProxy.GetHighscores())
            {
                _highscores.Add(_accountProxy.FindById(kvp.Key).Username, kvp.Value);
            }
        }

        public IDictionary<string, int> Highscores
        {
            get { return _highscores; }
            set
            {
                Highscores = value;
                OnPropertyChanged("Highscores");
            }
        }
    }
}
