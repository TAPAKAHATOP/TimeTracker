using System.Collections.Generic;

using TimeTracker.Core.Data;
using TimeTracker.Core.Models;
using NHibernate.Criterion;


namespace TimeTracker.Core.Services.Implements
{
    public abstract class ABaseService<TDomain> : IBaseService<TDomain>
    where TDomain : ABaseItem
    {
        public INHibernateHelper Database { get; set; }
        public ABaseService(INHibernateHelper database)
        {
            this.Database = database;
        }
        public IEnumerable<TDomain> GetAll()
        {
            using (var session = this.Database.getSession())
            {
                return session.QueryOver<TDomain>().List();
            }
        }

        public TDomain GetById(int id)
        {
            using (var session = this.Database.getSession())
            {
                return session.QueryOver<TDomain>().Where(client => client.Id == id).SingleOrDefault();
            }
        }

        public IEnumerable<TDomain> GetByIds(int[] ids)
        {
            using (var session = this.Database.getSession())
            {
                TDomain clientAlias = null;
                return session.QueryOver<TDomain>().Where(() => clientAlias.Id.IsIn(ids)).List();
            }
        }

        public virtual TDomain Save(TDomain item)
        {
            using (var session = this.Database.getSession())
            {
                return (TDomain)session.Save(item);
            }
        }

    }
}