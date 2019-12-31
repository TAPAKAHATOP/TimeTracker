using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Dtos;
using TimeTracker.Core.Models;
using TimeTracker.Core.Services;

namespace TimeTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackableItemsController : ABaseController<TrackableItem, TrackableItemDTO, ITrackableItemService>
    {
        public TrackableItemsController(ITrackableItemService srv) : base(srv)
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
                Created = domain.Created
            };
        }
    }
}