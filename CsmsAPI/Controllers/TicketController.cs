using CsmsAPI.Base;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace CsmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService service;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TicketController(ITicketService service, IHttpContextAccessor httpContextAccessor = null)
        {
            this.service = service;
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
