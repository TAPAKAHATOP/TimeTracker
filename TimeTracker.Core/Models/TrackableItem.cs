using System.Collections.Generic;
using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Models
{
    public class TrackableItem : ABaseItem, ICountable, IBlockable
    {
        public virtual ClientProcess OwnerProcess { get; set; }

        public virtual string Description { get; set; }

        public virtual int Count { get; set; }

        public virtual List<Track> TrackList { get; set; }
    }
}