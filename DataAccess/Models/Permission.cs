using Lib.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Permission<T>
    {
        public PermissionType PermissionType { get; set; }
        public int GrantorId { get; set; }
        public Identity Grantor { get; set; }
        public int GranteeId { get; set; }
        public Group Grantee { get; set; }
        public int PermissionsId { get; set; }
        public Permissions<T> Permissions { get; set; }
    }

    public class EntityPermissionConfiguration : IEntityTypeConfiguration<Permission<Entity>>
    {
        public void Configure(EntityTypeBuilder<Permission<Entity>> builder)
        {
            builder.HasKey(p => new { p.GrantorId, p.GranteeId, p.PermissionsId });
            builder.Property(p => p.PermissionType)
                .IsRequired();
            builder.HasOne(p => p.Grantor)
                .WithMany(i => i.EntityGrants)
                .HasForeignKey(p => p.GrantorId)
                .IsRequired();
            builder.HasOne(p => p.Grantee)
                .WithMany(g => g.EntityGrants)
                .HasForeignKey(p => p.GranteeId)
                .IsRequired();
            builder.HasOne(p => p.Permissions)
                .WithMany(p => p.Perms)
                .HasForeignKey(p => p.PermissionsId)
                .IsRequired();

        }
    }
    public class DetailPermissionConfiguration : IEntityTypeConfiguration<Permission<Detail>>
    {
        public void Configure(EntityTypeBuilder<Permission<Detail>> builder)
        {
            builder.HasKey(p => new { p.GrantorId, p.GranteeId, p.PermissionsId });
            builder.Property(p => p.PermissionType)
                .IsRequired();
            builder.HasOne(p => p.Grantor)
                .WithMany(i => i.DetailGrants)
                .HasForeignKey(p => p.GrantorId)
                .IsRequired();
            builder.HasOne(p => p.Grantee)
                .WithMany(g => g.DetailGrants)
                .HasForeignKey(p => p.GranteeId)
                .IsRequired();
            builder.HasOne(p => p.Permissions)
                .WithMany(p => p.Perms)
                .HasForeignKey(p => p.PermissionsId)
                .IsRequired();

        }
    }
}
