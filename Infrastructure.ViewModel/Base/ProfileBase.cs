

using AutoMapper;

namespace Infrastructure.ViewModel.Base
{
    public abstract class ProfileBase : Profile
    {
        public ProfileBase()
        {
            Response();
            Request();
        }
        public abstract void Response();
        public abstract void Request();
    }
}
