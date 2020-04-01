using Lib.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Identity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GroupJoin<Identity>> IdentityGroups { get; set; }
        public IEnumerable<Permission> Grants { get; set; }
        public IEnumerable<Permissions> Authorships { get; set; }
    }
    public class IdentityConfiguration : IEntityTypeConfiguration<Identity>
    {
        public void Configure(EntityTypeBuilder<Identity> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name);
            builder.HasMany(i => i.IdentityGroups)
                .WithOne(ig => ig.Member)
                .HasForeignKey(ig => ig.MemberId);
            builder.HasMany(i => i.Grants)
                .WithOne(p => p.Grantor)
                .HasForeignKey(p => p.GrantorId);
            builder.HasMany(i => i.Authorships)
                .WithOne(ps => ps.Author)
                .HasForeignKey(ps => ps.AuthorId);
            builder.HasData(new Identity[]
            {
                new Identity()
                {
                    Id = 3,
                    Name = "Player 1"
                }
            });
        }
    }
}
