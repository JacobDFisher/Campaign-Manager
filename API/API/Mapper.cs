using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public static class Mapper
    {
        public static IEnumerable<EntityHeader> MapHeader(IEnumerable<Lib.Models.Entity> entities)
        {
            return from e in entities select MapHeader(e);
        }
        public static EntityHeader MapHeader(Lib.Models.Entity entity)
        {
            return new EntityHeader()
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = Map(entity.Permissions)
            };
        }

        public static IEnumerable<Models.Revealed> Map(IEnumerable<Lib.Models.Revealed> revealed){
            if (revealed == null)
                return null;
            return from r in revealed select Map(r);
        }

        internal static IEnumerable<Models.Identity> Map(IEnumerable<Lib.Models.Identity> identities)
        {
            if (identities == null)
                return null;
            return from i in identities select Map(i);
        }

        public static IEnumerable<Models.Group> Map(IEnumerable<Lib.Models.Group> groups)
        {
            if (groups == null)
                return null;
            return from g in groups select Map(g);
        }

        public static Models.Revealed Map(Lib.Models.Revealed revealed)
        {
            return new Models.Revealed()
            {
                //DestinationId = revealed.DestinationId,
                Percentage = revealed.Percentage,
                Source = Map(revealed.Source)
            };
        }

        public static IEnumerable<Models.Entity> Map(IEnumerable<Lib.Models.Entity> entities)
        {
            return from e in entities select Map(e);
        }
        public static Models.Entity Map(Lib.Models.Entity entity)
        {
            return new Models.Entity()
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = Map(entity.Permissions),
                Details = Map(entity.Details),
                Properties = Map(entity.Properties)
            };
        }

        public static IEnumerable<Models.Property> Map(IEnumerable<Lib.Models.Property> properties)
        {
            if (properties == null)
                return null;
            return from p in properties select Map(p);
        }

        public static Models.Property Map(Lib.Models.Property p)
        {
            return new Models.Property()
            {
                Name = p.Name,
                Detail = Map(p.Detail)
            };
        }

        public static IEnumerable<Models.Detail> Map(IEnumerable<Lib.Models.Detail> details)
        {
            if (details == null)
                return null;
            return from d in details select Map(d);
        }

        public static Models.Detail Map(Lib.Models.Detail d)
        {
            return new Models.Detail()
            {
                Description = d.Description,
                Permissions = Map(d.Permissions)
            };
        }

        public static Models.Permissions Map(Lib.Models.Permissions permissions)
        {
            if (permissions == null)
                return null;
            return new Models.Permissions()
            {
                Id = permissions.Id,
                Author = Map(permissions.Author),
                Perms = Map(permissions.Perms),
                Revealed = Map(permissions.Revealed)
            };
        }

        public static IEnumerable<Models.Permission> Map(IEnumerable<Lib.Models.Permission> perms)
        {
            if (perms == null)
                return null;
            return from perm in perms select Map(perm);
        }

        public static Models.Permission Map(Lib.Models.Permission perm)
        {
            if (perm == null)
                return null;
            return new Models.Permission()
            {
                Grantor = Map(perm.Grantor),
                Grantee = Map(perm.Grantee),
                PermissionType = perm.PermissionType
            };
        }

        public static Models.Group Map(Lib.Models.Group group)
        {
            if (group == null)
                return null;
            return new Models.Group()
            {
                Id = group.Id,
                Name = group.Name,
                MemberOf = group.MemberOf?.Select(g => g.Id)
            };
        }

        public static Models.Identity Map(Lib.Models.Identity identity)
        {
            if (identity == null)
                return null;
            return new Models.Identity()
            {
                Id = identity.Id,
                Groups = identity.Groups?.Select(g => g.Id),
                Name = identity.Name
            };
        }
    }
}
