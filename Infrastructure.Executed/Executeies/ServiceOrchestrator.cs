using AutoMapper;
using Domain.Entities.Base;
using Infrastructure.Executed.IExecuteies;
using Infrastructure.ViewModel.Base;
using Persistence.IRepository;
using System.Runtime.Remoting;

namespace Infrastructure.Executed.Executeies
{
    public class ServiceOrchestrator : IServiceOrchestrator
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ServiceOrchestrator(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TRespose> ExecutUnasyncFunc<TRequest, TRespose>(Func<TRequest, dynamic> orchestratorFunc, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel
        {
            var result = orchestratorFunc(request);
            await unitOfWork.SaveAsyncTransaction();

            var value = mapper.Map<TRespose>(result);

            return value;
        }

        public async Task<TRespose> ExecutAsync<TRequest, TRespose>(Func<TRequest, dynamic> orchestratorFuncAsync, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel
        {
            var result = await orchestratorFuncAsync(request);
            await unitOfWork.SaveAsyncTransaction();
            
            var value = mapper.Map<TRespose>(result);

            return value;
        }

        public TRespose Execut<TRequest, TRespose>
            (Func<TRequest, EntityBase> orchestratorFunc, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel
        {
            var result = orchestratorFunc(request);
            unitOfWork.SaveChangesTransaction();
            
            var value = mapper.Map<TRespose>(result);

            return value;
        }

        public async Task<TRespose> GetAsync<TRequest, TRespose>(Func<TRequest, dynamic> orchestratorFuncAsync, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel
        {
            var result = await orchestratorFuncAsync(request);

            var value = mapper.Map<TRespose>(result);

            return value;
        }

        public TRespose Get<TRequest, TRespose>(Func<TRequest, EntityBase> orchestratorFunc, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel
        {
            var result = orchestratorFunc(request);

            var type = result.GetType();

            var value = mapper.Map<TRespose>(result);

            return value;
        }

        public async Task<TViewModel> GetListAsync<TRequest, TViewModel>(Func<TRequest, dynamic> orchestratorFuncAsync, TRequest request)
            where TRequest : BaseViewModel
        {
            var result = await orchestratorFuncAsync(request);


             var value = mapper.Map<TViewModel>(result);


            return value;
        }

        public TRespose GetList<TRequest, TRespose>(Func<TRequest, object> orchestratorFunc, TRequest request)
            where TRequest : BaseViewModel

        {
            var result = orchestratorFunc(request);
            var value = mapper.Map<TRespose>(result);

            return value;
        }

        

        
    }

}
