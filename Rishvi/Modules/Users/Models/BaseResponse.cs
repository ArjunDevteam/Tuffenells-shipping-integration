using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public abstract class BaseResponse
    {
        public Boolean IsError { get; set; }
        public string ErrorMessage { get; set; }
        protected BaseResponse()
        { }
        protected BaseResponse(string error)
        {
            ErrorMessage = error;
            IsError = true;
        }
    }
}
