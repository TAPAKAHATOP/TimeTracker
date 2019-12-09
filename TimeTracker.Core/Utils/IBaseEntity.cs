using System;

namespace TimeTracker.Core.Utils
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        bool Deleted { get; set; }

        DateTime Created { get; set; }
        bool Blocked { get; set; }
    }
}