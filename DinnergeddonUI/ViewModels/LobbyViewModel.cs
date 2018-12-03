using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DinnergeddonUI.DinnergeddonService;

using DinnergeddonUI.Interfaces;

namespace DinnergeddonUI
{
    class LobbyViewModel : INotifyPropertyChanged
    {
        private static Lobby _lobby;
        private string _lobbyName;
        private IEnumerable<Account> _joinedPlayers;
        
        private readonly DelegateCommand _joinLobbyCommand;
        private readonly DelegateCommand _leaveLobbyCommand;
        LobbyServiceClient _proxy = new LobbyServiceClient();

        private Window _dashboardWindow;



        public LobbyViewModel(Lobby lobby, Window dashboardWindow)
        {
            _dashboardWindow = dashboardWindow;
            _lobby = lobby;

            _lobbyName = lobby.Name;
            _joinedPlayers = _proxy.GetLobbyById(_lobby.Id).Players;
            //_joinLobbyCommand = new DelegateCommand(JoinLobby, CanJoin);
            _leaveLobbyCommand = new DelegateCommand(LeaveLobby, CanLeave);

        }

        public DelegateCommand JoinLobbyCommand { get { return _joinLobbyCommand; } }
        public DelegateCommand LeaveLobbyCommand { get { return _leaveLobbyCommand; } }


      public string LobbyName
        {
            get { return _lobbyName; }
        }
        public IEnumerable<Account> JoinedPlayers
        {
            get { _joinedPlayers = _proxy.GetLobbyById(_lobby.Id).Players; return _joinedPlayers; }
            set { _joinedPlayers = value; NotifyPropertyChanged("JoinedUsers"); }
        }

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
            Window lb = parameter as Window;
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            var userId = customPrincipal.Identity.Id;
            _proxy.LeaveLobby(userId, _lobby.Id);

            lb.Close();
            _dashboardWindow.DataContext = new DashboardViewModel(_dashboardWindow);
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<EventArgs> RequestClose;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Lobby_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LeaveLobby(sender);
        }

    }
}
