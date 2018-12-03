using System;
using Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Controller.Tests
{
    [TestClass]
    public class JoinLobbyTests
    {
        private ILobbyContainer container;
        private ILobbyController controller;
        private IAccountController accountController;
        private Lobby lobby;
        private Account account;

        [TestInitialize]
        public void Setup()
        {
            container = LobbyContainer.GetInstance();
            accountController = new AccountControllerMock();
            controller = new LobbyController(container, accountController);
            lobby = new Lobby()
            {
                Id = Guid.NewGuid(),
                Limit = 3,
                Players = new List<Account>(),
            };
            account = new Account()
            {
                Id = Guid.NewGuid(),
            };

            container.Add(lobby);
            accountController.InsertAccount(account);
        }

        [TestMethod]
        public void Test_JoinLobby_LobbyDoesntExist_Fails()
        {
            // Setup
            Guid fakeID;

            do
            {
                fakeID = Guid.NewGuid();
            } while (lobby.Id == fakeID);

            // Action
            bool result = controller.JoinLobby(account.Id, Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_JoinLobby_AccountDoesntExist_Fails()
        {
            // Setup
            Guid fakeID;

            do
            {
                fakeID = Guid.NewGuid();
            } while (account.Id == fakeID);

            int expectedPlayerCount = lobby.Players.Count;

            // Action
            bool result = controller.JoinLobby(fakeID, lobby.Id);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedPlayerCount, lobby.Players.Count);
        }

        [TestMethod]
        public void Test_JoinLobby_Success()
        {
            // Setup
            int expectedPlayerCount = lobby.Players.Count + 1;

            // Action
            bool result = controller.JoinLobby(account.Id, lobby.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedPlayerCount, lobby.Players.Count);
        }

        [TestMethod]
        public void Test_JoinLobby_LobbyLimitReached_Fails()
        {
            // Setup
            Account dummy = new Account()
            {
                Id = Guid.NewGuid(),
            };

            lobby.Players.Add(dummy);
            lobby.Players.Add(dummy);
            lobby.Players.Add(dummy);

            accountController.InsertAccount(dummy);

            int expectedPlayerAmount = lobby.Players.Count;

            // Action
            bool result = controller.JoinLobby(account.Id, lobby.Id);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedPlayerAmount, lobby.Players.Count);
        }

        [TestMethod]
        public void Test_JoinLobby_AccountAlreadyInLobby_Fails()
        {
            // Setup
            lobby.Players.Add(account);
            int expectedPlayerAmount = lobby.Players.Count;

            // Action
            bool result = controller.JoinLobby(account.Id, lobby.Id);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedPlayerAmount, lobby.Players.Count);
        }

        [TestMethod]
        public void Test_JoinLobby_AccountAlreadyInAnotherLobby_Fails()
        {
            // Setup

            Lobby dummy = new Lobby()
            {
                Id = Guid.NewGuid(),
                Players = new List<Account>(),
            };
            dummy.Players.Add(account);
            container.Add(dummy);

            int expectedPlayerCount = lobby.Players.Count;

            // Action
            bool result = controller.JoinLobby(account.Id, lobby.Id);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedPlayerCount, lobby.Players.Count);
        }
    }

    public class AccountControllerMock : IAccountController
    {
        public List<Account> accounts = new List<Account>();

        public bool AddToRole(Guid accountId, string roleName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Account FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Account FindById(Guid id)
        {
            foreach(Account account in accounts)
            {
                if (account.Id == id)
                {
                    return account;
                }
            }
            return null;
        }

        public Account FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAccountRoles(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public bool InsertAccount(Account account)
        {
            accounts.Add(account);
            return true;
        }

        public bool IsInRole(Guid accountId, string roleName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAccount(Account updatedAccount)
        {
            throw new NotImplementedException();
        }

        public bool VerifyCredentials(string name, string password)
        {
            throw new NotImplementedException();
        }
    }
}
