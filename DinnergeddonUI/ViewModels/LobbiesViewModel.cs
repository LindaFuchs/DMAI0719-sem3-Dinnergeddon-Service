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
        private List<LobbyServiceReference.Lobby> _lobbies;
        private ICommand _joinLobby;
        private ICommand _createLobby;
        private CustomPrincipal customPrincipal;

        public ICommand CreateLobbyCommand
        {
            get
            {
                if(_createLobby == null)
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
        public List<LobbyServiceReference.Lobby> Lobbies
        {
            get {

               
                return _lobbies; }
            set { _lobbies = value;
                OnPropertyChanged("Lobbies");
            }
        }

        public LobbiesViewModel()
        {
            
            Lobbies = _lobbyProxy.GetLobbies().ToList();
           customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
        }

        private void CreateLobby(object parameter)
        {
            CreateLobbyDialog cld = new CreateLobbyDialog();
            cld.DataContext = new CreateLobbyViewModel();
            cld.Show();
        }

        private void JoinLobby(object parameter)
        {
            Guid lobbyId = (Guid)parameter;
            Guid userId = customPrincipal.Identity.Id;
            if (_lobbyProxy.JoinLobby(userId, lobbyId) && !IsJoinedInALobby(userId, lobbyId))
            {
 Mediator.Notify("LobbyJoined", lobbyId);
            Mediator.Notify("OpenLobby", lobbyId);
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
