using DinnergeddonUI.LobbyServiceReference;
using System;

namespace DinnergeddonUI.Helpers
{
    public class LobbyEventArgs : EventArgs
    {
        public Lobby Lobby { get; set; }
    }
}
