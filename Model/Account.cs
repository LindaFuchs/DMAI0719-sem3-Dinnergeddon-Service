namespace Model
{
    public class Account
    {
        public Account(string username, string email, string password, int ID)
        {
            Username = username;
            Email = email;
            Password = password;
            this.ID = ID;
        }

        public Account(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        private int ID { get; }
    }
}
