using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.DataBaseServices.Connection;
using Saving_Accelerator_Tools2.Model.Others;

namespace Saving_Accelerator_Tools2.DataBaseServices.Data
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Factories> Factories { get; set; }
        public DbSet<Devision> Devisions { get; set; }
        public DbSet<Leaders> Leaders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factories>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Devision>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Leaders>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<Devision>()
                  .HasOne<Factories>(s=>s.Factory)
                  .WithMany(g => g.Devisions)
                  .HasForeignKey(s => s.FactoryID);
            modelBuilder.Entity<Leaders>()
                .HasOne<Devision>(s => s.Devision)
                .WithMany(g => g.Leaders)
                .HasForeignKey(s => s.DevisionID);

        }
    }
}
