using Controller;
using Microsoft.AspNet.SignalR;
using Model;
using System;

namespace SignalR.Hubs
{
    public class LobbiesHub : Hub
    {
        private readonly ILobbyController lobbyController;

        public LobbiesHub()
        {
            lobbyController = new LobbyController(LobbyContainer.GetInstance(), new AccountController());
        }

        public void CreateLobby(string lobbyName, int playerLimit, string password)
        {
            Lobby lobby = null;

            if (password == string.Empty || password == null)
                lobby = lobbyController.CreateLobby(lobbyName, playerLimit);
            else
                lobby = lobbyController.CreateLobby(lobbyName, playerLimit, password);

            Clients.All.lobbyCreated(lobbyController.GetLobbies());
        }

        public void GetLobbies()
        {
            //TODO:Figure out how to return this
            //Clients.Caller.getLobbies("all lobbies here");
        }

        public void JoinLobby(Guid accountId, Guid lobbyId, string password)
        {
            bool success = false;

            if (password == string.Empty || password == null)
                success = lobbyController.JoinLobby(accountId, lobbyId, string.Empty);
            else
                success = lobbyController.JoinLobby(accountId, lobbyId, password);

            //TODO:Figure out how to return this
            //Clients.All.playerJoinedLobby("The lobby and player here");
        }

        public void LeaveLobby(Guid accountid, Guid lobbyid)
        {
            lobbyController.LeaveLobby(accountid, lobbyid);

            //TODO:Figure out how to return this
            //Clients.All.playerLeftLobby("The lobby and the player here");
        }

        public void GetLobbyById(Guid lobbyId)
        {
            //TODO:Figure out how to return this
            //Clients.Caller.getLobbyById("the lobby here");
        }
    }
}
