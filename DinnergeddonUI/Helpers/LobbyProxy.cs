using DinnergeddonUI.LobbyServiceReference;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;

namespace DinnergeddonUI.Helpers
{
    public class LobbyEventArgs : EventArgs
    {
        public IEnumerable<Lobby> Lobbies { get; set; }
    }

    public class LobbyProxy
    {
        private readonly HubConnection connection;
        private readonly IHubProxy hubProxy;

        public delegate void LobbyCreatedEventHandler(object source, LobbyEventArgs args);

        public event LobbyCreatedEventHandler LobbyCreated;

        public LobbyProxy() : this("http://localhost:8080") { }
        public LobbyProxy(string url)
        {
            connection = new HubConnection(url);
            hubProxy = connection.CreateHubProxy("LobbiesHub");

            connection.Start().Wait();
            SetupListeners();
        }

        private void SetupListeners()
        {
            hubProxy.On<IEnumerable<Lobby>>("lobbyCreated", (lobbies) => OnLobbyCreated(lobbies));
        }

        public void CreateLobby(string lobbyName, int playerLimit)
        {
            hubProxy.Invoke("CreateLobby", new object[] { lobbyName, playerLimit, "" });
        }

        protected virtual void OnLobbyCreated(IEnumerable<Lobby> lobbies)
        {
            if (LobbyCreated != null)
                LobbyCreated(this, new LobbyEventArgs() { Lobbies = lobbies });
        }
    }
}
