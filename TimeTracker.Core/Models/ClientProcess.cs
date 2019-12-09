using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Models
{
    public class ClientProcess : ABaseItem, IBlockable
    {
        public virtual string ProcessName { get; set; }
        public virtual Client OwnerClient { get; set; }
    }
}