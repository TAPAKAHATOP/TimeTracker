using NHibernate;

namespace TimeTracker.Core.Data
{
    public interface INHibernateHelper
    {
        ISession getSession();
    }
}