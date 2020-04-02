using API.Models;
using Lib.Exceptions;
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
    public class EntityController : ControllerBase
    {
        IEntityRepository _EntityRepository;
        public EntityController(IEntityRepository entityRepository)
        {
            _EntityRepository = entityRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entity>> GetEntity([FromRoute] int id)
        {
            Lib.Models.Entity entity;
            try { 
            entity = await _EntityRepository.GetEntity(id);
            return Ok(Mapper.Map(entity));
            } catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/header")]
        public async Task<ActionResult<Entity>> GetEntityHeader([FromRoute] int id)
        {
            Lib.Models.Entity entity;
            try
            {
                entity = await _EntityRepository.GetEntity(id, true);
                return Ok(Mapper.Map(entity));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entity>>> GetEntities([FromQuery] IEnumerable<int> ids)
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
            return Ok(Mapper.Map(entities));
        }

        [HttpGet("header")]
        public async Task<ActionResult<IEnumerable<EntityHeader>>> GetEntityHeaders([FromQuery] IEnumerable<int> ids)
        {
            IEnumerable<Lib.Models.Entity> entities;
            if(ids != null && ids.Count()>0) {
                entities = await _EntityRepository.GetEntities(ids, true);
            }
            else {
                entities = await _EntityRepository.GetEntities(header: true);
            }
            return Ok(Mapper.MapHeader(entities));
        }

    }
}
