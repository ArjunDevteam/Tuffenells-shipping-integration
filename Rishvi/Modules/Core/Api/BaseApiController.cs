using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rishvi.Modules.Core.Api
{
    public class BaseApiController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string linnworkUserToken { get; set; }
        public string linnworkServerUrl { get; set; }
        public BaseApiController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            linnworkUserToken = _httpContextAccessor.HttpContext.Request.Cookies["linnworkUserToken"] != null ? _httpContextAccessor.HttpContext.Request.Cookies["linnworkUserToken"] : string.Empty;
            linnworkServerUrl = _httpContextAccessor.HttpContext.Request.Cookies["linnworkServerUrl"] != null ? _httpContextAccessor.HttpContext.Request.Cookies["linnworkServerUrl"] : string.Empty;
        }
        protected ActionResult Result(dynamic entity)
        {
            return entity == null ? NotFound() : (ActionResult)Ok(entity);
        }
    }
}
