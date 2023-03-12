using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ViewModel.Response
{
    public class FailureResponse : FailureResponse<List<string>>
    {

    }

    public class FailureResponse<T>
    {
        public int Code { set; get; }
        public T Error { set; get; }
        public string Message {  set; get; }
    }

    public class BodyOfExcetion
    {
        public int Code { set; get; }
        public string Message { set; get; }
        public string StackTrace { get; set; }
        public Exception InnerException { get; set; }
    }
}
