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
        private readonly CampaignManagerDbContext _context;
        private readonly DataMapper _mapper;

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
                .ThenInclude(p => p.Grantor)
                .Include(e => e.Permissions)
                .ThenInclude(p => p.Revealeds)
                .ThenInclude(r => r.Source);
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
                    .ThenInclude(p => p.Grantor)
                    .Include(e => e.Details)
                    .ThenInclude(d => d.Permissions)
                    .ThenInclude(p => p.Revealeds)
                    .ThenInclude(r => r.Source);
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
                return SortByIds(ids, _mapper.Map(await entities.Where(e => ids.Contains(e.Id)).ToListAsync()));
            else
                return _mapper.Map(await entities.ToListAsync());
        }

        private IEnumerable<Entity> SortByIds(IEnumerable<int> ids, IEnumerable<Entity> entities)
        {
            return (from i in ids select entities.SingleOrDefault(e => e.Id == i)).Where(e => e != null);
        }

        public async Task<Entity> AddEntity(Entity entity)
        {
            Models.Entity storing = _mapper.Map(entity);
            _context.Entities.Add(storing);
            await _context.SaveChangesAsync();
            return _mapper.Map(storing);
        }

        public async Task UpdateEntity(Entity entity)
        {
            try {
                Models.Entity dbEntity = await GetBase().Where(e => e.Id == entity.Id).SingleAsync();
                Models.Entity mapped = _mapper.Map(entity);
                dbEntity.Name = mapped.Name;
                dbEntity.PermissionsId = mapped.PermissionsId;
                dbEntity.Details = mapped.Details;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new NotFoundException("Entity not found", e);
            }
           
        }

        public async Task PatchEntity(Entity entity)
        {
            try
            {
                Models.Entity dbEntity = await GetBase().Where(e => e.Id == entity.Id).SingleAsync();
                if (entity.Name != null)
                    dbEntity.Name = entity.Name;
                if (entity.Permissions != null && entity.Permissions.Id != 0)
                    dbEntity.PermissionsId = (int)entity.Permissions?.Id;
                if (entity.Details != null && entity.Details.Count() > 0)
                    dbEntity.Details = dbEntity.Details.Concat(_mapper.Map(entity.Details)).ToList();
                if (entity.Properties != null && entity.Properties.Count() > 0)
                    dbEntity.Details = dbEntity.Details.Concat(_mapper.Map(entity.Properties)).ToList();
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new NotFoundException("Entity not found", e);
            }
        }

        public async Task DeleteEntity(int id)
        {
            try { 
            _context.Entities.Remove(await _context.Entities.Where(e => e.Id == id).SingleAsync());
            await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new NotFoundException("Entity not found", e);
            }
        }
    }
}
