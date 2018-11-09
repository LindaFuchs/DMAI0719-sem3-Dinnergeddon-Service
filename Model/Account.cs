namespace Model
{
    public class Account
    {
        private string username;
        private string email;
        private string password;
        private int ID;

        public Account(string username, string email, string password, int ID)
        {
            this.username = username;
            this.email = email;
            this.password = password;
            this.ID = ID;
        }
    }
}
