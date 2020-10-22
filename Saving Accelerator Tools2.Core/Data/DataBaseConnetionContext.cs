﻿using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.Connection;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.Other.Data;
using Saving_Accelerator_Tools2.Core.Models.Other.InterTable;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using Saving_Accelerator_Tools2.Core.Models.Users;
using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Data
{
    public class DataBaseConnetionContext : DbContext
    {
        public DbSet<Users_DB> User { get; set; }

        public DbSet<Plant_DB> Plant { get; set; }
        public DbSet<Devision_DB> Devision { get; set; }
        public DbSet<PageVisibility_DB> Page { get; set; }
        public DbSet<Role_DB> Role { get; set; }
        public DbSet<ActionLeader_DB> ActionLeader { get; set; }

        //Dane
        public DbSet<MonthlyANC_DB> ANC_Monthly { get; set; }
        public DbSet<MonthlyPNC_DB> PNC_Monthly { get; set; }
        public DbSet<RevisionANC_DB> ANC_Revision { get; set; }
        public DbSet<RevisionPNC_DB> PNC_Revision { get; set; }
        public DbSet<PNCTotality_DB> PNC_Totality { get; set; }

        //Dodatkowe
        public DbSet<Currency_DB> Currency { get; set; }
        public DbSet<Targets_DB> Targets { get; set; }
        public DbSet<STK_DB> STK { get; set; }
        public DbSet<Approvals_DB> Approvals { get; set; }

        //Action
        public DbSet<Action_DB> Action { get; set; }

        //Action Dodatki
        public DbSet<Tag_DB> Tag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Modele dla Urzytkownika
            UserModelBuilder(modelBuilder);

            //Model dla Liderów Akcji
            ActionLeaderModelBuilder(modelBuilder);

            //Tworzenie modelu dla Akcji 
            ActionModelBuilder(modelBuilder);
        }

        private void ActionModelBuilder(ModelBuilder modelBuilder)
        {
            //Many to Many Action <<=>> Devision
            modelBuilder.Entity<Action_Devision_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.DevisionID });
            modelBuilder.Entity<Action_Devision_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_Devision)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_Devision_InterTable>()
                .HasOne(bc => bc.Devision)
                .WithMany(b => b.Action_Devision)
                .HasForeignKey(bc => bc.DevisionID);

            //Many to Many Action <<=>> Plant
            modelBuilder.Entity<Action_Plant_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.PlantID });
            modelBuilder.Entity<Action_Plant_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_Plant)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_Plant_InterTable>()
                .HasOne(bc => bc.Plant)
                .WithMany(b => b.Action_Plant)
                .HasForeignKey(bc => bc.PlantID);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.connectionString);
        }

        protected void ActionLeaderModelBuilder(ModelBuilder modelBuilder)
        {
            //Many to many ActionLeader <=> Plant
            modelBuilder.Entity<ActionLeader_Plant_DB>()
                .HasKey(bc => new { bc.LeaderID, bc.FactoryID });
            modelBuilder.Entity<ActionLeader_Plant_DB>()
                .HasOne(bc => bc.Leader)
                .WithMany(b => b.Leader_Plant)
                .HasForeignKey(bc => bc.LeaderID);
            modelBuilder.Entity<ActionLeader_Plant_DB>()
                .HasOne(bc => bc.Factory)
                .WithMany(b => b.Leader_Plant)
                .HasForeignKey(bc => bc.FactoryID);

            //Many to many ActionLeader <=> Devision
            modelBuilder.Entity<ActionLeader_Devision_DB>()
                .HasKey(bc => new { bc.LeaderID, bc.DevisionID });
            modelBuilder.Entity<ActionLeader_Devision_DB>()
                .HasOne(bc => bc.Leader)
                .WithMany(b => b.Leader_Devision)
                .HasForeignKey(bc => bc.LeaderID);
            modelBuilder.Entity<ActionLeader_Devision_DB>()
                .HasOne(bc => bc.Devision)
                .WithMany(b => b.Leader_Devisions)
                .HasForeignKey(bc => bc.DevisionID);
        }

        protected void UserModelBuilder(ModelBuilder modelBuilder)
        {
            //Many to many User <=> Plant
            modelBuilder.Entity<UserPlant_DB>()
                .HasKey(bc => new { bc.UserID, bc.PlantID });
            modelBuilder.Entity<UserPlant_DB>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.User_Plant)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<UserPlant_DB>()
                .HasOne(bc => bc.Plant)
                .WithMany(b => b.User_Plant)
                .HasForeignKey(bc => bc.PlantID);

            //Many to many User <=> Devision
            modelBuilder.Entity<User_Devision_DB>()
                .HasKey(bc => new { bc.UserID, bc.DevisionID });
            modelBuilder.Entity<User_Devision_DB>()
                .HasOne(bc => bc.Users)
                .WithMany(b => b.User_Devisions)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<User_Devision_DB>()
                .HasOne(bc => bc.Devisions)
                .WithMany(b => b.User_Devisions)
                .HasForeignKey(bc => bc.DevisionID);

            //Many to many User <=> Pages
            modelBuilder.Entity<User_Pages_DB>()
                .HasKey(bc => new { bc.UserID, bc.PageID });
            modelBuilder.Entity<User_Pages_DB>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.User_Pages)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<User_Pages_DB>()
                .HasOne(bc => bc.Page)
                .WithMany(b => b.User_Pages)
                .HasForeignKey(bc => bc.PageID);

            //Many to many User <=> Role
            modelBuilder.Entity<User_Role_DB>()
                .HasKey(bc => new { bc.UserID, bc.RoleID });
            modelBuilder.Entity<User_Role_DB>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.User_Role)
                .HasForeignKey(bc => bc.UserID);
            modelBuilder.Entity<User_Role_DB>()
                .HasOne(bc => bc.Role)
                .WithMany(b => b.User_Role)
                .HasForeignKey(bc => bc.RoleID);
        }
    }
}
