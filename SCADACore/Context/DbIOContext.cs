using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SCADACore.Context
{
    public class DbIOContext : DbContext
    {
        public DbSet<DigitalInput> DigitalInputs { get; set; }
        public DbSet<DigitalOutput> DigitalOutputs { get; set; }
        public DbSet<AnalogInput> AnalogInputs { get; set; }
        public DbSet<AnalogOutput> AnalogOutputs { get; set; }
        public DbSet<TagAlarm> TagAlarms { get; set; }
        public DbSet<TagReport> TagReports { get; set; }
        public DbSet<AlarmReport> AlarmReports { get; set; }
    }
}