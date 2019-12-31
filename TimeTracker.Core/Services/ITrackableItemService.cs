using System;
using System.Collections.Generic;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services
{
    public interface ITrackableItemService : IBaseService<TrackableItem>
    {
        bool CheckAvailableCountBetweenTimes(int id, DateTime from, DateTime to, int count = 1);
        bool CheckAvailableCountBetweenTimes(int[] ids, DateTime from, DateTime to, int count = 1);

        IEnumerable<TrackableItem> GetAllForProcessId(int processId);
        TrackableItem GetByTitleForProcess(string title, int processId);
    }
}