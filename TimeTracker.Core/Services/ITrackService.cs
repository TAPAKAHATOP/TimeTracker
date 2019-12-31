using System;
using System.Collections.Generic;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services
{
    public interface ITrackService : IBaseService<Track>
    {
        IEnumerable<Track> GetBetweenTimes(int id, DateTime from, DateTime to);
        Track CreateTrack(int itemId, DateTime from, DateTime to, int count);
        Track StartTrack(int itemId, DateTime from);
        Track FinishTrack(int trackId, DateTime finishDate);
        IEnumerable<Track> ReleaseTrackbleItemBetweenTimes(int itemId, DateTime from, DateTime to);
        IEnumerable<Track> ChangeTrackableCountBetweenDates(int[] itemIds, int count, DateTime from, DateTime to);
    }
}