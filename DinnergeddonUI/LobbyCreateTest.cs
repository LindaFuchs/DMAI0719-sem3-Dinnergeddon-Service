using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnergeddonUI
{
    class LobbyCreateTest
    {
        public ObservableCollection<Lobby> lobbies;
        private static LobbyCreateTest instance;

        private LobbyCreateTest()
        {
            lobbies = new ObservableCollection<Lobby>();
            lobbies.Add(new Lobby() { Name = "Hello", Limit = 4 });
            lobbies.Add(new Lobby() { Name = "Test", Limit = 3 });
            lobbies.Add(new Lobby() { Name = "Bdg", Limit = 3 });
            lobbies.Add(new Lobby() { Name = "ssss", Limit = 3 });
            lobbies.Add(new Lobby() { Name = "Bdddddg", Limit = 3 });
            lobbies.Add(new Lobby() { Name = "Bdffffg", Limit = 3 });
            lobbies.Add(new Lobby() { Name = "Bdg", Limit = 3 });
            lobbies.Add(new Lobby() { Name = "ssss", Limit = 3 });
            lobbies.Add(new Lobby() { Name = "Bdddddg", Limit = 3 });
            lobbies.Add(new Lobby() { Name = "Bdffffg", Limit = 3 });

        }

        public static LobbyCreateTest Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LobbyCreateTest();
                }
                return instance;
            }
        }

      

        public void CreateLobby(string name, int limit)
        {
            lobbies.Add(new Lobby { Name = name, Limit = limit });
        }
    }

   
}
