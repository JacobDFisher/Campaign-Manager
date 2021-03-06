﻿using Microsoft.EntityFrameworkCore;
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
        public Permissions Permissions { get; set; }
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
                .WithMany(p => p.Details)
                .HasForeignKey(d => d.PermissionsId)
                .IsRequired();
            builder.HasOne(d => d.Entity)
                .WithMany(e => e.Details)
                .HasForeignKey(d => d.EntityId)
                .IsRequired();
            builder.HasData(new Detail[]
            {
                new Detail()
                {
                    Id = -1,
                    Name = "name",
                    Description = "Character 1",
                    EntityId = -1,
                    PermissionsId = -1
                },
                new Detail()
                {
                    Id = -2,
                    Description = "Has a large family",
                    EntityId = -1,
                    PermissionsId = -1
                },
                new Detail()
                {
                    Id = -3,
                    Description = "Part of a global conspiracy",
                    EntityId = -1,
                    PermissionsId = -2
                },
                new Detail()
                {
                    Id = -4,
                    Name = "name",
                    Description = "Character 2",
                    EntityId = -2,
                    PermissionsId = -3
                },
                new Detail()
                {
                    Id = -5,
                    Description = "Tall",
                    EntityId = -2,
                    PermissionsId = -4
                }
            });
        }
    }
}
