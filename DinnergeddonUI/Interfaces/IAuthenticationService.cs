using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinnergeddonUI.Model;

namespace DinnergeddonUI.Interfaces
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }
}
