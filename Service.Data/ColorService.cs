using AutoMapper;
using Domain.Entities.Models;
using Infrastructure.ViewModel.Base;
using Infrastructure.ViewModel.VM;
using Persistence.IRepository;
using Persistence.IRepository.IEntityRepository;
using Service.Data.Service_Base;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Data
{
    public class ColorService : ServiceBase<Color, ResReqColor>, IColorService
    {
        public ColorService(IRepository<Color> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public async Task<List<Color>> GetColorsAsync(BaseViewModel req)
        {
            return await Repository.ToListAsync();
        }


        public ICollection<Color> GetAllsAsync(BaseViewModel req)
        {
            return Repository.ToList();
        }
    }
}
