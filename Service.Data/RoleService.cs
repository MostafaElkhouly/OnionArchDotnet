using AutoMapper;
using Domain.Entities;
using Infrastructure.ViewModel.VM;
using Microsoft.AspNetCore.Identity;
using Persistence.IRepository.IUserRepository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Data
{
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AddRole(ResReqCreateGetRole role)
        {
            var result = await roleRepository.CreateRoleAsync(new IdentityRole
            {
                Name = role.Name,
                NormalizedName = role.Name.ToUpper(),
            });
            return result;
        }



        public List<ResReqCreateGetRole> GetAllRole()
        {
            return mapper.Map<List<ResReqCreateGetRole>>(roleRepository.GetAllRole());
        }

        public ResReqCreateGetRole GetFirst(string RoleName)
        {
            return mapper.Map<ResReqCreateGetRole>(roleRepository.GetRole(RoleName));
        }


        public ResReqCreateGetRole GetRoleById(string pId)
        {
            var value = roleRepository.GetRoleById(pId);
            return mapper.Map<ResReqCreateGetRole>(value);
        }

        public async Task<bool> RemoveRole(string pId)
        {
            var value = roleRepository.GetRoleById(pId);

            var result = await roleRepository.RemoveRole(value);


            return result.Succeeded;
        }

        public async Task<bool> UpdateRole(string pId, ResReqCreateGetRole req)
        {
            var value = roleRepository.GetRoleById(pId);
            mapper.Map(req, value);
            var result = await roleRepository.UpdateRole(value);
            return result.Succeeded;
        }

        public async Task<bool> SwitchDelete(string Id)
        {
            var value = roleRepository.GetRoleById(Id);

            if (value == null)
                throw new Exception("This Object Is Not Found");

            var result = await roleRepository.UpdateRole(value);

            return result.Succeeded;

        }
    }
}
