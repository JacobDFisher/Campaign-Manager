using Lib.Interfaces;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        CampaignManagerDbContext _context;
        public EntityRepository(CampaignManagerDbContext context)
        {
            _context = context;
        }
        #region Entity
        public async Task<Entity> GetEntity(int id, bool header = false)
        {
            await Task.Yield();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Entity>> GetEntities(bool header = false)
        {
            var retrieved = _context.Entities
                .Include(e => e.Permissions)
                .ThenInclude(p => p.Author) // Entity Author
                .Include(e => e.Permissions)
                .ThenInclude(p => p.Perms)
                .Include(e => e.Permissions)
                .ThenInclude(p => p.Revealed)
                .AsNoTracking();
            if (!header)
            {
                retrieved = retrieved.Include(e => e.Details)
                    .ThenInclude(d => d.Permissions)
                    .ThenInclude(p => p.Author) // Detail Author
                    .Include(e => e.Details)
                    .ThenInclude(d => d.Permissions)
                    .ThenInclude(p => p.Perms)
                    .Include(e => e.Details)
                    .ThenInclude(d => d.Permissions)
                    .ThenInclude(p => p.Revealed);
                    //.Include(e => e.EntityGroups);
            }
            return Mapper.Map(await retrieved.ToListAsync());
        }

        public async Task<IEnumerable<Entity>> GetEntities(IEnumerable<int> ids, bool header = false)
        {
            await Task.Yield();
            throw new NotImplementedException();
        }
        #endregion

    }
}
