using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Mappings
{
    public class ClientProcessMapping : IAutoMappingOverride<ClientProcess>
    {
        public void Override(AutoMapping<ClientProcess> mapping)
        {
            mapping.HasMany(item => item.Items);
            mapping.References(item => item.OwnerClient).Cascade.All();
        }
    }
}