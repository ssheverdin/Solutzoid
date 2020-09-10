using DataUnitOfWork.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SourceSystemConnectorDataAccess
{
    public class SourceSystemConnectorContext : DbContext
    {
        public SourceSystemConnectorContext(DbContextOptions<SourceSystemConnectorContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var baseType = typeof(EntityBase);
            var entityTypes = Assembly.GetExecutingAssembly().GetTypes().Where(i => i.IsClass && i != baseType && baseType.IsAssignableFrom(i)).ToList();
            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type).HasKey("Id");
            }
            modelBuilder.EnableAutoHistory(null);
        }
    }
}
