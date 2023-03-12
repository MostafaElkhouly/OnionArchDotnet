using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IRoleService
    {
        public Task<bool> AddRole(ResReqCreateGetRole role);

        public Task<bool> RemoveRole(string pId);
        public List<ResReqCreateGetRole> GetAllRole();
        public ResReqCreateGetRole GetRoleById(string pId);
        public ResReqCreateGetRole GetFirst(string RoleName);
        public Task<bool> UpdateRole(string pId, ResReqCreateGetRole role);
        public Task<bool> SwitchDelete(string Id);
    }
}
