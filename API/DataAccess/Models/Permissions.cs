using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Permissions
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Identity Author { get; set; }
        public IEnumerable<Detail> Details { get; set; }
        public IEnumerable<Entity> Entities { get; set; }
        public IEnumerable<Permission> Perms { get; set; }
        public IEnumerable<Revealed> Revealeds { get; set; }
    }
    public class PermissionsConfiguration : IEntityTypeConfiguration<Permissions>
    {
        public void Configure(EntityTypeBuilder<Permissions> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Author)
                .WithMany(a => a.Authorships)
                .HasForeignKey(p => p.AuthorId)
                .IsRequired();
            builder.HasMany(p => p.Details)
                .WithOne(e => e.Permissions)
                .HasForeignKey(e => e.PermissionsId)
                .IsRequired();
            builder.HasMany(p => p.Entities)
                .WithOne(e => e.Permissions)
                .HasForeignKey(e => e.PermissionsId)
                .IsRequired();
            builder.HasMany(p => p.Perms)
                .WithOne(p => p.Permissions)
                .HasForeignKey(p => p.PermissionsId)
                .IsRequired();
            builder.HasMany(p => p.Revealeds)
                .WithOne(r => r.Permissions)
                .HasForeignKey(r => r.PermissionsId)
                .IsRequired();
            builder.HasData(new Permissions[]
            {
                new Permissions()
                {
                    Id = 1,
                    AuthorId = 3
                }
            });
        }
    }
}
