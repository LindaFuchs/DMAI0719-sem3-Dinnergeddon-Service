using System;

namespace DinnergeddonUI.Model
{
    public class User
    {
        public User(Guid id, string username, string email, string[] roles)
        {
            Id = id;
            Username = username;
            Email = email;
            Roles = roles;
        }

        public string Username { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
