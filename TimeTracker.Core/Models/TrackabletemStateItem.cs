using System;

namespace TimeTracker.Core.Models
{
    public class TrackabletemStateItem : ABaseItem
    {
        public virtual TrackableItem Item { get; set; }
        public virtual DateTime From { get; set; }
        public virtual bool Deparecated { get; set; }
    }
}