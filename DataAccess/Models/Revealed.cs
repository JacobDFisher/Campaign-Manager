using Lib.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Revealed<T>
    {
        public double Percentage { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int SourceId { get; set; }
        public Entity Source { get; set; }
        public int PermissionsId { get; set; }
        public Permissions<T> Permissions { get; set; }
    }

    public class EntityRevealedConfiguration : IEntityTypeConfiguration<Revealed<Entity>>
    {
        public void Configure(EntityTypeBuilder<Revealed<Entity>> builder)
        {
            builder.HasKey(r => new { r.GroupId, r.SourceId, r.PermissionsId });
            builder.Property(r => r.Percentage);
            builder.HasOne(r => r.Group)
                .WithMany(g => g.RevealedEntities)
                .HasForeignKey(p => p.GroupId)
                .IsRequired();
            builder.HasOne(r => r.Source)
                .WithMany(e => e.RevealedEntities)
                .HasForeignKey(r => r.SourceId);
            builder.HasOne(r => r.Permissions)
                .WithMany(p => p.Revealed)
                .HasForeignKey(r => r.PermissionsId)
                .IsRequired();
        }
    }
    public class DetailRevealedConfiguration : IEntityTypeConfiguration<Revealed<Detail>>
    {
        public void Configure(EntityTypeBuilder<Revealed<Detail>> builder)
        {
            builder.HasKey(r => new { r.GroupId, r.SourceId, r.PermissionsId });
            builder.Property(r => r.Percentage);
            builder.HasOne(r => r.Group)
                .WithMany(g => g.RevealedDetails)
                .HasForeignKey(p => p.GroupId)
                .IsRequired();
            builder.HasOne(r => r.Source)
                .WithMany(e => e.RevealedDetails)
                .HasForeignKey(r => r.SourceId);
            builder.HasOne(r => r.Permissions)
                .WithMany(p => p.Revealed)
                .HasForeignKey(r => r.PermissionsId)
                .IsRequired();
        }
    }
}
