using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DataAccess.Models
{
    // TODO: Add Permissions to group
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Revealed> Revealeds { get; set; }
        public IEnumerable<Permission> Grants { get; set; }
        public IEnumerable<GroupJoin<Group>> MemberOf { get; set; }
        public IEnumerable<GroupJoin<Group>> MemberGroups { get; set; }
        public IEnumerable<GroupJoin<Identity>> MemberIdentities { get; set; }
        public IEnumerable<GroupJoin<Entity>> MemberEntities { get; set; }
    }
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public static IEnumerable<Group> groups = new List<Group>() {
            new Group()
                {
                    Id = -1,
                    Name = "All"
                },
                new Group()
                {
                    Id = -2,
                    Name = "Runner"
                },
                new Group()
                {
                    Id = -3,
                    Name = "Players"
                },
                new Group()
                {
                    Id = -4,
                    Name="Player 1"
                },
                new Group()
                {
                    Id = -5,
                    Name="Player 2"
                }
        };
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name);
            builder.HasMany(g => g.Revealeds)
                .WithOne(r => r.Group)
                .HasForeignKey(r => r.GroupId);
            builder.HasMany(g => g.Grants)
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
            builder.HasData(groups);
        }
    }
}
