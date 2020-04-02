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
    public class IdentityController : ControllerBase
    {
        private IIdentityRepository _repo;

        public IdentityController(IIdentityRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Identity>>> GetIdentities([FromQuery] IEnumerable<int> ids)
        {
            IEnumerable<Lib.Models.Identity> identities;
            if (ids != null && ids.Count() > 0)
            {
                identities = await _repo.GetIdentities(ids);
            }
            else
            {
                identities = await _repo.GetIdentities();
            }
            return Ok(Mapper.Map(identities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Identity>> GetIdentity([FromRoute] int id)
        {
            try {
            Lib.Models.Identity identity = await _repo.GetIdentity(id);
                return Ok(Mapper.Map(identity));
            } catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}