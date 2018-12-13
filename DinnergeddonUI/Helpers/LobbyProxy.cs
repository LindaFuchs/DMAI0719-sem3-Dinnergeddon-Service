using DinnergeddonUI.DinnergeddonServiceReference;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;

namespace DinnergeddonUI.Helpers
{
    public class LobbyProxy
    {
        private readonly HubConnection connection;
        private readonly IHubProxy hubProxy;

        public LobbyProxy() : this("http://10.31.134.203:8080") { }
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
            hubProxy.On<Guid>("lobbyDeleted", (lobbyId) => OnLobbyDeleted(lobbyId));
        }

        #region WS Callers

        public void CreateLobby(string lobbyName, int playerLimit, string password)
        {
            hubProxy.Invoke("CreateLobby", new object[] { lobbyName, playerLimit, password });
        }
        public void CreateLobby(string lobbyName, int playerLimit)
        {
            CreateLobby(lobbyName, playerLimit, "");
        }

        public Lobby JoinLobby(Guid accountId, Guid lobbyId, string password)
        {
            return hubProxy.Invoke<Lobby>("JoinLobby", new object[] { accountId, lobbyId, password }).Result;
        }

        public Lobby JoinLobby(Guid accountId, Guid lobbyId)
        {
            return JoinLobby(accountId, lobbyId, "");
        }

        public IEnumerable<Lobby> GetLobbies()
        {
            return hubProxy.Invoke<IEnumerable<Lobby>>("GetLobbies").Result;
        }

        public Lobby GetLobbyById(Guid lobbyId)
        {
             return hubProxy.Invoke<Lobby>("GetLobbyById", new object[] { lobbyId }).Result;
        }

        public void LeaveLobby(Guid accountId, Guid lobbyId)
        {
            hubProxy.Invoke("LeaveLobby", new object[] { accountId, lobbyId });
        }

        #endregion

        #region Events

        public event EventHandler<LobbyEventArgs> LobbyCreated;
        public event EventHandler<LobbyEventArgs> LobbyUpdated;
        public event EventHandler<Guid> LobbyDeleted;

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

        protected virtual void OnLobbyDeleted(Guid lobbyId)
        {
            if (LobbyDeleted != null)
                LobbyDeleted.Invoke(this, lobbyId);
        }

        #endregion

    }
}
