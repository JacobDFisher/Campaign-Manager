using DataAccess.Models;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    static class Mapper
    {
        // Map Up
        public static IEnumerable<Lib.Models.Entity> Map(IEnumerable<Models.Entity> entities)
        {
            if (entities == null)
                return null;
            return from entity in entities select Map(entity);
        }
        
        public static IEnumerable<Lib.Models.Revealed> Map<T>(IEnumerable<Models.Revealed<T>> revealed)
        {
            if (revealed == null)
                return null;
            return from rev in revealed select Map(rev);
        }
        
        public static IEnumerable<Lib.Models.Permission> Map<T>(IEnumerable<Models.Permission<T>> perms)
        {
            if (perms == null)
                return null;
            return from perm in perms select Map(perm);
        }

        public static Lib.Models.Detail Map(Models.Detail detail)
        {
            if (detail == null)
                return null;
            return new Lib.Models.Detail()
            {
                Id = detail.Id,
                Description = detail.Description,
                Permissions = Map(detail.Permissions)
            };
        }

        public static Lib.Models.Entity Map(Models.Entity entity)
        {
            if (entity == null)
                return null;
            var ent = new Lib.Models.Entity()
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = Map(entity.Permissions)
            };

            if(entity.Details != null)
            {
                ent.Details = new List<Lib.Models.Detail>();
                ent.Properties = new List<Lib.Models.Property>();
                foreach(var detail in entity.Details)
                {
                    if(detail.Name != null)
                    {
                        ent.Properties.Append(new Lib.Models.Property()
                        {
                            Name = detail.Name,
                            Detail = Map(detail)
                        });
                    } else
                    {
                        ent.Details.Append(Map(detail));
                    }
                }
            }

            return ent;
        }

        public static Lib.Models.Group Map(Models.Group group)
        {
            if (group == null)
                return null;
            return new Lib.Models.Group()
            {
                Id = group.Id,
                Name = group.Name,
                MemberOf = (group.MemberOf == null) ? null : group.MemberOf.Select(g => (g.Group == null) ? new Lib.Models.Group() { Id = g.GroupId } : Map(g.Group))
            };
        }

        public static Lib.Models.Identity Map(Models.Identity identity)
        {
            if (identity == null)
                return null;
            return new Lib.Models.Identity()
            {
                Id = identity.Id,
                Name = identity.Name,
                Groups = (identity.IdentityGroups == null) ? null : from g in identity.IdentityGroups select (g.Group == null) ? new Lib.Models.Group() { Id = g.GroupId } : Map(g.Group),
            };
        }

        public static Lib.Models.Permission Map<T>(Models.Permission<T> perm)
        {
            if (perm == null)
                return null;
            return new Lib.Models.Permission()
            {
                Grantee = (perm.Grantee == null) ? new Lib.Models.Group() { Id = perm.GranteeId } : Map(perm.Grantee),
                Grantor = (perm.Grantor == null) ? new Lib.Models.Identity() { Id = perm.GrantorId } : Map(perm.Grantor),
                PermissionType = perm.PermissionType,
            };
        }

        public static Lib.Models.Permissions Map<T>(Models.Permissions<T> permissions)
        {
            if (permissions == null)
                return null;
            return new Lib.Models.Permissions()
            {
                Author = (permissions.Author == null)? new Lib.Models.Identity() { Id = permissions.AuthorId } : Map(permissions.Author),
                Perms = Map(permissions.Perms),
                Revealed = Map(permissions.Revealed)
            };
        }
        
        public static Lib.Models.Revealed Map<T>(Models.Revealed<T> revealed)
        {
            if (revealed == null)
                return null;
            return new Lib.Models.Revealed()
            {
                Percentage = revealed.Percentage,
                Group = (revealed.Group == null)? new Lib.Models.Group() { Id = revealed.GroupId } : Map(revealed.Group),
                Source = (revealed.Source == null)? new Lib.Models.Entity() { Id = revealed.SourceId } : Map(revealed.Source)
            };
        }

        // Map Down
        public static IEnumerable<Models.Entity> Map(IEnumerable<Lib.Models.Entity> entities)
        {
            if (entities == null)
                return null;
            return from entity in entities select Map(entity);
        }
        private static IEnumerable<Revealed<T>> Map<T>(IEnumerable<Revealed> revealed)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Permission<T>> Map<T>(IEnumerable<Permission> perms)
        {
            throw new NotImplementedException();
        }
        private static IEnumerable<GroupJoin<Models.Entity>> Map(IEnumerable<Lib.Models.Group> groups)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Models.Detail> Map(IEnumerable<Property> properties)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Models.Detail> Map(IEnumerable<Lib.Models.Detail> details)
        {
            throw new NotImplementedException();
        }

        public static Models.Entity Map(Lib.Models.Entity entity)
        {
            if (entity == null)
                return null;
            return new Models.Entity()
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = Map<Models.Entity>(entity.Permissions),
                Details = Map(entity.Details).Concat(Map(entity.Properties)),
                EntityGroups = Map(entity.Groups)
            };
        }

        public static Models.Permissions<T> Map<T>(Lib.Models.Permissions permissions)
        {
            if (permissions == null)
                return null;
            return new Models.Permissions<T>()
            {
                Author = Map(permissions.Author),
                AuthorId = (int)permissions.Author?.Id,
                Perms = Map<T>(permissions.Perms),
                Revealed = Map<T>(permissions.Revealed)
            };
        }

        private static Models.Identity Map(Lib.Models.Identity identity)
        {
            throw new NotImplementedException();
        }
    }
}
