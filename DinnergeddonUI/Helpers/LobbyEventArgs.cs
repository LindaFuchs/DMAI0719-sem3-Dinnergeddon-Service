using DinnergeddonUI.AccountServiceReference;
using System;

namespace DinnergeddonUI.Helpers
{
    public class LobbyEventArgs : EventArgs
    {
        public Lobby Lobby { get; set; }
    }
}
