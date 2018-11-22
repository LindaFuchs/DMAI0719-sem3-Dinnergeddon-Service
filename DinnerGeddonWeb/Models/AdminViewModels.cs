using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DinnergeddonWeb.Models
{
    public class DisplayUserModel
    {

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }

    public class EditUserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}