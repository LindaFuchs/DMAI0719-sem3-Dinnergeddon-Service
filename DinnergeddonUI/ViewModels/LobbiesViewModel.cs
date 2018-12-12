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
        private ObservableCollection<LobbyServiceReference.Lobby> _lobbies;
        private LobbyProxy _proxy;
        private ICommand _joinLobby;
        private ICommand _createLobby;
        private ICommand _openLobby;
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

        public ICommand OpenLobbyCommand
        {
            get
            {
                if(_openLobby == null)
                {
                    _openLobby = new RelayCommand(OpenLobby);
                }
                return _openLobby;
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
            _proxy.LobbyJoined += OnJoinLobby;
            _proxy.LobbyCreated += OnLobbyCreated;
            _proxy.LobbyUpdated += OnLobbyUpdated;
            _proxy.GetLobbiesResponse += OnLobbiesRecieved;
            _proxy.GetLobbyByIdResponse += OnLobbyRecieved;

            _proxy.GetLobbies();
            ///
           // _lobbies = new ObservableCollection<LobbyServiceReference.Lobby>(_proxy.GetLobbies());
            //Lobbies = _lobbies;
            //_lobbies = new List<LobbyServiceReference.Lobby>();
            customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
        }
        private void OnLobbiesRecieved(object sender,  IEnumerable<LobbyServiceReference.Lobby> lobbies)
        {
            Lobbies = new ObservableCollection<LobbyServiceReference.Lobby>(lobbies);
        }

        private void OnLobbyRecieved(object sender, LobbyEventArgs args)
        {
            JoinedLobby = args.Lobby;

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

        private void OnLobbyUpdated(object sender ,LobbyEventArgs args)
        {
            LobbyServiceReference.Lobby updatedLobby = args.Lobby;
            LobbyServiceReference.Lobby lobbyToUpdate = _lobbies.Where(x => x.Id == updatedLobby.Id).FirstOrDefault();
            lobbyToUpdate.Players = updatedLobby.Players;
            lobbyToUpdate.Limit = updatedLobby.Limit;
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
            if (!IsJoinedInALobby(userId))
            {
                _proxy.JoinLobby(userId, lobbyId);
               // LobbyServiceReference.Lobby joinedLobby = _proxy.GetLobbyById(lobbyId);
                //notify the LobbyViewModel to change its properties for the current lobby
                 IsJoined = true;
                // JoinedLobby = _proxy.GetLobbyById(lobbyId);
                //  Lobbies = new ObservableCollection<LobbyServiceReference.Lobby>(_proxy.GetLobbies().ToList());
                _proxy.GetLobbyById(lobbyId);
                Mediator.Notify("LobbyJoined", lobbyId);

                //notify the mainWindowViewModel to change the viewmodel to the lobbyViewModel
                Mediator.Notify("OpenLobby", lobbyId);
               
                //LobbyServiceReference.Lobby l = _proxy.GetLobbyById(lobbyId);

            }



            else
            {
                MessageBox.Show("You already joined a lobby", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void OnJoinLobby(object s, bool result)
        {
            _proxy.GetLobbies();
            
        }

        private void OpenLobby(object parameter)
        {
            Guid lobbyId = (Guid)parameter;
            Mediator.Notify("OpenLobby", lobbyId);

        }

        private bool IsJoinedInALobby(Guid userId)
        {
            _proxy.GetLobbies();
            //Lobbies = new ObservableCollection<LobbyServiceReference.Lobby>(_proxy.GetLobbies().ToList());
            foreach (LobbyServiceReference.Lobby lobby in Lobbies)
            {
                foreach (Account a in lobby.Players)
                {
                    if (a.Id == userId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }



    }

}
