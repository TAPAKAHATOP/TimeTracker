using System.Collections.Generic;
using NHibernate.Criterion;
using TimeTracker.Core.Data;
using TimeTracker.Core.Models;
using NHibernate.Linq;

namespace TimeTracker.Core.Services.Implements
{
    public class ClientService : ABaseService<Client>, IClientService
    {
        public ClientService(INHibernateHelper database) : base(database)
        {
        }

        public Client GetClientByTitle(string title)
        {
            using (var session = this.Database.getSession())
            {
                return session.QueryOver<Client>().Where(cl => cl.Title == title).SingleOrDefault();
            }
        }

        public override Client Save(Client item)
        {
            using (var session = this.Database.getSession())
            {
                var res = session.Save(item);
                return item; //(Client)session.Save(item);
            }
        }
    }
}