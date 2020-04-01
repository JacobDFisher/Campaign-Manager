using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class GroupJoin<T>
    {
        public int MemberId { get; set; }
        public T Member { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }

    public class GroupIdentityConfiguration : IEntityTypeConfiguration<GroupJoin<Identity>>
    {
        public void Configure(EntityTypeBuilder<GroupJoin<Identity>> builder)
        {
            builder.HasKey(g => new { g.MemberId, g.GroupId });
            builder.HasOne(g => g.Member)
                .WithMany(m => m.IdentityGroups)
                .HasForeignKey(g => g.MemberId)
                .IsRequired();
            builder.HasOne(g => g.Group)
                .WithMany(g => g.MemberIdentities)
                .HasForeignKey(g => g.GroupId)
                .IsRequired();
        }
    }
    public class GroupGroupConfiguration : IEntityTypeConfiguration<GroupJoin<Group>>
    {
        public void Configure(EntityTypeBuilder<GroupJoin<Group>> builder)
        {
            builder.HasKey(g => new { g.MemberId, g.GroupId });
            builder.HasOne(g => g.Member)
                .WithMany(m => m.MemberOf)
                .HasForeignKey(g => g.MemberId)
                .IsRequired();
            builder.HasOne(g => g.Group)
                .WithMany(g => g.MemberGroups)
                .HasForeignKey(g => g.GroupId)
                .IsRequired();
        }
    }
    public class GroupEntityConfiguration : IEntityTypeConfiguration<GroupJoin<Entity>>
    {
        public void Configure(EntityTypeBuilder<GroupJoin<Entity>> builder)
        {
            builder.HasKey(g => new { g.MemberId, g.GroupId });
            builder.HasOne(g => g.Member)
                .WithMany(m => m.EntityGroups)
                .HasForeignKey(g => g.MemberId)
                .IsRequired();
            builder.HasOne(g => g.Group)
                .WithMany(g => g.MemberEntities)
                .HasForeignKey(g => g.GroupId)
                .IsRequired();
        }
    }
}
