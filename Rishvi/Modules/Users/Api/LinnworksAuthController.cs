using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rishvi.Modules.Core;
using Rishvi.Modules.Core.Api;
using Rishvi.Modules.Users.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rishvi.Modules.Users.Api
{
    [Route("api/linnwork")]
    [ApiController]
    public class LinnworksAuthController : BaseApiController
    {
        private readonly ILinnworksAuthService _linnworksAuthService;

        public LinnworksAuthController(ILinnworksAuthService linnworksAuthService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _linnworksAuthService = linnworksAuthService;
        }

        [HttpGet, Route("auth")]
        public ActionResult Auth()
        {
            if (!string.IsNullOrEmpty(linnworkUserToken) && !string.IsNullOrEmpty(linnworkServerUrl))
            {
                return Result(new Result().SetSuccess("Authorized successfully."));
            }
            else
            {
                var linnworkAuth = _linnworksAuthService.Auth();
                if (linnworkAuth.Success)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddSeconds(604800);
                    Response.Cookies.Append("linnworkUserToken", linnworkAuth.Data.Token, option);
                    Response.Cookies.Append("linnworkServerUrl", linnworkAuth.Data.Server, option);
                }
                else
                {
                    Response.Cookies.Delete("linnworkUserToken");
                    Response.Cookies.Delete("linnworkServerUrl");
                }
                return Result(linnworkAuth);
            }
        }
    }
}
