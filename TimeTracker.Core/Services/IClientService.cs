using TimeTracker.Core.Dtos;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services
{
    public interface IClientService : IBaseService<Client>
    {
        Client GetClientByTitle(string title);
    }
}