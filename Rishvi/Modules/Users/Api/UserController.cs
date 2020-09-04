using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rishvi.Modules.AddNewRequest.Models.DTOs;
using Rishvi.Modules.Core;
using Rishvi.Modules.Core.Api;
using Rishvi.Modules.Label.Models.DTOs;
using Rishvi.Modules.NewUserResponse.Models.DTOs;
using Rishvi.Modules.Users.Models;
using Rishvi.Modules.Users.Models.DTOs;
using Rishvi.Modules.Users.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rishvi.Modules.Users.Api
{
    [ApiController]
    //[Authorize]
    [Route("api/users")]    
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(
            IUserService userService,  IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userService = userService;
        }

        [HttpPost, Route("add-new-user")]
        public ActionResult AddNewUser([FromBody] AddNewRequestDto dto)
        {
            return Result(_userService.CreateNewUser(dto));
        }

        [HttpPost, Route("user-config")]
        public ActionResult UserConfig([FromBody] UserConfigRequest dto)
        {
            return Result(_userService.UserConfig(dto));
        }

        [HttpPost, Route("update-config")]
        public ActionResult UpdateConfig([FromBody] UpdateConfigRequest dto)
        {
            return Result(_userService.UpdateConfig(dto));
        }

        [HttpPost, Route("delete-config")]
        public ActionResult ConfigDelete(ConfigDeleteRequest dto)
        {
            return Result(_userService.ConfigDelete(dto));
        }

        [HttpPost, Route("user-available-services")]
        public ActionResult UserAvailableServices(UserAvailableServicesRequest dto)
        {
            return Result(_userService.UserAvailableServices(dto));
        }

        [HttpPost, Route("extended-property-mapping")]
        public ActionResult ExtendedPropertyMapping(ExtendedPropertyMappingRequest dto)
        {
            return Result(_userService.ExtendedPropertyMapping(dto));
        }

        [HttpPost, Route("generate-label")]
        public ActionResult GenerateLabel(Models.GenerateLabelRequest dto)
        {
            return Result(_userService.GenerateLabel(dto));
        }

        [HttpPost, Route("cancel-label")]
        public ActionResult CancelLabel(Models.CancelLabelRequest dto)
        {
            return Result(_userService.CancelLabel(dto));
        }

        [HttpPost, Route("create-manifest")]
        public ActionResult CreateManifest(Models.CreateManifestRequest dto)
        {
            return Result(_userService.CreateManifest(dto));
        }

        [HttpPost, Route("print-manifest")]
        public ActionResult PrintManifest(Models.PrintManifestRequest dto)
        {
            return Result(_userService.PrintManifest(dto));
        }

        [HttpGet, Route("test")]
        public ActionResult test()
        {
            return Result(_userService.test());
        }
    }
}
