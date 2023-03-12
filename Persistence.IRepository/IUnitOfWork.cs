
using Domain.Entities.Base;
using Persistence.IRepository.IEntityRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IRepository
{
    public interface IUnitOfWork
    {
        long SaveChanges();
        long SaveChangesTransaction();
        /// <summary>
        /// SaveAsync Inteface
        /// </summary>
        Task<long> SaveAsync();

        Task<long> SaveAsyncTransaction();


    }
}
