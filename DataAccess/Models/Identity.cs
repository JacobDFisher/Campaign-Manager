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
        public IEnumerable<Permission<Entity>> EntityGrants { get; set; }
        public IEnumerable<Permission<Detail>> DetailGrants { get; set; }
        public IEnumerable<Permissions<Entity>> EntityAuthorships { get; set; }
        public IEnumerable<Permissions<Detail>> DetailAuthorships { get; set; }
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
            builder.HasMany(i => i.EntityGrants)
                .WithOne(p => p.Grantor)
                .HasForeignKey(p => p.GrantorId);
            builder.HasMany(i => i.DetailGrants)
                .WithOne(p => p.Grantor)
                .HasForeignKey(p => p.GrantorId);
            builder.HasMany(i => i.EntityAuthorships)
                .WithOne(ps => ps.Author)
                .HasForeignKey(ps => ps.AuthorId);
            builder.HasMany(i => i.DetailAuthorships)
                .WithOne(ps => ps.Author)
                .HasForeignKey(ps => ps.AuthorId);
        }
    }
}
