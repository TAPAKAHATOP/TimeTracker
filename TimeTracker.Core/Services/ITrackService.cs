using System;
using System.Collections.Generic;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services
{
    public interface ITrackService : IBaseService<Track>
    {
        IEnumerable<Track> GetBetweenTimes(int id, DateTime from, DateTime to);
        bool CheckAvailableCountBetweenTime(int id, int count, DateTime from, DateTime to);
    }
}