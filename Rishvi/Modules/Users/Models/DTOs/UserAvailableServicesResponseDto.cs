using System;
using System.Collections.Generic;

namespace Rishvi.Modules.Users.Models.DTOs
{
    public class UserAvailableServicesResponseDto
    {
        public string error { get; set; }

        public List<CourierServiceDto> Services = new List<CourierServiceDto>();
    }
}