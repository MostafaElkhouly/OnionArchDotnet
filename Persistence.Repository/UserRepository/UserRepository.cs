using Domain.Configration.EntitiesProperties;
using Persistence.IRepository.IUserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Domain.Entities;
using Domain.Entities.Models;

namespace Persistence.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbSet<Account> DbSet;
        private readonly AppDbContext context;
        private readonly UserManager<Account> userManager;
        public UserRepository(UserManager<Account> userManager, AppDbContext context)
        {
            DbSet = context.Set<Account>();
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<IdentityResult> RegisterUserAsync(Account user)
        {
            return await userManager.CreateAsync(user);
        }

        public bool CheckPasswordsMatch(Account user, string newPassword)
        {
            PasswordVerificationResult passwordMatch = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, newPassword);
            return (passwordMatch == PasswordVerificationResult.Success);
        }

        public async Task<IdentityResult> AddUserToRole(Account user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }
        public Task<IdentityResult> ChangePassword(Account user, string password)
        {
            var newPassword = userManager.PasswordHasher.HashPassword(user, password);
            user.PasswordHash = newPassword;
            return userManager.UpdateAsync(user);
        }
        public async Task<bool> CheckPasswordAsync(Account user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
        public async Task<IdentityResult> CreateUserAsync(Account user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }
        public void Delete(Account entity)
        {
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteRing(Account[] entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }
        public void DeleteWhere(Expression<Func<Account, bool>> predicate)
        {
            var entity = DbSet.Where(predicate).FirstOrDefault();
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> FindByEmailAsync(string Email)
        {
            return await userManager.FindByEmailAsync(Email);
        }
        public async Task<Account> FindByIdAsync(string Id)
        {
            return await userManager.FindByIdAsync(Id);
        }
        public async Task<Account> FindByNameAsync(string Name)
        {
            return await userManager.FindByNameAsync(Name);
        }
        public async Task<Account> FindUser(Expression<Func<Account, bool>> predicate)
        {
            return await this.DbSet.Where(predicate).FirstOrDefaultAsync();
        }
        public string GetRolesAsync(Account pUser)
        {

            return userManager.GetRolesAsync(pUser).Result.FirstOrDefault();
        }
        public bool HasAny(Expression<Func<Account, bool>> predicate)
        {
            return DbSet.Where(predicate).Any();
        }
        public async Task SoftDelete(Account user)
        {
            var logins = await userManager.GetLoginsAsync(user);
            var rolesForUser = await userManager.GetRolesAsync(user);

            using (var transaction = context.Database.BeginTransaction())
            {
                IdentityResult result = IdentityResult.Success;
                foreach (var login in logins)
                {
                    result = await userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (result != IdentityResult.Success)
                        break;
                }
                if (result == IdentityResult.Success)
                {
                    foreach (var item in rolesForUser)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, item);
                        if (result != IdentityResult.Success)
                            break;
                    }
                }
                if (result == IdentityResult.Success)
                {
                    result = await userManager.DeleteAsync(user);
                    if (result == IdentityResult.Success)
                        transaction.Commit(); //only commit if user and all his logins/roles have been deleted  
                }
            }
        }
        public async Task SoftDeleteRing(Account[] entities)
        {
            foreach (var item in entities)
            {
                await SoftDelete(item);
            }
        }
        public void SoftDeleteWhere(Expression<Func<Account, bool>> predicate)
        {
            var entity = DbSet.Where(predicate).FirstOrDefault();
            entity.IsDeleted = true;
            try
            {
                EntityEntry dbEntityEntry = DbSet.Attach(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<IdentityResult> UpdateUser(Account user)
        {
            return userManager.UpdateAsync(user);
        }
        public List<Account> GetListOfUsers(Func<IQueryable<Account>, IIncludableQueryable<Account, object>> include = null)
        {
            IQueryable<Account> query = DbSet;
            if (include != null)
            {
                query = include(query);
            }
            return DbSet.ToList();
        }

        public List<Account> GetUsers(Expression<Func<Account, bool>> predicate = null,
            Func<IQueryable<Account>, IIncludableQueryable<Account, object>> include = null)
        {
            IQueryable<Account> query = DbSet;

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


        public Account GetUserById(string Id)
        {
            return userManager.FindByIdAsync(Id).Result;
        }
        public async Task<Account> GetUserByProvider(string loginProvider, string providerKey)
        {
            return await userManager.FindByLoginAsync(loginProvider, providerKey);
        }
        public async Task<IdentityResult> AddLoginAsync(Account user, UserLoginInfo loginInfo)
        {
            return await userManager.AddLoginAsync(user, loginInfo);
        }
        public Task<IdentityResult> DeleteRolesAsync(string roleName, Account user)
        {
            return userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> DeleteAsync(Account user1)
        {
            var user = await userManager.FindByIdAsync(user1.Id);

            return await userManager.DeleteAsync(user);

        }
        public Task<IdentityResult> ChangePassword(Account user, String token, string newPassword)
        {
            return userManager.ResetPasswordAsync(user, token, newPassword);
        }

    }
}