using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DinnergeddonUI.Helpers;

namespace DinnergeddonUI.ViewModels
{
    class CreateLobbyViewModel : BaseViewModel, IPageViewModel
    {
        private string _lobbyName;
        private ICommand _createLobby;
        private LobbyProxy _proxy;
        private string _errorMessage;
        private bool[] _modeArray = new bool[] { false, false, true, false, false };
        private bool _isValid = true;
        private bool _isPrivate;

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                OnPropertyChanged("IsValid");
            }
        }

        public bool[] ModeArray
        {
            get { return _modeArray; }
        }

        public int SelectedMode
        {
            get { return Array.IndexOf(_modeArray, true); }
        }

        public bool IsPrivate
        {
            get
            {
                return _isPrivate;
            }
            set
            {
                _isPrivate = value;
                OnPropertyChanged("IsPrivate");
            }
        }

        public ICommand CreateLobbyCommand
        {
            get
            {
                if (_createLobby == null)
                {
                    _createLobby = new RelayCommand(CreateLobby);
                }
                return _createLobby;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public string LobbyName
        {
            get
            {
                return _lobbyName;
            }
            set
            {
                _lobbyName = value;
                OnPropertyChanged("LobbyName");
            }
        }

        public CreateLobbyViewModel()
        {
            _proxy = new LobbyProxy();
            _proxy.LobbyCreated += OnLobbyCreated;
        }


        private void OnLobbyCreated(object sender, LobbyEventArgs args)
        {
            DinnergeddonServiceReference.Lobby  lobby = args.Lobby;

            Mediator.Notify("LobbyCreated",lobby.Id);

        }
        private void CreateLobby(object parameter)
        {
            var passwordBoxes = parameter as List<object>;
            PasswordBox pb = passwordBoxes.ElementAt(0) as PasswordBox;
            PasswordBox pb_conf = passwordBoxes.ElementAt(1) as PasswordBox;

            string pw = pb.Password;
            string pw_conf = pb_conf.Password;
            string ln = _lobbyName;

            int nr = SelectedMode;




            if (ln != null && ln.Length > 2 && nr > 1)
            {
                if (IsPrivate)
                {

                }
                else
                {
                }
                _proxy.CreateLobby(ln, nr);






            }
            else
            {
                IsValid = false;
            }

        }


    }
}
