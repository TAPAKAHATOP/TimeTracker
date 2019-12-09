using System.Collections.Generic;
using TimeTracker.Core.Dtos;
using TimeTracker.Core.Models;
using TimeTracker.Core.Services;

namespace TimeTracker.Web.Controllers
{
    public class TrackableItemController : ABaseController<TrackableItem, TrackableItemDTO, ITrackableItemService>
    {
        public TrackableItemController(ITrackableItemService srv) : base(srv)
        {
        }

        public override TrackableItem ToDomain(TrackableItemDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public override TrackableItemDTO ToDTO(TrackableItem domain)
        {
            return new TrackableItemDTO()
            {
                Id = domain.Id,
                Created = domain.Created,
                Count = domain.Count
            };
        }
    }
}