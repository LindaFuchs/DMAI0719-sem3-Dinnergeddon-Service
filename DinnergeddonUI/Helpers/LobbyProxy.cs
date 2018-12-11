using DinnergeddonUI.LobbyServiceReference;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;

namespace DinnergeddonUI.Helpers
{
    public class LobbyProxy
    {
        private readonly HubConnection connection;
        private readonly IHubProxy hubProxy;

        public LobbyProxy() : this("http://localhost:8080") { }
        public LobbyProxy(string url)
        {
            connection = new HubConnection(url);
            hubProxy = connection.CreateHubProxy("LobbiesHub");
            SetupListeners();

            connection.Start().Wait();
        }

        private void SetupListeners()
        {
            hubProxy.On<Lobby>("lobbyCreated", (lobby) => OnLobbyCreated(lobby));
            hubProxy.On<Lobby>("lobbyUpdated", (lobby) => OnLobbyUpdated(lobby));
            hubProxy.On<Lobby>("lobbyDeleted", (lobby) => OnLobbyDeleted(lobby));
            hubProxy.On<bool>("joined", (joined) => OnLobbyJoined(joined));
        }

        public Lobby GetLobbyById(Guid id)
        {
            return null;
        }

        #region WS Callers

        public void CreateLobby(string lobbyName, int playerLimit)
        {
            hubProxy.Invoke("CreateLobby", new object[] { lobbyName, playerLimit, "" });
        }

        public void JoinLobby(Guid accountId, Guid lobbyId, string password)
        {
            hubProxy.Invoke("JoinLobby", new object[] { accountId, lobbyId, password });
        }

        public void JoinLobby(Guid accountId, Guid lobbyId)
        {
            JoinLobby(accountId, lobbyId, "");
        }
        
        #endregion

        #region Events
        
        public event EventHandler<LobbyEventArgs> LobbyCreated;
        public event EventHandler<LobbyEventArgs> LobbyUpdated;
        public event EventHandler<LobbyEventArgs> LobbyDeleted;
        public event EventHandler<bool> LobbyJoined;

        protected virtual void OnLobbyCreated(Lobby newLobby)
        {
            if (LobbyCreated != null)
                LobbyCreated.Invoke(this, new LobbyEventArgs() { Lobby = newLobby });
        }

        protected virtual void OnLobbyUpdated(Lobby updatedLobby)
        {
            if (LobbyUpdated != null)
                LobbyUpdated.Invoke(this, new LobbyEventArgs() { Lobby = updatedLobby });
        }

        protected virtual void OnLobbyDeleted(Lobby deletedLobby)
        {
            if (LobbyDeleted != null)
                LobbyDeleted.Invoke(this, new LobbyEventArgs() { Lobby = deletedLobby });
        }

        protected virtual void OnLobbyJoined(bool joined)
        {
            if (LobbyJoined != null)
                LobbyJoined.Invoke(this, joined);
        }

        #endregion

    }
}
