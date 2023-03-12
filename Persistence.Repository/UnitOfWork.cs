
using Persistence.Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using Domain.Configration.EntitiesProperties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Persistence.IRepository.IEntityRepository;
using Domain.Entities.Base;
using Persistence.IRepository;
using Newtonsoft.Json;

namespace Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private Dictionary<Type, object> _repositories;

        /// <summary>
        /// Constructor For Db Context (movies)
        /// </summary>
        /// <param name="Dbcontext"></param>
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

       

        //public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase
        //{
        //    if (_repositories == null) _repositories = new Dictionary<Type, object>();

        //    var type = typeof(TEntity);
        //    if (!_repositories.ContainsKey(type))
        //        _repositories[type] = new Repository<TEntity>(context, false);

        //    return (IRepository<TEntity>)_repositories[type];
        //}

        //public IRepository<TEntity> GetRepository<TEntity>()
        //    where TEntity : EntityBase
        //{
        //    if (_repositories == null) _repositories = new Dictionary<Type, object>();

        //    var type = typeof(TEntity);
        //    if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(context, false);
        //    return (IRepository<TEntity>)_repositories[type];
        //}

        public async Task<long> SaveAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (((SqlException)ex.InnerException).Number == 2627)//dublicate Key
                {
                    throw new Exception("dublicate Key");
                }
                else if (((SqlException)ex.InnerException).Number == 547)//// Foreign Key violation
                {
                    throw new Exception("Foreign Key violation");
                }

                else if (((SqlException)ex.InnerException).Number == 2601)//// Primary key violation
                {
                    throw new Exception("Primary key violation");
                }
            }
            throw new Exception("This Item Is Not Saved");
        }

        public async Task<long> SaveAsyncTransaction()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var result = await context.SaveChangesAsync();

                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public long SaveChanges()
        {

            try
            {
                //var list = 0;

                return context.SaveChanges();

            }
            catch (Exception ex)
            {
                if (((SqlException)ex.InnerException).Number == 2627)//dublicate Key
                {
                    throw new Exception("dublicate Key");
                }
                else if (((SqlException)ex.InnerException).Number == 547)//// Foreign Key violation
                {

                    throw new Exception("Foreign Key violation");
                }

                else if (((SqlException)ex.InnerException).Number == 2601)//// Primary key violation
                {
                    throw new Exception("Primary key violation");
                }
            }
            throw new Exception("This Item Is Not Saved");
        }

        public long SaveChangesTransaction()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var result = context.SaveChanges();

                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        


    }

}
