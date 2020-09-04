using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rishvi.Modules.Users.Models
{
    public class AddNewUserResponse : BaseResponse
    {
        public string AuthorizationToken;
        public AddNewUserResponse(string error) : base(error) { }
        public AddNewUserResponse() : base() { }
    }

    public class UserConfigResponse : BaseResponse
    {
        public Boolean IsConfigActive;
        public string ConfigStatus;
        public ConfigStage ConfigStage;
        public UserConfigResponse(string error) : base(error) { }
        public UserConfigResponse() : base() { }
    }

    public class UpdateConfigResponse : BaseResponse
    {

        public UpdateConfigResponse(string error) : base(error) { }
        public UpdateConfigResponse() : base() { }
    }

    public class ConfigDeleteResponse : BaseResponse
    {

        public ConfigDeleteResponse(string error) : base(error) { }
        public ConfigDeleteResponse() : base() { }
    }

    public class UserAvailableServicesResponse : BaseResponse
    {
        public UserAvailableServicesResponse(string error) : base(error) { }
        public UserAvailableServicesResponse() : base() { }

        public List<CourierService> Services = new List<CourierService>();

        internal static List<CourierService> LoadServiceFile()
        {
            List<CourierService> output = new List<CourierService>();
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Authorization"  + "\\services.json")))
            {
                string json = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Authorization" + "\\services.json"));
                output = JsonConvert.DeserializeObject<List<CourierService>>(json);
            }
            return output;
        }
    }

    public class ExtendedPropertyMappingResponse : BaseResponse
    {
        public ExtendedPropertyMappingResponse(string error) : base(error) { }
        public ExtendedPropertyMappingResponse() : base() { }
        public List<ExtendedPropertyMapping> Items = new List<ExtendedPropertyMapping>();
    }

    public class ExtendedPropertyMapping
    {
        public string PropertyTitle = "";
        public string PropertyName = "";
        public string PropertyDescription = "";
        public string PropertyType = ""; // ITEM or ORDER        
    }
}
