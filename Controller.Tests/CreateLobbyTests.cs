using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;

namespace Controller.Tests
{
    [TestClass]
    public class CreateLobbyTests
    {
        private LobbyContainerMock container;
        private ILobbyController controller;

        [TestInitialize]
        public void Setup()
        {
            container = new LobbyContainerMock();
            controller = new LobbyController(container, new AccountControllerMock());
        }

        [TestMethod]
        public void Test_CreateLobby_Added_To_Container_Succeeds()
        {
            // Action
            controller.CreateLobby("Random name", 3);

            // Assert
            Assert.AreEqual<int>(1, container.lobbies.Count);
        }

        [TestMethod]
        public void Test_CreateLobby_HasID_Succeeds()
        {
            // Action
            Lobby lobby = controller.CreateLobby("Random name", 3);

            // Assert
            Assert.AreNotEqual(new Guid(), lobby.Id);
        }

        [TestMethod]
        public void Test_CreateLobby_ReturnsLobbyObjectWithCorrectName_WithoutPassword_Succeeds()
        {
            // Setup
            string name = "Random name here";
            int limit = 3;

            // Action
            Lobby lobby = controller.CreateLobby(name, limit);

            // Assert
            Assert.AreEqual<string>(name, lobby.Name);
        }

        [TestMethod]
        public void Test_CreateLobby_ReturnsLobbyObjectWithCorrectPlayerLimit_WithoutPassword_Succeeds()
        {
            // Setup
            string name = "Random name here";
            int limit = 3;

            // Action
            Lobby lobby = controller.CreateLobby(name, limit);

            // Assert
            Assert.AreEqual<string>(name, lobby.Name);
        }

        [TestMethod]
        public void Test_CreateLobby_ReturnsSameLobbyAsTheOneItSavesInContainer_Succeeds()
        {
            // Setup
            string name = "Random name here";
            int limit = 3;

            // Action
            Lobby lobby = controller.CreateLobby(name, limit);

            // Assert
            Assert.AreEqual<Lobby>(lobby, container.lobbies[0]);
        }

        [TestMethod]
        public void Test_CreateLobby_ReturnsLobbyObjectWithCorrectParameters_WithPassword_Succeeds()
        {
            // Setup
            string name = "Random name here";
            int limit = 3;
            string password = "as;ldfjaosfuhbasofiugaosdhfl";

            // Action
            Lobby lobby = controller.CreateLobby(name, limit, password);

            // Assert
            Assert.IsTrue(PasswordHasher.VerifyPassword(lobby.PasswordHash, password));
        }

        [TestMethod]
        public void Test_CreateLobby_BlankName_ReturnsNull()
        {
            // Setup
            string name = "";
            int limit = 1;

            // Action
            Lobby lobby = controller.CreateLobby(name, limit);

            // Assert
            Assert.IsNull(lobby);
        }

        [TestMethod]
        public void Test_CreateLobby_LessThan2Players_ReturnsNull()
        {
            // Setup
            string name = "a name";
            int limit = 1;

            // Action
            Lobby lobby = controller.CreateLobby(name, limit);

            // Assert
            Assert.IsNull(lobby);
        }

        [TestMethod]
        public void Test_CreateLobby_MoreThan4Players_Fails()
        {
            // Setup
            string name = "a name";
            int limit = 5;

            // Action
            Lobby lobby = controller.CreateLobby(name, limit);

            // Assert
            Assert.IsNull(lobby);
        }

        [TestMethod]
        public void Test_CreateLobby_2PLayers_Succeeds()
        {
            // Setup
            string name = "a name";
            int limit = 2;

            // Action
            Lobby lobby = controller.CreateLobby(name, limit);

            // Assert
            Assert.AreEqual<int>(limit, lobby.Limit);
        }

        [TestMethod]
        public void Test_CreateLobby_4PLayers_Succeeds()
        {
            // Setup
            string name = "a name";
            int limit = 4;

            // Action
            Lobby lobby = controller.CreateLobby(name, limit);

            // Assert
            Assert.AreEqual<int>(limit, lobby.Limit);
        }
    }

    public class LobbyContainerMock : ILobbyContainer
    {
        public List<Lobby> lobbies;

        public LobbyContainerMock()
        {
            lobbies = new List<Lobby>();
        }

        public bool AccountInLobby(Guid id)
        {
            throw new NotImplementedException();
        }

        public void AccountNotInLobby(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Lobby lobby)
        {
            lobbies.Add(lobby);
        }

        public void AddAccountAsInLobby(Account a)
        {
            throw new NotImplementedException();
        }

        public List<Guid> GetAccountInLobbies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lobby> GetLobbies()
        {
            throw new NotImplementedException();
        }

        public Lobby GetLobbyById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
