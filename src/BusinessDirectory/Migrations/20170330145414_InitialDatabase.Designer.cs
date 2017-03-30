using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BusinessDirectory.Models;

namespace BusinessDirectory.Migrations
{
    [DbContext(typeof(BusinessDbContext))]
    [Migration("20170330145414_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusinessDirectory.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("CityId");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Description");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Phone");

                    b.Property<int>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CityId");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("BusinessDirectory.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BusinessDirectory.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("BusinessDirectory.Models.Business", b =>
                {
                    b.HasOne("BusinessDirectory.Models.Category")
                        .WithMany("Businesses")
                        .HasForeignKey("CategoryId");

                    b.HasOne("BusinessDirectory.Models.City")
                        .WithMany("Businesses")
                        .HasForeignKey("CityId");
                });
        }
    }
}
