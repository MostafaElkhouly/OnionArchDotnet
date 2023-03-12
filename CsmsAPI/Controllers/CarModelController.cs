using CsmsAPI.Base;
using Domain.Entities.Models;
using Infrastructure.Executed;
using Infrastructure.Executed.Executeies;
using Infrastructure.Executed.IExecuteies;
using Infrastructure.ViewModel.Response;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.Interface;
using System.ComponentModel.DataAnnotations;

namespace CsmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly ICarModelService service;
        private readonly IServiceOrchestrator orchestrator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CarModelController(ICarModelService service, IServiceOrchestrator orchestrator, IHttpContextAccessor httpContextAccessor = null) 
        {
            this.service = service;
            this.orchestrator = orchestrator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody, Required] ResReqCarModel req)
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

            //var result = await orchestrator.ExecutAsync<ResReqCarModel, ResReqCarModel, CarModel>(service.Create, req);
            ResReqCarModel result = null;
      
            return Ok(new SuccessResponse<ResReqCarModel>()
            {
                Code = 200,
                Data = result
            });
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> Update([FromRoute, Required] Guid carId, [FromBody, Required] ResReqCarModel req)
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

            //var result = await orchestrator.ExecutAsync<ResReqCarModel, ResReqCarModel, CarModel>(service.UpdateAsync, req);
            ResReqCarModel result = null;
            return Ok(new SuccessResponse<ResReqCarModel>()
            {
                Code = 200,
                Data = result
            });

        }
    }
}
