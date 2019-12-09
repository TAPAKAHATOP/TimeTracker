using System.Collections.Generic;
using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Models
{
    public class Client : ABaseItem, IBlockable
    {
        public virtual string Title { get; set; }
        public virtual IEnumerable<ClientProcess> Processes { get; set; }

    }
}