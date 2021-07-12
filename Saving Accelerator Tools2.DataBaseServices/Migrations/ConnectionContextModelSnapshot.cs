﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saving_Accelerator_Tools2.DataBaseServices.Data;

namespace Saving_Accelerator_Tools2.DataBaseServices.Migrations
{
    [DbContext(typeof(ConnectionContext))]
    partial class ConnectionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Data.ANC", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FactoryID")
                        .HasColumnType("int");

                    b.Property<string>("Item")
                        .HasColumnType("varchar(9)");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Revision")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.HasIndex("FactoryID");

                    b.ToTable("ANCs");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Data.PNC", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FactoryID")
                        .HasColumnType("int");

                    b.Property<string>("Item")
                        .HasColumnType("varchar(9)");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(14,0)");

                    b.Property<string>("Revision")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.HasIndex("FactoryID");

                    b.ToTable("PNCs");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Data.PNCPlatform", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FactoryID")
                        .HasColumnType("int");

                    b.Property<string>("Installation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Revision")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Structure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("FactoryID");

                    b.ToTable("PNCPlatforms");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Data.StandardCost", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FactoryID")
                        .HasColumnType("int");

                    b.Property<string>("IDCO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Item")
                        .HasColumnType("varchar(9)");

                    b.Property<decimal>("STK3")
                        .HasColumnType("decimal(12,4)");

                    b.Property<decimal>("Year")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("ID");

                    b.HasIndex("FactoryID");

                    b.ToTable("StandardCosts");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Devision", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("FactoryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FactoryID");

                    b.ToTable("Devisions");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Factories", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Plant")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Factories");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Leaders", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("DevisionID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DevisionID");

                    b.ToTable("Leaders");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("FactoryID")
                        .HasColumnType("int");

                    b.Property<decimal>("From")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Until")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("FactoryID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("BIZLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DevisionID")
                        .HasColumnType("int");

                    b.Property<int?>("FactoriesID")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MailSubscription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DevisionID");

                    b.HasIndex("FactoriesID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Data.ANC", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Factories", "Factory")
                        .WithMany("ANCs")
                        .HasForeignKey("FactoryID");

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Data.PNC", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Factories", "Factory")
                        .WithMany("PNCs")
                        .HasForeignKey("FactoryID");

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Data.PNCPlatform", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Factories", "Factory")
                        .WithMany("PNCPlatforms")
                        .HasForeignKey("FactoryID");

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Data.StandardCost", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Factories", "Factory")
                        .WithMany("StandardCosts")
                        .HasForeignKey("FactoryID");

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Devision", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Factories", "Factory")
                        .WithMany("Devisions")
                        .HasForeignKey("FactoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Leaders", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Devision", "Devision")
                        .WithMany("Leaders")
                        .HasForeignKey("DevisionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Devision");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Tag", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Factories", "Facotry")
                        .WithMany("Tags")
                        .HasForeignKey("FactoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facotry");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.User", b =>
                {
                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Devision", "Devision")
                        .WithMany("Users")
                        .HasForeignKey("DevisionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Saving_Accelerator_Tools2.Model.Others.Factories", null)
                        .WithMany("Users")
                        .HasForeignKey("FactoriesID");

                    b.Navigation("Devision");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Devision", b =>
                {
                    b.Navigation("Leaders");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Factories", b =>
                {
                    b.Navigation("ANCs");

                    b.Navigation("Devisions");

                    b.Navigation("PNCPlatforms");

                    b.Navigation("PNCs");

                    b.Navigation("StandardCosts");

                    b.Navigation("Tags");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
