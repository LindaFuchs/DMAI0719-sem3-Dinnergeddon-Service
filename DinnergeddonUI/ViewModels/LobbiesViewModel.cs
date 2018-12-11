using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DinnergeddonUI.Helpers;
using DinnergeddonUI.LobbyServiceReference;
using DinnergeddonUI.Views;

namespace DinnergeddonUI.ViewModels
{
    class LobbiesViewModel : BaseViewModel, IPageViewModel
    {
        private LobbyServiceClient _lobbyProxy = new LobbyServiceClient();
        private ObservableCollection<LobbyServiceReference.Lobby> _lobbies;
        private LobbyProxy _proxy;
        private ICommand _joinLobby;
        private ICommand _createLobby;
        private CustomPrincipal customPrincipal;
        private CreateLobbyDialog cld;
        private string _buttonText;
        private bool _isJoined;
        private LobbyServiceReference.Lobby _joinedLobby;

        public bool IsJoined
        {
            get
            {
                return _isJoined;
            }
            set
            {
                _isJoined = value;
                OnPropertyChanged("IsJoined");
            }
        }
        
        public LobbyServiceReference.Lobby JoinedLobby
        {
            get
            {
                return _joinedLobby;
            }
            set
            {
                _joinedLobby = value;
                OnPropertyChanged("JoinedLobby");
                
            }
        }
        public string ButtonText
        {
            get
            {
                return _buttonText;
            }
            set
            {
                _buttonText = value;
                OnPropertyChanged("ButtonText");
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
        public ICommand JoinLobbyCommand
        {
            get
            {
                if (_joinLobby == null)
                {
                    _joinLobby = new RelayCommand<object>(JoinLobby);
                }

                return _joinLobby;
            }
        }
        public ObservableCollection<LobbyServiceReference.Lobby> Lobbies
        {
            get
            {
                return _lobbies;
            }
            set
            {
                _lobbies = value;
                OnPropertyChanged("Lobbies");
            }
        }

        public LobbiesViewModel()
        {

            _proxy = new LobbyProxy();
            _proxy.LobbyCreated += OnLobbyCreated;
            ///Todo: change to _proxy
            ///
            _lobbies = new ObservableCollection<LobbyServiceReference.Lobby>();
            Lobbies = _lobbies;
            //_lobbies = new List<LobbyServiceReference.Lobby>();
            customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
        }

        private void OnLobbyCreated(object sender, LobbyEventArgs args)
        {

            LobbyServiceReference.Lobby createdLobby = args.Lobby;

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                _lobbies.Add(createdLobby);
            });

            Lobbies = _lobbies;
            OnPropertyChanged("Lobbies");

        }

        private void CreateLobby(object parameter)
        {
            cld = new CreateLobbyDialog
            {
                DataContext = new CreateLobbyViewModel()
            };

            cld.Show();
            Mediator.Subscribe("LobbyCreated", new Action<object>((x) => cld.Close()));

           

        }

        private void JoinLobby(object parameter)
        {
            Guid lobbyId = (Guid)parameter;
            Guid userId = customPrincipal.Identity.Id;
            if (_lobbyProxy.JoinLobby(userId, lobbyId) && !IsJoinedInALobby(userId, lobbyId))
            {
                Mediator.Notify("LobbyJoined", lobbyId);
                Mediator.Notify("OpenLobby", lobbyId);
                IsJoined = true;
                JoinedLobby = _lobbyProxy.GetLobbyById(lobbyId);
            }
            else
            {
                MessageBox.Show("You already joined a lobby", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }




        }

        private bool IsJoinedInALobby(Guid userId, Guid lobbyId)
        {
            foreach (Account a in _lobbyProxy.GetLobbyById(lobbyId).Players)
            {
                if (a.Id == userId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
