using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBContext
{
    public class DataBaseContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RoleEntity> Roles { get; set; }
        public virtual DbSet<UserStateEntity> UserStates { get; set; }
        public virtual DbSet<BoardGameEntity> BoardGames { get; set; }
        public virtual DbSet<BoardTypeEntity> BoardTypes { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<AditionalFileEntity> AditionalFiles { get; set; }
        public virtual DbSet<ImageEntity> Images { get; set; }
        public virtual DbSet<TableBoardStateEntity> TableBoardStates { get; set; }
        public virtual DbSet<ActiveGameEntity> ActiveGames { get; set; }
        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<ActiveGameStateEntity> ActiveGameStates { get; set; }
        public virtual DbSet<SentInvitationEntity> SentInvitations { get; set; }
        public virtual DbSet<InvitationStateEntity> InvitationStates { get; set; }
        public virtual DbSet<UserAddressEntity> UserAddress { get; set; } 
        public virtual DbSet<ReviewEntity> Reviews { get; set; }

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString(), sqlServerOptions => sqlServerOptions.CommandTimeout(360));
            }
        }

        public static string GetConnectionString()
        {
            return "data source=boardgamedatabase.database.windows.net;initial catalog=boardgame;persist security info=false;user id=hakaigod420;password=Krokodilas0858; TrustServerCertificate=True";
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BoardGameEntity>()
                .HasMany(x => x.Categories)
                .WithMany(y => y.Boards)
                .UsingEntity(j => j.ToTable("BoardGameCategory"));

            builder.Entity<BoardGameEntity>()
                .HasMany(x => x.BoardTypes)
                .WithMany(y => y.Boards)
                .UsingEntity(j => j.ToTable("BoardGameType"));

            builder.Entity<UserEntity>()
                .HasMany(x => x.ActiveGamesParcipators)
                .WithMany(y => y.Users)
                .UsingEntity(j => j.ToTable("UserActiveGame"));

            builder.Entity<UserEntity>().HasMany(x => x.ActiveGamesCreators)
                .WithOne(j => j.Creator).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<AddressEntity>().HasMany(x => x.ActiveGameInThisAddress).WithOne(j => j.Address);

            builder.Entity<ActiveGameEntity>().HasOne(x => x.Creator).WithMany(j => j.ActiveGamesCreators);

            builder.Entity<ActiveGameEntity>().HasOne(x=>x.BoardGame).WithMany(j=>j.ActiveGames).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SentInvitationEntity>().HasOne(x => x.SelectedActiveGame).WithMany(x=>x.SentInvitations).HasForeignKey(x=>x.SelectedActiveGameId);

            builder.Entity<SentInvitationEntity>().HasOne(x => x.User).WithMany(x => x.ReceivedInvitations).HasForeignKey(x => x.UserId);

            builder.Entity<SentInvitationEntity>().HasOne(x => x.InvitationState).WithMany(x => x.Invitations).HasForeignKey(x => x.InvitationStateId);

            builder.Entity<ReviewEntity>().HasOne(x => x.Writer).WithMany(x => x.Reviews).HasForeignKey(x=>x.WriterId);

            builder.Entity<ReviewEntity>().HasOne(x => x.SelectedBoardGame).WithMany(x => x.Reviews).HasForeignKey(x => x.SelectedBoardGameId).OnDelete(DeleteBehavior.NoAction);



            /*
             * 
            builder.Entity<ImageEntity>().
                HasKey(x => new { x.BoardGameId });

            builder.Entity<ImageEntity>()
                .HasOne(x => x.BoardGame)
                .WithMany(z => z.Images)
                .HasForeignKey (x => x.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            */
        }
    }
}
