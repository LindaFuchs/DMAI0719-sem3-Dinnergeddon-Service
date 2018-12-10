using Microsoft.AspNet.SignalR.Client;
using System;

namespace SignalRTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:8080");

            IHubProxy proxy = connection.CreateHubProxy("LobbiesHub");

            proxy.On<string, string, int>("addMessage", (lobbyName, lobbyCreator, limit) =>
            {
                Console.WriteLine("Another user craeted a new lobby with a limit of {0}", limit);
                Console.WriteLine("{0} by {1}", lobbyName, lobbyCreator);
            });

            connection.Start().Wait();

            Console.WriteLine("Connection to the hub was made");
            proxy.Invoke("NewLobby", "Federlizer's lobby", "Federlizer", 3);
            Console.WriteLine("method called...");
            Console.ReadLine();
        }
    }
}
