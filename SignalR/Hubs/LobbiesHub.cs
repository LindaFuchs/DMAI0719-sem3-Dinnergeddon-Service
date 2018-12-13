using Controller;
using Microsoft.AspNet.SignalR;
using Model;
using System;
using System.Collections.Generic;

namespace SignalR.Hubs
{
    /// <summary>
    /// 
    /// ###Event names###
    /// lobbyCreated - when a new lobby is created, that lobby is passed to the clients
    /// lobbyUpdated - when a lobby is updated, that lobby is passed to the clients
    /// lobbyDeleted - when a lobby is removed, that lobby is passed to the clients for deletion
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
        
        public IEnumerable<Lobby> GetLobbies()
        {
            return lobbyController.GetLobbies();
        }

        public Lobby JoinLobby(Guid accountId, Guid lobbyId, string password)
        {
            bool success = false;

            if (password == string.Empty || password == null)
                success = lobbyController.JoinLobby(accountId, lobbyId, string.Empty);
            else
                success = lobbyController.JoinLobby(accountId, lobbyId, password);

            if (success)
            {
                Clients.All.lobbyUpdated(lobbyController.GetLobbyById(lobbyId));
                return lobbyController.GetLobbyById(lobbyId);
            } else
            {
                return null;
            }
        }
        
        public void LeaveLobby(Guid accountId, Guid lobbyId)
        {
            lobbyController.LeaveLobby(accountId, lobbyId);

            Lobby lobby = lobbyController.GetLobbyById(lobbyId);

            if (lobby == null)
                Clients.All.lobbyDeleted(lobbyId);
            else
                Clients.All.lobbyUpdated(lobby);
        }
        
        public Lobby GetLobbyById(Guid lobbyId)
        {
            return lobbyController.GetLobbyById(lobbyId);
        }
    }
}
