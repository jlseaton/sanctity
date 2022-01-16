using System.Collections.Generic;

namespace Game.Data
{
    public partial class Model// : DbContext
    {
        public Model()
            //: base("name=DatabaseModel")
        {
        }

        public virtual List<Player> Players { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual database_firewall_rules database_firewall_rules { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<database_firewall_rules>()
        //        .Property(e => e.start_ip_address)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<database_firewall_rules>()
        //        .Property(e => e.end_ip_address)
        //        .IsUnicode(false);
        //}
    }
}
