using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ViewModel.Response
{
    public class SuccessResponse<T>
    {
        public int Code { set; get; } = 200;
        public T Data { set; get; }
    }
}
