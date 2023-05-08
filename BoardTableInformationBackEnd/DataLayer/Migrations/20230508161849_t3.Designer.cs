﻿// <auto-generated />
using System;
using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20230508161849_t3")]
    partial class t3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ActiveGameEntityUserEntity", b =>
                {
                    b.Property<int>("ActiveGamesParcipatorsActiveGameId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("ActiveGamesParcipatorsActiveGameId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("UserActiveGame", (string)null);
                });

            modelBuilder.Entity("BoardGameEntityBoardTypeEntity", b =>
                {
                    b.Property<int>("BoardTypesBoardTypeId")
                        .HasColumnType("int");

                    b.Property<int>("BoardsBoardGameId")
                        .HasColumnType("int");

                    b.HasKey("BoardTypesBoardTypeId", "BoardsBoardGameId");

                    b.HasIndex("BoardsBoardGameId");

                    b.ToTable("BoardGameType", (string)null);
                });

            modelBuilder.Entity("BoardGameEntityCategoryEntity", b =>
                {
                    b.Property<int>("BoardsBoardGameId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.HasKey("BoardsBoardGameId", "CategoriesCategoryId");

                    b.HasIndex("CategoriesCategoryId");

                    b.ToTable("BoardGameCategory", (string)null);
                });

            modelBuilder.Entity("DataLayer.Models.ActiveGameEntity", b =>
                {
                    b.Property<int>("ActiveGameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActiveGameId"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("BoardGameId")
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("InvitationStateId")
                        .HasColumnType("int");

                    b.Property<float>("Map_X_Cords")
                        .HasColumnType("real");

                    b.Property<float>("Map_Y_Cords")
                        .HasColumnType("real");

                    b.Property<DateTime>("MeetDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlayersNeed")
                        .HasColumnType("int");

                    b.Property<int>("RegistredPlayerCount")
                        .HasColumnType("int");

                    b.HasKey("ActiveGameId");

                    b.HasIndex("AddressId");

                    b.HasIndex("BoardGameId");

                    b.HasIndex("CreatorId");

                    b.ToTable("ActiveGames");
                });

            modelBuilder.Entity("DataLayer.Models.ActiveGameStateEntity", b =>
                {
                    b.Property<int>("ActiveGameStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActiveGameStateId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActiveGameStateId");

                    b.ToTable("ActiveGameStates");
                });

            modelBuilder.Entity("DataLayer.Models.AddressEntity", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
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
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

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

                    b.Property<int?>("BoardGameId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.HasIndex("BoardGameId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("DataLayer.Models.InvitationStateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InvitationStates");
                });

            modelBuilder.Entity("DataLayer.Models.ReviewEntity", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("SelectedBoardGameId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WriteDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WriterId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("SelectedBoardGameId");

                    b.HasIndex("WriterId");

                    b.ToTable("Reviews");
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

            modelBuilder.Entity("DataLayer.Models.SentInvitationEntity", b =>
                {
                    b.Property<int>("SentInvitationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SentInvitationId"), 1L, 1);

                    b.Property<int>("InvitationStateId")
                        .HasColumnType("int");

                    b.Property<int>("SelectedActiveGameId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SentInvitationId");

                    b.HasIndex("InvitationStateId");

                    b.HasIndex("SelectedActiveGameId");

                    b.HasIndex("UserId");

                    b.ToTable("SentInvitations");
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

            modelBuilder.Entity("DataLayer.Models.UserAddressEntity", b =>
                {
                    b.Property<int>("UserAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserAddressId"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Map_X_Coords")
                        .HasColumnType("float");

                    b.Property<double?>("Map_Y_Coords")
                        .HasColumnType("float");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserAddressId");

                    b.ToTable("UserAddress");
                });

            modelBuilder.Entity("DataLayer.Models.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EnableInvitationNotifications")
                        .HasColumnType("bit");

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

                    b.HasIndex("AddressId");

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

            modelBuilder.Entity("ActiveGameEntityUserEntity", b =>
                {
                    b.HasOne("DataLayer.Models.ActiveGameEntity", null)
                        .WithMany()
                        .HasForeignKey("ActiveGamesParcipatorsActiveGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("DataLayer.Models.ActiveGameEntity", b =>
                {
                    b.HasOne("DataLayer.Models.AddressEntity", "Address")
                        .WithMany("ActiveGameInThisAddress")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.BoardGameEntity", "BoardGame")
                        .WithMany("ActiveGames")
                        .HasForeignKey("BoardGameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.UserEntity", "Creator")
                        .WithMany("ActiveGamesCreators")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("BoardGame");

                    b.Navigation("Creator");
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
                        .HasForeignKey("BoardGameId");

                    b.Navigation("BoardGame");
                });

            modelBuilder.Entity("DataLayer.Models.ReviewEntity", b =>
                {
                    b.HasOne("DataLayer.Models.BoardGameEntity", "SelectedBoardGame")
                        .WithMany("Reviews")
                        .HasForeignKey("SelectedBoardGameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.UserEntity", "Writer")
                        .WithMany("Reviews")
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SelectedBoardGame");

                    b.Navigation("Writer");
                });

            modelBuilder.Entity("DataLayer.Models.SentInvitationEntity", b =>
                {
                    b.HasOne("DataLayer.Models.InvitationStateEntity", "InvitationState")
                        .WithMany("Invitations")
                        .HasForeignKey("InvitationStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.ActiveGameEntity", "SelectedActiveGame")
                        .WithMany("SentInvitations")
                        .HasForeignKey("SelectedActiveGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.UserEntity", "User")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvitationState");

                    b.Navigation("SelectedActiveGame");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.UserEntity", b =>
                {
                    b.HasOne("DataLayer.Models.UserAddressEntity", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

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

                    b.Navigation("Address");

                    b.Navigation("Role");

                    b.Navigation("UserState");
                });

            modelBuilder.Entity("DataLayer.Models.ActiveGameEntity", b =>
                {
                    b.Navigation("SentInvitations");
                });

            modelBuilder.Entity("DataLayer.Models.AddressEntity", b =>
                {
                    b.Navigation("ActiveGameInThisAddress");
                });

            modelBuilder.Entity("DataLayer.Models.BoardGameEntity", b =>
                {
                    b.Navigation("ActiveGames");

                    b.Navigation("AditionalFiles");

                    b.Navigation("Images");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("DataLayer.Models.InvitationStateEntity", b =>
                {
                    b.Navigation("Invitations");
                });

            modelBuilder.Entity("DataLayer.Models.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DataLayer.Models.TableBoardStateEntity", b =>
                {
                    b.Navigation("BoardGames");
                });

            modelBuilder.Entity("DataLayer.Models.UserEntity", b =>
                {
                    b.Navigation("ActiveGamesCreators");

                    b.Navigation("ReceivedInvitations");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("DataLayer.Models.UserStateEntity", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
