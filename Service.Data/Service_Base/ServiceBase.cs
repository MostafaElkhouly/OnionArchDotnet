
using AutoMapper;

using Domain.Entities.Base;
using Infrastructure.ViewModel.Base;
using Persistence.IRepository.IEntityRepository;
using Service.Interface.IService_Base;

namespace Service.Data.Service_Base
{

    public class ServiceBase<TEntity, TViewModelCreate, TViewModelUpdate> : ServiceBase<TEntity>, IServiceBase<TEntity, TViewModelCreate, TViewModelUpdate>
        where TEntity : EntityBase, new()
        where TViewModelCreate : BaseViewModel, new()
        where TViewModelUpdate : BaseViewModel, new()
    {
        public ServiceBase(IRepository<TEntity> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public virtual TEntity Create(TViewModelCreate req)
        {
            var value = mapper.Map<TEntity>(req);

            Repository.Add(value);


            return value;
        }

        public virtual async Task<TEntity> UpdateAsync(TViewModelUpdate req)
        {
            var result = await Repository.GetSingleAsync(req.Id);
            if (result == null)
                throw new Exception("This Object Is Not Found!");

            mapper.Map(req, result);
            Repository.Update(result);
            return result;
        }
    }

    public class ServiceBase<TEntity, TViewModel> : ServiceBase<TEntity>, IServiceBase<TEntity, TViewModel>
        where TEntity : EntityBase, new()
        where TViewModel : BaseViewModel, new()
    {
        public ServiceBase(IRepository<TEntity> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public virtual TEntity Create(TViewModel req)
        {
            var value = mapper.Map<TEntity>(req);

            Repository.Add(value);
            return value;
        }

        public virtual async Task<TEntity> UpdateAsync(TViewModel req)
        {
            var result = await Repository.GetSingleAsync(req.Id);
            if (result == null)
                throw new Exception("This Object Is Not Found!");

            mapper.Map(req, result);
            Repository.Update(result);
            return result;
        }
    }

    public class ServiceBase<TEntity> : ServiceBase, IServiceBase<TEntity>
        where TEntity : EntityBase, new()
    {
        
        protected IRepository<TEntity> Repository;


        public ServiceBase(IRepository<TEntity> repository, IMapper mapper) : base(mapper)
        {
            
            Repository = repository;

        }

        #region

        public virtual async Task<TEntity> DeleteByIdAsync(BaseViewModel req)
        {
            var value = await Repository.GetSingleAsync(req.Id);

            if (value == null)
                throw new Exception("This Object Is Not Found!");

            value.UserName = req.UserName;
            value.DateOfCreate = DateTime.UtcNow;
            Repository.SoftDelete(value);
            return value;
        }
        /// <summary>
        /// <typeparamref name="TEntity"/>
        ///
        /// </summary>

        public async Task<List<TEntity>> GetAllAsync(BaseViewModel req)
        {
            var value = await Repository.ToListAsync(e => e.IsDeleted == req.IsDeleted);

            return value;
        }
        /**
         * <returns>
         *  List<TEntity>
         * </returns>
         */
        public List<TEntity> GetAll(BaseViewModel req)
        {
            var value = Repository.ToList(e => e.IsDeleted == req.IsDeleted);

            return value;
        }

        /**
         * <returns>
         *  TEntity
         * </returns>
         */
        public virtual async Task<TEntity> GetByIdAsync(BaseViewModel req)
        {
            var result = await Repository.GetSingleAsync(req.Id);

            if (result == null)
                throw new Exception("This Object Is Not Found!");

            return result;
        }

        #endregion
    }

    public class ServiceBase : IServiceBase
    {
        protected readonly IMapper mapper;
        public ServiceBase(IMapper mapper)
        {
            this.mapper = mapper;

        }
    }
}
