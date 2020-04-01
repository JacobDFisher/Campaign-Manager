using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Permissions<T>
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Identity Author { get; set; }
        public int EndPointId { get; set; }
        public T EndPoint { get; set; }
        public IEnumerable<Permission<T>> Perms { get; set; }
        public IEnumerable<Revealed<T>> Revealed { get; set; }
    }
    public class EntityPermissionsConfiguration : IEntityTypeConfiguration<Permissions<Entity>>
    {
        public void Configure(EntityTypeBuilder<Permissions<Entity>> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Author)
                .WithMany(a => a.EntityAuthorships)
                .HasForeignKey(p => p.AuthorId)
                .IsRequired();
            builder.HasOne(p => p.EndPoint)
                .WithOne(e => e.Permissions)
                .HasForeignKey<Permissions<Entity>>(p => p.EndPointId)
                // .HasForeignKey<Entity>(e => e.PermissionsId)
                .IsRequired();
        }
    }
    public class DetailPermissionsConfiguration : IEntityTypeConfiguration<Permissions<Detail>>
    {
        public void Configure(EntityTypeBuilder<Permissions<Detail>> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Author)
                .WithMany(a => a.DetailAuthorships)
                .HasForeignKey(p => p.AuthorId)
                .IsRequired();
            builder.HasOne(p => p.EndPoint)
                .WithOne(e => e.Permissions)
                .HasForeignKey<Permissions<Detail>>(p => p.EndPointId)
                // .HasForeignKey<Detail>(e => e.PermissionsId)
                .IsRequired();
        }
    }
}
