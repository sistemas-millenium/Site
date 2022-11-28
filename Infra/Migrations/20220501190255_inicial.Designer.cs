﻿// <auto-generated />
using System;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.Migrations
{
    [DbContext(typeof(Context.Context))]
    [Migration("20220501190255_inicial")]
    partial class inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infra.Entities.Carrossel", b =>
                {
                    b.Property<Guid>("CarrosselId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caminho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ordenacao")
                        .HasColumnType("int");

                    b.HasKey("CarrosselId");

                    b.ToTable("Carrossel");
                });

            modelBuilder.Entity("Infra.Entities.Dado", b =>
                {
                    b.Property<Guid>("DadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caminho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ordenacao")
                        .HasColumnType("int");

                    b.Property<string>("Subtitulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DadoId");

                    b.ToTable("Dado");
                });

            modelBuilder.Entity("Infra.Entities.Menu", b =>
                {
                    b.Property<Guid>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descriacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ordenacao")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("Infra.Entities.MiniCarrossel", b =>
                {
                    b.Property<Guid>("MiniCarrosselId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caminho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ordenacao")
                        .HasColumnType("int");

                    b.HasKey("MiniCarrosselId");

                    b.ToTable("MiniCarrossel");
                });

            modelBuilder.Entity("Infra.Entities.Pergunta", b =>
                {
                    b.Property<Guid>("PerguntaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ordenacao")
                        .HasColumnType("int");

                    b.Property<string>("Resposta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PerguntaId");

                    b.ToTable("Pergunta");
                });

            modelBuilder.Entity("Infra.Entities.Projeto", b =>
                {
                    b.Property<Guid>("ProjetoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caminho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ordenacao")
                        .HasColumnType("int");

                    b.Property<string>("Subtitulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjetoId");

                    b.ToTable("Projeto");
                });

            modelBuilder.Entity("Infra.Entities.SobreNos", b =>
                {
                    b.Property<Guid>("SobreNosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caminho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextoPrincipal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextoSecundario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TituloChamada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TituloPrincipal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TituloSecundario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SobreNosId");

                    b.ToTable("SobreNos");
                });

            modelBuilder.Entity("Infra.Entities.TituloProjeto", b =>
                {
                    b.Property<Guid>("TituloProjetoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TituloProjetoId");

                    b.ToTable("TituloProjeto");
                });

            modelBuilder.Entity("Infra.Entities.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
