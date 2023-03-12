using CsmsAPI.Base;
using Domain.Entities.Models;
using Infrastructure.Executed.IExecuteies;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.Response;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.Interface;

namespace CsmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService service;
        private readonly IServiceOrchestrator orchestrator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ColorController(IColorService service, IServiceOrchestrator orchestrator, IHttpContextAccessor httpContextAccessor = null) 
        {
            this.service = service;
            this.orchestrator = orchestrator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CreateColor([FromBody] ResReqColor req)
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



            var result = await  orchestrator.ExecutUnasyncFunc<ResReqColor, ResReqColor>(service.Create, req);
            
            return Ok(new SuccessResponse<ResReqColor>()
            {
                Code = 200,
                Data = result
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateColor([FromBody] ResReqColor req)
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



            var result = await orchestrator.ExecutAsync<ResReqColor, ResReqColor>(service.UpdateAsync, req);

            return Ok(new SuccessResponse<ResReqColor>()
            {
                Code = 200,
                Data = result
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = orchestrator.GetList<BaseViewModel, List<ResReqColor>>(service.GetAllsAsync, new BaseViewModel
            {
                IsDeleted = false
            });

            return Ok(new SuccessResponse<List<ResReqColor>>()
            {
                Code = 200,
                Data = result
            });
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]Guid Id)
        {
            var result = await orchestrator.GetAsync<BaseViewModel, ResReqColor>(service.GetByIdAsync, new BaseViewModel
            {
                IsDeleted = false,
                Id = Id
            });

            return Ok(new SuccessResponse<ResReqColor>()
            {
                Code = 200,
                Data = result
            });
        }

        [HttpGet("Async")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await orchestrator.GetListAsync<BaseViewModel, List<ResReqColor>> (service.GetColorsAsync, new BaseViewModel
            {
                IsDeleted = false
            });

            return Ok(new SuccessResponse<List<ResReqColor>>()
            {
                Code = 200,
                Data = result
            });
        }
    }
}
