
using AutoMapper;
using Domain.Entities.Models;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.IRepository;
using Persistence.IRepository.IUserRepository;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

//using Grpc.Net.Client;

namespace Service.Data
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly IRoleService roleService;
        private readonly ICarService carService;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration config,
            IRoleService roleService,
            ICarService carService
            )
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.config = config;
            this.roleService = roleService;
            this.carService = carService;
            this.unitOfWork = unitOfWork;

        }

        public async Task<bool> ActivitUser(string UserId, string ModifiedBy)
        {
            var user = await userRepository.FindUser(e => e.Id == UserId);

            if (user == null)
                throw new Exception("This User Is Not Found!");

            user.IsDeleted = !user.IsDeleted;

            var result = await userRepository.UpdateUser(user);

            if (!result.Succeeded)
            {
                throw new Exception("there exception in update profile");
            }

            return result.Succeeded;
        }


        public async Task<bool> ChangePassWord(string userId, string password)
        {
            try
            {
                var user = await userRepository.FindByIdAsync(userId);
                if (user == null)
                    throw new Exception("This User Is Not Found");



                var result = await userRepository.ChangePassword(user, password);
                if (result.Succeeded) return true;
                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CreateToken(Account user, string role)
        {

            IdentityOptions _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("Name",user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role),
                        new Claim("Role",role),
                        new Claim("UserName", user.UserName.ToString()),
                        new Claim("isPublished","true"),


                }),
                Expires = DateTime.UtcNow.AddDays(1),///???
                //Expires = DateTime.UtcNow.AddMinutes(6),///???
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("(G+KbPeShVmYq3t6w9z$B&E)H@McQfTjWnZr4u7x!A%D*F-JaNdRgUkXp2s5v8y/B?E(H+KbPeShVmYq3t6w9z$C&F)J@NcQfTjWnZr4u7x!A%D*G-KaPdSgUkXp2s5v")),
                            SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        private string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public List<ResGetUser> GetAllUser()
        {
            //unitOfWork.Get();
            var user = userRepository.GetListOfUsers();
            user = user.FindAll(x => x.IsDeleted == false).ToList();
            return mapper.Map<List<ResGetUser>>(user);
        }





        public ResGetUser GetUserById(string UserId)
        {
            var user = userRepository.GetUserById(UserId);

            var userVM = mapper.Map<ResGetUser>(user);
            userVM.HasPassword = user.PasswordHash != null;
            return userVM;

        }


        public async Task<ResGetUser?> GetUserByMailAndMobile(string Email, string Phone)
        {
            var user = await userRepository.FindUser(e => (!string.IsNullOrEmpty(Email) && e.Email.Equals(Email))
               || (!string.IsNullOrEmpty(Phone) && e.PhoneNumber.Equals(Phone)));

            if (user == null || user.IsDeleted)
            {
                return null;
            }

            var result = mapper.Map<ResGetUser>(user);

            return result;


        }
        public async Task<ResGetUser?> GetUserByMail(string Email)
        {
            var user = await userRepository.FindUser(e => e.Email.Equals(Email));

            if (user == null || user.IsDeleted)
            {
                return null;
            }

            var result = mapper.Map<ResGetUser>(user);

            return result;


        }
        public async Task<ResGetUser?> GetUserByMobile(string Phone)
        {
            var user = await userRepository.FindUser(e => e.PhoneNumber.Equals(Phone));

            if (user == null || user.IsDeleted)
            {
                return null;
            }

            var result = mapper.Map<ResGetUser>(user);

            return result;


        }

        public async Task<ResGetUser?> GetUser(string search)
        {
            var user = await userRepository.FindUser(e => e.Email.Equals(search.Trim())
               || e.PhoneNumber.Contains(search));

            if (user == null || user.IsDeleted)
            {
                return null;
            }

            var result = mapper.Map<ResGetUser>(user);

            return result;


        }

        public async Task<CacheModel?> Login(LoginVM model)
        {
            var user = new Account();




            user = await userRepository.FindUser(e => (e.Email.Equals(model.UserName.Trim())
          || e.PhoneNumber.Equals(model.UserName)));


            if (user == null || user.IsDeleted)
                throw new Exception("No Users Found!");



            var checkPassword = await userRepository.CheckPasswordAsync(user, model.Password);

            if (!checkPassword)
                throw new Exception("Your UserName Or Password Is Not Correct!");



            if (user == null)
                throw new Exception("Your Mail Is Not Correct!");

            //var role = roleService.GetRoleById(req.RoleId);

            //await checkUserRole(req, role);

            //var res = await CreateUser(user, req.Password, role.Name);

            //var cache = CreateTokenAll(user, role.Name, Guid.Parse(req.RoleId));

            return null;
        }

        public async Task<bool> CheckMailAndPhone(string phone, string? email)
        {
            Account user;
            if (!string.IsNullOrEmpty(email))
            {
                user = await userRepository.FindUser(e => e.EmailConfirmed && e.Email.Equals(email.Trim()));
                if (user != null && !user.IsDeleted)
                {
                    throw new Exception("Email Exsits!");
                }
            }
            if (!string.IsNullOrEmpty(phone))
            {
                user = await userRepository.FindUser(e =>/* e.Email.Equals(mail.Trim())
           && */e.PhoneNumber.Equals(phone));

                if (user != null && !user.IsDeleted)
                    throw new Exception(" Phone Exsit!");
            }


            return true;
        }
        public async Task<bool> CheckMail(string email)
        {

            Account user = await userRepository.FindUser(e => e.EmailConfirmed && e.Email.Equals(email.Trim()));
            if (user != null && !user.IsDeleted)
            {
                return true;
            }



            return false;
        }
        public async Task<bool> CheckPhone(string phone)
        {

            Account user = await userRepository.FindUser(e => e.PhoneNumberConfirmed && e.PhoneNumber.Equals(phone.Trim()));
            if (user != null && !user.IsDeleted)
            {
                return true;
            }



            return false;
        }

        private CacheModel CreateTokenAll(Account user, string roleName, Guid roleId)
        {
            var token = CreateToken(user, roleName);
            var refreshToken = GenerateToken();

            CacheModel cache = new CacheModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                Role = roleName,
                RoleId = roleId,
                Email = user.Email,
                RefreshToken = refreshToken,
                Token = token
            };


            return cache;
        }
        public async Task<bool> ChangePasswordByUser(ReqChangePasswordByUser req, string userId)
        {
            var user = await userRepository.FindUser(e => e.Id.Equals(userId));
            if (user == null)
            {
                throw new Exception("This User Is Not Found");
            }

            if (req.NewPassword.Equals(req.ConfirmedPassword))
            {
                var isSameOldPassord = userRepository.CheckPasswordsMatch(user, req.NewPassword);
                if (isSameOldPassord)
                    throw new Exception("This Password Is Same Old Password!");
                //throw new Exception("This Password Is Same Old Password!");

                var result = await userRepository.ChangePassword(user, req.NewPassword);
                return result.Succeeded;
            }
            else
                throw new Exception("This Password Is Not Match!");


        }

        public async Task<bool> ChangePasswordByEmailAndOTP(ReqChangePasswordByEmail req)
        {
            var user = await userRepository.FindUser(e => e.Email.Equals(req.Email));

            if (user == null)
            {
                throw new Exception("This User Is Not Found");
            }

            if (req.NewPassword.Equals(req.ConfirmedPassword))
            {
                var result = await userRepository.ChangePassword(user, req.NewPassword);
                return result.Succeeded;
            }
            else
                throw new Exception("This Password Is Not Match!");

        }



        public async Task<CacheModel> RegisterUser(ReqCreateUser req, string CreatedBy)
        {
            var user = mapper.Map<Account>(req);

            var role = roleService.GetRoleById(req.RoleId);

            await checkUserRole(req, role);

            var res = await CreateUser(user, req.Password, role.Name);

            var cache = CreateTokenAll(user, role.Name, Guid.Parse(req.RoleId));
            var car = req.Car;
            car.AccountId = user.Id;
            var created = carService.Create(car);

            return cache;
        }

        private async Task<bool> CreateUser(Account user, string password, string roleName)
        {
            var result = new IdentityResult();


            result = await userRepository.CreateUserAsync(user, password);



            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception(error.Description + " " + error.Code);
                }
            }

            var roleResult = await userRepository.AddUserToRole(user, roleName);

            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    throw new Exception(error.Description + " " + error.Code);
                }
            }



            return true;
        }


        private async Task checkUserRole(ReqCreateUser req, ResReqCreateGetRole role)
        {

            if (role == null)
                throw new Exception("This Role Is Not Found!");

            Account oldUser;

            oldUser = await userRepository.FindUser(e => e.PhoneNumber.Equals(req.PhoneNumber) && !string.IsNullOrEmpty(e.PhoneNumber));

            if (oldUser != null)
            {
                throw new Exception("This Phone Number Is Used Before");
            }

        }
        public async Task<bool> UpdateUser(string userId, ReqUpdateUser req/*, string UpdatedBy*/)
        {
            if (!req.Id.Equals(userId))
                throw new Exception("This Id Is Not Match!");

            var user = await userRepository.FindUser(e => e.Id == userId);

            if (user == null)
                throw new Exception("This User Is Not Found!");
            var userEmail = await userRepository.FindUser(e => e.Id != userId && e.EmailConfirmed && e.Email == req.Email && !string.IsNullOrEmpty(req.Email));
            if (userEmail != null)
                throw new Exception("This Email Is Used Before!");

            if (await userRepository.FindUser(e => e.Id != userId && e.PhoneNumber == req.PhoneNumber && !string.IsNullOrEmpty(req.PhoneNumber)) != null)
                throw new Exception("This Phone Number Is Used Before!");
            mapper.Map(req, user);
            //  user.MacAddress = String.Empty;
            var result = await userRepository.UpdateUser(user);

            if (!result.Succeeded)
            {
                throw new Exception("there exception in update profile");
            }

            var role = roleService.GetRoleById(req.RoleId);

            var oldRole = userRepository.GetRolesAsync(user);
            var oldRoleObj = roleService.GetFirst(oldRole);

            await userRepository.AddUserToRole(user, role.Name);

            return unitOfWork.SaveChanges() > 0;
        }

        public async Task<bool> UpdatePhoneNumber(string OTP, string UserId, string NewPhonerNumber)
        {
            var user = await userRepository.FindByIdAsync(UserId);

            user.PhoneNumber = NewPhonerNumber;
            var result = await this.userRepository.UpdateUser(user);

            return result.Succeeded;

        }





        #region AdminPanel
        public List<ResGetUser> GetAllUserWithChilds()
        {
            var allUsers = userRepository.GetUsers(p => p.IsDeleted == false);
            var users = mapper.Map<List<ResGetUser>>(allUsers);

            return users;
        }




        private async Task checkUserRole(ReqAddUser req, ResReqCreateGetRole role)
        {

            if (role == null)
                throw new Exception("This Role Is Not Found!");

            Account oldUser;

            oldUser = await userRepository.FindUser(e => e.PhoneNumber.Equals(req.PhoneNumber));

            if (oldUser == null)
            {
                oldUser = await userRepository.FindUser(e => e.Email.Equals(req.Email));

                if (oldUser != null)
                    throw new Exception("This Email Is Used Before");
            }
            else
            {
                throw new Exception("This Phone Number Is Used Before");
            }

        }

        public async Task<bool> AddUser(ReqAddUser req)
        {
            var user = mapper.Map<Account>(req);

            var role = roleService.GetRoleById(req.RoleId);

            await checkUserRole(req, role);
            var res = await CreateUser(user, req.Password, role.Name);

            return res;
        }

        #endregion
    }
}
