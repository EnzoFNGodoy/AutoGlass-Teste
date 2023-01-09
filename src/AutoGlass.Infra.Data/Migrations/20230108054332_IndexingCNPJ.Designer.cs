﻿// <auto-generated />
using System;
using AutoGlass.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoGlass.Infra.Data.Migrations
{
    [DbContext(typeof(AutoGlassContext))]
    [Migration("20230108054332_IndexingCNPJ")]
    partial class IndexingCNPJ
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AutoGlass.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("ExpirationDate");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime")
                        .HasColumnName("ProductionDate");

                    b.Property<Guid?>("ProviderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AutoGlass.Domain.Entities.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("AutoGlass.Domain.Entities.Product", b =>
                {
                    b.HasOne("AutoGlass.Domain.Entities.Provider", "Provider")
                        .WithMany("Products")
                        .HasForeignKey("ProviderId");

                    b.OwnsOne("AutoGlass.Domain.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Description");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("AutoGlass.Domain.Entities.Provider", b =>
                {
                    b.OwnsOne("AutoGlass.Domain.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("ProviderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Description");

                            b1.HasKey("ProviderId");

                            b1.ToTable("Providers");

                            b1.WithOwner()
                                .HasForeignKey("ProviderId");
                        });

                    b.OwnsOne("AutoGlass.Domain.ValueObjects.CNPJ", "CNPJ", b1 =>
                        {
                            b1.Property<Guid>("ProviderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("varchar(14)")
                                .HasColumnName("CNPJ");

                            b1.HasKey("ProviderId");

                            b1.HasIndex("Number")
                                .IsUnique();

                            b1.ToTable("Providers");

                            b1.WithOwner()
                                .HasForeignKey("ProviderId");
                        });

                    b.Navigation("CNPJ")
                        .IsRequired();

                    b.Navigation("Description")
                        .IsRequired();
                });

            modelBuilder.Entity("AutoGlass.Domain.Entities.Provider", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
