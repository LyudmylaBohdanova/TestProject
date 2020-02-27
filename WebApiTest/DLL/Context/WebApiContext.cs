using DLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class WebApiContext : DbContext
    {
        public WebApiContext() : base("WebApiContext") { }

        public DbSet<Interval> Intervals { get; set; }
        public DbSet<LogInterval> LogIntervals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
