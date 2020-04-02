using Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Interfaces
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<Identity>> GetIdentities(IEnumerable<int> ids = null);
        Task<Identity> GetIdentity(int id);
    }
}
