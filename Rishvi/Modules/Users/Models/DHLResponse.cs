using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Users.Models
{
    public class DHLResponse
    {
        public string TrackingNumber { get; set; }
        public string Labelurl { get; set; }
        public string Labelurlbyts { get; set; }
        public bool IsError { get; set; }
        public string Error { get; set; }
        public string Labelid { get; set; }
    }
}
