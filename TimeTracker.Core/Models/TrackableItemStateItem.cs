using System;
using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Models
{
    public class TrackableItemStateItem : ABaseItem, ICountable, IBlockable
    {
        public TrackableItemStateItem() : base()
        {
            Count = 1;
        }
        public virtual TrackableItem Item { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime Finish { get; set; }

        public virtual int Count { get; set; }


    }
}