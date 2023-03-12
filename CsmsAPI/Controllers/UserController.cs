using Infrastructure.ViewModel.Response;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserController(IUserService service, IHttpContextAccessor httpContextAccessor)
        {
            this.service = service;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<CacheModel>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginVM login)
        {
            var result = await service.Login(login);
            return Ok(new SuccessResponse<CacheModel>
            {
                Data = result
            });
        }
        

        [HttpPut("UpdatePhoneNumber/{OTP}/{UserId}/{PhoneNumber}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] string OTP, [FromRoute] string UserId, [FromRoute] string PhoneNumber)
        {
            var result = await service.UpdatePhoneNumber(OTP, UserId, PhoneNumber);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }

        [HttpPost("ChangePasswordByEmailAndOTP")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> ChangePasswordByEmailAndOTP([FromBody] ReqChangePasswordByEmail req)
        {
            var result = await service.ChangePasswordByEmailAndOTP(req);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }


        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<List<ResGetUser>>), 200)]
        public IActionResult GetAllUser()
        {
            var result = service.GetAllUser();
            return Ok(new SuccessResponse<List<ResGetUser>>
            {
                Data = result
            });
        }

        [HttpGet("GetUserByMailAndMobile/{Email}/{Phone}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<ResGetUser>), 200)]
        public async Task<IActionResult> GetUserByMailAndMobile([FromRoute] string Email, [FromRoute] string Phone)
        {
            var result = await service.GetUserByMailAndMobile(Email, Phone);
            return Ok(new SuccessResponse<ResGetUser>
            {
                Data = result
            });
        }
        [HttpGet("GetUserByMail/{Email}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<ResGetUser>), 200)]
        public async Task<IActionResult> GetUserByMail([FromRoute] string Email)
        {
            var result = await service.GetUserByMail(Email);
            return Ok(new SuccessResponse<ResGetUser>
            {
                Data = result
            });
        }
        [HttpGet("GetUserByMobile/{Phone}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<ResGetUser>), 200)]
        public async Task<IActionResult> GetUserByMobile([FromRoute] string Phone)
        {
            var result = await service.GetUserByMobile(Phone);
            return Ok(new SuccessResponse<ResGetUser>
            {
                Data = result
            });
        }



        [HttpGet("CheckMailAndPhone/{Phone}/{email}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> CheckMailAndPhone([FromRoute] string Phone, [FromRoute] string? email)
        {
            var result = await service.CheckMailAndPhone(Phone, email);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }
        [HttpGet("CheckMail/{email}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> CheckMail([FromRoute] string email)
        {
            var result = await service.CheckMail(email);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }
        [HttpGet("CheckPhone/{Phone}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> CheckPhone([FromRoute] string Phone)
        {
            var result = await service.CheckPhone(Phone);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] ReqUpdateUser req)
        {
            //var tokenData = httpContextAccessor.HttpContext.User.Claims.ToArray();

            //var userId = tokenData[0].Value;

            var result = await service.UpdateUser(id, req/*, req.Id*/);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<CacheModel>), 200)]
        public async Task<IActionResult> Register(ReqCreateUser req)
        {
            

            var result = await service.RegisterUser(req, null);
            return Ok(new SuccessResponse<CacheModel>
            {
                Data = result
            });
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<CacheModel>), 200)]
        public async Task<IActionResult> Create(ReqCreateUser req)
        {
            var result = await service.RegisterUser(req, null);
            return Ok(new SuccessResponse<CacheModel>
            {
                Data = result
            });
        }


        [HttpPut("Active/{UserId}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> Active([FromRoute] string UserId, [FromQuery] bool isActive)
        {
            var tokenData = httpContextAccessor.HttpContext.User.Claims.ToArray();
            //var role = userId.T

            var userId = tokenData[0].Value;



            var result = await service.ActivitUser(UserId, userId);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<ResGetUser>), 200)]
        public IActionResult GetUserById([FromRoute] string UserId)
        {
            var result = service.GetUserById(UserId);
            return Ok(new SuccessResponse<ResGetUser>
            {
                Data = result
            });
        }

        [HttpPut("ChangePassword/{UserId}/{newPassword}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> ChangePassWord(string UserId, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(UserId)) return BadRequest();
            var result = await service.ChangePassWord(UserId, newPassword);
            if (result == false) return StatusCode(500);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }


        #region AdminPanel

        
        [HttpGet("GetAllUserWithChilds")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<List<ResGetUser>>), 200)]
        public IActionResult GetAllUserWithChilds()
        {
            var result = service.GetAllUserWithChilds();
            return Ok(new SuccessResponse<List<ResGetUser>>
            {
                Data = result
            });
        }
        
        [HttpPost("AddUser")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<List<ReqCreateUser>>), 200)]
        public async Task<IActionResult> AddUser(ReqAddUser req)
        {
            var result = await service.AddUser(req);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }

        

        [HttpPost("ChangePasswordByUserId/{id}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<bool>), 200)]
        public async Task<IActionResult> ChangePasswordByUserId([FromBody] ReqChangePasswordByUser req, string id)
        {
            var result = await service.ChangePasswordByUser(req, id);
            return Ok(new SuccessResponse<bool>
            {
                Data = result
            });
        }
        [HttpDelete("switchDelete/{Id}")]
        [ProducesResponseType(typeof(FailureResponse), 500)]
        [ProducesResponseType(typeof(SuccessResponse<Task<bool>>), 200)]
        public IActionResult Switchdelete(string Id)
        {
            if (Id == null)
            {
                return BadRequest(new FailureResponse()
                {
                    Code = 400,
                    Error = new List<string>()
                    {
                        "BadRequest"
                    }
                });
            }
            var result = service.ActivitUser(Id, "");
            return Ok(new SuccessResponse<Task<bool>>()
            {
                Code = 200,
                Data = result
            });
        }
        
        #endregion
    }
}
