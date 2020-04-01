using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PermissionsId { get; set; }
        public Permissions<Detail> Permissions { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
    }

    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name);
            builder.Property(d => d.Description)
                .IsRequired();
            builder.HasOne(d => d.Permissions)
                .WithOne(p => p.EndPoint)
                .HasForeignKey<Detail>(d => d.PermissionsId)
                // .HasForeignKey<Permissions<Detail>>(p => p.EndPointId)
                .IsRequired();
            builder.HasOne(d => d.Entity)
                .WithMany(e => e.Details)
                .HasForeignKey(d => d.EntityId)
                .IsRequired();
        }
    }
}
