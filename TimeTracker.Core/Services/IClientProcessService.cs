using System.Collections.Generic;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services
{
    public interface IClientProcessService : IBaseService<ClientProcess>
    {
        IEnumerable<ClientProcess> GetByClientId(int id);
    }
}