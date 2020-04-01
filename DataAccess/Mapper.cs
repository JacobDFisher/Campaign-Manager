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
        internal static IEnumerable<Lib.Models.Entity> Map(IEnumerable<Models.Entity> entities)
        {
            if (entities == null)
                return null;
            return from entity in entities select Map(entity);
        }

        private static Lib.Models.Entity Map(Models.Entity entity)
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

        private static Lib.Models.Detail Map(Models.Detail detail)
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

        private static Lib.Models.Permissions<Lib.Models.Detail> Map(Models.Permissions<Models.Detail> permissions)
        {
            if (permissions == null)
                return null;
            return new Lib.Models.Permissions<Lib.Models.Detail>()
            {
                Author = (permissions.Author == null)? new Lib.Models.Identity() { Id = permissions.AuthorId } : Map(permissions.Author),
                Perms = Map(permissions.Perms),
                Revealed = Map(permissions.Revealed)
            };
        }

        private static IEnumerable<Lib.Models.Revealed> Map(IEnumerable<Models.Revealed<Models.Detail>> revealed)
        {
            if (revealed == null)
                return null;
            return from rev in revealed select Map(rev);
        }

        private static Lib.Models.Revealed Map(Models.Revealed<Models.Detail> revealed)
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

        private static Lib.Models.Group Map(Models.Group group)
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

        private static IEnumerable<Lib.Models.Permission> Map(IEnumerable<Models.Permission<Models.Detail>> perms)
        {
            if (perms == null)
                return null;
            return from perm in perms select Map(perm);
        }

        private static Lib.Models.Permission Map(Permission<Models.Detail> perm)
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

        private static Lib.Models.Identity Map(Models.Identity identity)
        {
            if (identity == null)
                return null;
            return new Lib.Models.Identity()
            {
                Id = identity.Id,
                Name = identity.Name,
                Groups = (identity.IdentityGroups == null)? null : from g in identity.IdentityGroups select (g.Group == null)? new Lib.Models.Group() { Id = g.GroupId } : Map(g.Group),
            };
        }

        private static Lib.Models.Permissions<Lib.Models.Entity> Map(Models.Permissions<Models.Entity> permissions)
        {
            if (permissions == null)
                return null;
            if (permissions == null)
                return null;
            return new Lib.Models.Permissions<Lib.Models.Entity>()
            {
                Author = (permissions.Author == null) ? new Lib.Models.Identity() { Id = permissions.AuthorId } : Map(permissions.Author),
                Perms = Map(permissions.Perms),
                Revealed = Map(permissions.Revealed)
            };
        }

        private static IEnumerable<Revealed> Map(IEnumerable<Revealed<Models.Entity>> revealed)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Permission> Map(IEnumerable<Permission<Models.Entity>> perms)
        {
            throw new NotImplementedException();
        }
    }
}
