using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Dtos;
using TimeTracker.Core.Models;
using TimeTracker.Core.Services;

namespace TimeTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ABaseController<Track, TrackDTO, ITrackService>
    {
        public TracksController(ITrackService srv) : base(srv)
        {

        }

        public IEnumerable<TrackDTO> getBetweenTimes(int id, DateTime from, DateTime to)
        {
            return this.ToDTO(this.Service.GetBetweenTimes(id, from, to));
        }



        public override Track ToDomain(TrackDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public override TrackDTO ToDTO(Track domain)
        {
            return new TrackDTO()
            {
                Id = domain.Id,
                Count = domain.Count,
                Created = domain.Created
            };
        }
    }
}