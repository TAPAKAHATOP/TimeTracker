using System;
using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Models
{
    public class Track : ABaseItem, ICountable
    {
        public virtual DateTime Start { get; set; }
        public virtual DateTime Finish { get; set; }

        public virtual TrackableItem Item { get; set; }

        public virtual int Count { get; set; }
        public virtual bool Canceled { get; set; }
        public virtual bool Infinity { get; set; }
    }
}