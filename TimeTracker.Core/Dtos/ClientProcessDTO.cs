using System;

namespace TimeTracker.Core.Dtos
{
    public class ClientProcessDTO : IDTO
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime Created { get; set; }
        public bool Blocked { get; set; }
    }
}