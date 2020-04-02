﻿using Lib.Interfaces;
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
        private CampaignManagerDbContext _context;
        private DataMapper _mapper;

        public EntityRepository(CampaignManagerDbContext context, DataMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
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
                .ThenInclude(p => p.Revealeds)
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
                    .ThenInclude(p => p.Revealeds);
                    //.Include(e => e.EntityGroups);
            }
            return _mapper.Map(await retrieved.ToListAsync());
        }

        public async Task<IEnumerable<Entity>> GetEntities(IEnumerable<int> ids, bool header = false)
        {
            await Task.Yield();
            throw new NotImplementedException();
        }

    }
}