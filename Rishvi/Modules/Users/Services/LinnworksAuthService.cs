using AutoMapper;
using Microsoft.Extensions.Configuration;
using Rishvi.Modules.Core;
using Rishvi.Modules.Core.Data;

namespace Rishvi.Modules.Users.Services
{
    public interface ILinnworksAuthService
    {
        Result Auth();
    }

    public class LinnworksAuthService : ILinnworksAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public LinnworksAuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
            _unitOfWork = unitOfWork;
        }

        public Result Auth()
        {
            var result = new Result();

            //var client = new RestClient("https://api.linnworks.net/api/Auth/AuthorizeByApplication");
            //LinnworkAuthFilterDto filter = new LinnworkAuthFilterDto()
            //{
            //    ApplicationId = _config.GetSection("LinnworkAppCredential").GetSection("ApplicationId").Value,
            //    ApplicationSecret = _config.GetSection("LinnworkAppCredential").GetSection("ApplicationSecret").Value,
            //    Token = _config.GetSection("LinnworkAppCredential").GetSection("Token").Value
            //};
            //RestRequest request = new RestRequest(Method.POST);
            ////request.AddHeader("Authorization", token);
            ////request.AddHeader("Content-Type", "application/json");
            ////var filterJson = @"{
            ////                        ""ApplicationId"": ""c72b1b12-c98c-4eb0-a841-ad95921fe1fb"",
            ////                        ""ApplicationSecret"": ""d6b4cd7d-6412-468e-a60a-d721181c610e"",
            ////                        ""Token"": ""f1b7ffe4-2ff5-39ab-3b13-5cdb61e46ac5""
            ////                    }";
            //var filterJson = JsonConvert.SerializeObject(filter);
            //request.AddParameter("application/json", filterJson, ParameterType.RequestBody);

            //IRestResponse response = client.Execute(request);
            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    result.SetSuccess("Result found successfully.");
            //    var linnworkAuthDto = JsonConvert.DeserializeObject<LinnworkAuthDto>(response.Content);
            //    result.Data = linnworkAuthDto;
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(response.Content))
            //        result.SetError(JsonConvert.DeserializeObject<RestResponseContent>(response.Content).Message);
            //    else
            //        result.SetError("Internal server error!");
            //}
            return result;
        }

    }
}
