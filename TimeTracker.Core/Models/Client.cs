using System.Collections.Generic;
using TimeTracker.Core.Utils;

namespace TimeTracker.Core.Models
{
    public class Client : ABaseItem, IBlockable
    {
        public Client() : base()
        {
            this.Processes = new List<ClientProcess>();
        }

        public virtual string Title { get; set; }
        public virtual IList<ClientProcess> Processes { get; set; }

    }
}