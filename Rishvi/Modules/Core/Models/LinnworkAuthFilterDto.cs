using System;

namespace Rishvi.Modules.LinnworkAuth.Models.DTOs
{
    public class LinnworkAuthFilterDto
    {
        public string ApplicationId { get; set; }
        public string ApplicationSecret { get; set; }
        public string Token { get; set; }
    }
}