using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Orders_management.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name = OrderDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Orders_management.Migrations.Configuration>());
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
