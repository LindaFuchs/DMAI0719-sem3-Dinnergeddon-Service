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
using DinnergeddonUI.DinnergeddonServiceReference;
using DinnergeddonUI.Views;

namespace DinnergeddonUI.ViewModels
{
    class LobbiesViewModel : BaseViewModel, IPageViewModel
    {
        private ObservableCollection<DinnergeddonServiceReference.Lobby> _lobbies;
        private LobbyProxy _proxy;
        private ICommand _joinLobby;
        private ICommand _createLobby;
        private ICommand _openLobby;
        private CustomPrincipal customPrincipal;
        //private CreateLobbyDialog cld;
        private string _buttonText;
        private bool _isJoined;
        private DinnergeddonServiceReference.Lobby _joinedLobby;
        private DinnergeddonServiceReference.Lobby _currentLobby;
        private static CreateLobbyDialog cld;
        private static InputPasswordDialog pd;

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

        public DinnergeddonServiceReference.Lobby JoinedLobby
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
                if (_openLobby == null)
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
                    _joinLobby = new RelayCommand<object>( JoinLobby);
                }

                return _joinLobby;
            }
        }
        public ObservableCollection<DinnergeddonServiceReference.Lobby> Lobbies
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
            //_proxy.LobbyJoined += OnJoinLobby;
            _proxy.LobbyCreated += OnLobbyCreated;
            _proxy.LobbyUpdated += OnLobbyUpdated;
            // _proxy.GetLobbiesResponse += OnLobbiesRecieved;
            // _proxy.GetLobbyByIdResponse += OnLobbyRecieved;
            _proxy.LobbyDeleted += OnLobbyDeleted;
            _proxy.LobbiesRecieved += OnLobbiesRecieved;

            ///
           // _lobbies = new ObservableCollection<LobbyServiceReference.Lobby>(_proxy.GetLobbies());
            //Lobbies = _lobbies;
            //_lobbies = new List<LobbyServiceReference.Lobby>();
            customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            Mediator.Subscribe("LobbyCreated", new Action<object>((x) =>
                {

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        cld.Close();
                        //JoinLobby(x);
                    });
                }));
            _proxy.GetLobbies();

          
            //RefreshLobbies();

        }
        private void OnLobbiesRecieved(object sender, IEnumerable<DinnergeddonServiceReference.Lobby> lobbies)
        {
            Lobbies = new ObservableCollection<DinnergeddonServiceReference.Lobby>(lobbies.Cast<DinnergeddonServiceReference.Lobby>());
            
        }

        //private async void RefreshLobbies()
        //{
        //    IEnumerable<DinnergeddonServiceReference.Lobby> lobbies = await _proxy.GetLobbies();

        //   // ObservableCollection<DinnergeddonServiceReference.Lobby> lobbies = new ObservableCollection<DinnergeddonServiceReference.Lobby> ((await _proxy.GetLobbies()).ToList());
        //    Lobbies = new ObservableCollection<DinnergeddonServiceReference.Lobby>(lobbies.Cast<DinnergeddonServiceReference.Lobby>());
        //}

        private void OnLobbyDeleted(object sender, Guid lobbyId)
        {
            IsJoined = false;
            JoinedLobby = null;

           // _proxy.GetLobbies();
        }

        //private void OnLobbiesRecieved(object sender, IEnumerable<DinnergeddonServiceReference.Lobby> lobbies)
        //{
        //    Lobbies = new ObservableCollection<DinnergeddonServiceReference.Lobby>(lobbies);
        //}

        //private void OnLobbyRecieved(object sender, LobbyEventArgs args)
        //{
        //    JoinedLobby = args.Lobby;
        //    _currentLobby = args.Lobby;

        //}

        private void OnLobbyCreated(object sender, LobbyEventArgs args)
        {

            DinnergeddonServiceReference.Lobby createdLobby = args.Lobby;

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                _lobbies.Add(createdLobby);
            });

            Lobbies = _lobbies;
            OnPropertyChanged("Lobbies");

        }

        private void OnLobbyUpdated(object sender, LobbyEventArgs args)
        {
            DinnergeddonServiceReference.Lobby updatedLobby = args.Lobby;
            DinnergeddonServiceReference.Lobby lobbyToUpdate = _lobbies.Where(x => x.Id == updatedLobby.Id).FirstOrDefault();
            lobbyToUpdate.Players = updatedLobby.Players;
            lobbyToUpdate.Limit = updatedLobby.Limit;

           // _proxy.GetLobbyById(updatedLobby.Id);
            if (! IsJoinedInTheLobby(customPrincipal.Identity.Id, updatedLobby.Id))
            {
                IsJoined = false;
                JoinedLobby = null;
            }
            OnPropertyChanged("Lobbies");
        }

        //private void OnJoinLobby(object s, bool result)
        //{
        //    if (!result)
        //    {
        //        MessageBox.Show("Joining this lobby is not possible", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        //    }
        //    else
        //    {

        //        OpenLobby(_currentLobby.Id);


        //    }


        //}
        private   void CreateLobby(object parameter)
        {
            Guid userId = customPrincipal.Identity.Id;

            if ( !IsJoinedInALobby(userId))
            {
                cld = new CreateLobbyDialog
                {
                    DataContext = new CreateLobbyViewModel()
                };

                cld.Show();
                //Mediator.Subscribe("LobbyCreated", new Action<object>((x) => {

                //    JoinLobby(x);
                //}));



            }
            else
            {
                MessageBox.Show("You already joined a lobby", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        private  void JoinLobby(object parameter)
        {
            Guid lobbyId = (Guid)parameter;
            DinnergeddonServiceReference.Lobby lobbyToJoin =  _proxy.GetLobbyById(lobbyId);
            Guid userId = customPrincipal.Identity.Id;
            if (!IsJoinedInALobby(userId))
            {
                //_proxy.GetLobbyById(lobbyId);

                if (lobbyToJoin.IsPrivate)
                {
                    pd = new InputPasswordDialog() { DataContext = new InputPasswordViewModel() };
                    pd.Show();
                    string password = "";
                    Mediator.Subscribe("PassWordCorrect", new Action<object>((x) =>
                    {

                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            pd.Close();
                            //_proxy.JoinLobby(userId, lobbyId, password);

                        });
                         password = x as String;

                        JoinPrivateLobby(customPrincipal.Identity.Id, lobbyId, password);





                    }));

                }
                else
                {
                    lobbyToJoin = _proxy.JoinLobby(userId, lobbyId);
                    
                    if (lobbyToJoin == null)
                    {
                        MessageBox.Show("Joining this lobby is not possible", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        OpenLobby(lobbyToJoin.Id);
                    }

                }

            }

            else
            {
                if ( IsJoinedInTheLobby(userId, lobbyId))
                {
                    // _proxy.GetLobbyById(lobbyId);
                    OpenLobby(lobbyId);
                }
                else
                {
                    MessageBox.Show("You already joined a lobby", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
        }

        private  void JoinPrivateLobby(Guid userId, Guid lobbyId, string password)
        {
            DinnergeddonServiceReference.Lobby lobbyToJoin = null;

            lobbyToJoin =  _proxy.JoinLobby(userId, lobbyId, password);
            if (lobbyToJoin == null)
            {
                MessageBox.Show("Joining this lobby is not possible", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                OpenLobby(lobbyToJoin.Id);
            }
        }

        private async void OpenLobby(object parameter)
        {
            Guid lobbyId = (Guid)parameter;
            DinnergeddonServiceReference.Lobby lobby =  _proxy.GetLobbyById(lobbyId);

            //Mediator.Notify("SendLobbyId", _currentLobby.Id);

            Mediator.Notify("OpenLobby", lobby.Id);

            //notify LobbyView to set its lobby field
            Mediator.Notify("LobbyJoined", lobby.Id);

            JoinedLobby = lobby;

            IsJoined = true;

        }

        private bool IsJoinedInALobby(Guid userId)
        {
            _proxy.GetLobbies();
            //Lobbies = new ObservableCollection<LobbyServiceReference.Lobby>(_proxy.GetLobbies().ToList());
            foreach (DinnergeddonServiceReference.Lobby lobby in Lobbies)
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
        private  bool IsJoinedInTheLobby(Guid userId, Guid lobbyId)
        {
            DinnergeddonServiceReference.Lobby lobby = _proxy.GetLobbyById(lobbyId);
            //Lobbies = new ObservableCollection<LobbyServiceReference.Lobby>(_proxy.GetLobbies().ToList());

            foreach (Account a in lobby.Players)
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
