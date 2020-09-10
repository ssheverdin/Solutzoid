using System;
using System.Collections.Generic;
using System.Text;
using MetadataUtility.Models;
using Microsoft.EntityFrameworkCore;

namespace MetadataUtility.Connectors.SqlServerConnector
{
    public class SqlServerContext : DbContext
    {
        private readonly string _connectionString;

        public SqlServerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<DomainEntity> DomainEntities { get; set; }
        public DbSet<DomainEntityAttribute> DomainEntityAttributes { get; set; }
        public DbSet<DomainEntityRelation> DomainEntityRelations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DomainEntity>().HasNoKey();
            modelBuilder.Entity<DomainEntityAttribute>().HasNoKey();
            modelBuilder.Entity<DomainEntityRelation>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
