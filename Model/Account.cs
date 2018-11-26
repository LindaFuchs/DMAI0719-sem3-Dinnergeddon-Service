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

        public override int GetHashCode()
        {
            var hashCode = -1621603198;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PasswordHash);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SecurityStamp);
            return hashCode;
        }
    }
}
