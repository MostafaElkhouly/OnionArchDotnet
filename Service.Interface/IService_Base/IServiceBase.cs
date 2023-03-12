
using Domain.Entities.Base;
using Infrastructure.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface.IService_Base
{

    public interface IServiceBase<TEntity, TViewModelCreate, TViewModelUpdate> : IServiceBase<TEntity>
        where TEntity : EntityBase, new()
        where TViewModelCreate : BaseViewModel, new()
        where TViewModelUpdate : BaseViewModel, new()
    {
        public TEntity Create(TViewModelCreate vm);
        public Task<TEntity> UpdateAsync(TViewModelUpdate vm);
    }
    public interface IServiceBase<TEntity, TViewModel> : IServiceBase<TEntity> 
        where TEntity : EntityBase, new()
        where TViewModel : BaseViewModel, new()

    {
        public TEntity Create(TViewModel vm);
        public Task<TEntity> UpdateAsync(TViewModel vm);
    }

    public  interface IServiceBase<TEntity> : IServiceBase
        where TEntity : EntityBase, new()
    {
        public Task<TEntity> DeleteByIdAsync(BaseViewModel VM);
        public Task<List<TEntity>> GetAllAsync(BaseViewModel VM);

        public List<TEntity> GetAll(BaseViewModel req);
        public Task<TEntity> GetByIdAsync(BaseViewModel VM);
    }

    public interface IServiceBase
    {

    }
}
