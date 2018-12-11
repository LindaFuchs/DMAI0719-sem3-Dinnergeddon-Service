using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

namespace DinnergeddonUI.Helpers
{
    class LobbyProxy
    {
        private readonly HubConnection connection;
        private readonly IHubProxy hubProxy;

        public delegate void LobbyCreatedEventHandler(object source, EventArgs args);

        public event LobbyCreatedEventHandler LobbyCreated;

        public LobbyProxy() : this("http://localhost:8080") { }
        public LobbyProxy(string url)
        {
            connection = new HubConnection(url);
            hubProxy = connection.CreateHubProxy("LobbiesHub");

            connection.Start().Wait();
        }

        public void CreateLobby(string lobbyName, int playerLimit)
        {
            hubProxy.Invoke("CreateLobby", new object[] { lobbyName, playerLimit, "" });
        }

        protected virtual void OnLobbyCreated()
        {
            if (LobbyCreated != null)
                LobbyCreated(this, EventArgs.Empty);
        }
    }
}
