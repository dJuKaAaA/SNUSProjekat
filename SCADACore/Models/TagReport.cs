using SCADACore.Execptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SCADACore.Models
{
    [DataContract]
    public class TagReport
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string TagName { get; set; }

        [DataMember]
        public int Timestamp { get; set; }

        [DataMember]
        public double Value { get; set; }

        [DataMember]
        public IOType TagType { get; set; }
    }
}