using Lib.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Revealed
    {
        public double Percentage { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int SourceId { get; set; }
        public Entity Source { get; set; }
        public int PermissionsId { get; set; }
        public Permissions Permissions { get; set; }
    }

    public class RevealedConfiguration : IEntityTypeConfiguration<Revealed>
    {
        public void Configure(EntityTypeBuilder<Revealed> builder)
        {
            builder.HasKey(r => new { r.GroupId, r.SourceId, r.PermissionsId });
            builder.Property(r => r.Percentage);
            builder.HasOne(r => r.Group)
                .WithMany(g => g.Revealeds)
                .HasForeignKey(p => p.GroupId)
                .IsRequired();
            builder.HasOne(r => r.Source)
                .WithMany(e => e.Revealeds)
                .HasForeignKey(r => r.SourceId);
            builder.HasOne(r => r.Permissions)
                .WithMany(p => p.Revealeds)
                .HasForeignKey(r => r.PermissionsId)
                .IsRequired();
        }
    }
}
