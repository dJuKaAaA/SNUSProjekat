using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SCADACore.Context
{
    public class DbAlarmContext : DbContext
    {
        public DbSet<TagAlarm> Alarms { get; set; }
    }
}