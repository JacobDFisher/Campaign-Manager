using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    // TODO: Add Permissions to group
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Revealed<Entity>> RevealedEntities { get; set; }
        public IEnumerable<Revealed<Detail>> RevealedDetails { get; set; }
        public IEnumerable<Permission<Entity>> EntityGrants { get; set; }
        public IEnumerable<Permission<Detail>> DetailGrants { get; set; }
        public IEnumerable<GroupJoin<Group>> MemberOf { get; set; }
        public IEnumerable<GroupJoin<Group>> MemberGroups { get; set; }
        public IEnumerable<GroupJoin<Identity>> MemberIdentities { get; set; }
        public IEnumerable<GroupJoin<Entity>> MemberEntities { get; set; }
    }
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name);
            builder.HasMany(g => g.RevealedDetails)
                .WithOne(r => r.Group)
                .HasForeignKey(r => r.GroupId);
            builder.HasMany(g => g.RevealedEntities)
                .WithOne(r => r.Group)
                .HasForeignKey(r => r.GroupId);
            builder.HasMany(g => g.EntityGrants)
                .WithOne(p => p.Grantee)
                .HasForeignKey(p => p.GranteeId);
            builder.HasMany(g => g.DetailGrants)
                .WithOne(p => p.Grantee)
                .HasForeignKey(p => p.GranteeId);
            builder.HasMany(g => g.MemberOf)
                .WithOne(gj => gj.Member)
                .HasForeignKey(gj => gj.MemberId);
            builder.HasMany(g => g.MemberGroups)
                .WithOne(gj => gj.Group)
                .HasForeignKey(gj => gj.GroupId);
            builder.HasMany(g => g.MemberIdentities)
                .WithOne(gj => gj.Group)
                .HasForeignKey(gj => gj.GroupId);
            builder.HasMany(g => g.MemberEntities)
                .WithOne(gj => gj.Group)
                .HasForeignKey(gj => gj.GroupId);
        }
    }
}
