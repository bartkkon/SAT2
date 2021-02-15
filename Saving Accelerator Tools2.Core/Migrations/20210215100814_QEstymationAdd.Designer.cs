﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saving_Accelerator_Tools2.Core.Data;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    [DbContext(typeof(DataBaseConnetionContext))]
    [Migration("20210215100814_QEstymationAdd")]
    partial class QEstymationAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.Action_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ActionID")
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("BI")
                        .HasColumnType("bit");

                    b.Property<bool>("D45")
                        .HasColumnType("bit");

                    b.Property<bool>("DMD")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(1000)");

                    b.Property<bool>("ECCC")
                        .HasColumnType("bit");

                    b.Property<bool>("ECCCSpec")
                        .HasColumnType("bit");

                    b.Property<decimal>("ECCCValue")
                        .HasColumnType("decimal(5,1)");

                    b.Property<bool>("FI")
                        .HasColumnType("bit");

                    b.Property<bool>("FS")
                        .HasColumnType("bit");

                    b.Property<bool>("FSBU")
                        .HasColumnType("bit");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("QEstymation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("StartYear")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("Action");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_ANCChage_InterTable", b =>
                {
                    b.Property<int>("ActionID")
                        .HasColumnType("int");

                    b.Property<int>("ANCChangeID")
                        .HasColumnType("int");

                    b.HasKey("ActionID", "ANCChangeID");

                    b.HasIndex("ANCChangeID");

                    b.ToTable("Action_ANCChage_InterTable");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_Devision_InterTable", b =>
                {
                    b.Property<int>("ActionID")
                        .HasColumnType("int");

                    b.Property<int>("DevisionID")
                        .HasColumnType("int");

                    b.HasKey("ActionID", "DevisionID");

                    b.HasIndex("DevisionID");

                    b.ToTable("Action_Devision_InterTable");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_Leader_InterTable", b =>
                {
                    b.Property<int>("ActionID")
                        .HasColumnType("int");

                    b.Property<int>("LeaderID")
                        .HasColumnType("int");

                    b.HasKey("ActionID", "LeaderID");

                    b.HasIndex("LeaderID");

                    b.ToTable("Action_Leader_InterTable");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_Plant_InterTable", b =>
                {
                    b.Property<int>("ActionID")
                        .HasColumnType("int");

                    b.Property<int>("PlantID")
                        .HasColumnType("int");

                    b.HasKey("ActionID", "PlantID");

                    b.HasIndex("PlantID");

                    b.ToTable("Action_Plant_InterTable");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_Tag_InterTable", b =>
                {
                    b.Property<int>("ActionID")
                        .HasColumnType("int");

                    b.Property<int>("TagID")
                        .HasColumnType("int");

                    b.HasKey("ActionID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("Action_Tag_InterTable");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.Specification.ANCChange_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Delta")
                        .HasColumnType("decimal(8,4)");

                    b.Property<decimal>("Estymacja")
                        .HasColumnType("decimal(8,4)");

                    b.Property<string>("NewANC")
                        .HasColumnType("varchar(9)");

                    b.Property<decimal>("NewANCQuantity")
                        .HasColumnType("decimal(8,4)");

                    b.Property<decimal>("NewSTK")
                        .HasColumnType("decimal(8,4)");

                    b.Property<string>("NextANC1")
                        .HasColumnType("varchar(9)");

                    b.Property<string>("NextANC2")
                        .HasColumnType("varchar(9)");

                    b.Property<string>("OldANC")
                        .HasColumnType("varchar(9)");

                    b.Property<decimal>("OldANCQuantity")
                        .HasColumnType("decimal(8,4)");

                    b.Property<decimal>("OldSTK")
                        .HasColumnType("decimal(8,4)");

                    b.Property<decimal>("Percent")
                        .HasColumnType("decimal(8,4)");

                    b.Property<decimal>("UserEstymacja")
                        .HasColumnType("decimal(8,4)");

                    b.HasKey("ID");

                    b.ToTable("ANCChange");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.Specification.Tag_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Finish")
                        .HasColumnType("decimal(4,0)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Start")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.ActionLeader_DB", b =>
                {
                    b.Property<int>("LeaderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeaderID");

                    b.ToTable("ActionLeader");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Data.MonthlyANC_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ANC")
                        .HasColumnType("varchar(9)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("ANC_Monthly");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Data.MonthlyPNC_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("PNC")
                        .HasColumnType("varchar(9)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("PNC_Monthly");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Data.PNCTotality_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Instalation")
                        .HasColumnType("varchar(4)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("Revision")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Structure")
                        .HasColumnType("varchar(5)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("PNC_Totality");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Data.RevisionANC_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ANC")
                        .HasColumnType("varchar(9)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Revision")
                        .HasColumnType("varchar(3)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("ANC_Revision");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Data.RevisionPNC_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("PNC")
                        .HasColumnType("varchar(9)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Revision")
                        .HasColumnType("varchar(3)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("PNC_Revision");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Devision_DB", b =>
                {
                    b.Property<int>("DevisionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Devision")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DevisionID");

                    b.ToTable("Devision");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.InterTable.ActionLeader_Devision_DB", b =>
                {
                    b.Property<int>("LeaderID")
                        .HasColumnType("int");

                    b.Property<int>("DevisionID")
                        .HasColumnType("int");

                    b.HasKey("LeaderID", "DevisionID");

                    b.HasIndex("DevisionID");

                    b.ToTable("ActionLeader_Devision_DB");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.InterTable.ActionLeader_Plant_DB", b =>
                {
                    b.Property<int>("LeaderID")
                        .HasColumnType("int");

                    b.Property<int>("FactoryID")
                        .HasColumnType("int");

                    b.HasKey("LeaderID", "FactoryID");

                    b.HasIndex("FactoryID");

                    b.ToTable("ActionLeader_Plant_DB");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Plant_DB", b =>
                {
                    b.Property<int>("PlantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Plant")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlantID");

                    b.ToTable("Plant");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.ProductionData.Approvals_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Devision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<string>("Revision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("Approvals");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.ProductionData.Currency_DB", b =>
                {
                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Dolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ECCC")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Euro")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Sek")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Year");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.ProductionData.STK_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ANC")
                        .HasColumnType("varchar(9)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDCO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("STD")
                        .HasColumnType("decimal(12,4)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.ToTable("STK");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.ProductionData.Targets_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("DM")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Electronic")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Mechanic")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NVR")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PC")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Revision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.InterTable.User_Devision_DB", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("DevisionID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "DevisionID");

                    b.HasIndex("DevisionID");

                    b.ToTable("User_Devision_DB");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.InterTable.User_Pages_DB", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "PageID");

                    b.HasIndex("PageID");

                    b.ToTable("User_Pages_DB");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.InterTable.User_Role_DB", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("User_Role_DB");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.PageVisibility_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Tab")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Page");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.Role_DB", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.UserPlant_DB", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("PlantID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "PlantID");

                    b.HasIndex("PlantID");

                    b.ToTable("UserPlant_DB");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.Users_DB", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_ANCChage_InterTable", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Action.Specification.ANCChange_DB", "ANCChange")
                        .WithMany("Action_ANCChange")
                        .HasForeignKey("ANCChangeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Action.Action_DB", "Action")
                        .WithMany("Action_ANCChange")
                        .HasForeignKey("ActionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("ANCChange");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_Devision_InterTable", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Action.Action_DB", "Action")
                        .WithMany("Action_Devision")
                        .HasForeignKey("ActionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.Devision_DB", "Devision")
                        .WithMany("Action_Devision")
                        .HasForeignKey("DevisionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Devision");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_Leader_InterTable", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Action.Action_DB", "Action")
                        .WithMany("Action_Leader")
                        .HasForeignKey("ActionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.ActionLeader_DB", "Leader")
                        .WithMany("Action_Leader")
                        .HasForeignKey("LeaderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_Plant_InterTable", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Action.Action_DB", "Action")
                        .WithMany("Action_Plant")
                        .HasForeignKey("ActionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.Plant_DB", "Plant")
                        .WithMany("Action_Plant")
                        .HasForeignKey("PlantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.InterTable.Action_Tag_InterTable", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Action.Action_DB", "Action")
                        .WithMany("Action_Tag")
                        .HasForeignKey("ActionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Action.Specification.Tag_DB", "Tag")
                        .WithMany("Action_Tag")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.InterTable.ActionLeader_Devision_DB", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.Devision_DB", "Devision")
                        .WithMany("Leader_Devisions")
                        .HasForeignKey("DevisionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.ActionLeader_DB", "Leader")
                        .WithMany("Leader_Devision")
                        .HasForeignKey("LeaderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Devision");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.InterTable.ActionLeader_Plant_DB", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.Plant_DB", "Factory")
                        .WithMany("Leader_Plant")
                        .HasForeignKey("FactoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.ActionLeader_DB", "Leader")
                        .WithMany("Leader_Plant")
                        .HasForeignKey("LeaderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.InterTable.User_Devision_DB", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.Devision_DB", "Devisions")
                        .WithMany("User_Devisions")
                        .HasForeignKey("DevisionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Users.Users_DB", "Users")
                        .WithMany("User_Devisions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Devisions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.InterTable.User_Pages_DB", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Users.PageVisibility_DB", "Page")
                        .WithMany("User_Pages")
                        .HasForeignKey("PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Users.Users_DB", "User")
                        .WithMany("User_Pages")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.InterTable.User_Role_DB", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Users.Role_DB", "Role")
                        .WithMany("User_Role")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Users.Users_DB", "User")
                        .WithMany("User_Role")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.UserPlant_DB", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Other.Plant_DB", "Plant")
                        .WithMany("User_Plant")
                        .HasForeignKey("PlantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Core.Models.Users.Users_DB", "User")
                        .WithMany("User_Plant")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.Action_DB", b =>
                {
                    b.Navigation("Action_ANCChange");

                    b.Navigation("Action_Devision");

                    b.Navigation("Action_Leader");

                    b.Navigation("Action_Plant");

                    b.Navigation("Action_Tag");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.Specification.ANCChange_DB", b =>
                {
                    b.Navigation("Action_ANCChange");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Action.Specification.Tag_DB", b =>
                {
                    b.Navigation("Action_Tag");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.ActionLeader_DB", b =>
                {
                    b.Navigation("Action_Leader");

                    b.Navigation("Leader_Devision");

                    b.Navigation("Leader_Plant");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Devision_DB", b =>
                {
                    b.Navigation("Action_Devision");

                    b.Navigation("Leader_Devisions");

                    b.Navigation("User_Devisions");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Plant_DB", b =>
                {
                    b.Navigation("Action_Plant");

                    b.Navigation("Leader_Plant");

                    b.Navigation("User_Plant");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.PageVisibility_DB", b =>
                {
                    b.Navigation("User_Pages");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.Role_DB", b =>
                {
                    b.Navigation("User_Role");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Users.Users_DB", b =>
                {
                    b.Navigation("User_Devisions");

                    b.Navigation("User_Pages");

                    b.Navigation("User_Plant");

                    b.Navigation("User_Role");
                });
#pragma warning restore 612, 618
        }
    }
}
