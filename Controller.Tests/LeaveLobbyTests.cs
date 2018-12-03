using System;
using Model;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Controller.Tests
{
    [TestClass]
    public class LeaveLobbyTests
    {
        private ILobbyContainer lobbyContainer;
        private ILobbyController lobbyController;
        private IAccountController accountController;
        private Lobby lobby;
        private Account account;

        [TestInitialize]
        public void Setup()
        {
            lobbyContainer = LobbyContainer.GetInstance();
            accountController = new AccountControllerMock();
            lobbyController = new LobbyController(lobbyContainer, accountController);

            lobby = new Lobby()
            {
                Id = Guid.NewGuid(),
                Limit = 4,
                Players = new List<Account>(),
            };

            account = new Account()
            {
                Id = Guid.NewGuid(),
            };

            lobbyContainer.Add(lobby);
        }

        public List<Account> CreateDummyPlayers(int n)
        {
            List<Account> accounts = new List<Account>();

            for (var i = 0; i < n; i++)
            {
                accounts.Add(new Account()
                {
                    Id = Guid.NewGuid(),
                });
            }

            return accounts;
        }

        [TestMethod]
        public void Test_LeaveLobby_LobbyDoesntExist_Fails()
        {
            // Setup
            Guid fakeID;
            do
            {
                fakeID = Guid.NewGuid();
            } while (fakeID == lobby.Id);

            Lobby fakeLobby = new Lobby()
            {
                Id = fakeID,
                Limit = 4,
                Players = new List<Account>(),
            };

            fakeLobby.Players.Add(account);
            List<Account> expectedList = fakeLobby.Players;
            int expectedPlayerCount = fakeLobby.Players.Count;

            // Action
            lobbyController.LeaveLobby(account.Id, fakeLobby.Id);

            // Assert
            Assert.AreEqual(expectedList, fakeLobby.Players);
            Assert.AreEqual(expectedPlayerCount, fakeLobby.Players.Count);
        }

        [TestMethod]
        public void Test_LeaveLobby_AccountNotInLobby_Fails()
        {
            // Setup
            lobby.Players.AddRange(CreateDummyPlayers(2));
            int expectedPlayerCount = lobby.Players.Count;

            // Action
            lobbyController.LeaveLobby(account.Id, lobby.Id);

            // Assert
            Assert.AreEqual(expectedPlayerCount, lobby.Players.Count);
        }

        [TestMethod]
        public void Test_LeaveLobby_Success()
        {
            // Setup
            lobby.Players.AddRange(CreateDummyPlayers(2));
            lobby.Players.Add(account);
            int expectedPlayerCount = lobby.Players.Count - 1;

            // Action
            lobbyController.LeaveLobby(account.Id, lobby.Id);

            // Assert
            Assert.AreEqual(expectedPlayerCount, lobby.Players.Count);
            foreach (Account player in lobby.Players)
            {
                Assert.IsFalse(player.Id == account.Id);
            }
        }

        [TestMethod]
        public void Test_LeaveLobby_LobbyGetsRemovedAfterLastPersonLeaves_Success()
        {
            // Setup
            lobby.Players.Add(account);

            // Action
            lobbyController.LeaveLobby(account.Id, lobby.Id);

            // Assert
            foreach (Lobby l in lobbyContainer.GetLobbies())
            {
                if (l.Id == lobby.Id)
                    Assert.Fail("A lobby with the same ID is still in the list");
            }
        }
    }
}
