using System;
using System.ServiceModel;
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
    }
}
