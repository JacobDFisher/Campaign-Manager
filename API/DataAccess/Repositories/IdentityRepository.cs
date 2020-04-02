using Lib.Exceptions;
using Lib.Interfaces;
using Lib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private CampaignManagerDbContext _context;
        private DataMapper _mapper;
        private IGroupRepository _gRepo;

        public IdentityRepository(CampaignManagerDbContext context, DataMapper mapper, IGroupRepository groupRepository)
        {
            _context = context;
            _mapper = mapper;
            _gRepo = groupRepository;
        }
        public async Task<IEnumerable<Identity>> GetIdentities(IEnumerable<int> ids = null)
        {
            var identities = _context.Identities
                .Include(i => i.Authorships)
                .Include(i => i.IdentityGroups)
                .Include(i => i.Grants);
            if(ids != null) {
                return _mapper.Map(await identities.Where(i => ids.Contains(i.Id)).ToListAsync());
            } else
            {
                return _mapper.Map(await identities.ToListAsync());
            }
        }

        public async Task<Identity> GetIdentity(int id)
        {
            Models.Identity identity;
            var identities = _context.Identities
                .Include(i => i.Authorships)
                .Include(i => i.IdentityGroups)
                .Include(i => i.Grants);
            try
            {
                identity = await identities.SingleAsync(i => i.Id == id);
            } catch (Exception e)
            {
                throw new NotFoundException("Identity not found", e);
            }
            var retId = _mapper.Map(identity);
            retId.Groups = await _gRepo.GetGroups(from g in retId.Groups select g.Id, true);
            return retId;
        }
    }
}
