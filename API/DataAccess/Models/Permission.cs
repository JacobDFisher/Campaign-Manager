using Lib.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Permission
    {
        public PermissionType PermissionType { get; set; }
        public int GrantorId { get; set; }
        public Identity Grantor { get; set; }
        public int GranteeId { get; set; }
        public Group Grantee { get; set; }
        public int PermissionsId { get; set; }
        public Permissions Permissions { get; set; }
    }

    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => new { p.GrantorId, p.GranteeId, p.PermissionsId });
            builder.Property(p => p.PermissionType)
                .IsRequired();
            builder.HasOne(p => p.Grantor)
                .WithMany(i => i.Grants)
                .HasForeignKey(p => p.GrantorId)
                .IsRequired();
            builder.HasOne(p => p.Grantee)
                .WithMany(g => g.Grants)
                .HasForeignKey(p => p.GranteeId)
                .IsRequired();
            builder.HasOne(p => p.Permissions)
                .WithMany(p => p.Perms)
                .HasForeignKey(p => p.PermissionsId)
                .IsRequired();
            builder.HasData(new Permission[]
            {
                new Permission()
                {
                    GrantorId = -2,
                    GranteeId = -1,
                    PermissionsId = -1,
                    PermissionType = PermissionType.Viewer
                },
                new Permission()
                {
                    GrantorId = -1,
                    GranteeId = -4,
                    PermissionsId = -2,
                    PermissionType = PermissionType.Viewer
                },
                new Permission()
                {
                    GrantorId = -3,
                    GranteeId = -1,
                    PermissionsId = -3,
                    PermissionType = PermissionType.Viewer
                },
                new Permission()
                {
                    GrantorId = -3,
                    GranteeId = -2,
                    PermissionsId = -4,
                    PermissionType = PermissionType.Viewer
                }
            });
        }
    }
}
