using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Lobby
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Account> Players { get; set; }
        [DataMember]
        public int Limit { get; set; }

        public string PasswordHash { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Lobby other))
                return false;

            if (other.Id != Id)
                return false;
        
            return true;
        }
    }
}
