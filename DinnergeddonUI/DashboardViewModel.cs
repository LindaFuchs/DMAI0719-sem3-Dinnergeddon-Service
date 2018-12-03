using System;
using System.Collections.Generic;
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
    class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;
        private string _username;
        private readonly DelegateCommand _joinLobbyCommand;
        private readonly DelegateCommand _logoutCommand;
        private readonly DelegateCommand _openLobbyCommand;
        private IEnumerable<Lobby> _lobbies;
        LobbyServiceClient _proxy = new LobbyServiceClient();
        AccountServiceClient _accountProxy = new AccountServiceClient();
        private DelegateCommand _createLobbyCommand;
        private Window _dashboardWindow;

        public DashboardViewModel(IAuthenticationService authenticationService, Window dashboardWindow)
        {
            _dashboardWindow = dashboardWindow;
            _authenticationService = authenticationService;
            _logoutCommand = new DelegateCommand(Logout, CanLogout);
            _joinLobbyCommand = new DelegateCommand(JoinLobby, CanJoin);
            _createLobbyCommand = new DelegateCommand(CreateLobby, CanJoin);
            _openLobbyCommand = new DelegateCommand(OpenLobby, CanJoin);
            _lobbies = _proxy.GetLobbies();


        }

        private void OpenLobby(object parameter)
        {
            Guid lobbyId = (Guid)parameter;
            Lobby lobbyToOpen = _proxy.GetLobbyById(lobbyId);
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            var userId = customPrincipal.Identity.Id;
            Account account = _accountProxy.FindById(userId);
            //check if its private
            //if (lobbyToJoin.pas)
            //{
            //    JoinLobbyPasswordDialog jlpd = new JoinLobbyPasswordDialog();
            //}
            if (ContainsAccount(userId, lobbyId))
            {



                LobbyWindow _lobbyWindow = new LobbyWindow(lobbyToOpen, _dashboardWindow);


                _lobbyWindow.Show();
                Lobbies = _proxy.GetLobbies();

            }
            else
            {
                MessageBox.Show("You already joined a lobby", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public DelegateCommand JoinLobbyCommand { get { return _joinLobbyCommand; } }
        public DelegateCommand LogoutCommand { get { return _logoutCommand; } }
        public DelegateCommand CreateLobbyCommand { get { return _createLobbyCommand; } }
        public DelegateCommand OpenLobbyCommand { get { return _openLobbyCommand; } }

        public bool IsJoined
        {
            get
            {
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                var userId = customPrincipal.Identity.Id;
                foreach (Lobby l in Lobbies)
                {
                    if (ContainsAccount(userId, l.Id))
                    {
                        return true;
                    }
                }
                return false;
            }
            set { NotifyPropertyChanged("IsJoined"); }
        }

        public Lobby JoinedLobby
        {

            get
            {
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                var userId = customPrincipal.Identity.Id;
                foreach (Lobby l in Lobbies)
                {
                    if (ContainsAccount(userId, l.Id))
                    {
                        return l;
                    }
                }
                return null;
            }
            set { NotifyPropertyChanged("JoinedLobby"); }
        }

        public IEnumerable<Lobby> Lobbies
        {
            get { _lobbies = _proxy.GetLobbies(); return _lobbies; }
            set { _lobbies = value; NotifyPropertyChanged("Lobbies"); }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged("Username"); }
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

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Hi, {0}! {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("admin") ? "You are an administrator!"
                              : "");

                return "Not authenticated!";
            }
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        private void JoinLobby(object parameter)
        {

            Guid lobbyId = (Guid)parameter;
            Lobby lobbyToJoin = _proxy.GetLobbyById(lobbyId);
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            var userId = customPrincipal.Identity.Id;
            Account account = _accountProxy.FindById(userId);
            //check if its private
            //if (lobbyToJoin.pas)
            //{
            //    JoinLobbyPasswordDialog jlpd = new JoinLobbyPasswordDialog();
            //}
            if (_proxy.JoinLobby(userId, lobbyId) || ContainsAccount(userId, lobbyId))
            {



                LobbyWindow _lobbyWindow = new LobbyWindow(lobbyToJoin, _dashboardWindow);


                _lobbyWindow.Show();
                Lobbies = _proxy.GetLobbies();
                IsJoined = true;
                JoinedLobby = lobbyToJoin;

            }
            else
            {
                MessageBox.Show("You already joined a lobby", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private bool ContainsAccount(Guid userId, Guid lobbyId)
        {
            foreach (Account a in _proxy.GetLobbyById(lobbyId).Players)
            {
                if (a.Id == userId)
                {
                    return true;
                }
            }
            return false;
        }


        private void Logout(object parameter)
        {
            Window dashboard = parameter as Window;
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                // _loginCommand.RaiseCanExecuteChanged();
                _logoutCommand.RaiseCanExecuteChanged();
                dashboard.Close();
                //MainWindow mw = new MainWindow();
                //mw.Show();
            }
        }

        private void CreateLobby(object parameter)
        {
            CreateLobbyDialog createLobbyDialog = new CreateLobbyDialog();


            createLobbyDialog.ShowDialog();

            Lobbies = _proxy.GetLobbies();
        }

        private void RefreshLobbies()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<EventArgs> RequestClose;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
