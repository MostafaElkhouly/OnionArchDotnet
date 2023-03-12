using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IRepository.IUserRepository
{
    public interface IRoleRepository
    {
        Task<bool> CreateRoleAsync(IdentityRole role);
        public IdentityRole GetRole(string RoleName);
        public List<IdentityRole> GetAllRole();
        public IdentityRole GetRoleById(string pId);
        public Task<IdentityResult> UpdateRole(IdentityRole role);

        public Task<IdentityResult> RemoveRole(IdentityRole role);
        public List<IdentityRole> GetListOfRoles(
            Expression<Func<IdentityRole, bool>> predicate = null,
            Func<IQueryable<IdentityRole>, IIncludableQueryable<IdentityRole, object>> include = null);

    }
}
