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

        #region GET

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

        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult> PostEntity([FromBody] Entity entity)
        {
            var e = await _EntityRepository.AddEntity(Mapper.Map(entity));
            return CreatedAtAction("GetEntity", new { id = e.Id }, e);
        }
        #endregion

        #region PUT
        [HttpPut]
        public async Task<ActionResult> PutEntity([FromBody] Entity entity)
        {
            try
            {
                await _EntityRepository.UpdateEntity(Mapper.Map(entity));
            } catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region PATCH
        [HttpPatch]
        public async Task<ActionResult> PatchEntity([FromBody] Entity entity)
        {
            try { 
            await _EntityRepository.PatchEntity(Mapper.Map(entity));
            } catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEntity([FromRoute] int id)
        {
            try { 
            await _EntityRepository.DeleteEntity(id);
            } catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
    }
}
