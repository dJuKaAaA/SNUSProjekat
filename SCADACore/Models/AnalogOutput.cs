using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SCADACore.Models
{
    [DataContract]
    public class AnalogOutput
    {
        [Key]
        [DataMember]
        public string TagName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int IOAddress { get; set; }

        [DataMember]
        public double Value { get; set; }

        [DataMember]
        public double LowLimit { get; set; }

        [DataMember]
        public double HighLimit { get; set; }

        [DataMember]
        public string Units { get; set; }

        public override string ToString()
        {
            return $"TagName: {TagName}, Description: {Description}, IOAddress: {IOAddress}, Value: {Value}, LowLimit: {LowLimit}, HighLimit: {HighLimit}, Units: {Units}";
        }
    }
}