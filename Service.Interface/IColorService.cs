using Domain.Entities.Models;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using Service.Interface.IService_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IColorService : IServiceBase<Color, ResReqColor>
    {
        public ICollection<Color> GetAllsAsync(BaseViewModel req);
        public Task<List<Color>> GetColorsAsync(BaseViewModel req);
    }
}
