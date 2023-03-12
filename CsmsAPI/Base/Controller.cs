using Infrastructure.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;

namespace CsmsAPI.Base
{
    [ApiController]
    public abstract class Controller : ControllerBase
    {
        
        private readonly string UserName;

        public Controller(HttpContextAccessor httpContextAccessor = null)
        {

        }
    }
}
