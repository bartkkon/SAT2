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
    [Migration("20200923082304_DropMonthlyANC")]
    partial class DropMonthlyANC
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.ActionLeader_DB", b =>
                {
                    b.Property<int>("LeaderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.Other.Devision_DB", b =>
                {
                    b.Property<int>("DevisionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Plant")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlantID");

                    b.ToTable("Plant");
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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ANC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDCO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("STD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("STK");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Core.Models.ProductionData.Targets_DB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                });
#pragma warning restore 612, 618
        }
    }
}
