using Domain.Entities.Base;
using Domain.Entities.Models;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Executed.IExecuteies
{
    public interface IServiceOrchestrator
    {
        public Task<TRespose> ExecutUnasyncFunc<TRequest, TRespose>(Func<TRequest, dynamic> orchestratorFunc, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel;




        public Task<TRespose> ExecutAsync<TRequest, TRespose>
            (Func<TRequest, dynamic> orchestratorFuncAsync, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel;

        

        public TRespose Execut<TRequest, TRespose>
            (Func<TRequest, EntityBase> orchestratorFunc, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel;

        public Task<TRespose> GetAsync<TRequest, TRespose>
            (Func<TRequest, dynamic> orchestratorFuncAsync, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel;



        public TRespose Get<TRequest, TRespose>
            (Func<TRequest, EntityBase> orchestratorFunc, TRequest request)
            where TRequest : BaseViewModel
            where TRespose : BaseViewModel;

        public Task<TViewModel> GetListAsync<TRequest, TViewModel>(Func<TRequest, dynamic> orchestratorFuncAsync, TRequest request)
            where TRequest : BaseViewModel ;

        public TRespose GetList<TRequest, TRespose>(Func<TRequest, object> orchestratorFunc, TRequest request)
            where TRequest : BaseViewModel;
    }
}
