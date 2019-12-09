using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Dtos;
using TimeTracker.Core.Models;
using TimeTracker.Core.Services;

namespace TimeTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ABaseController<Client, ClientDTO, IClientService>
    {

        public ClientsController(IClientService srv) : base(srv)
        {
        }

        public override ClientDTO ToDTO(Client domain)
        {
            return new ClientDTO()
            {
                Id = domain.Id,
                Created = domain.Created,
                Deleted = domain.Deleted,
                Blocked = domain.Blocked,

            };
        }

        public override Client ToDomain(ClientDTO dto)
        {
            return new Client()
            {
                Id = dto.Id
            };
        }
    }
}
