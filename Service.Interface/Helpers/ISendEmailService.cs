using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface.Helpers
{
    public interface ISendEmailService
    {
        public Task<(bool, string)> SendEmail(ResEmail mail);

    }
}
