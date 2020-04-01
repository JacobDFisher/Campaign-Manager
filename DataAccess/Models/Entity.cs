using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PermissionsId { get; set; }
        public Permissions Permissions { get; set; }
        public IEnumerable<Revealed> Revealeds { get; set; }
        public IEnumerable<Detail> Details { get; set; }
        public IEnumerable<GroupJoin<Entity>> EntityGroups { get; set; }
    }
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name);
            builder.HasOne(e => e.Permissions)
                .WithMany(p => p.Entities)
                .HasForeignKey(e => e.PermissionsId)
                .IsRequired();
            builder.HasMany(e => e.Details) 
                .WithOne(d => d.Entity)
                .HasForeignKey(d => d.EntityId);
            builder.HasMany(e => e.EntityGroups)
                .WithOne(eg => eg.Member)
                .HasForeignKey(eg => eg.MemberId);
            builder.HasMany(e => e.Revealeds)
                .WithOne(r => r.Source)
                .HasForeignKey(r => r.SourceId);
            builder.HasData(new Entity[]{
                new Entity() {
                    Id = 1,
                    Name = "Character 1",
                    PermissionsId = 1
                }
            });
        }
    }
}
