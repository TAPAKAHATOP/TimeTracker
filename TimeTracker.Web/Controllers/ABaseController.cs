using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Core.Dtos;
using TimeTracker.Core.Services;

namespace TimeTracker.Web.Controllers
{
    public abstract class ABaseController<TDomain, TDto, TDomainService> : ControllerBase
    where TDto : IDTO
    where TDomainService : IBaseService<TDomain>
    {
        protected TDomainService Service { get; set; }

        public ABaseController(TDomainService srv)
        {
            this.Service = srv;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TDto> GetAll()
        {
            return this.ToDTO(this.Service.GetAll());
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public TDto GetById(int id)
        {
            return this.ToDTO(this.Service.GetById(id));
        }

        [HttpPost]
        public TDto Save(TDto dto)
        {
            return this.ToDTO(this.Service.Save(this.ToDomain(dto)));
        }

        public IEnumerable<TDto> ToDTO(IEnumerable<TDomain> domains)
        {
            foreach (var item in domains)
            {
                yield return this.ToDTO(item);
            }
        }
        public abstract TDto ToDTO(TDomain domain);

        public IEnumerable<TDomain> ToDomain(IEnumerable<TDto> dtos)
        {
            foreach (var item in dtos)
            {
                yield return this.ToDomain(item);
            }
        }

        public abstract TDomain ToDomain(TDto dto);
    }
}