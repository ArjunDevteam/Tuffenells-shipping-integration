using Rishvi.Modules.NewUserResponse.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rishvi.Modules.Users.Models.DTOs
{
    public class CourierServiceDto
    {
        public string ServiceName;
        public string ServiceCode;
        public string ServiceTag;
        public string ServiceGroup;
        public Guid ServiceUniqueId;
        public List<ConfigItem> ConfigItems = new List<ConfigItem>();
        public List<ServiceProperty> ServiceProperty = new List<ServiceProperty>();
    }

    public class ServiceProperty
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}