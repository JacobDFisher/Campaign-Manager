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
                //Revealed = Map(entity.Revealed),
            };
        }

        public static IEnumerable<Models.Revealed> Map<T>(IEnumerable<Lib.Models.Revealed<T>> revealed){
            return from r in revealed select Map(r);
        }

        public static Revealed Map<T>(Lib.Models.Revealed<T> revealed)
        {
            return new Revealed()
            {
                //DestinationId = revealed.DestinationId,
                Percentage = revealed.Percentage,
                Source = Map(revealed.Source)
            };
        }

        internal static IEnumerable<Models.Entity> Map(IEnumerable<Lib.Models.Entity> entities)
        {
            return from e in entities select Map(e);
        }
        public static Entity Map(Lib.Models.Entity entity)
        {
            return new Entity()
            {
                Id = entity.Id,
                Name = entity.Name,
                //Revealed = Map(entity.Revealed),
                Details = Map(entity.Details),
                Properties = Map(entity.Properties)
            };
        }

        private static IEnumerable<Property> Map(IEnumerable<Lib.Models.Property> properties)
        {
            return from p in properties select Map(p);
        }

        private static Property Map(Lib.Models.Property p)
        {
            return new Property()
            {
                Name = p.Name,
                Detail = Map(p.Detail)
            };
        }

        private static IEnumerable<Models.Detail> Map(IEnumerable<Lib.Models.Detail> details)
        {
            return from d in details select Map(d);
        }

        private static Detail Map(Lib.Models.Detail d)
        {
            return new Detail()
            {
                //Author = d.Author,
                Description = d.Description,
               // Revealed = Map(d.Revealed)
            };
        }
    }
}
