using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DinnergeddonUI.Helpers;
using DinnergeddonUI.DinnergeddonServiceReference;

namespace DinnergeddonUI.ViewModels
{
    class LobbyViewModel : BaseViewModel, IPageViewModel
    {
        private static Lobby _lobby;
        private string _lobbyName;
        private IEnumerable<Account> _joinedPlayers;
        private ICommand _leaveLobby;
        private LobbyProxy _proxy;





        public LobbyViewModel()
        {
            Mediator.Subscribe("LobbyJoined", LobbyJoined);
            _proxy = new LobbyProxy();
            _proxy.GetLobbyByIdResponse += OnLobbyRecieved;
            //_joinLobbyCommand = new DelegateCommand(JoinLobby, CanJoin);

        }

        private void LobbyJoined(object parameter)
        {
            _proxy.GetLobbyById((Guid)parameter);
          //  LobbyServiceReference.Lobby joinedLobby = _proxy.GetLobbyById((Guid)parameter);

        }

        public ICommand LeaveLobbyCommand
        {
            get
            {
                if (_leaveLobby == null)
                {
                    _leaveLobby = new RelayCommand(LeaveLobby);
                }
                return _leaveLobby;
            }
        }


      public string LobbyName
        {
            get { return _lobbyName; }
            set { _lobbyName = value;
                OnPropertyChanged("LobbyName");
            }
        }
        //public IEnumerable<Account> JoinedPlayers
        //{
        //    get { _joinedPlayers = _proxy.GetLobbyById(_lobby.Id).Players; return _joinedPlayers; }
        //    set { _joinedPlayers = value; OnPropertyChanged("JoinedUsers"); }
        //}

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        private bool CanJoin(object parameter)
        {
            //implement check
            return true;
        }

        private bool CanLeave(object parameter)
        {
            //implement check
            return true;
        }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Signed in as {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("admin") ? "You are an administrator!"
                              : "You are NOT a member of the administrators group.");

                return "Not authenticated!";
            }
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }



        //private void JoinLobby(object parameter)
        //{
        //    Guid lobbyId = (Guid)parameter;
        //    CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
        //    var userId = customPrincipal.Identity.Id;
        //    _proxy.JoinLobby(userId, lobbyId);
           
        //   Window lobby = new LobbyWindow(lobbyId);
        //    lobby.Show();
                 
        //}

        private void LeaveLobby(object parameter)
        {
            
            //Window lb = parameter as Window;
            //CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            //var userId = customPrincipal.Identity.Id;
            //_proxy.LeaveLobby(userId, _lobby.Id);

            //lb.Close();
            
        }

        private void OnLobbyRecieved(object sender, LobbyEventArgs args)
        {
            _lobby = args.Lobby;
            LobbyName = _lobby.Name;
        }
        

    }
}
