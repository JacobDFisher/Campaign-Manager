using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private IEnumerable<GroupJoin<Identity>> identityData = new List<GroupJoin<Identity>>()
        {
            new GroupJoin<Identity>()
                {
                    GroupId = -2,
                    MemberId = -1
                },
                new GroupJoin<Identity>()
                {
                    GroupId = -4,
                    MemberId = -2
                },
                new GroupJoin<Identity>()
                {
                    GroupId = -5,
                    MemberId = -3
                }
        };
        public GroupIdentityConfiguration()
        {
            // Add all to "All"
            identityData = identityData.Concat(from i in IdentityConfiguration.identities.Select(i => i.Id) select new GroupJoin<Identity>() { GroupId = -1, MemberId = i });
        }
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
            builder.HasData(identityData);
        }
    }
    public class GroupGroupConfiguration : IEntityTypeConfiguration<GroupJoin<Group>>
    {
        private IEnumerable<GroupJoin<Group>> groupData = new List<GroupJoin<Group>>()
        {
            new GroupJoin<Group>()
                {
                    GroupId = -3,
                    MemberId = -4
                },
                new GroupJoin<Group>()
                {
                    GroupId = -3,
                    MemberId = -5
                }
        };
        public GroupGroupConfiguration()
        {
            // Add all to "All"
            groupData = groupData.Concat(from i in GroupConfiguration.groups.Select(g => g.Id).Where(n => n!=-1) select new GroupJoin<Group>() { GroupId = -1, MemberId = i });
        }
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
            builder.HasData(groupData);
        }
    }
    public class GroupEntityConfiguration : IEntityTypeConfiguration<GroupJoin<Entity>>
    {
        private IEnumerable<GroupJoin<Entity>> entityData = new List<GroupJoin<Entity>>();
        public GroupEntityConfiguration()
        {
            // Add all to "All"
            entityData = entityData.Concat(from i in EntityConfiguration.entities.Select(e => e.Id) select new GroupJoin<Entity>() { GroupId = -1, MemberId = i });
        }
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
            builder.HasData(entityData);
        }
    }
}
