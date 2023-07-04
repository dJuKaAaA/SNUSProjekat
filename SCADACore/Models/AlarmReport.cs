using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SCADACore.Models
{
    [DataContract]
    public class AlarmReport
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [ForeignKey("TagAlarm")]
        public int AlarmId { get; set; }

        [DataMember]
        public TagAlarm TagAlarm { get; set; }

        [DataMember]
        public int Timestamp { get; set; }

    }
}