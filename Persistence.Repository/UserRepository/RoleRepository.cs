using Persistence.IRepository.IUserRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Configration.EntitiesProperties;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Domain.Entities;

namespace Persistence.Repository.UserRepository
{
    public class RoleRepository : IRoleRepository
    {
        private RoleManager<IdentityRole> roleManager;
        protected readonly DbSet<IdentityRole> DbSet;

        public RoleRepository(RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            DbSet = context.Set<IdentityRole>();
            this.roleManager = roleManager;
        }

        public IdentityRole GetRole(string RoleName)
        {
            return DbSet.Where(e => e.NormalizedName.Equals(RoleName.ToUpper())).FirstOrDefault();
        }

        public async Task<bool> CreateRoleAsync(IdentityRole role)
        {
            try
            {
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return true;
                else
                {
                    foreach (var item in result.Errors)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return false;
        }

        public List<IdentityRole> GetAllRole()
        {
            return DbSet.ToList();
        }




        public List<IdentityRole> GetListOfRoles(
            Expression<Func<IdentityRole, bool>> predicate = null,
            Func<IQueryable<IdentityRole>, IIncludableQueryable<IdentityRole, object>> include = null)
        {
            IQueryable<IdentityRole> query = DbSet;

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }



            return query.ToList();
        }





        public async Task<IdentityResult> UpdateRole(IdentityRole role)
        {

            return await roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> RemoveRole(IdentityRole role)
        {
            return await roleManager.DeleteAsync(role);
        }

        public IdentityRole GetRoleById(string pId)
        {
            return DbSet.Where(e => e.Id.Equals(pId)).FirstOrDefault();
        }
    }
}
