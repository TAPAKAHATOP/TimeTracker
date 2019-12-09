using System.Collections.Generic;
using TimeTracker.Core.Data;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services.Implements
{
    public class ClientProcessService : ABaseService<ClientProcess>, IClientProcessService
    {
        public ClientProcessService(INHibernateHelper database) : base(database)
        {
        }

        public IEnumerable<ClientProcess> GetByClientId(int id)
        {
            using (var sess = this.Database.getSession())
            {
                return sess.QueryOver<ClientProcess>().Where(process => process.OwnerClient.Id == id).List();
            }
        }
    }
}