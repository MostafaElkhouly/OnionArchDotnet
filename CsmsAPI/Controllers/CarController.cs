using CsmsAPI.Base;
using Domain.Entities.Models;
using Infrastructure.Executed;
using Infrastructure.Executed.IExecuteies;
using Infrastructure.ViewModel.Response;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CsmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService service;
        private readonly IServiceOrchestrator orchestrator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CarController(ICarService service, IServiceOrchestrator orchestrator, IHttpContextAccessor httpContextAccessor = null)
        {
            this.service = service;
            this.orchestrator = orchestrator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] ResReqCar req)
        {

            if (req == null)
                return BadRequest(new FailureResponse()
                {
                    Code = 400
                });

            if (ModelState.IsValid is false)
            {
                return BadRequest(new FailureResponse<ModelStateDictionary>
                {
                    Error = ModelState
                });
            }



            //var result = await orchestrator.ExecutAsync<ResReqCar, ResReqCar>(service.Create, req);
            ResReqCar result = null;
            return Ok(new SuccessResponse<ResReqCar>()
            {
                Code = 200,
                Data = result
            });
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> Update([FromRoute, Required] Guid carId, [FromBody, Required] ResReqCar req)
        {
            if (req == null)
                return BadRequest(new FailureResponse()
                {
                    Code = 400
                });

            if (ModelState.IsValid is false)
            {
                return BadRequest(new FailureResponse<ModelStateDictionary>
                {
                    Error = ModelState
                });
            }

            if(carId != req.Id)
            {
                return BadRequest(new FailureResponse<ModelStateDictionary>
                {
                    Message = "The Ids are not match!"
                });
            }

            //var result = await orchestrator.ExecutAsync<ResReqCar, ResReqCar>(service.UpdateAsync, req);
            ResReqCar result = null;
            return Ok(new SuccessResponse<ResReqCar>()
            {
                Code = 200,
                Data = result
            });

        }

        [HttpGet("GetAllCarWithStatus")]
        public async Task<IActionResult> GetAllCarWithStatus()
        {
            var result = await service.GetAllCarWithStatus();
            return Ok(new SuccessResponse<List<ResCarWithStatus>>
            {
                Data = result
            });
        }
    }
}
