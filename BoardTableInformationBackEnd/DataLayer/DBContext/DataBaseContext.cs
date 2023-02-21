using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
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
    }
}
