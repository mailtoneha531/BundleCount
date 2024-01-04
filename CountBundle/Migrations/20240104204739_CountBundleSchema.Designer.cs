﻿// <auto-generated />
using CountBundle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CountBundle.Migrations
{
    [DbContext(typeof(BundleCountDbContext))]
    [Migration("20240104204739_CountBundleSchema")]
    partial class CountBundleSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CountBundle.Model.BundleEntity", b =>
                {
                    b.Property<int>("BundleEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BundleEntityId"));

                    b.Property<int>("InventoryCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsPairExist")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BundleEntityId");

                    b.ToTable("Bundles");
                });

            modelBuilder.Entity("CountBundle.Model.BundlePartEntity", b =>
                {
                    b.Property<int>("BundlePartEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BundlePartEntityId"));

                    b.Property<int>("BundleEntityId")
                        .HasColumnType("int");

                    b.Property<int>("InventoryCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsPairExist")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BundlePartEntityId");

                    b.HasIndex("BundleEntityId");

                    b.ToTable("BundleParts");
                });

            modelBuilder.Entity("CountBundle.Model.BundlePartSubEntity", b =>
                {
                    b.Property<int>("BundleSubEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BundleSubEntityId"));

                    b.Property<int>("BundlePartEntityId")
                        .HasColumnType("int");

                    b.Property<int>("InventoryCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsPairExist")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BundleSubEntityId");

                    b.HasIndex("BundlePartEntityId");

                    b.ToTable("BundlePartSubEntity");
                });

            modelBuilder.Entity("CountBundle.Model.BundlePartEntity", b =>
                {
                    b.HasOne("CountBundle.Model.BundleEntity", "BundleEntity")
                        .WithMany("Parts")
                        .HasForeignKey("BundleEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BundleEntity");
                });

            modelBuilder.Entity("CountBundle.Model.BundlePartSubEntity", b =>
                {
                    b.HasOne("CountBundle.Model.BundlePartEntity", "BundlePartEntity")
                        .WithMany("SubParts")
                        .HasForeignKey("BundlePartEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BundlePartEntity");
                });

            modelBuilder.Entity("CountBundle.Model.BundleEntity", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("CountBundle.Model.BundlePartEntity", b =>
                {
                    b.Navigation("SubParts");
                });
#pragma warning restore 612, 618
        }
    }
}