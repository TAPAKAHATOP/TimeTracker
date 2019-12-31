using System.Collections.Generic;
using TimeTracker.Core.Models.Utils;
using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Models
{
    public class TrackableItem : ABaseItem, IBlockable
    {
        public TrackableItem() : base()
        {
            TrackingPeriod = Period.None;
            TrackingStep = 1;

            this.TrackList = new List<Track>();
            this.States = new List<TrackableItemStateItem>();

            var state = new TrackableItemStateItem();
            state.Item = this;
            this.States.Add(state);
        }
        public virtual ClientProcess OwnerProcess { get; set; }

        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Track> TrackList { get; set; }
        public virtual IList<TrackableItemStateItem> States { get; set; }

        public virtual Period TrackingPeriod { get; set; }
        public virtual int TrackingStep { get; set; }
    }
}