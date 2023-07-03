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
    public class TagAlarm
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember] 
        public string AlarmName { get; set; }

        [DataMember]
        public AlarmType Type { get; set; }

        [DataMember]
        public PriorityType Priority { get; set; }

        [DataMember]
        public double Limit { get; set; }

        [ForeignKey("AnalogInput")]
        public string InputTagName { get; set; }

        [DataMember]
        public AnalogInput AnalogInput { get; set; }

    }

    public enum AlarmType
    {
        Low,
        High
    }

    public enum PriorityType
    {
        High,
        Medium,
        Low
    }
}