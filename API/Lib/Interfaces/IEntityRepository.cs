
using Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Interfaces
{
    public interface IEntityRepository
    {
        public Task<Entity> GetEntity(int id, bool header=false);
        public Task<IEnumerable<Entity>> GetEntities(bool header=false);
        public Task<IEnumerable<Entity>> GetEntities(IEnumerable<int> ids, bool header=false);
    }
}
