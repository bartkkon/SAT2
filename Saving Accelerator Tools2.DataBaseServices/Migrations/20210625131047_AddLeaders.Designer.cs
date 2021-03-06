﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Saving_Accelerator_Tools2.DataBaseServices.Data;

namespace Saving_Accelerator_Tools2.DataBaseServices.Migrations
{
    [DbContext(typeof(ConnectionContext))]
    [Migration("20210625131047_AddLeaders")]
    partial class AddLeaders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Devision", b =>
                {
                    b.Navigation("Leaders");
                });

            modelBuilder.Entity("Saving_Accelerator_Tools2.Model.Others.Factories", b =>
                {
                    b.Navigation("Devisions");
                });
#pragma warning restore 612, 618
        }
    }
}
