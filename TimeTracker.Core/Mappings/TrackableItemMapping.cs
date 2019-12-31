using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Mappings
{
    public class TrackableItemMapping : IAutoMappingOverride<TrackableItem>
    {
        public void Override(AutoMapping<TrackableItem> map)
        {
            map.HasMany(item => item.TrackList);
            map.HasMany(item => item.States);

            map.References(item => item.OwnerProcess).Cascade.All();
        }
    }
}