using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DatabaseManager.Models
{
    [DataContract]
    public class DigitalOutput
    {
        [Key]
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int IOAddress { get; set; }
        [DataMember]
        public bool Value { get; set; }
    }
}