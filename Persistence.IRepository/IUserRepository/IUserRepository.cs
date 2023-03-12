
using Domain.Entities;
using Domain.Entities.Models;
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
    public interface IUserRepository
    {
        public Task<IdentityResult> RegisterUserAsync(Account account);
        public bool CheckPasswordsMatch(Account account, string newPassword);

        Task<Account> FindByNameAsync(string Name);
        Task<Account> FindByEmailAsync(string Email);
        Task<Account> FindByIdAsync(string Id);
        Task<Account> FindUser(Expression<Func<Account, bool>> predicate);
        bool HasAny(Expression<Func<Account, bool>> predicate);
        string GetRolesAsync(Account pAccount);
        void Delete(Account account);
        void DeleteRing(Account[] entities);
        void DeleteWhere(Expression<Func<Account, bool>> predicate);


        Task SoftDelete(Account account);
        Task SoftDeleteRing(Account[] entities);
        void SoftDeleteWhere(Expression<Func<Account, bool>> predicate);
        Task<IdentityResult> CreateUserAsync(Account account, string password);
        Task<IdentityResult> AddUserToRole(Account account, string role);
        Task<bool> CheckPasswordAsync(Account account, string password);
        Task<IdentityResult> UpdateUser(Account account);
        Task<IdentityResult> ChangePassword(Account account, string password);
        public List<Account> GetListOfUsers(Func<IQueryable<Account>, IIncludableQueryable<Account, object>> include = null);
        public List<Account> GetUsers(Expression<Func<Account, bool>> predicate = null, Func<IQueryable<Account>, IIncludableQueryable<Account, object>> include = null);
        public Account GetUserById(string Id);
        public Task<Account> GetUserByProvider(string loginProvider, string providerKey);
        public Task<IdentityResult> AddLoginAsync(Account account, UserLoginInfo loginInfo);
        public Task<IdentityResult> DeleteRolesAsync(string roleName, Account account);
        public Task<IdentityResult> ChangePassword(Account account, String token, string newPassword);
        public Task<IdentityResult> DeleteAsync(Account account);
    }
}