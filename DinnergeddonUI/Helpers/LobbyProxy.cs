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
            hubProxy.On<Guid>("lobbyDeleted", (lobbyId) => OnLobbyDeleted(lobbyId));
            hubProxy.On<bool>("joined", (joined) => OnLobbyJoined(joined));
            hubProxy.On<IEnumerable<Lobby>>("getLobbiesResponse", (lobbies) => OnGetLobbiesResponse(lobbies));
            hubProxy.On<Lobby>("getLobbyByIdResponse", (lobby) => {
                Console.WriteLine("GetLobbyByIdResponse raised");
                OnGetLobbyByIdResponse(lobby);
            });
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

        public void GetLobbies()
        {
            hubProxy.Invoke("GetLobbies");
        }

        public void GetLobbyById(Guid lobbyId)
        {
            hubProxy.Invoke("GetLobbyById", new object[] { lobbyId });
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
        public event EventHandler<bool> LobbyJoined;
        public event EventHandler<IEnumerable<Lobby>> GetLobbiesResponse;
        public event EventHandler<LobbyEventArgs> GetLobbyByIdResponse;

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

        protected virtual void OnLobbyJoined(bool joined)
        {
            if (LobbyJoined != null)
                LobbyJoined.Invoke(this, joined);
        }

        protected virtual void OnGetLobbiesResponse(IEnumerable<Lobby> lobbies)
        {
            if (GetLobbiesResponse != null)
                GetLobbiesResponse.Invoke(this, lobbies);
        }

        protected virtual void OnGetLobbyByIdResponse(Lobby lobby)
        {
            if (GetLobbyByIdResponse != null)
                GetLobbyByIdResponse.Invoke(this, new LobbyEventArgs() { Lobby = lobby });
        }

        #endregion

    }
}
