using API.Models;
using Lib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityController
    {
        IEntityRepository _EntityRepository;
        public EntityController(IEntityRepository entityRepository)
        {
            _EntityRepository = entityRepository;
        }

        [HttpGet("{id}")]
        public async Task<Entity> GetEntity([FromRoute] int id)
        {
            Lib.Models.Entity entity;
            entity = await _EntityRepository.GetEntity(id, true);
            return Mapper.Map(entity);
        }

        [HttpGet]
        public async Task<IEnumerable<Entity>> GetEntities([FromRoute] IEnumerable<int> ids)
        {
            IEnumerable<Lib.Models.Entity> entities;
            if (ids != null && ids.Count() > 0)
            {
                entities = await _EntityRepository.GetEntities(ids);
            }
            else
            {
                entities = await _EntityRepository.GetEntities();
            }
            return Mapper.Map(entities);
        }

        [HttpGet("header")]
        public async Task<IEnumerable<EntityHeader>> GetEntityHeaders([FromQuery] IEnumerable<int> ids)
        {
            IEnumerable<Lib.Models.Entity> entities;
            if(ids != null && ids.Count()>0) {
                entities = await _EntityRepository.GetEntities(ids, true);
            }
            else {
                entities = await _EntityRepository.GetEntities(true);
            }
            return Mapper.MapHeader(entities);
        }

    }
}
