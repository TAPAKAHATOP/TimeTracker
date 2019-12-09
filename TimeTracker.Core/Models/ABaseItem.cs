using System;
using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Models
{
    public abstract class ABaseItem : IBaseEntity
    {
        public ABaseItem()
        {
            this.Created = DateTime.Now;
        }
        public virtual int Id { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual bool Blocked { get; set; }
        public virtual bool Deleted { get; set; }
    }
}