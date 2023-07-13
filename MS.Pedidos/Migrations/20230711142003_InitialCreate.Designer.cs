﻿// <auto-generated />
using System;
using MS.Pedidos.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MS.Pedidos.Migrations
{
    [DbContext(typeof(TransientDbContextFactory))]
    [Migration("20230711142003_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MS.Pedidos.Entities.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmailCliente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumeroCartao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumeroPedido")
                        .HasColumnType("int");

                    b.Property<int>("StatusPedido")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
