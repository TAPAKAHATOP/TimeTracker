using System.Collections.Generic;
using TimeTracker.Core.Data;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services.Implements
{
    public class TrackableItemService : ABaseService<TrackableItem>, ITrackableItemService
    {
        public TrackableItemService(INHibernateHelper database) : base(database)
        {
        }
    }
}