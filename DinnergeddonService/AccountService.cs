using System;

namespace DinnergeddonService
{
    public class AccountService : IAccountService
    {
        public bool Registration(string name, string email, string password, int id)
        {
            Console.WriteLine("Registering account with name {0}", name);
            return true;
        }
    }
}
