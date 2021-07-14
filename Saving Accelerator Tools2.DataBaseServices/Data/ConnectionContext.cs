using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.DataBaseServices.Connection;
using Saving_Accelerator_Tools2.Model;
using Saving_Accelerator_Tools2.Model.Approvals;
using Saving_Accelerator_Tools2.Model.Data;
using Saving_Accelerator_Tools2.Model.Others;
using SavingAcceleratorTools2.ProjectModels.Data;
using SavingAcceleratorTools2.ProjectModels.Helpers;
using SavingAcceleratorTools2.ProjectModels.Users;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Saving_Accelerator_Tools2.DataBaseServices.Data
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Factories> Factories { get; set; }
        public DbSet<Devision> Devisions { get; set; }
        public DbSet<Leaders> Leaders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StandardCost> StandardCosts { get; set; }

        public DbSet<PNC> PNCs { get; set; }
        public DbSet<ANC> ANCs { get; set; }
        public DbSet<PNCPlatform> PNCPlatforms { get; set; }
        public DbSet<Currencies> Currencies { get; set; }

        //Zatwierdzanie danych
        public DbSet<TeamApp> Teams { get; set; }
        public DbSet<Approval> Approvals { get; set; }

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
            modelBuilder.Entity<Tag>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<User>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<User>()
                .Property(e => e.Type)
                .HasConversion(v => v.ToString(), v => (AccountTypes)Enum.Parse(typeof(AccountTypes), v));
            modelBuilder.Entity<User>()
                .Property(e => e.MailSubscription)
                .HasConversion(
                v => JsonSerializer.Serialize(v, null),
                v => JsonSerializer.Deserialize<ICollection<Subscription>>(v, null));
            modelBuilder.Entity<StandardCost>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<PNC>()
                .HasKey(e => e.ID);
            modelBuilder.Entity<PNC>()
                .Property(e => e.Revision)
                .HasConversion(v => v.ToString(), v => (Revisions)Enum.Parse(typeof(Revisions), v));
            modelBuilder.Entity<PNC>()
                .Property(e => e.Month)
                .HasConversion(v => v.ToString(), v => (Months)Enum.Parse(typeof(Months), v));
            modelBuilder.Entity<Currencies>()
                .HasKey(e => e.ID);
            modelBuilder.Entity<Currencies>()
                .Property(e => e.Currency)
                .HasConversion(v => v.ToString(), v => (Currency)Enum.Parse(typeof(Currency), v));
            modelBuilder.Entity<StandardCost>()
                .Property(e => e.Currency)
                .HasConversion(v => v.ToString(), v => (Currency)Enum.Parse(typeof(Currency), v));

            modelBuilder.Entity<ANC>()
                .HasKey(e => e.ID);
            modelBuilder.Entity<ANC>()
                .Property(e => e.Revision)
                .HasConversion(v => v.ToString(), v => (Revisions)Enum.Parse(typeof(Revisions), v));
            modelBuilder.Entity<ANC>()
                .Property(e => e.Month)
                .HasConversion(v => v.ToString(), v => (Months)Enum.Parse(typeof(Months), v));

            modelBuilder.Entity<PNCPlatform>()
                .HasKey(e => e.ID);
            modelBuilder.Entity<PNCPlatform>()
                .Property(e => e.Revision)
                .HasConversion(v => v.ToString(), v => (Revisions)Enum.Parse(typeof(Revisions), v));
            modelBuilder.Entity<PNCPlatform>()
                .Property(e => e.Month)
                .HasConversion(v => v.ToString(), v => (Months)Enum.Parse(typeof(Months), v));
            modelBuilder.Entity<PNCPlatform>()
                .Property(e => e.Structure)
                .HasConversion(v => v.ToString(), v => (Structure)Enum.Parse(typeof(Structure), v));
            modelBuilder.Entity<PNCPlatform>()
                .Property(e => e.Installation)
                .HasConversion(v => v.ToString(), v => (Installation)Enum.Parse(typeof(Installation), v));

            modelBuilder.Entity<Devision>()
                  .HasOne<Factories>(s => s.Factory)
                  .WithMany(g => g.Devisions)
                  .HasForeignKey(s => s.FactoryID);
            modelBuilder.Entity<Leaders>()
                .HasOne<Devision>(s => s.Devision)
                .WithMany(g => g.Leaders)
                .HasForeignKey(s => s.DevisionID);
            modelBuilder.Entity<Tag>()
                .HasOne<Factories>(s => s.Facotry)
                .WithMany(g => g.Tags)
                .HasForeignKey(s => s.FactoryID);

            modelBuilder.Entity<StandardCost>()
                .HasOne<Factories>(s => s.Factory)
                .WithMany(g => g.StandardCosts);
            modelBuilder.Entity<ANC>()
                .HasOne<Factories>(s => s.Factory)
                .WithMany(g => g.ANCs);
            modelBuilder.Entity<PNC>()
                .HasOne<Factories>(s => s.Factory)
                .WithMany(g => g.PNCs);
            modelBuilder.Entity<PNCPlatform>()
                .HasOne<Factories>(s => s.Factory)
                .WithMany(g => g.PNCPlatforms);


            modelBuilder.Entity<User>()
                .HasOne<Devision>(s => s.Devision)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.DevisionID);

        }

        protected void ApprovalsDB(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamApp>()
                .HasKey(e => e.ID);
            modelBuilder.Entity<TeamApp>()
                .Property(e => e.Status)
                .HasConversion(v => v.ToString(), v => (Status)Enum.Parse(typeof(Status), v));
            modelBuilder.Entity<TeamApp>()
                .HasOne<Devision>(s => s.Devision)
                .WithMany(g => g.TeamApps);
            modelBuilder.Entity<TeamApp>()
                .HasOne<Approval>(g => g.Approval)
                .WithMany(g => g.Teams);


            modelBuilder.Entity<Approval>()
                .HasKey(e => e.ID);
            modelBuilder.Entity<Approval>()
                .Property(e => e.Status)
                .HasConversion(v => v.ToString(), v => (Status)Enum.Parse(typeof(Status), v));
            modelBuilder.Entity<Approval>()
                .Property(e => e.Revision)
                .HasConversion(v => v.ToString(), v => (Revisions)Enum.Parse(typeof(Revisions), v));
            modelBuilder.Entity<Approval>()
                .Property(e => e.Month)
                .HasConversion(v => v.ToString(), v => (Months)Enum.Parse(typeof(Months), v));
        }
    }
}
