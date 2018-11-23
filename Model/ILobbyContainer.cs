using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface ILobbyContainer
    {
        IEnumerable<Lobby> GetActiveLobbies();
        void Add(Lobby lobby);
    }
}
