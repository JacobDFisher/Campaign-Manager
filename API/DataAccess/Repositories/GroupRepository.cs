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
    public class GroupRepository : IGroupRepository
    {
        private CampaignManagerDbContext _context;
        private DataMapper _mapper;

        public GroupRepository(CampaignManagerDbContext context, DataMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Group> GetGroup(int id)
        {
            Models.Group retVal;
            var groups = _context.Groups
                .Include(g => g.MemberOf);
            try {
                 retVal = await groups.SingleAsync(g => g.Id == id);
            } catch
            {
                retVal = null;
            }
            return _mapper.Map(retVal);
        }

        public async Task<IEnumerable<Group>> GetGroups(IEnumerable<int> ids, bool parents = false)
        {
            if (parents)
                return await GetGroupsAndParents(ids);
            return _mapper.Map(await _context.Groups.Where(g => ids.Contains(g.Id)).Include(g => g.MemberOf).ToListAsync());
        }

        private async Task<IEnumerable<Group>> GetGroupsAndParents(IEnumerable<int> ids)
        {
            IEnumerable<Models.Group> groups = await _context.Groups.Where(g => ids.Contains(g.Id)).Include(g=>g.MemberOf).ToListAsync();
            HashSet<int> seenIds = new HashSet<int>();
            IEnumerable<int> newIds = new HashSet<int>(groups.SelectMany(g => g.MemberOf.Select(g => g.GroupId)).Distinct().Except(seenIds));
            while (newIds != null && newIds.Count() > 0)
            {
                seenIds.UnionWith(newIds);
                var newGroups = await _context.Groups.Where(g => newIds.Contains(g.Id)).Include(g => g.MemberOf).ToListAsync();
                groups = groups.Concat(newGroups);
                newIds = new HashSet<int>(newGroups.SelectMany(g => g.MemberOf.Select(g => g.GroupId)).Distinct().Except(seenIds));
            }
            return _mapper.Map(groups);
        }
    }
}
