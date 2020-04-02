using Lib.Interfaces;
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lib.Exceptions;

namespace DataAccess.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private CampaignManagerDbContext _context;
        private DataMapper _mapper;

        public EntityRepository(CampaignManagerDbContext context, DataMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IQueryable<Models.Entity> GetBaseForHeader()
        {
            return _context.Entities
                .Include(e => e.Permissions)
                .ThenInclude(p => p.Author) // Entity Author
                .Include(e => e.Permissions)
                .ThenInclude(p => p.Perms)
                .Include(e => e.Permissions)
                .ThenInclude(p => p.Revealeds);
        }

        private IQueryable<Models.Entity> GetBase()
        {
            return GetBaseForHeader()
                .Include(e => e.Details)
                    .ThenInclude(d => d.Permissions)
                    .ThenInclude(p => p.Author) // Detail Author
                    .Include(e => e.Details)
                    .ThenInclude(d => d.Permissions)
                    .ThenInclude(p => p.Perms)
                    .Include(e => e.Details)
                    .ThenInclude(d => d.Permissions)
                    .ThenInclude(p => p.Revealeds);
        }

        public async Task<Entity> GetEntity(int id, bool header = false)
        {
            IQueryable<Models.Entity> entities;
            if (header)
                entities = GetBaseForHeader().AsNoTracking();
            else
                entities = GetBase().AsNoTracking();
            try
            {
                return _mapper.Map(await entities.SingleAsync(e => e.Id == id));
            } catch (Exception e)
            {
                throw new NotFoundException("Entity not found", e);
            }
        }

        public async Task<IEnumerable<Entity>> GetEntities(IEnumerable<int> ids = null, bool header = false)
        {
            IQueryable<Models.Entity> entities;
            if (header)
                entities = GetBaseForHeader().AsNoTracking();
            else
                entities = GetBase().AsNoTracking();
            if (ids != null)
                return _mapper.Map(await entities.Where(e => ids.Contains(e.Id)).ToListAsync());
            else
                return _mapper.Map(await entities.ToListAsync());
        }

    }
}
