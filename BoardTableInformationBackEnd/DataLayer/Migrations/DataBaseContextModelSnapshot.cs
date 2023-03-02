﻿// <auto-generated />
using System;
using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BoardGameEntityBoardTypeEntity", b =>
                {
                    b.Property<int>("BoardTypesBoardTypeId")
                        .HasColumnType("int");

                    b.Property<int>("BoardsBoardGameId")
                        .HasColumnType("int");

                    b.HasKey("BoardTypesBoardTypeId", "BoardsBoardGameId");

                    b.HasIndex("BoardsBoardGameId");

                    b.ToTable("BoardGameEntityBoardTypeEntity");
                });

            modelBuilder.Entity("BoardGameEntityCategoryEntity", b =>
                {
                    b.Property<int>("BoardsBoardGameId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.HasKey("BoardsBoardGameId", "CategoriesCategoryId");

                    b.HasIndex("CategoriesCategoryId");

                    b.ToTable("BoardGameEntityCategoryEntity");
                });

            modelBuilder.Entity("DataLayer.Models.AditionalFileEntity", b =>
                {
                    b.Property<int>("AditionalFilesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AditionalFilesId"), 1L, 1);

                    b.Property<int>("BoardGameId")
                        .HasColumnType("int");

                    b.Property<string>("FileLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AditionalFilesId");

                    b.HasIndex("BoardGameId");

                    b.ToTable("AditionalFiles");
                });

            modelBuilder.Entity("DataLayer.Models.BoardGameEntity", b =>
                {
                    b.Property<int>("BoardGameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoardGameId"), 1L, 1);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<int>("PlayableAge")
                        .HasColumnType("int");

                    b.Property<int>("PlayerCount")
                        .HasColumnType("int");

                    b.Property<int?>("PlayingTime")
                        .HasColumnType("int");

                    b.Property<string>("Rules")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("TableBoardStateId")
                        .HasColumnType("int");

                    b.Property<string>("Thubnail_Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BoardGameId");

                    b.HasIndex("TableBoardStateId");

                    b.HasIndex("UserId");

                    b.ToTable("BoardGames");
                });

            modelBuilder.Entity("DataLayer.Models.BoardTypeEntity", b =>
                {
                    b.Property<int>("BoardTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoardTypeId"), 1L, 1);

                    b.Property<string>("BoardTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BoardTypeId");

                    b.ToTable("BoardTypes");
                });

            modelBuilder.Entity("DataLayer.Models.CategoryEntity", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataLayer.Models.ImageEntity", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"), 1L, 1);

                    b.Property<string>("Alias")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("BoardGameId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.HasIndex("BoardGameId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("DataLayer.Models.RoleEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DataLayer.Models.TableBoardStateEntity", b =>
                {
                    b.Property<int>("TableBoardStateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TableBoardStateId");

                    b.ToTable("TableBoardStates");
                });

            modelBuilder.Entity("DataLayer.Models.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastTimeConnection")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserStateId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserStateId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataLayer.Models.UserStateEntity", b =>
                {
                    b.Property<int>("UserStateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserStateId");

                    b.ToTable("UserStates");
                });

            modelBuilder.Entity("BoardGameEntityBoardTypeEntity", b =>
                {
                    b.HasOne("DataLayer.Models.BoardTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("BoardTypesBoardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.BoardGameEntity", null)
                        .WithMany()
                        .HasForeignKey("BoardsBoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BoardGameEntityCategoryEntity", b =>
                {
                    b.HasOne("DataLayer.Models.BoardGameEntity", null)
                        .WithMany()
                        .HasForeignKey("BoardsBoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.CategoryEntity", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.AditionalFileEntity", b =>
                {
                    b.HasOne("DataLayer.Models.BoardGameEntity", "BoardGame")
                        .WithMany("AditionalFiles")
                        .HasForeignKey("BoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoardGame");
                });

            modelBuilder.Entity("DataLayer.Models.BoardGameEntity", b =>
                {
                    b.HasOne("DataLayer.Models.TableBoardStateEntity", "TableBoardState")
                        .WithMany("BoardGames")
                        .HasForeignKey("TableBoardStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TableBoardState");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.ImageEntity", b =>
                {
                    b.HasOne("DataLayer.Models.BoardGameEntity", "BoardGame")
                        .WithMany("Images")
                        .HasForeignKey("BoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoardGame");
                });

            modelBuilder.Entity("DataLayer.Models.UserEntity", b =>
                {
                    b.HasOne("DataLayer.Models.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.UserStateEntity", "UserState")
                        .WithMany("Users")
                        .HasForeignKey("UserStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserState");
                });

            modelBuilder.Entity("DataLayer.Models.BoardGameEntity", b =>
                {
                    b.Navigation("AditionalFiles");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("DataLayer.Models.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DataLayer.Models.TableBoardStateEntity", b =>
                {
                    b.Navigation("BoardGames");
                });

            modelBuilder.Entity("DataLayer.Models.UserStateEntity", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
