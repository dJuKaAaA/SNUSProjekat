using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SCADACore.Models
{
    [DataContract]
    public class DigitalInput
    {
        [Key]
        [DataMember]
        public string TagName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int IOAddress { get; set; }

        [DataMember]
        public int ScanTime { get; set; }  // in milliseconds

        [DataMember]
        public bool OnScan { get; set; }

        [DataMember]
        public bool Value { get; set; }
    }
}