using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRTestClient
{
    class LobbyProxy
    {
        //-----------
        public delegate void LobbyCreatedEventHandler(object source, EventArgs args);

        public event LobbyCreatedEventHandler LobbyCreated;

        protected virtual void OnLobbyCreated()
        {
            if (LobbyCreated != null)
                LobbyCreated(this, EventArgs.Empty);
        }
        //-----------

        private readonly HubConnection connection;
        private readonly IHubProxy hubProxy;

        public LobbyProxy() : this("http://localhost:8080") { }
        public LobbyProxy(string url)
        {
            connection = new HubConnection(url);
            hubProxy = connection.CreateHubProxy("LobbiesHub");
        }

        public void Dispose()
        {
            OnLobbyCreated();
            connection.Dispose();
        }
    }
}
