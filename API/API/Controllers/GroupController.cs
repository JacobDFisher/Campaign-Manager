using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
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
        [HttpGet("{id}")]
        public async Task<Group> GetGroup([FromRoute] int id)
        {
            return Mapper.Map(await _repo.GetGroup(id));
        }

        [HttpGet("{id}/all")]
        public async Task<IEnumerable<Group>> GetGroups([FromRoute] int id)
        {
            return Mapper.Map(await _repo.GetGroups(new int[] { id }, true));
        }
    }
}