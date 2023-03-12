using CsmsAPI.Base;
using Infrastructure.ViewModel.Response;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace CsmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCardController : ControllerBase
    {
        private readonly IJobCardService service;
        private readonly IHttpContextAccessor httpContextAccessor;

        public JobCardController(IJobCardService service, IHttpContextAccessor httpContextAccessor = null)
        {
            this.service = service;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("RequestSparPartForJobCard")]
        public async Task<IActionResult> RequestSparPartForJobCard([FromBody] ReqSparPartForJobCard req)
        {
            //var result = await service.RequestSparPartForJobCard(req);
            bool result = false;
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }

        [HttpPost("AssignJobToMechanic/{jobId}/{mechanicId}")]
        public async Task<IActionResult> AssignJobToMechanic([FromRoute]Guid jobId, [FromRoute]string mechanicId)
        {
            //var result = await service.AssignJobToMechanic(jobId, mechanicId);
            bool result = false;

            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }

        [HttpGet("GetAllJobcardByMechanicId/{mechanicId}")]
        public async Task<IActionResult> GetAllJobcardByMechanicId([FromRoute]string mechanicId)
        {
            var result = await service.GetAllJobcardByMechanicId(mechanicId);
            return Ok(new SuccessResponse<List<ResReqJobCard>>
            {
                Data = result
            });
        }

        [HttpGet("GetAllJobcardByTicketId/{ticketId}")]
        public async Task<IActionResult> GetAllJobcardByTicketId([FromRoute]Guid ticketId)
        {
            var result = await service.GetAllJobcardByTicketId(ticketId);
            return Ok(new SuccessResponse<List<ResReqJobCard>>
            {
                Data = result
            });
        }

        [HttpPost("ChangeStatus/{JobcardId}/{status}")]
        public async Task<IActionResult> ChangeStatus([FromRoute]Guid JobcardId, [FromRoute]StatusCar status)
        {
            //var result = await service.ChangeStatus(JobcardId, status);
            var result = false;
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }

    }
}
