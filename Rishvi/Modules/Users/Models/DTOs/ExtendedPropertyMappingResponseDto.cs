using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.ExtendedPropertyMappingResponse.Models.DTOs
{
    public class ExtendedPropertyMappingResponseDto
    {
        public Guid AuthorizationToken;
        public string ErrorMessage { get; set; }
        public string error { get; set; }
        public bool IsError { get; set; }

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
