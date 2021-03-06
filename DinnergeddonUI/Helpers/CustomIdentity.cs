﻿using System;
using System.Security.Principal;

namespace DinnergeddonUI.Helpers

{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(Guid id, string userName, string email, string[] roles)
        {
            Id = id;
            Name = userName;
            Email = email;
            Roles = roles;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string[] Roles { get; private set; }
        public Guid Id { get; private set; }

        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }

    }

    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(Guid.Empty, string.Empty, string.Empty, new string[] { })
        { }
    }
}
