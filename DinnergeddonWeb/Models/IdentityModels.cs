using System;
using Microsoft.AspNet.Identity;

namespace DinnergeddonWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IUser 
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        // Might be problematic
        public string Id => UserId.ToString();

       

    }
   
}