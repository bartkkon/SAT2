using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tools2.Connection;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification.InterTable;
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
        public DbSet<ANCChange_DB> ANCChange { get; set; }
        public DbSet<CalcByPNC> PNCList { get; set; }
        //PNCSPecual
        public DbSet<PNCSpecial_PNC_DB> PNCSpecial_PNC { get; set; }
        public DbSet<PNCSpecial_ANC_DB> PNCSpecial_ANC { get; set; }

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
        public DbSet<PlatformCalc_DB> PlatformCalcs { get; set; }
        public DbSet<ANCSpecial_ByItems_DB> ANCSpecial_Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Modele dla Urzytkownika
            UserModelBuilder(modelBuilder);

            //Model dla Liderów Akcji
            ActionLeaderModelBuilder(modelBuilder);

            //Tworzenie modelu dla Akcji 
            ActionModelBuilder(modelBuilder);

            //Tworzenie powiązania między PNC a ANC dla PNCSpecial
            PNCSpecalBuilder(modelBuilder);
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

            //Many to Many Action <<=>> Leader
            modelBuilder.Entity<Action_Leader_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.LeaderID });
            modelBuilder.Entity<Action_Leader_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_Leader)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_Leader_InterTable>()
                .HasOne(bc => bc.Leader)
                .WithMany(b => b.Action_Leader)
                .HasForeignKey(bc => bc.LeaderID);

            //Many to Many Action <<=>> Tag
            modelBuilder.Entity<Action_Tag_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.TagID });
            modelBuilder.Entity<Action_Tag_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_Tag)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_Tag_InterTable>()
                .HasOne(bc => bc.Tag)
                .WithMany(b => b.Action_Tag)
                .HasForeignKey(bc => bc.TagID);

            //Many to Many Action <<=>> ANCChange 
            modelBuilder.Entity<Action_ANCChage_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.ANCChangeID });
            modelBuilder.Entity<Action_ANCChage_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_ANCChange)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_ANCChage_InterTable>()
                .HasOne(bc => bc.ANCChange)
                .WithMany(b => b.Action_ANCChange)
                .HasForeignKey(bc => bc.ANCChangeID);

            //Many to Many Action <<=>> CalcByPNC -->> List PNC do danej akcji jesli będzie wybrane liczenie po PNC 
            modelBuilder.Entity<Action_PNC_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.PNCID });
            modelBuilder.Entity<Action_PNC_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_PNC)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_PNC_InterTable>()
                .HasOne(bc => bc.List)
                .WithMany(b => b.Action_PNC)
                .HasForeignKey(bc => bc.PNCID);

            //Many to Many Action <<=>> ANCChange dla Platform 
            modelBuilder.Entity<Action_ANCChangePlatform_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.ChangeID });
            modelBuilder.Entity<Action_ANCChangePlatform_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_ANCChange_Platform)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_ANCChangePlatform_InterTable>()
                .HasOne(bc => bc.Platform)
                .WithMany(b => b.Action_ANCChange_Platform)
                .HasForeignKey(bc => bc.ChangeID);

            //Many to Many Action <<=>> ANCChange dla Itemów dodatnich i ujemnych 
            modelBuilder.Entity<Action_ANCChange_Items_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.ItemID });
            modelBuilder.Entity<Action_ANCChange_Items_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_ANCChange_Items)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_ANCChange_Items_InterTable>()
                .HasOne(bc => bc.Item)
                .WithMany(b => b.Action_ANCChange_Items)
                .HasForeignKey(bc => bc.ItemID);

            //Many to Many Action <<=>> PNCSpecial 
            modelBuilder.Entity<Action_PNCSpecial_InterTable>()
                .HasKey(bc => new { bc.ActionID, bc.PNCSpecID });
            modelBuilder.Entity<Action_PNCSpecial_InterTable>()
                .HasOne(bc => bc.Action)
                .WithMany(b => b.Action_PNCSpecial)
                .HasForeignKey(bc => bc.ActionID);
            modelBuilder.Entity<Action_PNCSpecial_InterTable>()
                .HasOne(bc => bc.PNCSpecial)
                .WithMany(b => b.Action_PNCSpecial)
                .HasForeignKey(bc => bc.PNCSpecID);
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

        protected void PNCSpecalBuilder(ModelBuilder modelBuilder)
        {
            //Many to many PNCSPecial_PNC <=> PNCSpecial_ANC
            modelBuilder.Entity<PNCSPecial_PNC_ANC_InterTable>()
                .HasKey(bc => new { bc.PNC_ID, bc.ANC_ID });
            modelBuilder.Entity<PNCSPecial_PNC_ANC_InterTable>()
                .HasOne(bc => bc.PNCChange)
                .WithMany(b => b.PNC_ANC_Special)
                .HasForeignKey(bc => bc.PNC_ID);
            modelBuilder.Entity<PNCSPecial_PNC_ANC_InterTable>()
                .HasOne(bc => bc.ANCChange)
                .WithMany(b => b.PNC_ANC_Special)
                .HasForeignKey(bc => bc.ANC_ID);
        }
    }
}
