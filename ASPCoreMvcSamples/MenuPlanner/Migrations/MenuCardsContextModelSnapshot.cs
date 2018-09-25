﻿// <auto-generated />
using System;
using MenuPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MenuPlanner.Migrations
{
    [DbContext(typeof(MenuCardsContext))]
    partial class MenuCardsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenuPlanner.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("Day");

                    b.Property<int>("MenuCardId");

                    b.Property<int>("Order");

                    b.Property<decimal>("Price");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("MenuCardId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("MenuPlanner.Models.MenuCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.ToTable("MenuCards");
                });

            modelBuilder.Entity("MenuPlanner.Models.Menu", b =>
                {
                    b.HasOne("MenuPlanner.Models.MenuCard", "MenuCard")
                        .WithMany("Menus")
                        .HasForeignKey("MenuCardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
