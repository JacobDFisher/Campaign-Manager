using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CampaignManagerDbContext: DbContext
    {
        public CampaignManagerDbContext(DbContextOptions<CampaignManagerDbContext> options): base(options)
        {
        }
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupJoin<Group>> GroupGroups { get; set; }
        public virtual DbSet<GroupJoin<Entity>> EntityGroups { get; set; }
        public virtual DbSet<GroupJoin<Identity>> IdentityGroups { get; set; }
        public virtual DbSet<Identity> Identities { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Permissions> Permissionses { get; set; }
        public virtual DbSet<Revealed> Revealeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DetailConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionsConfiguration());
            modelBuilder.ApplyConfiguration(new RevealedConfiguration());

            // Many-to-Many Tables
            modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GroupIdentityConfiguration());
            modelBuilder.ApplyConfiguration(new GroupGroupConfiguration());

        }
    }
}
