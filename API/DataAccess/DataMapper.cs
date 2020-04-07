using DataAccess.Models;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataMapper
    {
        public Dictionary<int, Lib.Models.Permissions> mappedPermissions;

        public DataMapper()
        {
            mappedPermissions = new Dictionary<int, Lib.Models.Permissions>();
        }

        // Map Up
        public IEnumerable<Lib.Models.Entity> Map(IEnumerable<Models.Entity> entities)
        {
            if (entities == null)
                return null;
            return from entity in entities select Map(entity);
        }

        public IEnumerable<Lib.Models.Revealed> Map(IEnumerable<Models.Revealed> revealed)
        {
            if (revealed == null)
                return null;
            return from rev in revealed select Map(rev);
        }

        internal IEnumerable<Lib.Models.Identity> Map(IEnumerable<Models.Identity> identities)
        {
            if (identities == null)
                return null;
            return from identity in identities select Map(identity);
        }

        public IEnumerable<Lib.Models.Permission> Map(IEnumerable<Models.Permission> perms)
        {
            if (perms == null)
                return null;
            return from perm in perms select Map(perm);
        }

        internal IEnumerable<Lib.Models.Group> Map(IEnumerable<Models.Group> groups)
        {
            if (groups == null)
                return null;
            return from g in groups select Map(g);
        }

        public Lib.Models.Detail Map(Models.Detail detail)
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

        public Lib.Models.Entity Map(Models.Entity entity)
        {
            if (entity == null)
                return null;
            var ent = new Lib.Models.Entity()
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = Map(entity.Permissions)
            };

            if (entity.Details != null)
            {
                ent.Details = new List<Lib.Models.Detail>();
                ent.Properties = new List<Lib.Models.Property>();
                foreach (var detail in entity.Details)
                {
                    if (detail.Name != null)
                    {
                        var test = new Lib.Models.Property()
                        {
                            Name = detail.Name,
                            Detail = Map(detail)
                        };
                        ent.Properties = ent.Properties.Append(test);
                    }
                    else
                    {
                        ent.Details = ent.Details.Append(Map(detail));
                    }
                }
            }

            return ent;
        }

        public Lib.Models.Group Map(Models.Group group)
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

        public Lib.Models.Identity Map(Models.Identity identity)
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

        public Lib.Models.Permission Map(Models.Permission perm)
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

        public Lib.Models.Permissions Map(Models.Permissions permissions)
        {
            if (permissions == null)
                return null;
            try
            {
                return mappedPermissions[permissions.Id];
            }
            catch
            {
                var newPerm = new Lib.Models.Permissions()
                {
                    Id = permissions.Id,
                    Author = (permissions.Author == null) ? new Lib.Models.Identity() { Id = permissions.AuthorId } : Map(permissions.Author),
                    Perms = Map(permissions.Perms),
                    Revealed = Map(permissions.Revealeds)
                };
                mappedPermissions[permissions.Id] = newPerm;
                return newPerm;
            }
        }

        public Lib.Models.Revealed Map(Models.Revealed revealed)
        {
            if (revealed == null)
                return null;
            return new Lib.Models.Revealed()
            {
                Percentage = revealed.Percentage,
                Group = (revealed.Group == null) ? new Lib.Models.Group() { Id = revealed.GroupId } : Map(revealed.Group),
                Source = (revealed.Source == null) ? new Lib.Models.Entity() { Id = revealed.SourceId } : Map(revealed.Source)
            };
        }

        // Map Down
        public IEnumerable<Models.Entity> Map(IEnumerable<Lib.Models.Entity> entities)
        {
            if (entities == null)
                return null;
            return from entity in entities select Map(entity);
        }

        public IEnumerable<Models.Revealed> Map(IEnumerable<Lib.Models.Revealed> revealed)
        {
            if (revealed == null)
                return null;
            return from rev in revealed select Map(rev);
        }

        public IEnumerable<Models.Permission> Map(IEnumerable<Lib.Models.Permission> perms)
        {
            if (perms == null)
                return null;
            return from perm in perms select Map(perm);
        }

        public IEnumerable<Models.Detail> Map(IEnumerable<Lib.Models.Property> properties)
        {
            if (properties == null)
                return null;
            return from prop in properties select Map(prop);
        }

        public IEnumerable<Models.Detail> Map(IEnumerable<Lib.Models.Detail> details)
        {
            if (details == null)
                return null;
            return from det in details select Map(det);
        }

        public IEnumerable<GroupJoin<T>> Map<T>(IEnumerable<Lib.Models.Group> groups)
        {
            if (groups == null)
                return null;
            return from g in groups select Map<T>(g);
        }

        public Models.Entity Map(Lib.Models.Entity entity)
        {
            if (entity == null)
                return null;
            return new Models.Entity()
            {
                Id = entity.Id,
                Name = entity.Name,
                PermissionsId = (int) entity.Permissions?.Id,
                //Permissions = Map(entity.Permissions),
                Details = Map(entity.Details).Concat(Map(entity.Properties)).ToList(),
                EntityGroups = Map<Models.Entity>(entity.Groups)
            };
        }

        public Models.Permissions Map(Lib.Models.Permissions permissions)
        {
            if (permissions == null)
                return null;
            return new Models.Permissions()
            {
                Id = permissions.Id,
                Author = Map(permissions.Author),
                AuthorId = (int)permissions.Author?.Id,
                Perms = Map(permissions.Perms),
                Revealeds = Map(permissions.Revealed)
            };
        }

        public Models.Identity Map(Lib.Models.Identity identity)
        {
            if (identity == null)
                return null;
            return new Models.Identity()
            {
                Id = identity.Id
            };
        }
        public Models.Detail Map(Property property)
        {
            var newDetail = Map(property.Detail);
            newDetail.Name = property.Name;
            return newDetail;
        }

        public Models.Detail Map(Lib.Models.Detail detail)
        {
            return new Models.Detail()
            {
                Id = detail.Id,
                Name = null,
                Description = detail.Description,
                //Entity = Map(detail.Entity),
                //EntityId = (int)detail.Entity?.Id,
                //Permissions = Map(detail.Permissions),
                PermissionsId = (int)detail.Permissions?.Id
            };
        }
        public Models.Revealed Map(Lib.Models.Revealed rev)
        {
            if (rev == null)
                return null;
            return new Models.Revealed()
            {
                Percentage = rev.Percentage,
                Source = Map(rev.Source),
                SourceId = (int)rev.Source?.Id,
                Group = Map(rev.Group),
                GroupId = (int)rev.Group?.Id
            };
        }
        public Models.Permission Map(Lib.Models.Permission perm)
        {
            if (perm == null)
                return null;
            return new Models.Permission()
            {
                Grantee = Map(perm.Grantee),
                GranteeId = (int)perm.Grantee?.Id,
                Grantor = Map(perm.Grantor),
                GrantorId = (int)perm.Grantor?.Id,
                PermissionType = perm.PermissionType
            };
        }

        public GroupJoin<T> Map<T>(Lib.Models.Group g)
        {
            if (g == null)
                return null;
            return new GroupJoin<T>()
            {
                Group = Map(g),
                GroupId = (int)g?.Id
            };
        }

        public Models.Group Map(Lib.Models.Group g)
        {
            if (g == null)
                return null;
            return new Models.Group()
            {
                Id = g.Id,
                Name = g.Name,
                MemberOf = Map<Models.Group>(g.MemberOf)
            };
        }
    }
}
