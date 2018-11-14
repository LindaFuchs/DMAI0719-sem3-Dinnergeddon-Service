using System;

namespace Model {
    public class Account{

        private Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        
    }
}
