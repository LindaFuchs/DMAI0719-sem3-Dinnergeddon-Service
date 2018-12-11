using Controller;
using Microsoft.AspNet.SignalR;
using Model;
using System;

namespace SignalR.Hubs
{
    /// <summary>
    /// 
    /// ###Event names###
    /// lobbyCreated - when a new lobby is created, that lobby is passed to the clients
    /// lobbyUpdated - when a lobby is updated, that lobby is passed to the clients
    /// lobbyDeleted - when a lobby is removed, that lobby is passed to the clients for deletion
    /// joined       - when a client joins a lobby, whether or not that client has joined
    /// ###Event names###
    /// 
    /// </summary>
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

            Clients.All.lobbyCreated(lobby);
        }
        
        //TODO: Implement
        public void GetLobbies()
        {
            throw new NotImplementedException();
        }

        public void JoinLobby(Guid accountId, Guid lobbyId, string password)
        {
            bool success = false;

            if (password == string.Empty || password == null)
                success = lobbyController.JoinLobby(accountId, lobbyId, string.Empty);
            else
                success = lobbyController.JoinLobby(accountId, lobbyId, password);

            Clients.Caller.joined(success);

            // If the caller has joined the lobby, update all other clients
            if (success)
                Clients.AllExcept(Clients.Caller).lobbyUpdated(lobbyController.GetLobbyById(lobbyId));
        }
        
        //TODO: Implement, and think about how to notify users for lobby deleted, lobby updated (get info from controller prolly)
        public void LeaveLobby(Guid accountid, Guid lobbyid)
        {
            throw new NotImplementedException();
        }
        
        //TODO: Implement
        public void GetLobbyById(Guid lobbyId)
        {
            throw new NotImplementedException();
        }
    }
}
