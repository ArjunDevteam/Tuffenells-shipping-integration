using Rishvi.Modules.Core.Filters;
using Rishvi.Modules.NewUserResponse.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rishvi.Modules.Users.Models.DTOs
{
    public class GenerateLabelFilterDto : BaseFilterDto
    {
        public GenerateLabelFilterDto()
        {
            SortColumn = "CreatedAt";
            SortType = "DESC";
        }
    }

}