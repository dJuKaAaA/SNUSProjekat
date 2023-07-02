using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SCADACore.Models
{
    public enum Role
    {
        Admin,
        Standard,
    }

    [DataContract]
    public class User
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public Role Role { get; set; }
        [DataMember]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}