using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public class LinnworkAuthFilterDto
    {
        public string ApplicationId { get; set; }
        public string ApplicationSecret { get; set; }
        public string Token { get; set; }
    }
}
