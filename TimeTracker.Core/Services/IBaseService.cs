using System.Collections.Generic;

namespace TimeTracker.Core.Services
{
    public interface IBaseService<TDomain>
    {
        IEnumerable<TDomain> GetAll();
        TDomain GetById(int id);

        IEnumerable<TDomain> GetByIds(int[] id);

        TDomain Save(TDomain item);
    }
}