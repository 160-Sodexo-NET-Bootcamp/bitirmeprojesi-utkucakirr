using System;

namespace Core.Result
{
    public class ResultModel
    {
        public ResultModel(bool succeeded,string message)
        {
            this.Succeeded = succeeded;
            this.Message = message;
        }
        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }

    public class ResultModel<T>:ResultModel
    {
        public ResultModel(bool succeeded, string message,T result) : base(succeeded, message)
        {
            this.Result = result;
        }

        public T Result { get; set; }
    }
}
