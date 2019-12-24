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
    public class ClientProcessesController : ABaseController<ClientProcess, ClientProcessDTO, IClientProcessService>
    {
        public ClientProcessesController(IClientProcessService srv) : base(srv)
        {
        }
        public override ClientProcessDTO ToDTO(ClientProcess domain)
        {
            return new ClientProcessDTO()
            {
                Id = domain.Id,
                Created = domain.Created,
                Deleted = domain.Deleted,
                Blocked = domain.Blocked,

            };
        }
        public override ClientProcess ToDomain(ClientProcessDTO dto)
        {
            return new ClientProcess()
            {
                Id = dto.Id
            };
        }

        [HttpGet]
        [Route("api/ClientProcess/ByClientId/{id}")]
        public IEnumerable<ClientProcessDTO> GetByClientId(int id)
        {
            return this.ToDTO(this.Service.GetByClientId(id));
        }
    }
}
