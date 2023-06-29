using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SCADACore.Context
{
    public class DbUserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}