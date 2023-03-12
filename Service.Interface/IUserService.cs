
using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Service.Interface
{
    public interface IUserService
    {
        public Task<ResGetUser?> GetUserByMailAndMobile(string Email, string Phone);
        public Task<ResGetUser?> GetUserByMail(string Email);
        public Task<ResGetUser?> GetUserByMobile(string Phone);

        public Task<ResGetUser?> GetUser(string search);

        public Task<CacheModel> RegisterUser(ReqCreateUser req, string CreatedBy);

        

        public Task<bool> ActivitUser(string UserId, string ModifiedBy);
        public Task<CacheModel?> Login(LoginVM model);

        public List<ResGetUser> GetAllUser();
        public ResGetUser GetUserById(string UserId);

        public Task<bool> UpdateUser(string userId, ReqUpdateUser req/*, string UpdatedBy*/);

        public Task<bool> ChangePassWord(string userId, string password);
       
        public Task<bool> UpdatePhoneNumber(string OTP, string UserId, string NewPhonerNumber);
        
        public Task<bool> ChangePasswordByEmailAndOTP(ReqChangePasswordByEmail req);
        public Task<bool> CheckMailAndPhone(string phone, string? email);
        public Task<bool> CheckMail(string email);
        public Task<bool> CheckPhone(string phone);


        #region AdminPanel

        public List<ResGetUser> GetAllUserWithChilds();
       
        public Task<bool> AddUser(ReqAddUser req);
        public Task<bool> ChangePasswordByUser(ReqChangePasswordByUser req, string userId);
        #endregion
    }
}
