using Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Interfaces
{
    public interface IGroupRepository
    {
        public Task<Group> GetGroup(int id);
        public Task<IEnumerable<Group>> GetGroups(IEnumerable<int> ids, bool parents = false);
    }
}
