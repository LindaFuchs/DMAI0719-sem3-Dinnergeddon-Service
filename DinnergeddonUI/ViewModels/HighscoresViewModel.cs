using System.Collections.Generic;

namespace DinnergeddonUI.ViewModels
{
    class HighscoresViewModel : BaseViewModel, IPageViewModel
    {
        private IDictionary<string, int> _highscores;

        public HighscoresViewModel()
        {
            _highscores = new Dictionary<string, int>();
            _highscores.Add("Nikola", 100);
            _highscores.Add("Alex", 200);
            _highscores.Add("Chicken", 300);
            _highscores.Add("Steffy", 400);
            _highscores.Add("Stiffy", 500);
            _highscores.Add("Linda", 600);
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
