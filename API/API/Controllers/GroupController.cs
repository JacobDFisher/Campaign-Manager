using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Lib.Exceptions;
using Lib.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        IGroupRepository _repo;
        public GroupController(IGroupRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups([FromQuery] IEnumerable<int> ids)
        {
            return Ok(Mapper.Map(await _repo.GetGroups(ids, true)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup([FromRoute] int id)
        {
            try
            {
                return Ok(Mapper.Map(await _repo.GetGroup(id)));
            } catch (NotFoundException)
            {
                return NotFound();
            }
            
        }

        [HttpGet("{id}/all")]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroupsByGroup([FromRoute] int id)
        {
            return Ok(Mapper.Map(await _repo.GetGroups(new int[] { id }, true)));
        }
    }
}