using System;
using System.Text.RegularExpressions;
using Model;

namespace DinnergeddonService
{
    public class AccountService : IAccountService
    {
        public bool CheckEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckUsername(string username)
        {
            if(username.Length < 3 || username.Length > 32)
            {
                return false;
            }
            return Regex.IsMatch(username, "^[a-zA-Z0-9._-]+$");
        }

        public bool CheckPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
        }

        public bool EditAccount(string username, string email, string password)
        {
            if()
            {
                throw new ArgumentException();
            }

            return true;
        }

        public Account GetInfo()
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool RegisterAccount(string username, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
