using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }
        [DataMember]
        public string SecurityStamp { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Account other))
                return false;

            if (other.Id != Id || other.Username != Username || other.Email != Email)
                return false;
            
            return true;
        }
    }
}
