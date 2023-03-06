using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
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

            /*
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
