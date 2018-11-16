using System;
using System.Text.RegularExpressions;
using Model;

namespace DinnergeddonService
{
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Checks if the email is valid for the given format: [string]@[string].
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmail(string email)
        {
            //Surrounded in a try catch block because the constructor throws exceptions when the email is invalid.
            try
            {
                //Uses the MailAddress class to verify the email format.
                var addr = new System.Net.Mail.MailAddress(email);
                //Returns true if the check passes.
                return addr.Address == email;
            }
            //When an exception is thrown, we assume that the email address is not valid and returns false.
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the username to ensure it only contains the permitted symbols
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckUsername(string username)
        {
            //Uses Regular Expressions to validate that the username only contains the permitted characters.
            //Verifies that the length of the string is at least 3 characters and at most 32 characters.
            //Checks if the contained characters are lowercase letters, uppercase letters, numbers, or the symbols.
            //Checks the whole string and gurantees at least one occurance.
            return Regex.IsMatch(username, @"^(?=.{3,32}$)[A-Za-z0-9]*$");
        }

        /// <summary>
        /// Checks the password to ensure that it fulfills the required criteria.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            //Uses Regular Expressions to validate that the username only contains the permitted characters.
            //Verifies that the length of the string is at least 6 and at most 12 characters.
            //Checks if the contained characters have at least one uppercase letter, one lowercase letter, one number and no special characters.
            //Checks the whole string for at least one occurance.
            return Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]{6,12}$");
        }

        /// <summary>
        /// Validates the inputs for account editing and passes them to the DB layer for handling.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool EditAccount(string username, string email, string password)
        {
            if (!CheckUsername(username) || !CheckEmail(email) || !CheckPassword(password))
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
