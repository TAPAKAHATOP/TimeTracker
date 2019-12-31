using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Mappings
{
    public class TrackableItemStateItemMapping : IAutoMappingOverride<TrackableItemStateItem>
    {
        public void Override(AutoMapping<TrackableItemStateItem> map)
        {
            map.References(item => item.Item).Cascade.All();
        }
    }
}