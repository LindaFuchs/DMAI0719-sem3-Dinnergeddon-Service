using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinnergeddonUI.LobbyServiceReference;

namespace DinnergeddonUI.ViewModels
{
    class LobbiesViewModel : BaseViewModel, IPageViewModel
    {
        private LobbyServiceClient _lobbyProxy = new LobbyServiceClient();
        private List<Lobby> _lobbies;

        public List<Lobby> Lobbies
        {
            get {

                _lobbyProxy.CreateLobby("first", 5);
                _lobbyProxy.CreateLobby("first", 5);
                _lobbyProxy.CreateLobby("first", 5);
                _lobbyProxy.CreateLobby("first", 5);
                return _lobbyProxy.GetLobbies().ToList(); }
            set { }
        }

        public LobbiesViewModel()
        {
            _lobbyProxy.CreateLobby("first", 5);
            _lobbyProxy.CreateLobby("first", 5);
            _lobbyProxy.CreateLobby("first", 5);
            _lobbyProxy.CreateLobby("first", 5);
            
        }
    }
}
