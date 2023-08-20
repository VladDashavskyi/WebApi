using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dal.Entities;
using WebApi.Dal.EntityTypeConfigurations;

namespace WebApi.Dal.Context
{
    public class ProjectContext : DbContext
    {
        private IDbContextTransaction _transaction;

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
         
        }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public bool Commit()
        {
            int resultCount;

            try
            {
                resultCount = SaveChanges();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
            }

            return resultCount > 0;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }

        public async Task<bool> SaveChangesAsync()
        {
            int changes = ChangeTracker
                          .Entries()
                          .Count(p => p.State == EntityState.Modified
                                   || p.State == EntityState.Deleted
                                   || p.State == EntityState.Added);

            if (changes == 0)
            {
                return true;
            }

            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new IncidentConfiguration());
        }
    }
}
