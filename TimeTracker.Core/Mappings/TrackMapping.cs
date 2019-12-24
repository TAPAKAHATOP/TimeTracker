using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Mappings
{
    public class TrackMapping : IAutoMappingOverride<Track>
    {
        public void Override(AutoMapping<Track> map)
        {
            map.References(item => item.Item).Cascade.All();
        }
    }
}